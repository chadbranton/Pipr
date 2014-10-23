using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipr.Models
{
    public class LexLink
    {
        [Key]
        [Column(Order = 1)]
        public int synset1id { get; set; }
        [Key]
        [Column(Order = 2)]
        public int word1id { get; set; }
        [Key]
        [Column(Order = 3)]
        public int synset2id { get; set; }
        [Key]
        [Column(Order = 4)]
        public int word2id { get; set; }
        [Key]
        [Column(Order = 5)]
        public int linkid { get; set; }
    }
}
