using System;
using System.Collections.Concurrent;

using AwsDemo.Models;

namespace AwsDemo.Services
{
	public class ProductService
	{
		private static readonly ConcurrentDictionary<int, Product> Products = new ConcurrentDictionary<int, Product>();

		public Product GetProduct(int productId)
		{
			if (Products.TryGetValue(productId, out var product))
			{
				return product;
			}

			return null;
		}

		public Product Add(int productId, Product product)
		{
			_ = product ?? throw new ArgumentNullException(nameof(product));

			if (Products.TryAdd(productId, product))
			{
				product.Id = productId;
				return product;
			}

			return null;
		}

		public Product Remove(int productId)
		{
			if (Products.TryRemove(productId, out var product))
			{
				return product;
			}

			return null;
		}
	}
}