using AutoMapper;
using FluentValidation;
using InteractiveHelper.ItemService.Models;
using System.Text;

namespace InteractiveHelper.Api.Controllers.Catalog.Items.Models;

public class UpdateItemRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    /// <summary>
    /// Image byte code
    /// </summary>
    public string Image { get; set; }

    public int BrandId { get; set; }
    public int CategoryId { get; set; }
}

public class UpdateItemRequestValidator : AbstractValidator<UpdateItemRequest>
{
    public UpdateItemRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name is too long.");

        RuleFor(x => x.Price)
            .GreaterThan(0f).WithMessage("Price is 0.00 or less");

        RuleFor(x => x.Image)
            .Must(x => x.Length <= 2048).WithMessage("The image is too large.");
    }
}

public class UpdateItemRequestProfile : Profile
{
    public UpdateItemRequestProfile()
    {
        CreateMap<UpdateItemRequest, UpdateItemModel>()
                .ForMember(m => m.Image, a => a.MapFrom(r => Encoding.ASCII.GetBytes(r.Image)));
    }
}
