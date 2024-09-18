namespace combofind.Domain.Interface
{
    public interface IUnitOfWork
    {
        Task Commit(CancellationToken cancellationToken);
    }
}
