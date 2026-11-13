
using DSW_3P_PR02.Models;
using DSW_3P_PR02.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSW_3P_PR02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly IEventoService _service;

    public EventosController(IEventoService service)
    {
        _service = service;
    }

    // GET: api/Eventos
    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        return Ok(_service.ObtenerTodos());
    }

    // GET: api/Eventos/1
    [HttpGet("{id:int}")]
    public IActionResult ObtenerPorId(int id)
    {
        var evento = _service.ObtenerPorId(id);

        if (evento == null)
        {
            return NotFound(
                new { mensaje = "El evento no existe." }
            );
        }

        return Ok(evento);
    }

    // POST: api/Eventos
    [HttpPost]
    public IActionResult Agregar([FromBody] Evento nuevoEvento)
    {
        try
        {
            var creado = _service.Agregar(nuevoEvento);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = creado.Id },
                creado
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                new { mensaje = ex.Message }
            );
        }
    }

    // PUT: api/Eventos/1
    [HttpPut("{id:int}")]
    public IActionResult Actualizar(
        int id,
        [FromBody] Evento eventoActualizado)
    {
        try
        {
            if (!_service.Actualizar(id, eventoActualizado))
            {
                return NotFound(
                    new { mensaje = "El evento no existe." }
                );
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                new { mensaje = ex.Message }
            );
        }
    }

    // DELETE: api/Eventos/1
    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        if (!_service.Eliminar(id))
        {
            return NotFound(
                new { mensaje = "El evento no existe." }
            );
        }

        return NoContent();
    }
}
