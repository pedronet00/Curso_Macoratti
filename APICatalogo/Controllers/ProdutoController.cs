using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMeuServico _meuServico;

        public ProdutoController(AppDbContext context, IMeuServico meuServico)
        {
            _context = context;
            _meuServico = meuServico;
        }


        [HttpGet("Saudacao")]
        public ActionResult<string> Saudacao([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

            if (produtos is null)
            {
                return NotFound();
            }

            return produtos;
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (produto is null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {

            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);

            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
                return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }

    }
}
