using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.FAM.Data.CustomEntities
{
    public class UpdateProductViewModel
    { 
        public long Id { get; set; }

        [RegularExpression("^[a-zA-Z ]{0,50}$", ErrorMessage = "Please Enter Valid Name")]
        public string Name { get; set; }
        [RegularExpression(@"^([0-9]{0,5})$", ErrorMessage = "Please Enter Valid Price.")]
        public int Price { get; set; }
        public string Photo { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long CreatedBy { get; set; }
    }
}
