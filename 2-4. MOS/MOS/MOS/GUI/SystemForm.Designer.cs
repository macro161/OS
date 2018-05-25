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
            this.runAllProgramsB = new System.Windows.Forms.Button();
            this.runSelectedB = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
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
            // runAllProgramsB
            // 
            this.runAllProgramsB.Location = new System.Drawing.Point(25, 78);
            this.runAllProgramsB.Name = "runAllProgramsB";
            this.runAllProgramsB.Size = new System.Drawing.Size(195, 45);
            this.runAllProgramsB.TabIndex = 1;
            this.runAllProgramsB.Text = "Run all programs";
            this.runAllProgramsB.UseVisualStyleBackColor = true;
            this.runAllProgramsB.Click += new System.EventHandler(this.runAllProgramsB_Click);
            // 
            // runSelectedB
            // 
            this.runSelectedB.Location = new System.Drawing.Point(25, 129);
            this.runSelectedB.Name = "runSelectedB";
            this.runSelectedB.Size = new System.Drawing.Size(195, 49);
            this.runSelectedB.TabIndex = 2;
            this.runSelectedB.Text = "Run selected programs";
            this.runSelectedB.UseVisualStyleBackColor = true;
            this.runSelectedB.Click += new System.EventHandler(this.runSelectedB_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(25, 196);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(195, 173);
            this.listBox.TabIndex = 3;
            // 
            // SystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 390);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.runSelectedB);
            this.Controls.Add(this.runAllProgramsB);
            this.Controls.Add(this.mountFlashB);
            this.Name = "SystemForm";
            this.Text = "SystemForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mountFlashB;
        private System.Windows.Forms.Button runAllProgramsB;
        private System.Windows.Forms.Button runSelectedB;
        private System.Windows.Forms.ListBox listBox;
    }
}