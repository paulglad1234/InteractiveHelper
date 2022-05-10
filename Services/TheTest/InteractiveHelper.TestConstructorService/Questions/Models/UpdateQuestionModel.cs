using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class UpdateQuestionModel
{
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class UpdateQuestionModelValidator : AbstractValidator<UpdateQuestionModel>
{
    public UpdateQuestionModelValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(500);
    }
}

public class UpdateQuestionModelProfile : Profile
{
    public UpdateQuestionModelProfile()
    {
        CreateMap<UpdateQuestionModel, Question>();
    }
}
