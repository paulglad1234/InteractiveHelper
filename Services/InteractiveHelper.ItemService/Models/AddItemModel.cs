using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.ItemService.Models;

public class AddItemModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public byte[] Image { get; set; }

    public int BrandId { get; set; }
    public int CategoryId { get; set; }
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

        RuleFor(x => x.Image)
            .Must(x => x.Length <= 2048);
    }
}

public class AddItemModelProfile : Profile
{
    public AddItemModelProfile()
    {
        CreateMap<AddItemModel, Item>();
    }
}
