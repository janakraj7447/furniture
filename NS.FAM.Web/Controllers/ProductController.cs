using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NS.FAM.Business;
using NS.FAM.Data.CustomEntities;
using NS.FAM.Data.Entities;

namespace NS.ECommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductBusiness _iProductBusiness;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public ProductController(IProductBusiness productBusiness,  Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _iProductBusiness = productBusiness;
            _environment = environment;
        }
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Products()
        {
            try
            {
                var products = await _iProductBusiness.GetAllProducts();

                return View(products);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IActionResult> ActivateDeactivateProduct(int Id)
        {
            try
            {
                await _iProductBusiness.ActivateDeactivateProduct(Id);
                return RedirectToAction(actionName: "Products", controllerName: "Product");
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

        }

        public async Task<IActionResult> DeleteProduct(int Id)
        {
            try
            {
                await _iProductBusiness.DeleteProduct(Id);
                return RedirectToAction(actionName: "Products", controllerName: "Product");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddCategory1(CategoryViewModel category)
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                category.CreatedBy = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);

                _iProductBusiness.AddCategory(category);
                return RedirectToAction("AddCategory", "Product");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         [Authorize(Roles = "1")]
        public async Task<IActionResult> AddSubCategory()
        {
            try
            {
                ViewBag.Categories = new SelectList(await _iProductBusiness.GetCategoryList(), "Id", "Name");

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         [HttpPost]
        public async Task<IActionResult> AddSubCategory(SubCategoryViewModel subCategoryViewModel)
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                subCategoryViewModel.CreatedBy = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);

                await _iProductBusiness.AddSubCategory(subCategoryViewModel);
                return RedirectToAction("AddSubCategory", "Product");
            }
            catch (Exception ex)
            {
                  throw new Exception(ex.Message);
            }
        }

          public async Task<JsonResult> GetSubCategory(string id)
        {
            try
            {
                long categoryId = Convert.ToInt64(id);
                var subcategories = await _iProductBusiness.GetSubCategoryItems(categoryId);
                return Json(subcategories);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

          public async Task<IActionResult> AddProduct()
        {
            try
            {
                ViewBag.Categories = new SelectList(await _iProductBusiness.GetCategoryList(), "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }

        }

          [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> AddProduct1(AddProductViewModel addProductViewModel, IFormFile Photo)
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                addProductViewModel.CreatedBy = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);

                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;
                string path = Path.Combine(this._environment.WebRootPath, "UploadProduct");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();
                string fileName = Path.GetFileName(Photo.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    Photo.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                    ViewBag.Message += string.Format("<b>{0}</b> Photo uploaded.<br />", fileName);
                }

                addProductViewModel.Photo = fileName;

                await _iProductBusiness.AddProduct(addProductViewModel);

                return RedirectToAction("Products", "Product");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

          public async Task<IActionResult> UserProducts()
        {
            try
            {
                var products = await _iProductBusiness.GetAvailableProducts();

                return View(products);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult> Details(int categoryId)
        {
            try
            {
                var productList = await _iProductBusiness.GetProductByCategoryId(categoryId);

                return PartialView("_AvailableProducts", productList);
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

           [HttpGet]
        public async Task<IActionResult> ProductsBySubCategory(int subCategoryId)
        {
            try
            {
                var productList = await _iProductBusiness.GetProductBySubCategoryId(subCategoryId);

                return PartialView("_AvailableProducts", productList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

         public async Task<IActionResult> AddProductInCart(int Id)
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                long userId = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);
                await _iProductBusiness.AddProductInCart(Id, userId);
                return RedirectToAction("UserProducts", "Product");
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

          public async Task<IActionResult> UserCart()
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                long userId = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);
                var items = await _iProductBusiness.GetUserCartItems(userId);
                return View(items);
            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message);
            }

        }

         [HttpPost]
        public async Task<int> AddQuantity(int productId)
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                long userId = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);
                return await _iProductBusiness.AddQuantity(productId, Convert.ToInt64(userId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        public async Task<int> SubtractQuantity(int productId)
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                long userId = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);
                return await _iProductBusiness.SubtractQuantity(productId, Convert.ToInt64(userId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<IActionResult> DeleteItem(int Id)
        {
            try
            {
                await _iProductBusiness.DeleteCartItem(Id);
                return RedirectToAction(actionName: "UserCart", controllerName: "Product");
            }
            catch (Exception ex)
            {
                  throw new Exception(ex.Message);
            }

        }

        
        public async Task<IActionResult> EditProduct(int id)
        {
            try
            {
                var product = await _iProductBusiness.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

          [HttpPost]
        public async Task<IActionResult> EditProduct(UpdateProductViewModel editedProduct)
        {
            try
            {
                var claims = User.Identities.First().Claims.ToList();
                editedProduct.CreatedBy = Convert.ToInt64(claims.FirstOrDefault(x => x.Type.Contains("UserId", StringComparison.OrdinalIgnoreCase))?.Value);
                await _iProductBusiness.EditProduct(editedProduct);
                return RedirectToAction("products", "Product");
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }



    }
}