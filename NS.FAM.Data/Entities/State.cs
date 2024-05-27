using System;
using System.Collections.Generic;

namespace NS.FAM.Data.Entities
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<City> Cities { get; set; }
    }
}
