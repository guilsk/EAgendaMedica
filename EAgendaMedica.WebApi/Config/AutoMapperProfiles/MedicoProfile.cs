using AutoMapper;
using EAgendaMedica.Dominio.ModuloMedico;
using EAgendaMedica.WebApi.ViewModels;

namespace EAgendaMedica.WebApi.Config.AutoMapperProfiles {
    public class MedicoProfile : Profile{
        public MedicoProfile() {
            CreateMap<Medico, ListarMedicoViewModel>();
            CreateMap<Medico, VisualizarMedicoViewModel>();

            CreateMap<InserirMedicoViewModel, Medico>();
            CreateMap<EditarMedicoViewModel, Medico>();
        }
    }
}
