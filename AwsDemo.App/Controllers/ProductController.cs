using System;
using System.Threading.Tasks;
using AutoMapper;
using AwsDemo.App.Models;
using AwsDemo.Bll.Contracts.Domains;
using AwsDemo.Bll.Contracts.Services;

using Microsoft.AspNetCore.Mvc;

namespace AwsDemo.App.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IProductService _productService;

		public ProductController(IMapper mapper, IProductService productService)
		{
			_mapper = mapper;
			_productService = productService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var products = _productService.Get(null, null);

			return Ok(_mapper.Map<ProductDto[]>(products));
		}

		[HttpGet("{productId}", Name = "GetProduct")]
		public async Task<IActionResult> Get(Guid productId)
		{
			var product = await _productService.GetAsync(productId);
			if (product == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<ProductDto>(product));
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] ProductDto product)
		{
			var created = await _productService.AddAsync(_mapper.Map<Product>(product));

			return CreatedAtRoute(
				"GetProduct",
				new
				{
					productId = created.Id
				},
				_mapper.Map<ProductDto>(created));
		}

		[HttpPut("{productId}")]
		public async Task<IActionResult> Update(Guid productId, [FromBody] ProductDto product)
		{
			var updated = await _productService.UpdateAsync(productId, _mapper.Map<Product>(product));
			return Ok(_mapper.Map<ProductDto>(updated));
		}

		[HttpDelete("{productId}")]
		public async Task<IActionResult> Remove(Guid productId)
		{
			await _productService.RemoveAsync(productId);
			return Ok();
		}
	}
}