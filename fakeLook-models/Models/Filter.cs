using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakeLook_models.Models
{
    public class Filter
    {
        public ICollection<string> Publishers { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public ICollection<string> Tags { get; set; }
        public ICollection<string> UsersTaggedInPost { get; set; }

    }
}
