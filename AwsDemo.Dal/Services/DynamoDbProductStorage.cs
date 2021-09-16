using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using AwsDemo.Bll.Contracts.Domains;
using AwsDemo.Bll.Contracts.Services;
using AwsDemo.Dal.Entities;

namespace AwsDemo.Dal.Services
{
	public class DynamoDbProductStorage : IProductStorage
	{
		private const string TimestampFormat = "yyyy-MM-ddTHH:mm:ss";
		private const string TableName = "aws-demo-products";
		private readonly DynamoDBContext _dbContext;

		private readonly DynamoDBOperationConfig _opConfig = new DynamoDBOperationConfig
		{
			OverrideTableName = TableName,
			IsEmptyStringValueEnabled = true,
			IgnoreNullValues = false
		};

		public DynamoDbProductStorage(string accessKey, string secretKey, string regionName)
		{
			_ = accessKey ?? throw new ArgumentNullException(nameof(accessKey));
			_ = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
			_ = regionName ?? throw new ArgumentNullException(nameof(regionName));

			var credentials = new BasicAWSCredentials(accessKey, secretKey);
			var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.GetBySystemName(regionName));
			_dbContext = new DynamoDBContext(client);
		}

		public DynamoDbProductStorage(AwsOptions options)
			: this(options?.AccessKey, options?.SecretKey, options?.RegionName)
		{ }

		public IQueryable<Product> Products { get; }

		public async Task<Guid> CreateAsync(Product product)
		{
			var index = Guid.NewGuid();
			var productPoco = new ProductPoco
			{
				Id = index.ToString(),
				Timestamp = DateTime.Now.ToString(TimestampFormat),
				Category = product.Category,
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
			};

			await _dbContext.SaveAsync(productPoco, _opConfig);
			return index;
		}

		public async Task<Product> ReadAsync(Guid productId)
		{
			var productPoco = await _dbContext.LoadAsync(
				new ProductPoco
				{
					Id = productId.ToString()
				},
				_opConfig);

			if (productPoco == null)
				return null;

			return new Product(
				productId,
				productPoco.Category,
				productPoco.Name,
				productPoco.Price,
				productPoco.Stock);
		}

		public async Task<bool> UpdateAsync(Guid productId, Product product)
		{
			var productPoco = new ProductPoco
			{
				Id = productId.ToString(),
				Timestamp = DateTime.Now.ToString(TimestampFormat),
				Category = product.Category,
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
			};

			await _dbContext.SaveAsync(productPoco, _opConfig);
			return true;
		}

		public async Task<bool> DeleteAsync(Guid productId)
		{
			await _dbContext.DeleteAsync(
				new ProductPoco
				{
					Id = productId.ToString()
				},
				_opConfig);

			return true;
		}
	}
}
