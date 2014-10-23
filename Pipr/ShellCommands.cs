using Pipr.Models;
using Pipr.Properties;
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
    public partial class ShellCommands : Form
    {
        PiprDB context = new PiprDB();
        
        public ShellCommands()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ShellCommands_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShellCommand command = new ShellCommand();
            command.command = txtCommand.Text;
            command.response = txtResponse.Text;
            command.location = txtLocation.Text;

            context.ShellCommands.Add(command);
            context.SaveChanges();

            MainWindow window = new MainWindow();
            window.UpdateCommands();
                       
            this.Close();       
            
            
            
        }
    }
}
