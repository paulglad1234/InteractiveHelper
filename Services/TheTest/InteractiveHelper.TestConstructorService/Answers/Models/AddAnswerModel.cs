using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class AddAnswerModel
{
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class AddAnswerModelValidator : AbstractValidator<AddAnswerModel>
{
    public AddAnswerModelValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(200);
    }
}

public class AddAnswerModelProfile : Profile
{
    public AddAnswerModelProfile()
    {
        CreateMap<AddAnswerModel, Answer>();
    }
}
