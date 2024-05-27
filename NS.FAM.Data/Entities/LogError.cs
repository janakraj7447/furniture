using System;
using System.Collections.Generic;

namespace NS.FAM.Data.Entities
{
    public partial class LogError
    {
        public long Id { get; set; }
        public string Method { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
        public string StackTrace { get; set; } = null!;
        public DateTime DateTime { get; set; }
    }
}
