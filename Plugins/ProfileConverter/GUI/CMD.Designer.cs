namespace ProfileConverter.GUI
{
    partial class CMD
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
            this.ConvertProfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConvertProfile
            // 
            this.ConvertProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConvertProfile.Location = new System.Drawing.Point(0, 0);
            this.ConvertProfile.Name = "ConvertProfile";
            this.ConvertProfile.Size = new System.Drawing.Size(134, 61);
            this.ConvertProfile.TabIndex = 0;
            this.ConvertProfile.Text = "Convert Profile";
            this.ConvertProfile.UseVisualStyleBackColor = true;
            this.ConvertProfile.Click += new System.EventHandler(this.ConvertProfile_Click);
            // 
            // CMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(134, 61);
            this.Controls.Add(this.ConvertProfile);
            this.Name = "CMD";
            this.Text = "CMD";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ConvertProfile;
    }
}