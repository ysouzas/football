namespace F.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}