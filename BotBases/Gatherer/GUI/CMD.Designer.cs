using System;

namespace Gatherer.GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMD));
            this.LoadProfileButton = new System.Windows.Forms.Button();
            this.LoadProfileOFD = new System.Windows.Forms.OpenFileDialog();
            this.Herbs = new System.Windows.Forms.CheckedListBox();
            this.Ores = new System.Windows.Forms.CheckedListBox();
            this.disableMountBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LoadProfileButton
            // 
            this.LoadProfileButton.Location = new System.Drawing.Point(0, 0);
            this.LoadProfileButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LoadProfileButton.Name = "LoadProfileButton";
            this.LoadProfileButton.Size = new System.Drawing.Size(112, 35);
            this.LoadProfileButton.TabIndex = 4;
            // 
            // LoadProfileOFD
            // 
            this.LoadProfileOFD.FileName = "LoadProfileOFD";
            this.LoadProfileOFD.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadProfileOFD_FileOk);
            // 
            // Herbs
            // 
            this.Herbs.CheckOnClick = true;
            this.Herbs.FormattingEnabled = true;
            this.Herbs.Items.AddRange(Enum.GetNames(typeof(Models.Herbs)));
            this.Herbs.Location = new System.Drawing.Point(18, 18);
            this.Herbs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Herbs.Name = "Herbs";
            this.Herbs.Size = new System.Drawing.Size(206, 382);
            this.Herbs.TabIndex = 1;
            this.Herbs.MouseLeave += new System.EventHandler(this.HerbCheckListBox_MouseLeave);
            // 
            // Ores
            // 
            this.Ores.CheckOnClick = true;
            this.Ores.FormattingEnabled = true;
            this.Ores.Items.AddRange(Enum.GetNames(typeof(Models.Ores)));
            this.Ores.Location = new System.Drawing.Point(236, 18);
            this.Ores.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Ores.Name = "Ores";
            this.Ores.ScrollAlwaysVisible = true;
            this.Ores.Size = new System.Drawing.Size(304, 382);
            this.Ores.TabIndex = 2;
            this.Ores.MouseLeave += new System.EventHandler(this.MineCheckListBox_MouseLeave);
            // 
            // disableMountBox
            // 
            this.disableMountBox.AutoSize = true;
            this.disableMountBox.Location = new System.Drawing.Point(20, 443);
            this.disableMountBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.disableMountBox.Name = "disableMountBox";
            this.disableMountBox.Size = new System.Drawing.Size(137, 24);
            this.disableMountBox.TabIndex = 3;
            this.disableMountBox.Text = "Disable Mount";
            this.disableMountBox.UseVisualStyleBackColor = true;
            this.disableMountBox.MouseLeave += new System.EventHandler(this.disableMount_MouseLeave);
            // 
            // CMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 488);
            this.Controls.Add(this.disableMountBox);
            this.Controls.Add(this.Ores);
            this.Controls.Add(this.Herbs);
            this.Controls.Add(this.LoadProfileButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CMD";
            this.Text = "Harvester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadProfileButton;
        private System.Windows.Forms.OpenFileDialog LoadProfileOFD;
        private System.Windows.Forms.CheckedListBox Herbs;
        private System.Windows.Forms.CheckedListBox Ores;
        private System.Windows.Forms.CheckBox disableMountBox;
    }
}