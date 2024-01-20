using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    public class ProductsWithTypesAndBrandsSpecificatons : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecificatons(ProductSpecifications specifications) :
            base( x =>
                    (String.IsNullOrEmpty(specifications.Search) || x.Name.Trim().ToLower().Contains(specifications.Search.Trim().ToLower() )) &&
                    (!specifications.BrandId.HasValue || x.ProductBrandId== specifications.BrandId) &&
                    (!specifications.TypeId.HasValue || x.ProductTypeId == specifications.TypeId) 
                )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            AddOrderBy(p => p.Name);
            ApplyPagination(specifications.PageSize * ( specifications.PagIndex - 1) , specifications.PageSize);
            if (!String.IsNullOrEmpty(specifications.Sort))
            {   
                switch(specifications.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecificatons(int? id) :
            base( x => x.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

    }
}
