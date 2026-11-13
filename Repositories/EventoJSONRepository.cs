
using System.Text.Json;
using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Repositories;

public class EventoJSONRepository : IEventoRepository
{
    private readonly string _ruta;

    private readonly JsonSerializerOptions _opciones = new()
    {
        WriteIndented = true
    };

    public EventoJSONRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(
            environment.ContentRootPath,
            "Data",
            "eventos.json"
        );

        Directory.CreateDirectory(
            Path.GetDirectoryName(_ruta)!
        );

        if (!File.Exists(_ruta))
        {
            File.WriteAllText(_ruta, "[]");
        }
    }

    // Leer los eventos almacenados en el archivo JSON.
    private List<Evento> Leer()
    {
        string json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<Evento>>(json)
               ?? new List<Evento>();
    }

    // Guardar los eventos en el archivo JSON.
    private void Guardar(List<Evento> datos)
    {
        string json = JsonSerializer.Serialize(datos, _opciones);

        File.WriteAllText(_ruta, json);
    }

    // Obtener todos los eventos.
    public List<Evento> ObtenerTodos()
    {
        return Leer();
    }

    // Buscar un evento por su identificador.
    public Evento? ObtenerPorId(int id)
    {
        return Leer().FirstOrDefault(e => e.Id == id);
    }

    // Registrar un nuevo evento.
    public Evento Agregar(Evento nuevoEvento)
    {
        var datos = Leer();

        nuevoEvento.Id = datos.Count == 0
            ? 1
            : datos.Max(e => e.Id) + 1;

        datos.Add(nuevoEvento);

        Guardar(datos);

        return nuevoEvento;
    }

    // Actualizar un evento existente.
    public bool Actualizar(int id, Evento eventoActualizado)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(e => e.Id == id);

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

        Guardar(datos);

        return true;
    }

    // Eliminar un evento existente.
    public bool Eliminar(int id)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(e => e.Id == id);

        if (existente == null)
        {
            return false;
        }

        datos.Remove(existente);

        Guardar(datos);

        return true;
    }
}
