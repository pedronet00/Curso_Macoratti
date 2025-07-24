using APICatalogo.Context;
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
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            _logger.LogInformation("### Executando -> Get Categorias ###");

            var categorias = _repo.GetCategorias();

            return Ok(categorias);
            
        }

        [HttpGet("{id:int:min(1)}")]
        public ActionResult<Categoria> Get(int id)
        {
            var categorias = _repo.GetById(id);

            if (categorias is null)
                return NotFound($"Categoria com o id {id} não encontrada.");

            return categorias;
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            _repo.InsertCategoria(categoria);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            var categoriaExistente = _repo.GetById(id);

            if (categoria is null)
                return NotFound();

            _repo.UpdateCategoria(categoria);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var categoria = _repo.GetById(id);

            if (categoria is null)
                return NotFound();

            _repo.DeleteCategoria(id);

            return Ok();
        }
    }
}
