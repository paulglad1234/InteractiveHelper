using InteractiveHelper.Db.Entities.Catalog;

namespace InteractiveHelper.Db.Entities.Quiz;

// in case of Answer.Metric usage
public class MetricResult : BaseEntity
{
    public int LowerBorder { get; set; }
    public int HigherBorder { get; set; }

    public virtual ICollection<Item> Items { get; set; }
}
