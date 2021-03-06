using AutoMapper;
using InteractiveHelper.QuizConstructionServices.Answers.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Answers.Models;

public class AnswerResponse
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class AnswerResponseProfile : Profile
{
    public AnswerResponseProfile()
    {
        CreateMap<OutputAnswerModel, AnswerResponse>();
    }
}
