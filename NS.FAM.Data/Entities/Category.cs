using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS.FAM.Data.CustomEntities;

namespace NS.FAM.Data.Entities
{
    [Table("Category")]
    public class Category
    {
        public Category() { }
        public Category(CategoryViewModel categoryViewModel)
        {
            Name = categoryViewModel.Name;
            IsActive = true;
            CreatedBy = categoryViewModel.CreatedBy;
            CreatedAt = DateTime.Now;
        }
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set;}
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<SubCategory> Subcategory { get; set; }


    }
}
