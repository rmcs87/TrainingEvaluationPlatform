namespace TEP.Domain.Entities.ManyToMany
{
    public class AssetCategory
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
