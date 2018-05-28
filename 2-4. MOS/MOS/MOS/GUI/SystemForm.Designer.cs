namespace MOS.GUI
{
    partial class SystemForm
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
            this.mountFlashB = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // mountFlashB
            // 
            this.mountFlashB.Location = new System.Drawing.Point(25, 27);
            this.mountFlashB.Name = "mountFlashB";
            this.mountFlashB.Size = new System.Drawing.Size(195, 45);
            this.mountFlashB.TabIndex = 0;
            this.mountFlashB.Text = "Mount flash";
            this.mountFlashB.UseVisualStyleBackColor = true;
            this.mountFlashB.Click += new System.EventHandler(this.mountFlashB_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(25, 90);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(195, 173);
            this.listBox1.TabIndex = 3;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // SystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 284);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.mountFlashB);
            this.Name = "SystemForm";
            this.Text = "SystemForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mountFlashB;
        private System.Windows.Forms.ListBox listBox1;
    }
}