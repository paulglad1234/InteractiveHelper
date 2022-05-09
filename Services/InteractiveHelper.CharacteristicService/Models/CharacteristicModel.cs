using AutoMapper;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.CharactetisricService.Models;

public class CharacteristicModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CharactericticModelProfile : Profile
{
    public CharactericticModelProfile()
    {
        CreateMap<Characteristic, CharacteristicModel>();
    }
}
