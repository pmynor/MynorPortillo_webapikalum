using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;


namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/ExamenAdmision")]
    public class ExamenAdmisionController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<ExamenAdmisionController> Logger;
        public ExamenAdmisionController(KalumDbContext _DbContext, ILogger<ExamenAdmisionController> _Logger)
        {
            this.DbContext = _DbContext;
            this.Logger = _Logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamenAdmision>>> Get()
        {
            List<ExamenAdmision> examenAdmisiones = null;
            Logger.LogDebug("Inciciando proceso de consulta de examenes de admisiones.");
            examenAdmisiones = await DbContext.ExamenAdmision.Include(c => c.Aspirantes).ToListAsync();
            if (examenAdmisiones == null || examenAdmisiones.Count == 0)
            {
                Logger.LogWarning("No existe carreras técnicas");
                return new NoContentResult();
            }
            Logger.LogInformation("OK");
            return Ok(examenAdmisiones);

        }
        [HttpGet("{id}", Name = "GetExamenAdmision")]
        public async Task<ActionResult<ExamenAdmision>> GetExamenAdmision(string id)
        {
            Logger.LogDebug("Inicando el proceso de busqueda con el id" + id);
            var examen = await DbContext.ExamenAdmision.FirstOrDefaultAsync(ct => ct.ExamenId == id);
            if (examen == null)
            {
                Logger.LogWarning("No existe examen admision con el id" + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finalizando el proceso de busqueda de forma exitosa");
            return Ok(examen);
        }
        [HttpPost]
        public async Task<ActionResult<ExamenAdmision>> Post([FromBody] ExamenAdmision value)
        {
            Logger.LogDebug("Iniciando el proceso de agregar nueva fecha de Examen de Admision");
            value.ExamenId = Guid.NewGuid().ToString().ToUpper();
            await DbContext.ExamenAdmision.AddAsync(value);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Finalizando proceso de crear nueva fecha de Examen de Admision");
            return new CreatedAtRouteResult("GetExamenAdmision",new {id = value.ExamenId}, value);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamenAdmision>> Delete(string id)
        {
            Logger.LogDebug("Iniciando proceso de Eliminación");
            ExamenAdmision examenAdmision = await DbContext.ExamenAdmision.FirstOrDefaultAsync(ct => ct.ExamenId == id);
            if(examenAdmision == null)
            {
                Logger.LogWarning($"No se encontró ninguna fecha de Examen de admision con el Id {id}");
                return NotFound();
            }
            else
            {
                DbContext.ExamenAdmision.Remove(examenAdmision);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Se elimino el Examen de Admision con id {id}");
                return examenAdmision;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ExamenAdmision value)
        {
            Logger.LogDebug($"Iniciando el proceso de actualización del Examen de Admision con el id{id}");
            ExamenAdmision examenAdmision = await DbContext.ExamenAdmision.FirstOrDefaultAsync(ct => ct.ExamenId == id);
            if(examenAdmision == null)
            {
                Logger.LogWarning($"No existe Examen de Admision el Id {id}");
                return BadRequest();
            }
            examenAdmision.FechaExamen = value.FechaExamen;
            DbContext.Entry(examenAdmision).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}