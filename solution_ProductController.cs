using Microsoft.AspNetCore.Mvc;
using SampleWebApplication.Models;
using SampleWebApplication.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItemModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id);
            }

            var item = await _productService.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // GET api/<ProductController>/
        [HttpGet("")]
        public async Task<ActionResult<List<ProductItemModel>>> GetAll()
        {
            var items = await _productService.GetAllItems();
            return Ok(items.ToList());
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductItemModel>> Post([FromBody] AddProductItemModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedItem = await _productService.Add(value);
            return CreatedAtAction("Get", new { id = addedItem.Id }, addedItem);
        }

        // PUT api/<ProductController>/5
        [HttpPut("")]
        public async Task<ActionResult<ProductItemModel>> Put([FromBody] ProductItemModel value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingItem = await _productService.GetById(value.Id);
            if (existingItem == null)
            {
                return NotFound();
            }
            var updatedItem = await _productService.Update(value.Id, value.Name, value.Price);

            return CreatedAtAction("Get", new { id = updatedItem.Id }, updatedItem);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id);
            }
            var existingItem = await _productService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            await _productService.Remove(id);
            return Ok();
        }
    }
}
