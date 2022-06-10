using AutoMapper;
using InteractiveHelper.QuizConstructionServices.Answers.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Quizes.Models;

public class QuizResponse
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
}

public class AnswerResponseProfile : Profile
{
    public AnswerResponseProfile()
    {
        CreateMap<OutputAnswerModel, QuizResponse>();
    }
}
