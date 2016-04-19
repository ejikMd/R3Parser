using System.ComponentModel;
using System.Windows.Forms;

namespace RealtorParser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.GetDataButtn = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.Label();
            this.getDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GetDataButtn
            // 
            this.GetDataButtn.Location = new System.Drawing.Point(13, 13);
            this.GetDataButtn.Name = "GetDataButtn";
            this.GetDataButtn.Size = new System.Drawing.Size(75, 23);
            this.GetDataButtn.TabIndex = 0;
            this.GetDataButtn.Text = "Get Data";
            this.GetDataButtn.UseVisualStyleBackColor = true;
            this.GetDataButtn.Click += new System.EventHandler(this.GetDataButtn_Click);
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(12, 39);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(100, 23);
            this.Status.TabIndex = 0;
            // 
            // getDetails
            // 
            this.getDetails.Location = new System.Drawing.Point(15, 110);
            this.getDetails.Name = "getDetails";
            this.getDetails.Size = new System.Drawing.Size(75, 23);
            this.getDetails.TabIndex = 1;
            this.getDetails.Text = "Get Details";
            this.getDetails.UseVisualStyleBackColor = true;
            this.getDetails.Click += new System.EventHandler(this.getDetails_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.getDetails);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.GetDataButtn);
            this.Name = "Form1";
            this.Text = "R3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button GetDataButton;
        private Button GetDataButtn;
        private Label Status;
        private Button getDetails;
    }
}

