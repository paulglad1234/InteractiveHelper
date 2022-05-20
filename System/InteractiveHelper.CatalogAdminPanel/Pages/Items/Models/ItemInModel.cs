namespace InteractiveHelper.CatalogAdminPanel;

using FluentValidation;

public class ItemInModel
{
    public int? Id { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public float Price { get; set; }
    public byte[] Image { get; set; }
}

public class ItemInModelValidator : AbstractValidator<ItemInModel>
{
    public ItemInModelValidator()
    {
        RuleFor(v => v.Name)
              .NotEmpty().WithMessage("Name is required")
              .MaximumLength(200).WithMessage("Name length must be less then 200");

        RuleFor(v => v.BrandId)
            .GreaterThan(0).WithMessage("Please, select a brand");

        RuleFor(v => v.CategoryId)
            .GreaterThan(0).WithMessage("Please, select a category");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price is 0.00 or less");

        RuleFor(x => x.Image)
        .Must(x => x.Length <= 2048).WithMessage("The image is too large.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ItemInModel>.CreateWithOptions(
            (ItemInModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}