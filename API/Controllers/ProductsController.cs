using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]

    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
           var products = await _context.Products.ToListAsync();
           return Ok(products);
        }

        //        [HttpGet]
        // public ActionResult<List<Product>> GetProducts()
        // {
        //    var products = _context.Products.ToList();
        //    return Ok(products);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByID(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        
    }
}