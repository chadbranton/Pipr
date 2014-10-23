using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipr.Models
{
    public enum POSenums
    {
        n,v,a,r,s
    }
    public class Synset
   
    {
        public int synsetid { get; set; }
        public POSenums pos { get; set; }
        public int lexdomainid { get; set; }
        public string definition { get; set; }
    }
}
