namespace WebApiKalum.Dtos
{
    public class CarreraTecnicaAspiranteList
    {
        public string CarreraId { get; set; }
        public string Nombre { get; set; }
        public List<CarreraTecnicaAspiranteList> Aspirantes {get; set; }
        public List<InscripcionListDTO> Inscripciones {get; set; }

    }
}