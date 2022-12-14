using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        //Introduce Generics:

        // private readonly IProductRepository _repo;
        // public ProductsController(IProductRepository repo)
        // {
        //     _repo = repo;
        // }

        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        public ProductsController(IGenericRepository<Product> productsRepo,
                IGenericRepository<ProductBrand> productBrandRepo,
                IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _productRepo = productsRepo;

            _productTypeRepo = productTypeRepo;

            _productBrandRepo = productBrandRepo;

            _mapper = mapper;

        }
        
        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts
        ([FromQuery] ProductSpecParams productParms)
        {
            var spec = new ProductWithTypeAndBrandsSpecifications(productParms);

            var countSpec = new ProductWithFiltersCountSpecification(productParms);

            var totalItems= await _productRepo.CountAsync(countSpec);
            
            var products = await _productRepo.ListAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParms.PageIndex, productParms.PageSize, totalItems,data));

        }

         [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByID(int id)
        {
            var spec = new ProductWithTypeAndBrandsSpecifications(id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

         [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {

            return Ok(await _productBrandRepo.ListAllAsync());
        }

         [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetAllProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}