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

        public async Task<bool> DeleteProduto(int id)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto is null)
                return false;

            _context.Produtos.Remove(produto);
            await _uow.Commit();

            return true;

        }

        public async Task<Produto> GetProdutoById(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        //public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters)
        //{
        //    return _context.Produtos
        //        .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
        //        .Take(produtosParameters.PageSize)
        //        .ToList();
        //}

        public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
        {
            var produtos =  _context.Produtos.OrderBy(p => p.Id).AsQueryable();

            var produtosOrdenados = await PagedList<Produto>.ToPagedListAsync(produtos, produtosParameters.PageNumber, produtosParameters.PageSize);

            return produtosOrdenados;
        }

        public async Task<Produto> InsertProduto(Produto produto)
        {
            if (produto is null)
                throw new ArgumentNullException(nameof(produto));

            _context.Produtos.Add(produto);
            await _uow.Commit();

            return produto;
        }

        public async Task<bool> UpdateProduto(Produto produto)
        {
            if (produto is null)
                throw new ArgumentNullException(nameof(produto));

            _context.Entry(produto).State = EntityState.Modified;
            var result = await _uow.Commit();

            return result;
        }
    }
}
