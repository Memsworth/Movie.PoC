using Domain.Abstractions;
using Domain.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IValidator<RegistrationDto> _validator;
        public RegistrationModel(IAuthService authService, IValidator<RegistrationDto> validator)
        {
            _authService = authService;
            _validator = validator;
        }

        [BindProperty]
        public RegistrationDto Registration { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _validator.ValidateAsync(Registration);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                return Page();
            }

            var serviceResponse = await _authService.Register(Registration);
            
            if (serviceResponse is not null)
            {
                ModelState.AddModelError("Registration.Email", serviceResponse.Message);
                return Page();
            }
            return RedirectToPage("./Index");

        }
    }
}
