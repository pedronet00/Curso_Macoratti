using APICatalogo.Models;

namespace APICatalogo.Repositories.Contracts;

public interface ICategoriaRepository
{

    Task<IEnumerable<Categoria>> GetCategoriasAsync();

    Task<Categoria> InsertCategoriaAsync(Categoria categoria);

    Task<bool> UpdateCategoriaAsync(Categoria categoria);

    Task<bool> DeleteCategoriaAsync(int id);

    Task<Categoria> GetByIdAsync(int id);
}
