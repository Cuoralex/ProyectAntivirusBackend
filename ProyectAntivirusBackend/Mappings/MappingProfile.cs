using AutoMapper;
using ProyectAntivirusBackend.DTOs;
using ProyectAntivirusBackend.Models;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        // Mapeo de Opportunity
        CreateMap<CreateOpportunityDTO, Opportunity>()
            .ForMember(dest => dest.SectorId, opt => opt.MapFrom(src => src.SectorId))
            .ForMember(dest => dest.OpportunityTypeId, opt => opt.MapFrom(src => src.OpportunityTypeId))
            .ForMember(dest => dest.InstitutionId, opt => opt.MapFrom(src => src.InstitutionId));

        CreateMap<Opportunity, OpportunityDTO>()
            .ForMember(dest => dest.Sector, opt => opt.MapFrom(src => src.Sector.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.OpportunityType.Name))
            .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Institution.Name));
    }
}
