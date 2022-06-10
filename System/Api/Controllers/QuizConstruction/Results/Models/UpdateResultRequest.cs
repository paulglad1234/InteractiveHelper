using AutoMapper;
using FluentValidation;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Results.Models;

public class UpdateResultRequest
{
    public string Conclusion { get; set; }
}

public class UpdateResultRequestValidator : AbstractValidator<UpdateResultRequest>
{
    public UpdateResultRequestValidator()
    {
        RuleFor(x => x.Conclusion)
            .MaximumLength(1000);
    }
}

public class UpdateResultRequestProfile : Profile
{
    public UpdateResultRequestProfile()
    {
        //CreateMap<UpdateResultRequest, UpdateResultModel>();
    }
}
