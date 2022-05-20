using AutoMapper;
using FluentValidation;
using InteractiveHelper.ItemService.Models;
using System.Text;

namespace InteractiveHelper.Api.Controllers.Catalog.Items.Models;

public class AddItemRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    /// <summary>
    /// Image byte code
    /// </summary>
    public byte[] Image { get; set; }

    public int BrandId { get; set; }
    public int CategoryId { get; set; }

    public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
    {
        public AddItemRequestValidator()
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

    public class AddItemRequestProfile : Profile
    {
        public AddItemRequestProfile()
        {
            CreateMap<AddItemRequest, AddItemModel>();
        }
    }
}
