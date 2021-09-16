namespace AwsDemo.Dal.Entities
{
	public class ProductPoco
	{
		public string Id { get; set; }

		public string Timestamp { get; set; }

		public string Category { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public int Stock { get; set; }
	}
}