using AutoMapper;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class QuestionModel
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class QuestionModelProfile : Profile
{
    public QuestionModelProfile()
    {
        CreateMap<Question, QuestionModel>();
    }
}