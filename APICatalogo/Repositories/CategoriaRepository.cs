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

    public async Task<bool> DeleteCategoriaAsync(int id)
    {
        var categoria = await this.GetByIdAsync(id);

        _context.Categorias.Remove(categoria);

        await _unitOfWork.Commit();

        return true;
    }

    public async Task<Categoria> GetByIdAsync(int id)
    {
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria is null)
            return null;

        return categoria;
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
    {
        return await _context.Categorias
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Categoria> InsertCategoriaAsync(Categoria categoria)
    {
        if (categoria == null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Categorias.Add(categoria);
        await _unitOfWork.Commit();

        return categoria;
    }

    public async Task<bool> UpdateCategoriaAsync(Categoria categoria)
    {
        if (categoria == null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }

        _context.Entry(categoria).State = EntityState.Modified;
        await _unitOfWork.Commit();

        return true;
    }
}
