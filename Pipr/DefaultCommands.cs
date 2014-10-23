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
    public partial class DefaultCommands : Form
    {
        PiprDB context = new PiprDB();
        //MainWindow win = new MainWindow();

        public DefaultCommands()
        {
            InitializeComponent();

            var commands = from i in context.DefaultCommands select i.command;
            foreach (var comm in commands)
            {
                txtCommands.Items.Add(comm);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DefaultCommand command = new DefaultCommand();
            command.command = txtCommand.Text;            

            context.DefaultCommands.Add(command);
            context.SaveChanges();          

            this.Close();       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string comm = txtCommands.SelectedItem.ToString();
            //var command = from i in context.DefaultCommands where i.command == comm select i;
           // DefaultCommand dc = context.DefaultCommands.Find(Convert.ToInt32(command));            
           // context.DefaultCommands.Remove(dc);

            //win.Piper.SpeakAsync("Your command has been removed!");
            
            
        }
    }
}
