using AutoMapper;
using K.Company.Core.DAOs;
using K.Company.Core.DTOs;

namespace K.Company.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>()
                .ForPath(dest => dest.Inventory.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<Store, StoreResponse>();
            CreateMap<StoreRequest, Store>();

            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>();

            CreateMap<OrderRequest, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderItems.Sum(x => x.Quantity * x.UnitPrice)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Waiting"));

            CreateMap<OrderItemRequest, OrderItem>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));

            CreateMap<Inventory, InventoryResponse>();

            CreateMap<Sales, SalesResponse>();

            CreateMap<Order, OrderResponse>();

            CreateMap<OrderItem, OrderItemResponse>();
        }
    }
}
