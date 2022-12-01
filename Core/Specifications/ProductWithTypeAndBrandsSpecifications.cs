using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandsSpecifications : BaseSpecifications<Product>
    {
        public ProductWithTypeAndBrandsSpecifications()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

          public ProductWithTypeAndBrandsSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }

}