namespace Market.Api.Controllers;

using Market.Api.Dtos.ProductDetail;
using Market.Api.Entities;
using Market.Api.service;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class ProductDetailController : ControllerBase
{
    private readonly IProductDetailService _productDetailService;

    public ProductDetailController(IProductDetailService productDetailService)
    {
        _productDetailService = productDetailService;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductDetail(Guid productId)
    {
        var detail = await _productDetailService.GetDetailsByProductIdAsync(productId);
        if (detail == null) return NotFound();

       
        var result = new ProductDetailReadDto
        {
            Id = detail.Id,
            Description = detail.Description,
            Color = detail.Color,
            Material = detail.Material,
            Weight = detail.Weight,
            QuantityInStock = detail.QuantityInStock,
            ManufactureDate = detail.ManufactureDate,
            ExpiryDate = detail.ExpiryDate,
            Size = detail.Size,
            Manufacturer = detail.Manufacturer,
            CountryOfOrigin = detail.CountryOfOrigin
        };

        return Ok(result);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> CreateProductDetail(Guid productId, [FromBody] ProductDetailCreateDto createDto)
    {
        var detail = new ProductDetail
        {
            Description = createDto.Description,
            Color = createDto.Color,
            Material = createDto.Material,
            Weight = createDto.Weight,
            QuantityInStock = createDto.QuantityInStock,
            ManufactureDate = createDto.ManufactureDate,
            ExpiryDate = createDto.ExpiryDate,
            Size = createDto.Size,
            Manufacturer = createDto.Manufacturer,
            CountryOfOrigin = createDto.CountryOfOrigin
        };

        var createdDetail = await _productDetailService.CreateProductDetailAsync(productId, detail);

        return CreatedAtAction(nameof(GetProductDetail), new { productId = createdDetail.ProductId }, createdDetail);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProductDetail(Guid productId, [FromBody] ProductDetailUpdateDto updateDto)
    {
        var detail = new ProductDetail
        {
            Description = updateDto.Description,
            Color = updateDto.Color,
            Material = updateDto.Material,
            Weight = updateDto.Weight,
            QuantityInStock = updateDto.QuantityInStock,
            ManufactureDate = updateDto.ManufactureDate,
            ExpiryDate = updateDto.ExpiryDate,
            Size = updateDto.Size,
            Manufacturer = updateDto.Manufacturer,
            CountryOfOrigin = updateDto.CountryOfOrigin
        };

        var updatedDetail = await _productDetailService.UpdateProductDetailAsync(productId, detail);

        if (updatedDetail == null) return NotFound();
        return Ok(updatedDetail);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProductDetail(Guid productId)
    {
        var isDeleted = await _productDetailService.DeleteProductDetailAsync(productId);
        if (!isDeleted) return NotFound();

        return NoContent();
    }
}
