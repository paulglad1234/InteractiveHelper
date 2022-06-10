using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Questions.Models;

public class InputQuestionModel
{
    public string Text { get; set; }
}

public class InputQuestionModelValidator : AbstractValidator<InputQuestionModel>
{
    public InputQuestionModelValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(300);
    }
}

public class InputQuestionModelProfile : Profile
{
    public InputQuestionModelProfile()
    {
        CreateMap<InputQuestionModel, Question>();
    }
}
