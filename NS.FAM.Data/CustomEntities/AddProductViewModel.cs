using System.ComponentModel.DataAnnotations;

namespace NS.FAM.Data.CustomEntities
{
    public class AddProductViewModel
    {
        public long Id { get; set; }

        [RegularExpression("^[a-zA-Z ]{0,50}$", ErrorMessage = "Please Enter Valid Name")]
        public string Name { get; set; }
        [RegularExpression(@"^([0-9]{0,5})$", ErrorMessage = "Please Enter Valid Price.")]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public long CreatedBy { get; set; }
        [Required]
        public long? CategoryId { get; set; }
        [Required]
        public long? SubCategoryId { get; set; }

    }
}
