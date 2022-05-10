using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class UpdateAnswerModel
{
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class UpdateAnswerModelValidator : AbstractValidator<UpdateAnswerModel>
{
    public UpdateAnswerModelValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(200);
    }
}

public class UpdateAnswerModelProfile : Profile
{
    public UpdateAnswerModelProfile()
    {
        CreateMap<UpdateAnswerModel, Answer>();
    }
}
