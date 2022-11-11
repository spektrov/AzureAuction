using Auction.Business.Helpers;
using Auction.Business.Interfaces;
using Auction.Business.Services;
using Auction.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITaskService, TaskService>();

var issuer = builder.Configuration.GetSection("AccessToken:Issuer").Value ?? string.Empty;
var audience = builder.Configuration.GetSection("AccessToken:Audience").Value ?? string.Empty;
var secret = builder.Configuration.GetSection("AccessToken:Secret").Value  ?? string.Empty;
builder.Services.AddSingleton(new TokenHelper(issuer, audience, secret));

var connStr = builder.Configuration.GetConnectionString("AuctionDb");
builder.Services.AddDbContext<AuctionDbContext>(options => options.UseSqlServer(connStr));

const string allowAllHeadersPolicy = "AllowAllHeadersPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAllHeadersPolicy,
        config =>
        {
            config.WithOrigins("http://localhost:4200")
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowAllHeadersPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();