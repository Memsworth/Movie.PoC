﻿using System.ComponentModel.DataAnnotations;

namespace Movie.PoC.Api.Contracts.Requests;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}