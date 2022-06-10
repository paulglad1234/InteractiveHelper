using AutoMapper;
using InteractiveHelper.CatalogServices.Characteristics.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Characteristics.Models;

public class ItemCharacteristicResponse
{
    public int CharacteristicId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

public class ItemCharacteristicResponseProfile : Profile
{
    public ItemCharacteristicResponseProfile()
    {
        CreateMap<ItemCharacteristicModel, ItemCharacteristicResponse>();
    }
}
