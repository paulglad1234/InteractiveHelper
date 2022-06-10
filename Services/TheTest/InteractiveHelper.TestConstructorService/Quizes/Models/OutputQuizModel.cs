using AutoMapper;
using InteractiveHelper.Db.Entities.Quiz;
using InteractiveHelper.QuizConstructionServices.Questions.Models;

namespace InteractiveHelper.QuizConstructionServices.Quizes.Models;

public class OutputQuizModel
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string HelloMessage { get; set; }
    public OutputQuestionModel Head { get; set; }
}

public class OutputQuizModelProfile : Profile
{
    public OutputQuizModelProfile()
    {
        CreateMap<Quiz, OutputQuizModel>()
            .ForMember(m => m.Head, a => a.AllowNull());
    }
}
