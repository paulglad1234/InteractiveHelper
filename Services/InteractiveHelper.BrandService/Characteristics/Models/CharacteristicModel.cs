using AutoMapper;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.CatalogServices.Characteristics.Models;

public class CharacteristicModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CharacteristicModelProfile : Profile
{
    public CharacteristicModelProfile()
    {
        CreateMap<Characteristic, CharacteristicModel>();
    }
}
