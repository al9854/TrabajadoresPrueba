using AutoMapper;
using MantenimientoTrabajadores.Dominio.Entidades;
using MantenimientoTrabajadores.Model.Request;
using MantenimientoTrabajadores.Model.Response;



namespace MantenimientoTrabajadores.Mappings
{ 
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Trabajadores, TrabajadorResponse>();
            CreateMap<TrabajadorRequest, Trabajadores>();
            CreateMap<Provincia, ProvinciaResponse>();
            CreateMap<ProvinciaRequest, Provincia>();
            CreateMap<Departamento, DepartamentoResponse>();
            CreateMap<DepartamentoRequest, Departamento>();
            CreateMap<Distrito, DistritoResponse>();
            CreateMap<DistritoRequest, Distrito>();
        }
    }

}