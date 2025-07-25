using APICatalogo.Repositories.Contracts;

namespace APICatalogo.UnitOfWork.Contracts;

public interface IUnitOfWork
{
    bool Commit();

    void Rollback();
}
