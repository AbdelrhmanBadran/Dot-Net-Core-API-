using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecifications specifications)
            : base(x =>
                    (String.IsNullOrEmpty(specifications.Search) || x.Name.Trim().ToLower().Contains(specifications.Search.Trim().ToLower())) &&
                    (!specifications.BrandId.HasValue || x.ProductBrandId == specifications.BrandId) &&
                    (!specifications.TypeId.HasValue || x.ProductTypeId == specifications.TypeId)
                )
        {

        }
    }
}
