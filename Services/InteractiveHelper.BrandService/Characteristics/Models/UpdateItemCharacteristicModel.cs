using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.CatalogServices.Characteristics.Models;

public class UpdateItemCharacteristicModel
{
    public int CharacteristicId { get; set; }
    public string Value { get; set; }
}

public class UpdateItemCharacteristicModelValidator : AbstractValidator<UpdateItemCharacteristicModel>
{
    public UpdateItemCharacteristicModelValidator()
    {
        RuleFor(x => x.Value)
            .MaximumLength(50).WithMessage("Name is too long.");
    }
}

public class UpdateItemCharacteristicModelProfile : Profile
{
    public UpdateItemCharacteristicModelProfile()
    {
        CreateMap<UpdateItemCharacteristicModel, ItemCharacteristic>();
    }
}
