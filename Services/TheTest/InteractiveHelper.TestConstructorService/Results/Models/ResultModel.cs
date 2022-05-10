using AutoMapper;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.QuizConstructionServices.Models;

public class ResultModel
{
    public int Id { get; set; }
    public string Conclusion { get; set; }
}

public class ResultModelProfile : Profile
{
    public ResultModelProfile()
    {
        CreateMap<Result, ResultModel>();
    }
}
