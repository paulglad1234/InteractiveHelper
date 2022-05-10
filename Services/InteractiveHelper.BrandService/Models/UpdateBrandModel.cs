using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.BrandService.Models;

public class UpdateBrandModel
{
    public string Name { get; set; }
}

public class UpdateBrandModelValidator : AbstractValidator<UpdateBrandModel>
{
    public UpdateBrandModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name is too long.");
    }
}

public class UpdateBrandModelProfile : Profile
{
    public UpdateBrandModelProfile()
    {
        CreateMap<UpdateBrandModel, Brand>();
    }
}