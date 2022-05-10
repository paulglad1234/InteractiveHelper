using AutoMapper;
using InteractiveHelper.QuizConstructionServices.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Questions.Models;

public class QuestionResponse
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class QuestionResponseProfile : Profile
{
    public QuestionResponseProfile()
    {
        CreateMap<QuestionModel, QuestionResponse>();
    }
}
