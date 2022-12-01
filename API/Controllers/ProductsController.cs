using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class ProductsController : ControllerBase
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
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetAllProducts()
        {
            var spec = new ProductWithTypeAndBrandsSpecifications();
            var products = await _productRepo.ListAsync(spec);
            return Ok(_mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));

            // products.Select(product => new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // }).ToList();
        }

        //        [HttpGet]
        // public ActionResult<List<Product>> GetProducts()
        // {
        //    var products = _context.Products.ToList();
        //    return Ok(products);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductByID(int id)
        {
            var spec = new ProductWithTypeAndBrandsSpecifications(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            // return new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // };

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {

            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetAllProductTypes()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }
    }
}