using Microsoft.EntityFrameworkCore;
using EShop.Data;
using EShop.Repository.IRepository;
using static EShop.Repository.IRepository.IProductRepository;

namespace EShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductRepository(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        public async Task<Product> CreateAsync(Product product)
        {
            if (product is not null)
            {
                _db.Add(product);
                await _db.SaveChangesAsync();
                return product;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = _db.Product.FirstOrDefault(u => u.Id == id);
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            if (product is not null)
            {
                _db.Remove(product);
                return await _db.SaveChangesAsync() > 0;
            }
            return true;
        }

        public async Task<Product> Get(int id)
        {
            var product = _db.Product.FirstOrDefault(u => u.Id == id);
            if(product is not null)
            {
                return product;
            }
            return new Product();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            return await _db.Product.Include(u => u.Category).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productToUpdate = _db.Product.FirstOrDefault(u => u.Id == product.Id);
            if (productToUpdate is not null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Description = product.Description;
                productToUpdate.SpecialTag = product.SpecialTag;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.Category = product.Category;
                productToUpdate.ImageUrl = product.ImageUrl;

                _db.Update(productToUpdate);
                await _db.SaveChangesAsync();
                return product;
            }
            return product;
        }
    }
}
