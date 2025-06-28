using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductBrand Brand { get; set; }
        public int BrandId { get; set; }
        public ProductType Type { get; set; }
        public int TypeId { get; set; }
    }
}
