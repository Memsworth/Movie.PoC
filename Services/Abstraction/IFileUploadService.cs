using Microsoft.AspNetCore.Http;

namespace Services.Abstraction
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string uploadFolder);
    }
}
