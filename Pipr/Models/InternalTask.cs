using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipr.Models
{
    public class InternalTask
    {
        public int internalTaskId { get; set; }
        public string taskName { get; set; }
        public bool complete { get; set; }
    }
}
