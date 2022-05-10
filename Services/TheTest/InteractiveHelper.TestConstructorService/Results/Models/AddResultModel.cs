using AutoMapper;
using FluentValidation;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class AddResultModel
{
    public string Conclusion { get; set; }
}

public class AddResultModelValidator : AbstractValidator<AddResultModel>
{
    public AddResultModelValidator()
    {
        RuleFor(x => x.Conclusion)
            .MaximumLength(1000);
    }
}

public class AddResultModelProfile : Profile
{
    public AddResultModelProfile()
    {
        CreateMap<AddResultModel, Result>();
    }
}
