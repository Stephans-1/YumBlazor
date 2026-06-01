using Microsoft.EntityFrameworkCore;
using EShop.Data;
using EShop.Repository.IRepository;

namespace EShop.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> ClearCartAsync(string? userId)
        {
            var cartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();
            _db.ShoppingCart.RemoveRange(cartItems);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _db.ShoppingCart.Where(u => u.UserId == userId)
                .Include(u => u.Product)
                .ToListAsync();
        }

        public async Task<int> GetTotalCartItemCountAsync(string? userId)
        {
            int cartCount = 0;
            var cartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();
            foreach(var item in cartItems)
            {
                cartCount += item.Count;
            }
            return cartCount;
        }

        public async Task<bool> UpadateCartAsync(string userId, int productId, int updateBy)
        {
            if(string.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            var cart = _db.ShoppingCart.FirstOrDefault(u => u.UserId == userId && u.ProductId == productId);
            if(cart is null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = updateBy
                };
                await _db.ShoppingCart.AddRangeAsync(cart);
            }
            else
            {
                cart.Count += updateBy;
                if(cart.Count <= 0)
                {
                    _db.ShoppingCart.Remove(cart);
                }
            }
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
