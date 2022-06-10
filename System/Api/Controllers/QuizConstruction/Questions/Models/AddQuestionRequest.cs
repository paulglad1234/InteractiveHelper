using AutoMapper;
using FluentValidation;
using InteractiveHelper.QuizConstructionServices.Questions.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Questions.Models;

public class AddQuestionRequest
{
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class AddQuestionRequestValidator : AbstractValidator<AddQuestionRequest>
{
    public AddQuestionRequestValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(500);
    }
}

public class AddQuestionRequestProfile : Profile
{
    public AddQuestionRequestProfile()
    {
        CreateMap<AddQuestionRequest, InputQuestionModel>();
    }
}
