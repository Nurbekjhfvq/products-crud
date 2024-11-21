using Market.Api.Entities;  
using Microsoft.EntityFrameworkCore;  
using Microsoft.Extensions.Logging;  

namespace Market.Api.service  
{  
    public class ProductDetailService : IProductDetailService  
    {  
        private readonly AppDbContext dbContext;  
        private readonly ILogger<ProductDetailService> logger;  

        public ProductDetailService(AppDbContext dbContext, ILogger<ProductDetailService> logger)  
        {  
            this.dbContext = dbContext;  
            this.logger = logger;  
        }  

        public async Task<ProductDetail> GetDetailsByProductIdAsync(Guid productId)  
        {  
            logger.LogInformation("Mahsulot ID: {productId} bo'yicha tafsilotlarni olish jarayoni boshlandi.", productId);  
            var detail = await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);  

            if (detail != null)  
            {  
                logger.LogInformation("Mahsulot tafsilotlari muvaffaqiyatli topildi: {detail}", detail);  
            }  
            else  
            {  
                logger.LogWarning("Mahsulot ID: {productId} bo'yicha tafsilotlar topilmadi.", productId);  
            }  

            return detail;  
        }  

        public async Task<ProductDetail> CreateProductDetailAsync(Guid productId, ProductDetail detail)  
        {  
            detail.Id = Guid.NewGuid();  
            detail.ProductId = productId;  

            logger.LogInformation("Yangi mahsulot tafsilotlarini yaratish jarayoni boshlandi: {detail}", detail);  
            dbContext.ProductDetails.Add(detail);  
            await dbContext.SaveChangesAsync();  

            logger.LogInformation("Mahsulot tafsilotlari muvaffaqiyatli yaratildi: {detail}", detail);  
            return detail;  
        }  

        public async Task<ProductDetail> UpdateProductDetailAsync(Guid productId, ProductDetail detail)  
        {  
            logger.LogInformation("Mahsulot ID: {productId} bo'yicha tafsilotlarni yangilash jarayoni boshlandi.", productId);  
            var existingDetail = await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);  
            
            if (existingDetail == null)   
            {  
                logger.LogWarning("Mahsulot ID: {productId} bo'yicha yangilanadigan tafsilot topilmadi.", productId);  
                return null;  
            }  

            existingDetail.Description = detail.Description;  
            existingDetail.Color = detail.Color;  
            existingDetail.Material = detail.Material;  
            existingDetail.Weight = detail.Weight;  
            existingDetail.QuantityInStock = detail.QuantityInStock;  
            existingDetail.ManufactureDate = detail.ManufactureDate;  
            existingDetail.ExpiryDate = detail.ExpiryDate;  
            existingDetail.Size = detail.Size;  
            existingDetail.Manufacturer = detail.Manufacturer;  
            existingDetail.CountryOfOrigin = detail.CountryOfOrigin;  

            await dbContext.SaveChangesAsync();  
            logger.LogInformation("Mahsulot ID: {productId} bo'yicha tafsilotlar muvaffaqiyatli yangilandi: {detail}", productId, existingDetail);  
            return existingDetail;  
        }  

        public async Task<bool> DeleteProductDetailAsync(Guid productId)  
        {  
            logger.LogInformation("Mahsulot ID: {productId} bo'yicha tafsilotlarni o'chirish jarayoni boshlandi.", productId);  
            var detail = await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);  
            
            if (detail == null)   
            {  
                logger.LogWarning("Mahsulot ID: {productId} bo'yicha o'chiriladigan tafsilot topilmadi.", productId);  
                return false;  
            }  

            dbContext.ProductDetails.Remove(detail);  
            await dbContext.SaveChangesAsync();  
            logger.LogInformation("Mahsulot ID: {productId} bo'yicha tafsilotlar muvaffaqiyatli o'chirildi.", productId);  
            return true;  
        }  
    }  
}