using AutoMapper;
using Portal.DevTest.API.ViewModels;
using Portal.DevTest.Date.Model;
using System.Collections.Generic;

namespace PortalTele.DevTest.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ProductViewModel, ProductModel>();
            // .ForMember(d => d.lstOrderItems, a => a.MapFrom(a => a.lstOrderItems));

            CreateMap<OrderViewModel, OrderModel>();
            //  .ForMember(d => d.User, a => a.MapFrom(a => a.User));

            CreateMap<List<OrderViewModel>, List<OrderModel>>();

            CreateMap<OrderItemViewModel, OrderItemModel>();
            //   .ForMember(d => d.Product, a => a.MapFrom(a => a.Product));

            CreateMap<List<OrderItemViewModel>, List<OrderItemModel>>();

            CreateMap<UserViewModel, UserModel>();
            // .ForMember(d => d.lstOrders, a => a.MapFrom(a => a.lstOrders));
        }
    }
}
