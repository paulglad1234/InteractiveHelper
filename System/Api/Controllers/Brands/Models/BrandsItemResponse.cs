﻿using AutoMapper;
using InteractiveHelper.BrandService.Models;

namespace InteractiveHelper.Api.Controllers.Brands.Models;

public class BrandsItemResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public byte[] Image { get; set; }
}

public class ItemResponseProfile : Profile
{
    public ItemResponseProfile()
    {
        CreateMap<ItemModel, BrandsItemResponse>();
    }
}
