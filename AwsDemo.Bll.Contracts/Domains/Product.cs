using System;

namespace AwsDemo.Bll.Contracts.Domains
{
	public class Product
	{
		public Product(
			Guid id,
			string category,
			string name,
			decimal price,
			int stock = 0)
		{
			Id = id;
			Category = category;
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Price = price;
			Stock = stock;
		}

		public Guid Id { get; set; }

		public string Category { get; }

		public string Name { get; }

		public decimal Price { get; }

		public int Stock { get; }

		public override string ToString()
		{
			return Name;
		}

		protected bool Equals(Product other)
		{
			return Id == other.Id && Category == other.Category && Name == other.Name && Price == other.Price && Stock == other.Stock;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != GetType())
			{
				return false;
			}

			return Equals((Product)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Id.GetHashCode();
				hashCode = (hashCode * 397) ^ (Category?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Name.GetHashCode());
				hashCode = (hashCode * 397) ^ Price.GetHashCode();
				hashCode = (hashCode * 397) ^ Stock;
				return hashCode;
			}
		}
	}
}
