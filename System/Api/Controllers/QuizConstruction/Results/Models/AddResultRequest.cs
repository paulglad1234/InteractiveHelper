using AutoMapper;
using FluentValidation;
using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Results.Models;

public class AddResultRequest
{
    public string Conclusion { get; set; }
}

public class AddResultRequestValidator : AbstractValidator<AddResultRequest>
{
    public AddResultRequestValidator()
    {
        RuleFor(x => x.Conclusion)
            .MaximumLength(1000);
    }
}

public class AddResultRequestProfile : Profile
{
    public AddResultRequestProfile()
    {
        CreateMap<AddResultRequest, AddResultModel>();
    }
}
