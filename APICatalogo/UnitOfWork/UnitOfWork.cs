using APICatalogo.Context;
using APICatalogo.Repositories;
using APICatalogo.Repositories.Contracts;
using APICatalogo.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao salvar alterações no banco de dados", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado durante a confirmação da transação", ex);
            }
        }

        public void Rollback()
        {
            _context.Dispose();
        }
    }
}
