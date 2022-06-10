using AutoMapper;
using InteractiveHelper.CatalogServices.Characteristics.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Characteristics.Models;

public class CharacteristicResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CharacteristicResponseProfile : Profile
{
    public CharacteristicResponseProfile()
    {
        CreateMap<CharacteristicModel, CharacteristicResponse>();
    }
}
