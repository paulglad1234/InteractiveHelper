using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.ItemService.Models;

public class UpdateItemModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string ImagePath { get; set; }
}

public class UpdateItemModelValidator : AbstractValidator<UpdateItemModel>
{
    public UpdateItemModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name is too long.");

        RuleFor(x => x.Price)
            .GreaterThan(0f).WithMessage("Price is 0.00 or less");
    }
}

public class UpdateItemModelProfile : Profile
{
    public UpdateItemModelProfile()
    {
        CreateMap<UpdateItemModel, Item>();
    }
}