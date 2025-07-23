using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoriaController(AppDbContext context, 
                                   IConfiguration configuration, 
                                   ILogger<CategoriaController> logger)
        {
            _context = context;
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

            try
            {
                var categorias = _context.Categorias.AsNoTracking().ToList();

                if (categorias is null)
                    return NotFound();

                return categorias;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação.");
            }
            
        }

        [HttpGet("{id:int:min(1)}")]
        public ActionResult<Categoria> Get(int id)
        {
            var categorias = (from c in _context.Categorias
                              where c.Id == id
                              select c)
                              .FirstOrDefault();

            if (categorias is null)
                return NotFound($"Categoria com o id {id} não encontrada.");

            return categorias;
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            var categoriaExistente = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria is null)
                return NotFound();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria is null)
                return NotFound();  

            _context.Categorias.Remove(categoria);

            _context.SaveChanges();

            return Ok();
        }
    }
}
