using Market.Api.Entities;  
using Microsoft.EntityFrameworkCore;  
using Microsoft.Extensions.Logging;  

namespace Market.Api.service  
{  
    public class ProductService : IProductService  
    {  
        private readonly AppDbContext dbContext;  
        private readonly ILogger<ProductService> logger;  

        public ProductService(AppDbContext dbContext, ILogger<ProductService> logger)  
        {  
            this.dbContext = dbContext;  
            this.logger = logger;  
        }  

        public async Task<IEnumerable<Product>> GetAllProductsAsync()  
        {  
            logger.LogInformation("Barcha mahsulotlarni olish jarayoni boshlandi.");  
            var products = await dbContext.Products.Include(p => p.ProductDetail).ToListAsync();  
            logger.LogInformation("Barcha mahsulotlar muvaffaqiyatli olindi. Mahsulotlar soni: {count}", products.Count());  
            return products;  
        }  

        public async Task<Product> GetProductByIdAsync(Guid id)  
        {  
            logger.LogInformation("Mahsulot ID: {id} bo'yicha ma'lumotlarni olish jarayoni boshlandi.", id);  
            var product = await dbContext.Products.Include(p => p.ProductDetail).FirstOrDefaultAsync(p => p.Id == id);  

            if (product != null)  
            {  
                logger.LogInformation("Mahsulot ID: {id} bo'yicha ma'lumot muvaffaqiyatli topildi: {product}", id, product);  
            }  
            else  
            {  
                logger.LogWarning("Mahsulot ID: {id} bo'yicha ma'lumot topilmadi.", id);  
            }  

            return product;  
        }  

        public async Task<Product> CreateProductAsync(Product product)  
        {  
            product.Id = Guid.NewGuid();  
            product.CreatedAt = DateTime.UtcNow;  
            product.ModifiedAt = DateTime.UtcNow;  

            logger.LogInformation("Yangi mahsulot yaratish jarayoni boshlandi: {product}", product);  
            dbContext.Products.Add(product);  
            await dbContext.SaveChangesAsync();  
            logger.LogInformation("Mahsulot muvaffaqiyatli yaratildi: {product}", product);  
            return product;  
        }  

        public async Task<Product> UpdateProductAsync(Guid id, Product product)  
        {  
            logger.LogInformation("Mahsulot ID: {id} bo'yicha yangilash jarayoni boshlandi.", id);  
            var existingProduct = await dbContext.Products.FindAsync(id);  
            if (existingProduct == null)  
            {  
                logger.LogWarning("Mahsulot ID: {id} bo'yicha yangilanish uchun ma'lumot topilmadi.", id);  
                return null;  
            }  

            existingProduct.Name = product.Name;  
            existingProduct.Price = product.Price;  
            existingProduct.ModifiedAt = DateTime.UtcNow;  
            existingProduct.Status = product.Status;  

            await dbContext.SaveChangesAsync();  
            logger.LogInformation("Mahsulot ID: {id} bo'yicha muvaffaqiyatli yangilandi: {product}", id, existingProduct);  
            return existingProduct;  
        }  

        public async Task<bool> DeleteProductAsync(Guid id)  
        {  
            logger.LogInformation("Mahsulot ID: {id} bo'yicha o'chirish jarayoni boshlandi.", id);  
            var product = await dbContext.Products.FindAsync(id);  
            if (product == null)  
            {  
                logger.LogWarning("Mahsulot ID: {id} bo'yicha o'chirish uchun ma'lumot topilmadi.", id);  
                return false;  
            }  

            dbContext.Products.Remove(product);  
            await dbContext.SaveChangesAsync();  
            logger.LogInformation("Mahsulot ID: {id} bo'yicha muvaffaqiyatli o'chirildi.", id);  
            return true;  
        }  
    }  
}