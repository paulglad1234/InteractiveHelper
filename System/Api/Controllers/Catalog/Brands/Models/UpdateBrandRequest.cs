using AutoMapper;
using FluentValidation;
using InteractiveHelper.CatalogServices.Brands.Models;

namespace InteractiveHelper.Api.Controllers.Catalog.Brands.Models;

public class UpdateBrandRequest
{
    public string Name { get; set; }
    public class UpdateBrandRequestValidator : AbstractValidator<UpdateBrandRequest>
    {
        public UpdateBrandRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is too long.");
        }
    }
    public class UpdateBrandRequestProfile : Profile
    {
        public UpdateBrandRequestProfile()
        {
            CreateMap<UpdateBrandRequest, UpdateBrandModel>();
        }
    }
}
