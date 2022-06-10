using AutoMapper;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Questions.Models;

public class OutputQuestionModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public OutputQuestionModel Previous { get; set; }
    public OutputQuestionModel Next { get; set; }
}

public class OutputQuestionModelProfile : Profile
{
    public OutputQuestionModelProfile()
    {
        CreateMap<Question, OutputQuestionModel>()
            .ForMember(m => m.Previous, a => a.Ignore())
            .ForMember(m => m.Next, a => a.AllowNull());
    }
}