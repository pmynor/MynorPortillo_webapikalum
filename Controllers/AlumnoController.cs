using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;
namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/Alumno")]
    public class AlumnoController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<AlumnoController> Logger;
        public AlumnoController(KalumDbContext _DbContext, ILogger<AlumnoController> _Logger)
        {
            this.DbContext = _DbContext;
            this.Logger = _Logger;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Alumno>>> Get()
        {
            List<Alumno> alumnos = null;
            Logger.LogDebug("Inciciando proceso de consulta de examenes de admisiones.");
            alumnos = await DbContext.Alumno.Include(c => c.Inscripciones).Include(c => c.CuentasXCobrar).ToListAsync();
            if (alumnos == null || alumnos.Count == 0)
            {
                Logger.LogWarning("No existe carreras técnicas");
                return new NoContentResult();
            }
            Logger.LogInformation("Se ejecuta la petición de forma exitosa");
            return Ok(alumnos);

        }

        [HttpGet("{id}", Name = "GetAlumno")]
        public async Task<ActionResult<Alumno>> GetAlumnos(string id)
        {
            Logger.LogDebug("Inicando el proceso de busqueda con el id" + id);
            var alumnos = await DbContext.Alumno.FirstOrDefaultAsync(ct => ct.Carne == id);
            if (alumnos == null)
            {
                Logger.LogWarning("No existe la carrera técnica con el id" + id);
                return new NoContentResult();
            }
            Logger.LogInformation(":tRAN OK:");
            return Ok(alumnos);
        }





    }
}