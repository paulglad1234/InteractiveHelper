using AutoMapper;
using FluentValidation;
using InteractiveHelper.CatalogServices.Characteristics.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Characteristics.Models;

public class UpdateItemCharacteristicRequest
{
    public int CharacteristicId { get; set; }
    public string Value { get; set; }
}

public class UpdateItemCharacteristicRequestValidator : AbstractValidator<UpdateItemCharacteristicRequest>
{
    public UpdateItemCharacteristicRequestValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.");
    }
}

public class UpdateItemCharacteristicRequestProfile : Profile
{
    public UpdateItemCharacteristicRequestProfile()
    {
        CreateMap<UpdateItemCharacteristicRequest, UpdateItemCharacteristicModel>();
    }
}
