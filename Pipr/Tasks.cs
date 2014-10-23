using Pipr.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pipr
{
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
        }

        PiprDB context = new PiprDB();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {    
                     
            InternalTask task = new InternalTask();
            task.taskName = this.TaskBox.Text;
             
            context.Tasks.Add(task);
            context.SaveChanges();
            this.Close();

        }
    }
}
