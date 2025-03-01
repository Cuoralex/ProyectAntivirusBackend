using AutoMapper;
using ProyectAntivirusBackend.DTOs;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Profiles
{
    public class OpportunityProfile : AutoMapper.Profile
    {
        public OpportunityProfile()
        {
            CreateMap<Opportunity, OpportunityDTO>()
            .ForMember(dest => dest.Sectors_id, opt => opt.MapFrom(src => src.Sectors.Id))
            .ForMember(dest => dest.Institutions_id, opt => opt.MapFrom(src => src.Institutions.Id))
            .ForMember(dest => dest.Opportunity_Types_id, opt => opt.MapFrom(src => src.OpportunityTypes.Id));

        CreateMap<CreateOpportunityDTO, Opportunity>()
            .ForMember(dest => dest.SectorsId, opt => opt.MapFrom(src => src.SectorsId))
            .ForMember(dest => dest.InstitutionsId, opt => opt.MapFrom(src => src.InstitutionsId))
            .ForMember(dest => dest.OpportunityTypesId, opt => opt.MapFrom(src => src.OpportunityTypesId));
        }
    }
}