using AutoMapper;
using FluentValidation;
using InteractiveHelper.CharactetisricService.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Characteristics.Models;

public class UpdateItemCharacteristicRequest
{
    public int CharacteristicId { get; set; }
    public string Value { get; set; }
}

public class UpdateItemCharacteristicRequestValidator : AbstractValidator<UpdateCharacteristicRequest>
{
    public UpdateItemCharacteristicRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(30).WithMessage("Name is too long.");
    }
}

public class UpdateItemCharacteristicRequestProfile : Profile
{
    public UpdateItemCharacteristicRequestProfile()
    {
        CreateMap<UpdateItemCharacteristicRequest, UpdateItemCharacteristicModel>();
    }
}
