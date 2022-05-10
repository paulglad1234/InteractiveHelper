using AutoMapper;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.CharactetisricService.Models;

public class ItemCharacteristicModel
{
    public int CharacteristicId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

public class ItemCharacteristicModelProfile : Profile
{
    public ItemCharacteristicModelProfile()
    {
        CreateMap<ItemCharacteristic, ItemCharacteristicModel>()
            .ForMember(model => model.Name, a => a.MapFrom(ic => ic.Characteristic.Name)); // Thanks to ef core lazy loading proxy
    }
}
