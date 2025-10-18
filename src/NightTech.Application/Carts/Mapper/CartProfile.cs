using AutoMapper;
using NightTech.Application.Carts.Commands.UpdateCartItem;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;

namespace NightTech.Application.Carts.Mapper;

internal class CartProfile : Profile
{
    public CartProfile()
    {
        // CreateMap<Source, Destination>();
        CreateMap<Cart, PublicDtos.CartDto>();
        CreateMap<CartItem, PublicDtos.CartItemDto>();

        CreateMap<Cart, CartDto>()
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src =>
                src.Items != null ? src.Items.Sum(i => i.Price * i.Quantity) : 0))
            .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src =>
                src.Items != null ? src.Items.Count : 0))
            .ReverseMap();
        CreateMap<UpdateCartItemCommand, CartItem>();
    }
}
