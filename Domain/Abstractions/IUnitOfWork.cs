namespace Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        public IFilmRepository FilmRepository { get; }
        public IUserRepository UserRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public Task CommitAsync();
    }
}
