using AutoMapper;
using FluentValidation;
using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Questions.Models;

public class UpdateQuestionRequest
{
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class UpdateQuestionRequestValidator : AbstractValidator<UpdateQuestionRequest>
{
    public UpdateQuestionRequestValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(200);
    }
}

public class UpdateQuestionRequestProfile : Profile
{
    public UpdateQuestionRequestProfile()
    {
        CreateMap<UpdateQuestionRequest, UpdateQuestionModel>();
    }
}
