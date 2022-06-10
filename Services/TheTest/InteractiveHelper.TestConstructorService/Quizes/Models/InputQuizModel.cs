using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Quizes.Models;

public class InputQuizModel
{
    public string Url { get; set; }
    public string Title { get; set; }
    public string HelloMessage { get; set; }
}

public class InputQuizModelValidator : AbstractValidator<InputQuizModel>
{
    public InputQuizModelValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required.")
            .MaximumLength(16).WithMessage("Url is too long");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(16).WithMessage("Title is too long");
        RuleFor(x => x.HelloMessage)
            .NotEmpty().WithMessage("HelloMessage is required.")
            .MaximumLength(200).WithMessage("HelloMessage is too long");
    }
}

public class InputQuizModelProfile : Profile
{
    public InputQuizModelProfile()
    {
        CreateMap<InputQuizModel, Quiz>();
    }
}
