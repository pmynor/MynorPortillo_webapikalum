using System.ComponentModel.DataAnnotations;

namespace WebApiKalum.Dtos
{
    public class CarreraTecnicaCreateDTO
    {
        public string CarreraId { get; set; }
        
        
        [StringLength(128,MinimumLength = 5, ErrorMessage = "Mensaje a mostrar.")]
        public string Nombre { get; set; }
       
    }


}