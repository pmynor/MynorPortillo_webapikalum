using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;


namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/Jornada")]
    public class JornadaController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<JornadaController> Logger;
        public JornadaController(KalumDbContext _DbContext, ILogger<JornadaController> _Logger)
        {
            this.DbContext = _DbContext;
            this.Logger = _Logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JornadaController>>> Get()
        {
            List<Jornada> jornada = null;
            Logger.LogDebug("Inciciando proceso de consulta de jornadas.");
            jornada = await DbContext.Jornada.Include(c => c.Aspirantes).Include(c => c.Inscripciones).ToListAsync();
            if (jornada == null || jornada.Count == 0)
            {
                Logger.LogWarning("No existe jornadas");
                return new NoContentResult();
            }
            Logger.LogInformation("Se ejecuta la petición de forma exitosa");
            return Ok(jornada);
        }

        [HttpGet("{id}", Name = "GetJornada")]
        public async Task<ActionResult<Jornada>> GetJornada(string id)
        {
            Logger.LogDebug("Inicando el proceso de busqueda con el id" + id);
            var jornadas = await DbContext.Jornada.FirstOrDefaultAsync(ct => ct.JornadaId == id);
            if (jornadas == null)
            {
                Logger.LogWarning("No existe la carrera técnica con el id" + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finalizando el proceso de busqueda de forma exitosa");
            return Ok(jornadas);
        }
        [HttpPost]
        public async Task<ActionResult<Jornada>> Post([FromBody] Jornada value)
        {
            Logger.LogDebug("Iniciando el proceso de agregar nueva Jornada");
            value.JornadaId = Guid.NewGuid().ToString().ToUpper();
            await DbContext.Jornada.AddAsync(value);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Finalizando proceso de crear nueva Jornada");
            return new CreatedAtRouteResult("GetJornada",new {id = value.JornadaId}, value);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Jornada>> Delete(string id)
        {
            Logger.LogDebug("Iniciando proceso de Eliminación");
            Jornada jornada = await DbContext.Jornada.FirstOrDefaultAsync(ct => ct.JornadaId == id);
            if(jornada == null)
            {
                Logger.LogWarning($"No se encontró ninguna Jornada con el Id {id}");
                return NotFound();
            }
            else
            {
                DbContext.Jornada.Remove(jornada);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Se elimino la Jornada con id {id}");
                return jornada;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Jornada value)
        {
            Logger.LogDebug($"Iniciando el proceso de actualización de la  Jornada con el id{id}");
            Jornada jornada = await DbContext.Jornada.FirstOrDefaultAsync(ct => ct.JornadaId == id);
            if(jornada == null)
            {
                Logger.LogWarning($"No existe la Jornada con el Id {id}");
                return BadRequest();
            }
            jornada.NombreCorto = value.NombreCorto;
            jornada.Descripcion = value.Descripcion;
            DbContext.Entry(jornada).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Los datos han sido actualizados correctamente.");
            return NoContent();
        }





    }
}