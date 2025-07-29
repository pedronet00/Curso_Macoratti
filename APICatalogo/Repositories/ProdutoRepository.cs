using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Contracts;
using APICatalogo.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {

        private AppDbContext _context;
        private IUnitOfWork _uow;

        public ProdutoRepository(AppDbContext context, IUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        public bool DeleteProduto(int id)
        {
            var produto = _context.Produtos
                .FirstOrDefault(p => p.Id == id);

            if (produto is null)
                return false;

            _context.Produtos.Remove(produto);
            _uow.Commit();

            return true;

        }

        public Produto GetProdutoById(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            return _context.Produtos
                .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
                .Take(produtosParameters.PageSize)
                .ToList();
        }

        public Produto InsertProduto(Produto produto)
        {
            if (produto is null)
                throw new ArgumentNullException(nameof(produto));

            _context.Produtos.Add(produto);
            _uow.Commit();

            return produto;
        }

        public bool UpdateProduto(Produto produto)
        {
            if (produto is null)
                throw new ArgumentNullException(nameof(produto));

            _context.Entry(produto).State = EntityState.Modified;
            var result = _uow.Commit();

            return result;
        }
    }
}
