using AutoMapper;
using RedLine.Services.Catalog.Models;

namespace RedLine.Services.Catalog.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {

        CreateMap<GunModels, GunDto>().ReverseMap();
        CreateMap<GunModels, GunUpdateDto>().ReverseMap();
        CreateMap<GunModels, GunCreateDto>().ReverseMap();
        CreateMap<GunCreateDto, GunDto>().ReverseMap();
        CreateMap<GunUpdateDto, GunDto>().ReverseMap();

        CreateMap<CategoryModels, CategoryDto>().ReverseMap();
        CreateMap<CategoryModels, CategoryUpdateDto>().ReverseMap();
        CreateMap<CategoryModels, CategoryCreateDto>().ReverseMap();

        CreateMap<FeatureModels, FeatureDto>().ReverseMap();
    }
}
