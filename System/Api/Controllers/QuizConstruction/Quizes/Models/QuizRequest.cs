using AutoMapper;
using FluentValidation;
using InteractiveHelper.QuizConstructionServices.Quizes.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Quizes.Models;

public class QuizRequest
{
    public string Url { get; set; }
    public string Title { get; set; }
    public string HelloMessage { get; set; }
}

public class QuizRequestValidator : AbstractValidator<QuizRequest>
{
    public QuizRequestValidator()
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

public class QuizRequestProfile : Profile
{
    public QuizRequestProfile()
    {
        CreateMap<QuizRequest, InputQuizModel>();
    }
}
