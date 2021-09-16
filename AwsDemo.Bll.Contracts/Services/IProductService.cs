using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwsDemo.Bll.Contracts.Domains;

namespace AwsDemo.Bll.Contracts.Services
{
	public interface IProductService
	{
		ICollection<Product> Get(string category, string nameFilter);

		Task<Product> GetAsync(Guid productId);

		Task<Product> AddAsync(Product product);

		Task<Product> UpdateAsync(Guid productId, Product product);

		Task<bool> RemoveAsync(Guid productId);
	}
}
