using Store.Data.Entities;

namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductWithFilterAndcountSpecifications : BaseSpecification<Product>
    {
        public ProductWithFilterAndcountSpecifications(ProductSpecification specs)
            : base(
                 product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
                            (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value) &&
                            (string.IsNullOrEmpty(specs.Search) || product.Name.Trim().ToLower().Contains(specs.Search))
                 )
        {

        }
    }
}
