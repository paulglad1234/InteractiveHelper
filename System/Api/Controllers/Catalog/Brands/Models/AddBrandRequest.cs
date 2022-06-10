using AutoMapper;
using FluentValidation;
using InteractiveHelper.CatalogServices.Brands.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Brands.Models;

public class AddBrandRequest
{
    public string Name { get; set; }
    public class AddBrandRequestValidator : AbstractValidator<AddBrandRequest>
    {
        public AddBrandRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is too long.");
        }
    }
    public class AddBrandRequestProfile : Profile
    {
        public AddBrandRequestProfile()
        {
            CreateMap<AddBrandRequest, AddBrandModel>();
        }
    }
}
