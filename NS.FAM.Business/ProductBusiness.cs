using NS.FAM.Data.CustomEntities;
using NS.FAM.Data.Entities;
using NS.FAM.Repository;
using System.Web.Mvc;

namespace NS.FAM.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _iProductRepository;

        public ProductBusiness(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;
        }


        public async Task<List<Products>> GetAllProducts()
        {
            try
            {
                return await _iProductRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ActivateDeactivateProduct(int productId)
        {
            try
            {
                return await _iProductRepository.ActivateDeactivateProduct(productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> DeleteProduct(int productId)
        {
            try
            {
                return await _iProductRepository.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public async Task<int> AddCategory(CategoryViewModel category)
        {
            try
            {
                var categories = new Category(category);
                return _iProductRepository.AddCategory(categories);
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
                return await _iProductRepository.GetCategoryList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<int> AddSubCategory(SubCategoryViewModel subCategoryViewModel)
        {
            try
            {
                var subCategory = new SubCategory(subCategoryViewModel);
                return await _iProductRepository.AddSubCategory(subCategory);
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
                return await _iProductRepository.GetSubCategoryList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<SelectListItem>> GetSubCategoryItems(long categoryId)
        {
            try
            {
                var subcategories = await _iProductRepository.GetSubCategoriesByCategoryId(categoryId);
                return subcategories.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }).ToList();
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
                return await _iProductRepository.GetAvailableProducts();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Products>> GetProductByCategoryId(int categoryId)
        {
            try
            {
                return await _iProductRepository.GetProductsByCategoryId(categoryId);
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
                return await _iProductRepository.GetProductBySubCategoryId(subCategoryId);
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
                return await _iProductRepository.AddProductInCart(productId, userId);
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
                return await _iProductRepository.GetUserCartItems(userId);
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
                return await _iProductRepository.DeleteCartItem(cartItemId);
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
                return await _iProductRepository.AddQuantity(productId, userId);
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
                return await _iProductRepository.SubtractQuantity(productId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddProduct(AddProductViewModel productViewModel)
        {
            try
            {
                var product = new Products(productViewModel);
                return await _iProductRepository.AddProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

                public async Task<UpdateProductViewModel> GetById(int productId)
        {
            try
            {
                return await _iProductRepository.GetById(productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

          public async Task<int> EditProduct(UpdateProductViewModel editProductViewModel)
        {
            try
            {
                return await _iProductRepository.EditProduct(new Products()
                {
                    Id = editProductViewModel.Id,
                    Name = editProductViewModel.Name,
                    Price = editProductViewModel.Price,
                    Photo = editProductViewModel.Photo,
                    Description = editProductViewModel.Description,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = editProductViewModel.CreatedBy
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

       



    }
}
