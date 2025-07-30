using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Contracts
{
    public interface IProdutoRepository
    {

        //IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
        Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);

        Task<Produto> GetProdutoById(int id);

        Task<Produto> InsertProduto(Produto produto);

        Task<bool> UpdateProduto(Produto produto);

        Task<bool> DeleteProduto(int id);
    }
}
