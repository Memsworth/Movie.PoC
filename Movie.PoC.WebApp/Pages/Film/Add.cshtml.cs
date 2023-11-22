using Domain.Abstractions;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Abstraction;

namespace Movie.PoC.WebApp.Pages.Film
{
    public class AddModel : PageModel
    {
        private readonly IFilmService _filmService;
        private readonly IFileUploadService _fileUploadService;

        [BindProperty]
        public required FilmDto Film { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }
        
        public AddModel(IFilmService filmService, IFileUploadService fileUploadService)
        {
            _filmService = filmService;
            _fileUploadService = fileUploadService;
        }
        
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var imageUrl = await _fileUploadService.UploadFileAsync(ImageFile, "Images");
            await _filmService.AddFilm(Film, imageUrl);
            
            return RedirectToPage("../Index");
        }
    }
}
