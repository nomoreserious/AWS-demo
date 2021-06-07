using AwsDemo.Models;
using AwsDemo.Services;

using Microsoft.AspNetCore.Mvc;

namespace AwsDemo.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductController : ControllerBase
	{
		private readonly ProductService _productService;

		public ProductController()
		{
			_productService = new ProductService();
		}

		[HttpGet("{productId}", Name = "GetProduct")]
		public IActionResult Get(int productId)
		{
			var product = _productService.GetProduct(productId);
			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		[HttpPut("{productId}/add")]
		public IActionResult Add(int productId, [FromBody] Product product)
		{
			var created = _productService.Add(productId, product);
			if (created == null)
			{
				return Conflict();
			}

			return CreatedAtRoute("GetProduct", new {productId = created.Id}, created);
		}

		[HttpDelete("{productId}")]
		public IActionResult Remove(int productId)
		{
			var removed = _productService.Remove(productId);
			if (removed == null)
			{
				return NotFound();
			}

			return Ok(removed);
		}
	}
}