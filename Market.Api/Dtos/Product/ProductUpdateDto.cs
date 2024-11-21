using Market.Api.Entities;

namespace Market.Api.Dtos.Product;
public class ProductUpdateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public EProductStatus Status { get; set; }
}