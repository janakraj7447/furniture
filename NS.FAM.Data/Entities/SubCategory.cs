using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS.FAM.Data.CustomEntities;

namespace NS.FAM.Data.Entities
{
    public class SubCategory
    {
        public SubCategory() { }
        public SubCategory(SubCategoryViewModel subCategoryViewModel)
        { 
            Name = subCategoryViewModel.Name;
            CategoryId = subCategoryViewModel.CategoryId;
            IsActive = true;
            CreatedBy = subCategoryViewModel.CreatedBy;
            CreatedAt = DateTime.Now;
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public long? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
       
        public List<Products> Products { get; set; }
        public Category Category { get; set; }

    }
}
