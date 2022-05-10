using AutoMapper;
using FluentValidation;
using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Answers.Models;

public class UpdateAnswerRequest
{
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class UpdateAnswerRequestValidator : AbstractValidator<UpdateAnswerRequest>
{
    public UpdateAnswerRequestValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(200);
    }
}

public class UpdateAnswerRequestProfile : Profile
{
    public UpdateAnswerRequestProfile()
    {
        CreateMap<UpdateAnswerRequest, UpdateAnswerModel>();
    }
}
