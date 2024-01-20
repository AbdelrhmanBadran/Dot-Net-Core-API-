using AutoMapper;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.AspNetCore.Mvc;
using Services.HandleResponse;
using Services.Helper;
using Services.Services.ProductServices.Dto;

namespace Services.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductResultDto> GetProductByIdAsync(int? id)
        {
            var specs = new ProductsWithTypesAndBrandsSpecificatons(id);

            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecificationsAsync(specs);

            //if(product is null)
            
            var mappedProduct = _mapper.Map<ProductResultDto>(product);

            return mappedProduct;
        }


        public async Task<Pagination<ProductResultDto>> GetProductsAsync(ProductSpecifications specifications)
        {
            var specs = new ProductsWithTypesAndBrandsSpecificatons(specifications);

            var products = await _unitOfWork.Repository<Product>().GetAllyWithSpecificationsAsync(specs);

            var totalItem = await _unitOfWork.Repository<Product>().CountAsync(specs);

            var mappedProduct = _mapper.Map<IReadOnlyList<ProductResultDto>>(products);

            return new Pagination<ProductResultDto>(specifications.PagIndex , specifications.PageSize , totalItem, mappedProduct);
        }


        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandAsync()
             => await _unitOfWork.Repository<ProductBrand>().GetByAllAsync();

        public async Task<IReadOnlyList<ProductType>> GetProductsTypesAsync()
             => await _unitOfWork.Repository<ProductType>().GetByAllAsync();

    }
}
