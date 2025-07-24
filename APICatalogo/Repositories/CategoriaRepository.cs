using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{

    private readonly AppDbContext _context;

    public bool DeleteCategoria(int id)
    {
        var categoria = this.GetById(id);

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return true;
    }

    public Categoria GetById(int id)
    {
        var categoria = _context.Categorias
            .FirstOrDefault(c => c.Id == id);

        return categoria;
    }

    public IEnumerable<Categoria> GetCategorias()
    {
        return _context.Categorias
            .AsNoTracking()
            .ToList();
    }

    public Categoria InsertCategoria(Categoria categoria)
    {
        if (categoria == null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return categoria;
    }

    public bool UpdateCategoria(Categoria categoria)
    {
        if (categoria == null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return true;
    }
}
