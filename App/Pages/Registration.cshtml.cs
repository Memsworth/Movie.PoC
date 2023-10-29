using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace App.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly AuthService _authService;
        public RegistrationModel(AuthService authService) => _authService = authService;

        [BindProperty]
        public RegistrationDto Registration { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _authService.Register(Registration);
                return RedirectToPage("./Index");

            }
            return Page();
        }
    }
}
