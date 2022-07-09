using AutoMapper;
using WebApiKalum.Dtos;
using WebApiKalum.Entities;

namespace WebApiKalum.Utilities
{
    public class AutoMapperProFiles : Profile
    {
        public AutoMapperProFiles()
        {
            CreateMap<CarreraTecnicaCreateDTO,CarreraTecnica>();
            CreateMap<CarreraTecnica, CarreraTecnicaCreateDTO>();
            CreateMap<Jornada,JornadaCreateDTO>();
            CreateMap<ExamenAdmision, ExamenAdmisionCreatedDTO>();
            CreateMap<Aspirante, AspiranteListDTO>().ConstructUsing(e =>new AspiranteListDTO{NombreCompleto = $"{e.Apellidos} {e.Nombres}"});
            CreateMap<Aspirante, CarreraTecnicaAspiranteList>().ConstructUsing(e =>new CarreraTecnicaAspiranteList{Nombre = $"{e.Apellidos} {e.Nombres}"});
            CreateMap<Inscripcion, InscripcionListDTO>();
            CreateMap<CarreraTecnica, CarreraTecnicaAspiranteList>();

        }
    }
}