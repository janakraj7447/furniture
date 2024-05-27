using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NS.FAM.Data.CustomEntities;
using NS.FAM.Data.Entities;

using NS.FAM.Repository;

namespace NS.FAM.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FAMDBContext _context;
        public ProductRepository(FAMDBContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> GetAllProducts()
        {
            try
            {
                var products = _context.Products.Where(it => !it.IsDeleted);

                return await products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> ActivateDeactivateProduct(int Id)
        {
            try
            {
                int result = 0;
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
                if (product != null)
                {
                    product.IsActive = !product.IsActive;
                    result = await _context.SaveChangesAsync();

                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> DeleteProduct(int Id)
        {
            try
            {
                var result = 0;
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
                if (product != null)
                {
                    product.IsDeleted = true;
                    result = await _context.SaveChangesAsync();

                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // public async Task<int> AddCategory(Categories category)
        // {
        //     try
        //     {
        //          var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == 5);
        //         await _context.Categories.AddAsync(category);

        //         var result = await _context.SaveChangesAsync();
        //         return result;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }

        // }


        public int AddCategory(Category category)
        {
            try
            {
                _context.Category.Add(category);

                var result = _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> GetCategoryList()
        {
            try
            {
                var categories = _context.Category
               .Include(it => it.Subcategory)
               .Where(it => it.IsActive == true);

                return await categories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddSubCategory(SubCategory subCategory)
        {
            try
            {
                await _context.AddAsync(subCategory);
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IList<SubCategory>> GetSubCategoriesByCategoryId(long categoryId)
        {
            try
            {
                return await _context.SubCategory.Where(m => m.CategoryId == categoryId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SubCategory>> GetSubCategoryList()
        {
            try
            {
                var subCategories = _context.SubCategory
               .Where(it => it.IsActive == true);

                return await subCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

         public async Task<List<Products>> GetAvailableProducts()
        {
            try
            {
                var products = _context.Products
             .Include(it => it.Category)
             .Include(it => it.SubCategory)
             .Where(it => !it.IsDeleted && it.IsActive);


                return products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

           public async Task<List<Products>> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                List<Products> productList = new List<Products>();
                if (categoryId > 0)
                {
                    productList = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
                }
                else
                {
                    productList = await _context.Products.ToListAsync();
                }
                return productList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

          public async Task<List<Products>> GetProductBySubCategoryId(int subCategoryId)
        {
            try
            {
                List<Products> productList = new List<Products>();
                productList = await _context.Products.Where(x => x.SubCategoryId == subCategoryId).ToListAsync();
                return productList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

         public async Task<int> AddProductInCart(int productId, long userId)
        {
            try
            {
                var result = 0;
                var existingCartItem = await _context.Cart.FirstOrDefaultAsync(cartItem => cartItem.ProductId == productId && cartItem.UserId == userId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;

                }
                else
                {
                    var product = await _context.Products.FirstOrDefaultAsync(it => it.Id == productId);

                    if (product != null)
                    {
                        var cart = new Cart();
                        cart.ProductId = product.Id;
                        cart.UserId = userId;
                        cart.Quantity = 1;
                        cart.CreatedDate = DateTime.UtcNow;
                        cart.IsActive = true;
                        cart.IsDeleted = false;

                        _context.Cart.Add(cart);
                    }
                }

                result = await _context.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

          public async Task<List<Cart>> GetUserCartItems(long userId)
        {
            try
            {
                var cartItems = await _context.Cart
                .Include(it => it.Product)
                .Include(it => it.User)
                .Where(it => it.UserId == userId && it.IsActive && !it.IsDeleted)
                .OrderByDescending(it => it.CreatedDate)
                .ToListAsync();

                return cartItems;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteCartItem(int cartItemId)
        {
            try
            {
                var result = 0;
                var item = await _context.Cart.FirstOrDefaultAsync(x => x.Id == cartItemId);
                if (item != null)
                {
                    _context.Cart.Remove(item);
                    result = await _context.SaveChangesAsync();

                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
    }

            public async Task<int> AddQuantity(int productId, long userId)
        {
            try
            {
                var result = 0;
                if (_context.Cart.Any(x => x.ProductId == productId && x.UserId == userId))
                {
                    Cart cart = await _context.Cart.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);
                    cart.Quantity = cart.Quantity + 1;
                    result = await _context.SaveChangesAsync();
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<int> SubtractQuantity(int productId, long userId)
        {
            try
            {
                var result = 0;
                if (_context.Cart.Any(x => x.ProductId == productId && x.UserId == userId))
                {
                    Cart cart = await _context.Cart.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);
                    cart.Quantity = cart.Quantity - 1;
                    result = await _context.SaveChangesAsync();
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Task<int> AddProduct(Products product)
        {
            try
            {
                _context.Add(product);
                var result = _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

          public async Task<UpdateProductViewModel> GetById(int id)
        {
            try
            {
                var editProductViewModel = new UpdateProductViewModel();
                var result = await _context.Products.FirstOrDefaultAsync(it => it.Id == id);
                editProductViewModel.Id = id;
                editProductViewModel.Price = result.Price;
                editProductViewModel.Name = result.Name;
                editProductViewModel.Description = result.Description;
                return editProductViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> EditProduct(Products prod)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == prod.Id);
                prod.Name = product.Name;
                prod.Price = product.Price;
                prod.CategoryId = product.CategoryId;
                prod.SubCategoryId = product.SubCategoryId;
                prod.Price = product.Price;
                prod.UpdatedBy = product.CreatedBy;
                prod.Updateddate = DateTime.UtcNow;
                prod.Description = product.Description;
                prod.Photo = product.Description;
                _context.Update(prod);
                var result = _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}