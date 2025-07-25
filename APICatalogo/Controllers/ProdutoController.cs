using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Services;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProdutoController(AppDbContext context, IMeuServico meuServico, IMapper mapper)
        {
            _context = context;
            _meuServico = meuServico;
            _mapper = mapper;
        }


        [HttpGet("Saudacao")]
        public ActionResult<string> Saudacao([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

            if (produtos is null)
            {
                return NotFound();
            }
            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDTO);
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (produto is null)
            {
                return NotFound();
            }

            // var destino = _mapper.Map<Destino>(origem);

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);


            return produtoDTO;
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDTO)
        {

            if (produtoDTO is null)
                return BadRequest();

            var produto = _mapper.Map<Produto>(produtoDTO);

            _context.Produtos.Add(produto);

            _context.SaveChanges();

            var novoProdutoDTO = _mapper.Map<ProdutoDTO>(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produtoDTO.Id }, produtoDTO);
        }

        [HttpPut("{id}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.Id)
                return BadRequest();

            var produto = _mapper.Map<Produto>(produtoDTO);



            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoAtualizadoDTO);

        }

        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
                return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDeletadoDTO);
        }

    }
}
