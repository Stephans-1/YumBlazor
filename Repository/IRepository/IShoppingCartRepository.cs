using EShop.Data;

namespace EShop.Repository.IRepository
{
    public interface IShoppingCartRepository
    {
        public Task<bool> UpadateCartAsync(string userId, int productId, int updateBy);
        public Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId);
        public Task<bool> ClearCartAsync(string? userId);
        public Task<int> GetTotalCartItemCountAsync(string? userId);
    }
}
