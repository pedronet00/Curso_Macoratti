using APICatalogo.Repositories.Contracts;

namespace APICatalogo.UnitOfWork.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();

    public Task Rollback();
}
