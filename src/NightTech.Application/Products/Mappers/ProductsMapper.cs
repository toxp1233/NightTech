using AutoMapper;
using NightTech.Application.Products.Commands.AddProduct;
using NightTech.Application.Products.Commands.UpdateProduct;
using NightTech.Domain.Entities;

namespace NightTech.Application.Products.Mappers;

public class ProductsMapper : Profile
{
    public ProductsMapper()
    {
        CreateMap<AddProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}
