using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipr.Models
{
    public class LinkType
    {
        [Key]
        public int linkid { get; set; }
        public string link { get; set; }
        public int recurses { get; set; }

    }
}
