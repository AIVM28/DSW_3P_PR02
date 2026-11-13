
using DSW_3P_PR02.Data;
using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Repositories;

public class EventoSQLRepository : IEventoRepository
{
    private readonly AppDbContext _context;

    public EventoSQLRepository(AppDbContext context)
    {
        _context = context;
    }

    // Obtener todos los eventos.
    public List<Evento> ObtenerTodos()
    {
        return _context.Eventos.ToList();
    }

    // Buscar un evento por su identificador.
    public Evento? ObtenerPorId(int id)
    {
        return _context.Eventos.Find(id);
    }

    // Registrar un nuevo evento.
    public Evento Agregar(Evento nuevoEvento)
    {
        nuevoEvento.Id = 0;

        _context.Eventos.Add(nuevoEvento);
        _context.SaveChanges();

        return nuevoEvento;
    }

    // Actualizar un evento existente.
    public bool Actualizar(int id, Evento eventoActualizado)
    {
        var existente = _context.Eventos.Find(id);

        if (existente == null)
        {
            return false;
        }

        existente.Nombre = eventoActualizado.Nombre;
        existente.Categoria = eventoActualizado.Categoria;
        existente.Fecha = eventoActualizado.Fecha;
        existente.Lugar = eventoActualizado.Lugar;
        existente.CupoMaximo = eventoActualizado.CupoMaximo;
        existente.Activo = eventoActualizado.Activo;

        _context.SaveChanges();

        return true;
    }

    // Eliminar un evento existente.
    public bool Eliminar(int id)
    {
        var existente = _context.Eventos.Find(id);

        if (existente == null)
        {
            return false;
        }

        _context.Eventos.Remove(existente);
        _context.SaveChanges();

        return true;
    }
}
