using System.Text.Json.Serialization;
using Auction.Business.Helpers;
using Auction.Business.Interfaces;
using Auction.Business.Services;
using Auction.Data;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IBlobService, BlobService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<ILotService, LotService>();
builder.Services.AddTransient<IBidService, BidService>(); 

var issuer = builder.Configuration.GetSection("AccessToken:Issuer").Value ?? string.Empty;
var audience = builder.Configuration.GetSection("AccessToken:Audience").Value ?? string.Empty;
var secret = builder.Configuration.GetSection("AccessToken:Secret").Value  ?? string.Empty;
builder.Services.AddSingleton(new TokenHelper(issuer, audience, secret));

builder.Services.AddDbContext<AuctionDbContext>(options => 
    options.UseSqlServer( builder.Configuration.GetConnectionString("AuctionAzureDb")));

builder.Services.AddTransient<ILotRepository, LotRepository>();

builder.Services.AddSingleton(x =>
    new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorageConnectionString")));

const string allowAllHeadersPolicy = "AllowAllHeadersPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAllHeadersPolicy,
        config =>
        {
            config.WithOrigins()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = TokenHelper.Issuer,
            ValidAudience = TokenHelper.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(allowAllHeadersPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();