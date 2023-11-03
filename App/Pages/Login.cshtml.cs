using Domain.Abstractions;
using Domain.Configurations;
using Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages
{
    public class LoginModel : PageModel
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IAuthService _authService;
        private readonly IValidator<LoginDto> _validator;


        public LoginModel(IAuthService authService, IValidator<LoginDto> validator, JwtConfig jwtConfig)
        {
            _authService = authService;
            _validator = validator;
            _jwtConfig = jwtConfig;
        }

        public void OnGet()
        {
        }
    }
}
