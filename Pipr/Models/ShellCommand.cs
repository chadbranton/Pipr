using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipr.Models
{
    public class ShellCommand
    {
        public int shellCommandId { get; set; }
        public string command { get; set; }
        public string response { get; set; }
        public string location { get; set; }
    }
}
