
using DSW_3P_PR02.Models;
using DSW_3P_PR02.Repositories;

namespace DSW_3P_PR02.Services;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _repository;

    public EventoService(IEventoRepository repository)
    {
        _repository = repository;
    }

    // Validar las reglas de negocio de un evento.
    private static void Validar(Evento evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nombre))
        {
            throw new ArgumentException(
                "El nombre del evento es obligatorio."
            );
        }

        if (evento.CupoMaximo <= 0)
        {
            throw new ArgumentException(
                "El cupo máximo debe ser mayor que cero."
            );
        }

        
        if (evento.Fecha.Date < DateTime.Today)
        {
            throw new ArgumentException(
                "La fecha del evento no puede ser anterior a la fecha actual."
            );
        }

    }

    // Obtener todos los eventos.
    public List<Evento> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    // Obtener un evento por su identificador.
    public Evento? ObtenerPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    // Registrar un nuevo evento.
    public Evento Agregar(Evento nuevoEvento)
    {
        Validar(nuevoEvento);

        // Todos los eventos nuevos comienzan activos.
        nuevoEvento.Activo = true;

        return _repository.Agregar(nuevoEvento);
    }

    // Actualizar un evento existente.
    public bool Actualizar(int id, Evento eventoActualizado)
    {
        if (_repository.ObtenerPorId(id) == null)
        {
            return false;
        }

        Validar(eventoActualizado);

        // El identificador de la URL prevalece sobre el del cuerpo.
        eventoActualizado.Id = id;

        return _repository.Actualizar(id, eventoActualizado);
    }

    // Eliminar un evento existente.
    public bool Eliminar(int id)
    {
        return _repository.Eliminar(id);
    }
}
