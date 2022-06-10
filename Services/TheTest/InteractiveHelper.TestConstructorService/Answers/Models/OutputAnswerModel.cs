using AutoMapper;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Answers.Models;

public class OutputAnswerModel
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string Text { get; set; }
}

public class OutputAnswerModelProfile : Profile
{
    public OutputAnswerModelProfile()
    {
        CreateMap<Answer, OutputAnswerModel>();
    }
}
