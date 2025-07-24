using APICatalogo.Models;

namespace APICatalogo.Repositories.Contracts;

public interface ICategoriaRepository
{

    IEnumerable<Categoria> GetCategorias();

    Categoria InsertCategoria(Categoria categoria);

    bool UpdateCategoria(Categoria categoria);

    bool DeleteCategoria(int id);

    Categoria GetById(int id);
}
