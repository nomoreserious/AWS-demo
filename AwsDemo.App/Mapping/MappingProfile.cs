using AutoMapper;
using AwsDemo.App.Models;
using AwsDemo.Bll.Contracts.Domains;
namespace AwsDemo.App.Mapping
{
 public class MappingProfile : Profile
 {
	public MappingProfile()
	{
		CreateMap<ProductDto, Product>().ReverseMap();
	}
 }
}
