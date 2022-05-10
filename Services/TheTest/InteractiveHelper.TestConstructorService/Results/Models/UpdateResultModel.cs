using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class UpdateResultModel
{
    public string Conclusion { get; set; }
}

public class UpdateResultModelValidator : AbstractValidator<UpdateResultModel>
{
    public UpdateResultModelValidator()
    {
        RuleFor(x => x.Conclusion)
            .MaximumLength(1000);
    }
}

public class UpdateResultModelProfile : Profile
{
    public UpdateResultModelProfile()
    {
        CreateMap<UpdateResultModel, Result>();
    }
}
