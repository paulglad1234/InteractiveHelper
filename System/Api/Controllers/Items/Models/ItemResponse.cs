﻿using AutoMapper;
using InteractiveHelper.ItemService.Models;

namespace InteractiveHelper.Api.Controllers.Items.Models;

public class ItemResponse
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
        CreateMap<ItemModel, ItemResponse>();
    }
}
