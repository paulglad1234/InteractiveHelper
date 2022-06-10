using AutoMapper;
using InteractiveHelper.QuizConstructionServices.Results.Models;

namespace InteractiveHelper.Api.Controllers.QuizConstruction.Results.Models;

public class ResultResponse
{
    public int Id { get; set; }
    public string Conclusion { get; set; }
}

public class ResultResponseProfile : Profile
{
    public ResultResponseProfile()
    {
        CreateMap<OutputNodeModel, ResultResponse>();
    }
}
