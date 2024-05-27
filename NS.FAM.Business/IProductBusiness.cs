using NS.FAM.Data.Entities;
using NS.FAM.Data.CustomEntities;
using System.Web.Mvc;

namespace NS.FAM.Business
{
    public interface IProductBusiness
    {     
       Task<List<Products>> GetAllProducts();
       Task<int> ActivateDeactivateProduct(int id);
       Task<int> DeleteProduct(int id);
       Task<int> AddCategory(CategoryViewModel category);
       Task<List<Category>> GetCategoryList();
       Task<int> AddSubCategory(SubCategoryViewModel subCategoryViewModel);
       Task<List<SubCategory>> GetSubCategoryList();
       Task<List<SelectListItem>> GetSubCategoryItems(long categoryId);
       Task<int> AddProduct(AddProductViewModel addProductViewModel);
       Task<List<Products>> GetAvailableProducts();
       Task<List<Products>> GetProductByCategoryId(int categoryId);
       Task<List<Products>> GetProductBySubCategoryId(int subCategoryId);
       Task<int> AddProductInCart(int productId, long userId);
       Task<List<Cart>> GetUserCartItems(long userId);
       Task<int> DeleteCartItem(int cartItemId);
       Task<int> AddQuantity(int productId, long userId);
       Task<int> SubtractQuantity(int productId, long userId);
       Task<int> EditProduct(UpdateProductViewModel editProductViewModel);
       Task<UpdateProductViewModel> GetById(int id);


    }
}