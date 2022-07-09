using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using WebApiKalum.Helpers;

namespace WebApiKalum.Entities
{

    public class Aspirante
    {
        [Required (ErrorMessage = "El campo {0} no debe ser")]
        [StringLength(12,MinimumLength = 12, ErrorMessage = "El campo número de expediente debe se de 12")]
        [NoExpediente]
        public string NoExpediente {get; set; }
        [Required (ErrorMessage = "El campo {0} no debe ser")]
        public string Apellidos {get; set; }
        [Required (ErrorMessage = "El campo {0} no debe ser")]
        public string Nombres {get; set; }
        [Required (ErrorMessage = "El campo {0} no debe ser")]
        public string Direccion {get; set; }
        [Required (ErrorMessage = "El campo {0} no debe ser")]
        public string Telefono {get; set; }
        [EmailAddress (ErrorMessage = "El correo electrónico no es valido")]
        public string Email {get; set; }
        public string Estatus {get; set; } = "NO ASIGNADO";
        public string CarreraId {get; set; }
        public string JornadaId { get;  set; }
        public string ExamenId {get; set; }
        public  virtual CarreraTecnica CarreraTecnica {get; set; }
        public  virtual  Jornada Jornada {get; set; }
        public virtual ExamenAdmision ExamenAdmision {get; set; }
        public virtual List<InscripcionPago> InscripcionPagos {get; set; }
        public virtual List<ResultadosExamenAdmision> ResultadosExamenAdmision { get; set; }

    
    }

}