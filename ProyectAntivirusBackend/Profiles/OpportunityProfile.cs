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
                .ForMember(dest => dest.SectorId, opt => opt.MapFrom(src => src.Sectors.Id))
                .ForMember(dest => dest.SectorName, opt => opt.MapFrom(src => src.Sectors.Name))
                .ForMember(dest => dest.InstitutionId, opt => opt.MapFrom(src => src.Institutions.Id))
                .ForMember(dest => dest.InstitutionName, opt => opt.MapFrom(src => src.Institutions.Name))
                .ForMember(dest => dest.InstitutionImage, opt => opt.MapFrom(src => src.Institutions.Image))
                .ForMember(dest => dest.InstitutionLink, opt => opt.MapFrom(src => src.Institutions.Link))
                .ForMember(dest => dest.OpportunityTypeId, opt => opt.MapFrom(src => src.OpportunityTypes.Id))
                .ForMember(dest => dest.OpportunityTypeName, opt => opt.MapFrom(src => src.OpportunityTypes.Name))
                .ForMember(dest => dest.LocalityId, opt => opt.MapFrom(src => src.Localities.Id))
                .ForMember(dest => dest.LocalityCity, opt => opt.MapFrom(src => src.Localities.City))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.OpportunityTypes.Categories.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.OpportunityTypes.Categories.Name));

            CreateMap<CreateOpportunityDTO, Opportunity>()
                .ForMember(dest => dest.SectorId, opt => opt.MapFrom(src => src.SectorId))
                .ForMember(dest => dest.Sectors, opt => opt.MapFrom(src => src.SectorName))
                .ForMember(dest => dest.InstitutionId, opt => opt.MapFrom(src => src.InstitutionId))
                .ForMember(dest => dest.Institutions, opt => opt.MapFrom(src => src.InstitutionName))
                .ForMember(dest => dest.Institutions, opt => opt.MapFrom(src => src.InstitutionImage))
                .ForMember(dest => dest.Institutions, opt => opt.MapFrom(src => src.InstitutionLink))
                .ForMember(dest => dest.OpportunityTypeId, opt => opt.MapFrom(src => src.OpportunityTypeId))
                .ForMember(dest => dest.OpportunityTypes, opt => opt.MapFrom(src => src.OpportunityTypeName))
                .ForMember(dest => dest.LocalityId, opt => opt.MapFrom(src => src.LocalityId))
                .ForMember(dest => dest.Localities, opt => opt.MapFrom(src => src.LocalityCity));

        }
    }
}