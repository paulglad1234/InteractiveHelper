using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.Catalog.Models;

public class AddItemModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string ImagePath { get; set; }
}

public class AddItemModelValidator : AbstractValidator<AddItemModel>
{
    public AddItemModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name is too long.");

        RuleFor(x => x.Price)
            .GreaterThan(0f).WithMessage("Price is 0.00 or less");
    }
}

public class AddItemModelProfile : Profile
{
    public AddItemModelProfile()
    {
        CreateMap<AddItemModel, Item>();
    }
}
