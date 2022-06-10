using AutoMapper;
using InteractiveHelper.CatalogServices.Items.Models;
using InteractiveHelper.Db.Entities.Quiz;
using InteractiveHelper.QuizConstructionServices.Answers.Models;
using InteractiveHelper.QuizConstructionServices.Questions.Models;

namespace InteractiveHelper.QuizConstructionServices.Results.Models;

public class OutputNodeModel
{
    public int Id { get; set; }
    public OutputQuestionModel Question { get; set; }
    public OutputAnswerModel Answer { get; set; }
    public IEnumerable<OutputNodeModel> Children { get; set; }
    public IEnumerable<ItemModel> Items { get; set; }
}

public class OutputNodeModelProfile : Profile
{
    public OutputNodeModelProfile()
    {
        CreateMap<ResultNode, OutputNodeModel>()
            .ForMember(m => m.Children, a => a.MapFrom(s => s.Children.ToList()))
            .ForMember(m => m.Items, a => a.MapFrom(s => s.Items.ToList()))
            .ForAllMembers(a => a.AllowNull());
    }
}
