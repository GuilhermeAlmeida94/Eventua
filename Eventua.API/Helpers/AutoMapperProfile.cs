using System.Linq;
using AutoMapper;
using Eventua.API.DTOs;
using Eventua.Domain.Identity;
using Eventua.Domain.Models;

namespace Eventua.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDTO>()
                .ForMember(dest => dest.Palestrantes, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(pe => pe.Palestrante).ToList());
                })
                .ReverseMap();

            CreateMap<Palestrante, PalestranteDTO>()
                .ForMember(dest => dest.Eventos, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(pe => pe.Evento).ToList());
                })
                .ReverseMap();

            CreateMap<Lote, LoteDTO>().ReverseMap();

            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
        
    }
}