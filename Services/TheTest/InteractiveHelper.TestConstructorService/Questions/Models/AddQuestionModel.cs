using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class AddQuestionModel
{
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class AddQuestionModelValidator : AbstractValidator<AddQuestionModel>
{
    public AddQuestionModelValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(500);
    }
}

public class AddQuestionModelProfile : Profile
{
    public AddQuestionModelProfile()
    {
        CreateMap<AddQuestionModel, Question>();
    }
}
