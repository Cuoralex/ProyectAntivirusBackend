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

            // Mapeo de CreateOpportunityDTO a Opportunity
            CreateMap<CreateOpportunityDTO, Opportunity>();
        }
    }
}