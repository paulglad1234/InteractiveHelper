using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.CharactetisricService.Models;

public class UpdateItemCharacteristicModel
{
    public int CharacteristicId { get; set; }
    public string Value { get; set; }
}

public class UpdateItemCharacteristicModelValidator : AbstractValidator<UpdateCharacteristicModel>
{
    public UpdateItemCharacteristicModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(30).WithMessage("Name is too long.");
    }
}

public class UpdateItemCharacteristicModelProfile : Profile
{
    public UpdateItemCharacteristicModelProfile()
    {
        CreateMap<UpdateItemCharacteristicModel, ItemCharacteristic>();
    }
}
