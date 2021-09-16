using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

using AwsDemo.Bll.Contracts.Domains;
using AwsDemo.Bll.Contracts.Services;

namespace AwsDemo.Dal.Services
{
	public class InMemoryProductStorage : IProductStorage
	{
		private static readonly ConcurrentDictionary<Guid, Product> ProductStore = new ConcurrentDictionary<Guid, Product>();

		public IQueryable<Product> Products => ProductStore.Values.AsQueryable();

		public Task<Guid> CreateAsync(Product product)
		{
			var index = Guid.NewGuid();
			if (ProductStore.TryAdd(index, product))
			{
				return Task.FromResult(index);
			}

			throw new Exception("Something went wrong");
		}

		public Task<Product> ReadAsync(Guid productId)
		{
			ProductStore.TryGetValue(productId, out var product);
			return Task.FromResult(product);
		}

		public async Task<bool> UpdateAsync(Guid productId, Product product)
		{
			var value = await ReadAsync(productId);

			var result = value != null
				? ProductStore.TryUpdate(productId, product, value)
				: ProductStore.TryAdd(productId, product);

			return result;
		}

		public Task<bool> DeleteAsync(Guid productId)
		{
			var result = ProductStore.TryRemove(productId, out _);
			return Task.FromResult(result);
		}
	}
}
