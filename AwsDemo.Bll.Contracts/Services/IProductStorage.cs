using System;
using System.Linq;
using System.Threading.Tasks;
using AwsDemo.Bll.Contracts.Domains;

namespace AwsDemo.Bll.Contracts.Services
{
	public interface IProductStorage
	{
		IQueryable<Product> Products { get; }

		Task<Guid> CreateAsync(Product product);

		Task<Product> ReadAsync(Guid productId);

		Task<bool> UpdateAsync(Guid productId, Product product);

		Task<bool> DeleteAsync(Guid productId);
	}
}
