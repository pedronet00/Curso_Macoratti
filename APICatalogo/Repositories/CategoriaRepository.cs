using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Contracts;
using APICatalogo.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{

    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaRepository(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public bool DeleteCategoria(int id)
    {
        var categoria = this.GetById(id);

        _context.Categorias.Remove(categoria);
        //_context.SaveChanges();
        _unitOfWork.Commit();

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
        //_context.SaveChanges();
        _unitOfWork.Commit();

        return categoria;
    }

    public bool UpdateCategoria(Categoria categoria)
    {
        if (categoria == null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Entry(categoria).State = EntityState.Modified;
        //_context.SaveChanges();
        _unitOfWork.Commit();

        return true;
    }
}
