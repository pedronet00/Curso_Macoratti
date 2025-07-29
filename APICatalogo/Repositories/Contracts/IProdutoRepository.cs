using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Contracts
{
    public interface IProdutoRepository
    {

        //IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);

        Produto GetProdutoById(int id);

        Produto InsertProduto(Produto produto);

        bool UpdateProduto(Produto produto);

        bool DeleteProduto(int id);
    }
}
