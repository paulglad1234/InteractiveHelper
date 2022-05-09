using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.CharactetisricService.Models;

public class UpdateCharacteristicModel
{
    public string Name { get; set; }
}

public class UpdateCharacteristicModelValidator : AbstractValidator<UpdateCharacteristicModel>
{
    public UpdateCharacteristicModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(30).WithMessage("Name is too long.");
    }
}

public class UpdateCharacteristicModelProfile : Profile
{
    public UpdateCharacteristicModelProfile()
    {
        CreateMap<UpdateCharacteristicModel, Characteristic>();
    }
}
