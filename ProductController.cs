using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET all products
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;

                var products = _context.Products.ToList();

                return Ok(new
                {
                    callerRole = role,
                    data = products
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET product by id
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                    return NotFound("Product not found");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST add product
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public IActionResult Add(Product product)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
                    return BadRequest("Invalid product data");

                _context.Products.Add(product);
                _context.SaveChanges();

                return StatusCode(201, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT update product
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updated)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                    return NotFound("Product not found");

                product.Name = updated.Name;
                product.Category = updated.Category;
                product.Price = updated.Price;
                product.Stock = updated.Stock;

                _context.SaveChanges();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE product
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                    return NotFound("Product not found");

                _context.Products.Remove(product);
                _context.SaveChanges();

                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET by category
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet("category/{category}")]
        public IActionResult GetByCategory(string category)
        {
            try
            {
                var products = _context.Products
                    .Where(p => p.Category.ToLower() == category.ToLower())
                    .ToList();

                if (products.Count == 0)
                    return NotFound("No products found");

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // SORT
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet("sort")]
        public IActionResult SortByPrice(string order = "asc")
        {
            try
            {
                var products = order.ToLower() == "desc"
                    ? _context.Products.OrderByDescending(p => p.Price).ToList()
                    : _context.Products.OrderBy(p => p.Price).ToList();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // OUT OF STOCK
        [Authorize(Roles = "Admin,Manager,Viewer")]
        [HttpGet("outofstock")]
        public IActionResult GetOutOfStockProducts()
        {
            var products = _context.Products
                                   .Where(p => p.Stock == 0)
                                   .ToList();

            return Ok(products);
        }
    }
}