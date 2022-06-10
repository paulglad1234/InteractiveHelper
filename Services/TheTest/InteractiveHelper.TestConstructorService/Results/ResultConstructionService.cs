using AutoMapper;
using InteractiveHelper.CatalogServices.Items.Models;
using InteractiveHelper.Common.Exceptions;
using InteractiveHelper.Db.Context;
using InteractiveHelper.Db.Entities.Catalog;
using InteractiveHelper.Db.Entities.Quiz;
using InteractiveHelper.QuizConstructionServices.Results.Models;
using Microsoft.EntityFrameworkCore;

namespace InteractiveHelper.QuizConstructionServices;

public class ResultConstructionService : IResultConstructionService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public ResultConstructionService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<OutputNodeModel> AddItemToLeaf(int itemId, int leafId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var leaf = await context.Nodes.FindAsync(leafId);
        CommonException.ThrowIfNull(leaf, "Leaf not found", 404);

        var item = await context.Items.FindAsync(itemId);
        CommonException.ThrowIfNull(item, "Item not found", 404);

        if (leaf.Items is null)
            leaf.Items = new List<Item>();

        leaf.Items.Add(item);
        await context.SaveChangesAsync();

        return mapper.Map<OutputNodeModel>(leaf);
    }

    public async Task<IEnumerable<ItemModel>> GetLeafItems(int leafId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var leaf = await context.Nodes.FindAsync(leafId);
        CommonException.ThrowIfNull(leaf, "Leaf not found", 404);

        return mapper.Map<IEnumerable<ItemModel>>(leaf.Items.ToList());
    }

    public async Task<OutputNodeModel> RegrowResultTreeForQuiz(int quizId)
    {
        //using var context = await dbContextFactory.CreateDbContextAsync();
        //var quiz = await context.Quizes.FindAsync(quizId);
        //CommonException.ThrowIfNull(quiz, "Quiz not found", 404);

        //var root = new ResultNode();
        //quiz.Root = root;
        //await context.Nodes.AddAsync(root);
        //context.Quizes.Update(quiz);

        throw new NotImplementedException();
    }

    private static void AddAnswersToTree()
    {

    }

    public async Task<OutputNodeModel> RemoveItemFromLeaf(int itemId, int leafId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var leaf = await context.Nodes.FindAsync(leafId);
        CommonException.ThrowIfNull(leaf, "Leaf not found", 404);

        var item = leaf.Items.FirstOrDefault(i => i.Id.Equals(itemId));
        if (item is not null)
        {
            leaf.Items.Remove(item);
            context.SaveChanges();
        }

        return mapper.Map<OutputNodeModel>(leaf);
    }
}
