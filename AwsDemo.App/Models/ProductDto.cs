using System;
using System.ComponentModel.DataAnnotations;

namespace AwsDemo.App.Models
{
	public class ProductDto
	{
		public Guid? Id { get; set; }

		public string Category { get; set; }

		[Required] public string Name { get; set; }

		[Required] public decimal Price { get; set; }

		public int Stock { get; set; }
	}
}