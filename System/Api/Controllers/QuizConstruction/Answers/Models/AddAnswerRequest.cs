using AutoMapper;
using FluentValidation;
using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Answers.Models;

public class AddAnswerRequest
{
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class AddAnswerRequestValidator : AbstractValidator<AddAnswerRequest>
{
    public AddAnswerRequestValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(200);
    }
}

public class AddAnswerRequestProfile : Profile
{
    public AddAnswerRequestProfile()
    {
        CreateMap<AddAnswerRequest, AddAnswerModel>();
    }
}
