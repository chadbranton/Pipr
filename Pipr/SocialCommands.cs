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
    public partial class SocialCommands : Form
    {
        PiprDB context = new PiprDB();

        public SocialCommands()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SocialCommand command = new SocialCommand();
            command.command = txtCommand.Text;
            command.response = txtResponse.Text;

            context.SocialCommands.Add(command);
            context.SaveChanges();

            this.Close(); 
        }
    }
}
