﻿using System.ComponentModel.DataAnnotations;

namespace Auction.Business.Requests;

public class SignupRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; } = string.Empty;
    
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required] 
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public DateTime Ts { get; set; }
}