using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipr.Models
{

    public enum POSenum
    {
        n, v, a, r, s
    }

    public class PosType
    {
        [Key]
        public POSenum pos { get; set; }
        public string posname { get; set; }
    }
}
