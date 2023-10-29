using Domain.Abstractions;
using Domain.Models;
using System.Text.RegularExpressions;

namespace Services
{
    public class ValidationService
    {
        public static async Task<BaseServiceResponse<bool>> IsValidEmail(string email, IUnitOfWork unitOfWork)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email)) return new BaseServiceResponse<bool>()
            {
                Data = false,
                Message = "Email is empty. Enter a valid email"
            };

            string pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            if (Regex.IsMatch(email, pattern) == false) return new BaseServiceResponse<bool>
            {
                Data = false,
                Message = "Email doesn't match pattern"
            };

            var userEmail = (await unitOfWork.UserRepository.GetAsync()).FirstOrDefault(x => x.Email == email);
            if (userEmail is not null) return new BaseServiceResponse<bool>
            {
                Data = false,
                Message = "Email already exists. Enter a valid email"
            };

            return new BaseServiceResponse<bool> { Data = true, };
        }

    }
}
