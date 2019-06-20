namespace ProfileCreator.GUI
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
            this.EnableGlobalHotkeys = new System.Windows.Forms.CheckBox();
            this.HotspotsListBox = new System.Windows.Forms.ListBox();
            this.InsertHotspot = new System.Windows.Forms.Button();
            this.DeleteHotspot = new System.Windows.Forms.Button();
            this.VendorHotspotsListBox = new System.Windows.Forms.ListBox();
            this.InsertVendorHotspot = new System.Windows.Forms.Button();
            this.DeleteVendorHotspot = new System.Windows.Forms.Button();
            this.VendorNameInput = new System.Windows.Forms.TextBox();
            this.SaveProfile = new System.Windows.Forms.Button();
            this.LoadProfile = new System.Windows.Forms.Button();
            this.HotspotsLabel = new System.Windows.Forms.Label();
            this.VendorHotspotsLabel = new System.Windows.Forms.Label();
            this.VendorNameLabel = new System.Windows.Forms.Label();
            this.NewProfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EnableGlobalHotkeys
            // 
            this.EnableGlobalHotkeys.AutoSize = true;
            this.EnableGlobalHotkeys.Location = new System.Drawing.Point(12, 12);
            this.EnableGlobalHotkeys.Name = "EnableGlobalHotkeys";
            this.EnableGlobalHotkeys.Size = new System.Drawing.Size(134, 17);
            this.EnableGlobalHotkeys.TabIndex = 3;
            this.EnableGlobalHotkeys.Text = "Enable Global Hotkeys";
            this.EnableGlobalHotkeys.UseVisualStyleBackColor = true;
            this.EnableGlobalHotkeys.CheckedChanged += new System.EventHandler(this.EnableGlobalHotkeys_CheckedChanged);
            // 
            // HotspotsListBox
            // 
            this.HotspotsListBox.FormattingEnabled = true;
            this.HotspotsListBox.Location = new System.Drawing.Point(12, 76);
            this.HotspotsListBox.Name = "HotspotsListBox";
            this.HotspotsListBox.Size = new System.Drawing.Size(200, 394);
            this.HotspotsListBox.TabIndex = 0;
            // 
            // InsertHotspot
            // 
            this.InsertHotspot.Location = new System.Drawing.Point(12, 476);
            this.InsertHotspot.Name = "InsertHotspot";
            this.InsertHotspot.Size = new System.Drawing.Size(75, 23);
            this.InsertHotspot.TabIndex = 1;
            this.InsertHotspot.Text = "Insert Hotspot";
            this.InsertHotspot.UseVisualStyleBackColor = true;
            this.InsertHotspot.Click += new System.EventHandler(this.InsertHotspot_Click);
            // 
            // DeleteHotspot
            // 
            this.DeleteHotspot.Location = new System.Drawing.Point(93, 476);
            this.DeleteHotspot.Name = "DeleteHotspot";
            this.DeleteHotspot.Size = new System.Drawing.Size(75, 23);
            this.DeleteHotspot.TabIndex = 2;
            this.DeleteHotspot.Text = "Delete Hotspot";
            this.DeleteHotspot.UseVisualStyleBackColor = true;
            this.DeleteHotspot.Click += new System.EventHandler(this.DeleteHotspot_Click);
            // 
            // VendorHotspotsListBox
            // 
            this.VendorHotspotsListBox.FormattingEnabled = true;
            this.VendorHotspotsListBox.Location = new System.Drawing.Point(218, 76);
            this.VendorHotspotsListBox.Name = "VendorHotspotsListBox";
            this.VendorHotspotsListBox.Size = new System.Drawing.Size(200, 394);
            this.VendorHotspotsListBox.TabIndex = 4;
            // 
            // InsertVendorHotspot
            // 
            this.InsertVendorHotspot.Location = new System.Drawing.Point(218, 476);
            this.InsertVendorHotspot.Name = "InsertVendorHotspot";
            this.InsertVendorHotspot.Size = new System.Drawing.Size(75, 23);
            this.InsertVendorHotspot.TabIndex = 5;
            this.InsertVendorHotspot.Text = "Insert Vendor Hotspot";
            this.InsertVendorHotspot.UseVisualStyleBackColor = true;
            this.InsertVendorHotspot.Click += new System.EventHandler(this.InsertVendorHotspot_Click);
            // 
            // DeleteVendorHotspot
            // 
            this.DeleteVendorHotspot.Location = new System.Drawing.Point(299, 476);
            this.DeleteVendorHotspot.Name = "DeleteVendorHotspot";
            this.DeleteVendorHotspot.Size = new System.Drawing.Size(75, 23);
            this.DeleteVendorHotspot.TabIndex = 6;
            this.DeleteVendorHotspot.Text = "Delete Vendor Hotspot";
            this.DeleteVendorHotspot.UseVisualStyleBackColor = true;
            this.DeleteVendorHotspot.Click += new System.EventHandler(this.DeleteVendorHotspot_Click);
            // 
            // VendorNameInput
            // 
            this.VendorNameInput.Location = new System.Drawing.Point(344, 9);
            this.VendorNameInput.Name = "VendorNameInput";
            this.VendorNameInput.Size = new System.Drawing.Size(153, 20);
            this.VendorNameInput.TabIndex = 7;
            this.VendorNameInput.TextChanged += new System.EventHandler(this.VendorNameInput_TextChanged);
            // 
            // SaveProfile
            // 
            this.SaveProfile.Location = new System.Drawing.Point(422, 476);
            this.SaveProfile.Name = "SaveProfile";
            this.SaveProfile.Size = new System.Drawing.Size(75, 23);
            this.SaveProfile.TabIndex = 8;
            this.SaveProfile.Text = "Save Profile";
            this.SaveProfile.UseVisualStyleBackColor = true;
            this.SaveProfile.Click += new System.EventHandler(this.SaveProfile_Click);
            // 
            // LoadProfile
            // 
            this.LoadProfile.Location = new System.Drawing.Point(422, 447);
            this.LoadProfile.Name = "LoadProfile";
            this.LoadProfile.Size = new System.Drawing.Size(75, 23);
            this.LoadProfile.TabIndex = 9;
            this.LoadProfile.Text = "Load Profile";
            this.LoadProfile.UseVisualStyleBackColor = true;
            this.LoadProfile.Click += new System.EventHandler(this.LoadProfile_Click);
            // 
            // HotspotsLabel
            // 
            this.HotspotsLabel.AutoSize = true;
            this.HotspotsLabel.Location = new System.Drawing.Point(12, 60);
            this.HotspotsLabel.Name = "HotspotsLabel";
            this.HotspotsLabel.Size = new System.Drawing.Size(49, 13);
            this.HotspotsLabel.TabIndex = 10;
            this.HotspotsLabel.Text = "Hotspots";
            // 
            // VendorHotspotsLabel
            // 
            this.VendorHotspotsLabel.AutoSize = true;
            this.VendorHotspotsLabel.Location = new System.Drawing.Point(215, 60);
            this.VendorHotspotsLabel.Name = "VendorHotspotsLabel";
            this.VendorHotspotsLabel.Size = new System.Drawing.Size(86, 13);
            this.VendorHotspotsLabel.TabIndex = 11;
            this.VendorHotspotsLabel.Text = "Vendor Hotspots";
            // 
            // VendorNameLabel
            // 
            this.VendorNameLabel.AutoSize = true;
            this.VendorNameLabel.Location = new System.Drawing.Point(266, 12);
            this.VendorNameLabel.Name = "VendorNameLabel";
            this.VendorNameLabel.Size = new System.Drawing.Size(72, 13);
            this.VendorNameLabel.TabIndex = 12;
            this.VendorNameLabel.Text = "Vendor Name";
            // 
            // NewProfile
            // 
            this.NewProfile.Location = new System.Drawing.Point(422, 418);
            this.NewProfile.Name = "NewProfile";
            this.NewProfile.Size = new System.Drawing.Size(75, 23);
            this.NewProfile.TabIndex = 13;
            this.NewProfile.Text = "New Profile";
            this.NewProfile.UseVisualStyleBackColor = true;
            this.NewProfile.Click += new System.EventHandler(this.NewProfile_Click);
            // 
            // CMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 511);
            this.Controls.Add(this.NewProfile);
            this.Controls.Add(this.VendorNameLabel);
            this.Controls.Add(this.VendorHotspotsLabel);
            this.Controls.Add(this.HotspotsLabel);
            this.Controls.Add(this.LoadProfile);
            this.Controls.Add(this.SaveProfile);
            this.Controls.Add(this.VendorNameInput);
            this.Controls.Add(this.DeleteVendorHotspot);
            this.Controls.Add(this.InsertVendorHotspot);
            this.Controls.Add(this.VendorHotspotsListBox);
            this.Controls.Add(this.DeleteHotspot);
            this.Controls.Add(this.InsertHotspot);
            this.Controls.Add(this.HotspotsListBox);
            this.Controls.Add(this.EnableGlobalHotkeys);
            this.Name = "CMD";
            this.Text = "Profile Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox EnableGlobalHotkeys;
        private System.Windows.Forms.ListBox HotspotsListBox;
        private System.Windows.Forms.Button InsertHotspot;
        private System.Windows.Forms.Button DeleteHotspot;
        private System.Windows.Forms.ListBox VendorHotspotsListBox;
        private System.Windows.Forms.Button InsertVendorHotspot;
        private System.Windows.Forms.Button DeleteVendorHotspot;
        private System.Windows.Forms.TextBox VendorNameInput;
        private System.Windows.Forms.Button SaveProfile;
        private System.Windows.Forms.Button LoadProfile;
        private System.Windows.Forms.Label HotspotsLabel;
        private System.Windows.Forms.Label VendorHotspotsLabel;
        private System.Windows.Forms.Label VendorNameLabel;
        private System.Windows.Forms.Button NewProfile;
    }
}