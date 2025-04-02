using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Data;
using ProyectAntivirusBackend.Models;
using ProyectAntivirusBackend.DTOs;

namespace ProyectAntivirusBackend.Controllers;

[ApiController]
[Route("api/v1/favorites")]
public class FavoriteController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public FavoriteController(ApplicationDbContext context, IMapper mapper) // Se añade el mapper al constructor
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Favorite>>> ObtenerFavoritos()
    {
        var favoritos = await _context.Favorites.ToListAsync();
        return Ok(favoritos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Favorite>> ObtenerFavoritoPorId(int id)
    {
        var favorito = await _context.Favorites.FindAsync(id);
        if (favorito == null)
        {
            return NotFound("Favorito no encontrado.");
        }
        return Ok(favorito);
    }

    [HttpGet("{userId}/favorites")]
    public async Task<ActionResult<List<Opportunity>>> GetUserFavorites(int userId)
    {
        var favorites = await _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Opportunity)
            .Select(f => f.Opportunity)
            .ToListAsync();

        return Ok(favorites);
    }

    [HttpPost]
    public async Task<IActionResult> MarcarFavorito([FromBody] Favorite favorite)
    {
        if (favorite == null)
        {
            return BadRequest("Datos inválidos.");
        }

        // Corrección: Usamos los nombres correctos de las propiedades en Favorite
        var estudiante = await _context.Users.FindAsync(favorite.UserId);
        var oportunidad = await _context.Opportunities.FindAsync(favorite.OportunityId); // Corrección de 'Oportunities' → 'Opportunities'

        if (estudiante == null || oportunidad == null)
        {
            return NotFound("Usuario u oportunidad no encontrados.");
        }

        // Corrección: Verificar si ya existe el favorito con los nombres correctos
        var favoriteExistente = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == favorite.UserId && f.OportunityId == favorite.OportunityId);

        if (favoriteExistente != null)
        {
            return Conflict("El favorito ya existe.");
        }

        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Favorito agregado correctamente.", data = favorite });
    }

    [HttpPost("favorites")]
    public async Task<IActionResult> AddFavorite([FromBody] Favorite favorite)
    {
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUserFavorites), new { userId = favorite.UserId }, favorite);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarFavorito(int id, [FromBody] Favorite favorite)
    {
        if (id != favorite.Id)
        {
            return BadRequest("El ID del favorito no coincide.");
        }

        var favoritoExistente = await _context.Favorites.FindAsync(id);
        if (favoritoExistente == null)
        {
            return NotFound("Favorito no encontrado.");
        }

        _context.Entry(favoritoExistente).CurrentValues.SetValues(favorite);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Favorito actualizado correctamente.", data = favorite });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarFavorito(int id)
    {
        var favorito = await _context.Favorites.FindAsync(id);
        if (favorito == null)
        {
            return NotFound("Favorito no encontrado.");
        }

        _context.Favorites.Remove(favorito);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Favorito eliminado correctamente." });
    }
}

