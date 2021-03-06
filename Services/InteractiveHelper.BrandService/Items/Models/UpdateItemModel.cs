using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.CatalogServices.Items.Models;

public class UpdateItemModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public byte[] Image { get; set; }

    public int BrandId { get; set; }
    public int CategoryId { get; set; }
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

        RuleFor(x => x.Image)
            .Must(x => x.Length <= 2048);
    }
}

public class UpdateItemModelProfile : Profile
{
    public UpdateItemModelProfile()
    {
        CreateMap<UpdateItemModel, Item>();
    }
}