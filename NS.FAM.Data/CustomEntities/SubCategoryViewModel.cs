using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.FAM.Data.CustomEntities
{
    public class SubCategoryViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? CategoryId { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
