using AutoMapper;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.CharactetisricService.Models;

public class ItemCharacteristicModel
{
    public int CharacteristicId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

public class ItemCharactericticModelProfile : Profile
{
    public ItemCharactericticModelProfile()
    {
        CreateMap<ItemCharacteristic, ItemCharacteristicModel>()
            .ForMember(model => model.Name, a => a.MapFrom(ic => ic.Characteristic.Name));
    }
}
