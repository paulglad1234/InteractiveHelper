using InteractiveHelper.CatalogServices.Items.Models;
using InteractiveHelper.QuizConstructionServices.Results.Models;

namespace InteractiveHelper.QuizConstructionServices;

public interface IResultConstructionService
{
    Task<OutputNodeModel> RegrowResultTreeForQuiz(int quizId);
    Task<IEnumerable<ItemModel>> GetLeafItems(int leafId);

    Task<OutputNodeModel> AddItemToLeaf(int itemId, int leafId);
    Task<OutputNodeModel> RemoveItemFromLeaf(int itemId, int leafId);
}
