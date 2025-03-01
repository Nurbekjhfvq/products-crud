namespace Market.Api.Entities;
public class ProductDetail
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public string Material { get; set; }
    public decimal Weight { get; set; }
    public int QuantityInStock { get; set; }
    public DateTime ManufactureDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Size { get; set; }
    public string Manufacturer { get; set; }
    public string CountryOfOrigin { get; set; }

    // Foreign key
    public Guid ProductId { get; set; }

    // faqatgina navigatsiya uchun
    public Product Product { get; set; }
}