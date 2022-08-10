namespace F.Core.Data;

public interface IRepository<T> : IDisposable
{
    IUnitOfWork UnitOfWork { get; }
}