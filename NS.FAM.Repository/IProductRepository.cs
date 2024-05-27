using NS.FAM.Data.Entities;
using NS.FAM.Data.CustomEntities;
using NS.FAM.Data.Entities;

namespace NS.FAM.Repository
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAllProducts();
        Task<int> ActivateDeactivateProduct(int id);
        Task<int> DeleteProduct(int id);
        // Task<int> AddCategory(Categories category);
        int AddCategory(Category category);
        Task<List<Category>> GetCategoryList();
        Task<int> AddSubCategory(SubCategory subCategory);
        Task<List<SubCategory>> GetSubCategoryList();
        Task<IList<SubCategory>> GetSubCategoriesByCategoryId(long categoryId);
        Task<List<Products>> GetAvailableProducts();
        Task<List<Products>> GetProductsByCategoryId(int categoryId);
        Task<List<Products>> GetProductBySubCategoryId(int subCategoryId);
        Task<int> AddProductInCart(int productId, long userId);
        Task<List<Cart>> GetUserCartItems(long userId);
        Task<int> DeleteCartItem(int cartItemId);
        Task<int> AddQuantity(int productId, long userId);
        Task<int> SubtractQuantity(int productId, long userId);
        Task<int> AddProduct(Products product);
        Task<UpdateProductViewModel> GetById(int id);      
        Task<int> EditProduct(Products product);

    }
}