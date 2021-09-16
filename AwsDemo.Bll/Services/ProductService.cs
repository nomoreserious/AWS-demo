using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwsDemo.Bll.Contracts.Domains;
using AwsDemo.Bll.Contracts.Services;

namespace AwsDemo.Bll.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductStorage _storage;

		public ProductService(IProductStorage storage)
		{
			_storage = storage;
		}

		public async Task<Product> GetAsync(Guid productId)
		{
			return await _storage.ReadAsync(productId);
		}

		public ICollection<Product> Get(string category, string nameFilter)
		{
			var products = _storage.Products;
			if (!string.IsNullOrWhiteSpace(category))
			{
				products = products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
			}

			if (!string.IsNullOrWhiteSpace(nameFilter))
			{
				products = products.Where(p => p.Name.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) != -1);
			}

			return products.ToArray();
			
		}

		public async Task<Product> AddAsync(Product product)
		{
			product.Id = await _storage.CreateAsync(product);
			return product;
		}

		public async Task<Product> UpdateAsync(Guid productId, Product product)
		{
			if (await _storage.UpdateAsync(productId, product))
			{
				product.Id = productId;
				return product;
			}

			return null;
		}

		public async Task<bool> RemoveAsync(Guid productId)
		{
			return await _storage.DeleteAsync(productId);
		}
	}
}