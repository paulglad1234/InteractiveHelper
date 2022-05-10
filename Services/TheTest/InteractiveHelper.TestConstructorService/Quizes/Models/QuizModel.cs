using AutoMapper;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class QuizModel
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
}

public class QuizModelProfile : Profile
{
    public QuizModelProfile()
    {
        CreateMap<Quiz, QuizModel>();
    }
}
