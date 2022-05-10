using AutoMapper;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class AnswerModel
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class AnswerModelProfile : Profile
{
    public AnswerModelProfile()
    {
        CreateMap<Answer, AnswerModel>();
    }
}
