using AutoMapper;
using DomainModel = ECommerceFeedback.Models.Domain.Response;
using DataModel = ECommerceFeedback.Models.Data;


namespace ECommerceFeedback.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataModel.Product, DomainModel.Products>().ReverseMap();

            CreateMap<DataModel.User, DomainModel.User>().ReverseMap();

            CreateMap<DataModel.ShoppingCartProducts, DomainModel.ShoppingCartProducts>()
               .ForPath(dest => dest.Product, opt => opt.MapFrom(src => src.Products));

            CreateMap<DataModel.UserCart, DomainModel.UserShoppingCart>().ReverseMap();


        }

    }
}
