using Microsoft.EntityFrameworkCore;
using EShop.Data;
using EShop.Repository.IRepository;
using static EShop.Repository.IRepository.ICategoryRepository;

namespace EShop.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<Category> CreateAsync(Category category)
        {
            if (category is not null)
            {
                _db.Add(category);
                await _db.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = _db.Category.FirstOrDefault(u => u.Id == id);
            if (category is not null)
            {
                _db.Remove(category);
                return await _db.SaveChangesAsync() > 0;
            }
            return true;
        }

        public async Task<Category> Get(int id)
        {
            var category = _db.Category.FirstOrDefault(u => u.Id == id);
            if(category is not null)
            {
                return category;
            }
            return new Category();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {

            return await _db.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            var categoryToUpdate = _db.Category.FirstOrDefault(u => u.Id == category.Id);
            if (categoryToUpdate is not null)
            {
                categoryToUpdate.Name = category.Name;
                _db.Update(categoryToUpdate);
                await _db.SaveChangesAsync();
                return category;
            }
            return category;
        }
    }
}
