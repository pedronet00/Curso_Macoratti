using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {

        private readonly ICategoriaRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoriaController(ICategoriaRepository repo, 
                                   IConfiguration configuration, 
                                   ILogger<CategoriaController> logger)
        {
            _repo = repo;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("LerArquivoConfiguracao")]
        public string GetValores()
        {
            var valor1 = _configuration["chave1"];
            var secao1 = _configuration["secao1:chave1secao1"];

            return $"valor1: {valor1}, secao1:chave1: {secao1}";
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {

            _logger.LogInformation("### Executando -> Get Categorias ###");

            var categorias = _repo.GetCategorias();

            var categoriasDTO = new List<CategoriaDTO>();
            foreach (var categoria in categorias)
            {
                var categoriaDTO = new CategoriaDTO()
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    ImagemUrl = categoria.ImagemUrl
                };
                categoriasDTO.Add(categoriaDTO);
            }

            return Ok(categoriasDTO);
            
        }

        [HttpGet("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _repo.GetById(id);

            if (categoria is null)
                return NotFound($"Categoria com o id {id} não encontrada.");

            var categoriaDTO = new CategoriaDTO();

            categoriaDTO.Id = categoria.Id;
            categoriaDTO.Nome = categoria.Nome;
            categoriaDTO.ImagemUrl = categoria.ImagemUrl;

            return categoriaDTO;
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null)
                return BadRequest();

            var categoria = new Categoria()
            {
                Id = categoriaDTO.Id,
                Nome = categoriaDTO.Nome,
                ImagemUrl = categoriaDTO.ImagemUrl
            };

            _repo.InsertCategoria(categoria);

            var novaCategoriaDTO = new CategoriaDTO()
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            return Ok(novaCategoriaDTO);
        }

        [HttpPut("{id}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
        {
            var categoriaExistente = _repo.GetById(id);

            if (categoriaDTO is null)
                return NotFound();

            var categoria = new Categoria()
            {
                Id = categoriaDTO.Id,
                Nome = categoriaDTO.Nome,
                ImagemUrl = categoriaDTO.ImagemUrl
            };

            _repo.UpdateCategoria(categoria);

            var categoriaDTOAtualizada = new CategoriaDTO()
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

            return Ok(categoriaDTOAtualizada);
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _repo.GetById(id);

            if (categoria is null)
                return NotFound();

            _repo.DeleteCategoria(id);

            return Ok();
        }
    }
}
