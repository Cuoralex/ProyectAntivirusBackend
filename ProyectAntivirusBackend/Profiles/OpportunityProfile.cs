using AutoMapper;
using ProyectAntivirusBackend.DTOs;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Profiles
{
    public class OpportunityProfile : AutoMapper.Profile
    {
        public OpportunityProfile()
        {
            // Mapeo de Opportunity a OpportunityDTO
            CreateMap<Opportunity, OpportunityDTO>();

            // Mapeo de CreateOpportunityDTO a Opportunity, ignorando Sector e Institution
            CreateMap<CreateOpportunityDTO, Opportunity>()
                .ForMember(dest => dest.Sector, opt => opt.Ignore()) // Se asignará en el controlador
                .ForMember(dest => dest.Institution, opt => opt.Ignore()) // También se asignará en el controlador
                .ForMember(dest => dest.OpportunityType, opt => opt.Ignore()); // Se asignará en el controlador
        }
    }
}
