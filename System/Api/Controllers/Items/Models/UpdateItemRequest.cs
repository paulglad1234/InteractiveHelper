using AutoMapper;
using FluentValidation;
using InteractiveHelper.ItemService.Models;

namespace InteractiveHelper.Api.Controllers.Items.Models;

public class UpdateItemRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public IFormFile Image { get; set; }
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
            .Must(x => Path.GetExtension(x.FileName) == ".jpeg").WithMessage("The file must have .jpg extension")
            .Must(x => x.Length <= 2048).WithMessage("The file is too large.");
    }
}

public class UpdateItemRequestProfile : Profile
{
    public UpdateItemRequestProfile()
    {
        CreateMap<UpdateItemRequest, UpdateItemModel>()
                .ForMember(m => m.Image, a => a.MapFrom(r => r.Image.IFormFileToByteArray()));
    }
}
