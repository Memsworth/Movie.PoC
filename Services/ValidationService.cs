using Domain.Abstractions;

namespace Services
{
    public static class ValidationService
    {
        internal async static Task<bool> EmailExists(string email, IUnitOfWork unitOfWork)
        {
            if (email is null)
                return true;

           return await unitOfWork.UserRepository.EmailExists(email);
        }
    }
}
