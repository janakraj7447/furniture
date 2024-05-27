
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS.FAM.Data.CustomEntities;
using NS.FAM.Data.CustomEntities;

namespace NS.FAM.Data.Entities
{
    public class Products
    {
        public Products() { }

        public Products(AddProductViewModel addProductViewModel)
        {
            Name = addProductViewModel.Name;
            Price = addProductViewModel.Price;
            CategoryId = addProductViewModel.CategoryId;
            SubCategoryId = addProductViewModel.SubCategoryId;
            Description = addProductViewModel.Description;
            Photo = addProductViewModel.Photo;
            CreatedBy = addProductViewModel.CreatedBy;
            IsActive = true;
            IsDeleted = false;
            CreatedDate = DateTime.Now;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? Updateddate { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public virtual ICollection<Cart> Carts { get; } = new List<Cart>();
      
        public Category Category { get; set; } 
      
        public SubCategory SubCategory { get; set; } 

    }
}
