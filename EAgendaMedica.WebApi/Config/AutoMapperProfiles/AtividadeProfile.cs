using AutoMapper;
using EAgendaMedica.Dominio.ModuloAtividade;
using EAgendaMedica.WebApi.ViewModels;

namespace EAgendaMedica.WebApi.Config.AutoMapperProfiles {
    public class AtividadeProfile : Profile {
        public AtividadeProfile() {
            CreateMap<Atividade, ListarAtividadeViewModel>();
            CreateMap<Atividade, VisualizarAtividadeViewModel>();

            CreateMap<InserirAtividadeViewModel, Atividade>();
            CreateMap<EditarAtividadeViewModel, Atividade>();
        }
    }
}