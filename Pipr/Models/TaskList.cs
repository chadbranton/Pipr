using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipr.Models
{
    public class TaskList
    {
        public int taskListId { get; set; }
        public string name { get; set; }
        public virtual ICollection<InternalTask> tasks { get; set; }
    }
}
