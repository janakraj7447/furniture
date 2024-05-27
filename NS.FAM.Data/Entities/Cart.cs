using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS.FAM.Data.Entities
{
    public class Cart
    {
        public Cart() { }
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set;}
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Products Product { get; set; }

        public virtual Users User { get; set; }
    }
}
