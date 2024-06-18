using AutoMapper;
using PTS.Application.Dto;
using PTS.Domain.Entities;
namespace PTS.Host
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, ProductEntity>().ReverseMap();
                config.CreateMap<CpuDto, CpuEntity>().ReverseMap();
                config.CreateMap<ColorDto, ColorEntity>().ReverseMap();
                config.CreateMap<VoucherDto, VoucherEntity>().ReverseMap();
                config.CreateMap<ContactDto, ContactEntity>().ReverseMap();
                config.CreateMap<HardDriveDto, HardDriveEntity>().ReverseMap();
                config.CreateMap<CreateProductDetailDto, ProductDetailEntity>().ReverseMap();
                config.CreateMap<ManagePostDto, ManagePostEntity>().ReverseMap();
                config.CreateMap<ManufacturerDto, ManufacturerEntity>().ReverseMap();
                config.CreateMap<ProductDto, ProductEntity>().ReverseMap();
                config.CreateMap<ProductTypeDto, ProductTypeEntity>().ReverseMap();
                config.CreateMap<RamDto, RamEntity>().ReverseMap();
                config.CreateMap<RoleDto, RoleEntity>().ReverseMap();
                config.CreateMap<ScreenDto, ScreenEntity>().ReverseMap();
                config.CreateMap<SerialDto, SerialEntity>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
