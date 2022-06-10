using AutoMapper;
using FluentValidation;
using InteractiveHelper.CatalogServices.Characteristics.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Characteristics.Models;

public class UpdateCharacteristicRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
}

public class UpdateCharacteristicRequestValidator : AbstractValidator<UpdateCharacteristicRequest>
{
    public UpdateCharacteristicRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(30).WithMessage("Name is too long.");
    }
}

public class UpdateCharacteristicRequestProfile : Profile
{
    public UpdateCharacteristicRequestProfile()
    {
        CreateMap<UpdateCharacteristicRequest, UpdateCharacteristicModel>();
    }
}
