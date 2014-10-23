using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipr.Models
{
    public class Sense
    {
        [Key]
        [Column(Order = 1)]
        public int wordid { get; set; }
        public int casedwordid { get; set; }
        [Key]
        [Column(Order = 2)]
        public int synsetid { get; set; }
        public int senseid { get; set; }
        public int sensenum { get; set; }
        public int lexid { get; set; }
        public int tagcount { get; set; }
        public string sensekey { get; set; }
    }
}
