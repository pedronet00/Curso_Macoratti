using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Contracts;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMeuServico _meuServico;
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _repo;

        public ProdutoController(AppDbContext context, IMeuServico meuServico, IMapper mapper, IProdutoRepository repo)
        {
            _context = context;
            _meuServico = meuServico;
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet("Saudacao")]
        public ActionResult<string> Saudacao([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }


        [HttpGet]
        public  ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = _repo.GetProdutos(produtosParameters);

            if (produtos is null)
            {
                return NotFound();
            }

            var metadata = new             
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDTO);
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _repo.GetProdutoById(id);

            if (produto is null)
            {
                return NotFound();
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return produtoDTO;
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDTO)
        {

            if (produtoDTO is null)
                return BadRequest();

            var produto = _mapper.Map<Produto>(produtoDTO);

            _repo.InsertProduto(produto);

            var novoProdutoDTO = _mapper.Map<ProdutoDTO>(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produtoDTO.Id }, produtoDTO);
        }

        [HttpPut("{id}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.Id)
                return BadRequest();

            var produto = _mapper.Map<Produto>(produtoDTO);

            _repo.UpdateProduto(produto);

            var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoAtualizadoDTO);

        }

        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _repo.GetProdutoById(id);

            if (produto is null)
                return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDeletadoDTO);
        }

    }
}
