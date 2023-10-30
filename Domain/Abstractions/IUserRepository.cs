using Domain.Models;

namespace Domain.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<bool> EmailExists(string email);
    }
}
