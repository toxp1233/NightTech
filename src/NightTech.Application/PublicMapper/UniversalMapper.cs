using AutoMapper;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;

namespace NightTech.Application.PublicMapper;

public class UniversalMapper : Profile
{
    public UniversalMapper()
    {
        // CreateMap<Source, Destination>();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Cart, CartDto>().ReverseMap();
        CreateMap<CartItem, CartItemDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}
