using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Answers.Models;

public class InputAnswerModel
{
    public string Text { get; set; }
}

public class InputAnswerModelValidator : AbstractValidator<InputAnswerModel>
{
    public InputAnswerModelValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(200);
    }
}

public class InputAnswerModelProfile : Profile
{
    public InputAnswerModelProfile()
    {
        CreateMap<InputAnswerModel, Answer>();
    }
}
