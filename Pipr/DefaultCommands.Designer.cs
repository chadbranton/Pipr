namespace Pipr
{
    partial class DefaultCommands
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtCommands = new System.Windows.Forms.ListBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCommands
            // 
            this.txtCommands.FormattingEnabled = true;
            this.txtCommands.Location = new System.Drawing.Point(441, 12);
            this.txtCommands.Name = "txtCommands";
            this.txtCommands.Size = new System.Drawing.Size(203, 316);
            this.txtCommands.TabIndex = 0;
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(85, 153);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(278, 20);
            this.txtCommand.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 42);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add Default Command";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(475, 334);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 42);
            this.button2.TabIndex = 3;
            this.button2.Text = "Delete Selected Command";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DefaultCommands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 403);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.txtCommands);
            this.Name = "DefaultCommands";
            this.Text = "DefaultCommands";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox txtCommands;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}