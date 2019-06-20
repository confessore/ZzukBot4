using System.Windows.Forms;

namespace Grinderv2.GUI
{
    partial class CMD : Form
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.KeepGreens = new System.Windows.Forms.CheckBox();
            this.KeepBlues = new System.Windows.Forms.CheckBox();
            this.KeepPurples = new System.Windows.Forms.CheckBox();
            this.ProtectedItems = new System.Windows.Forms.TextBox();
            this.EatAtInput = new System.Windows.Forms.NumericUpDown();
            this.EatAtLabel = new System.Windows.Forms.Label();
            this.DrinkAtInput = new System.Windows.Forms.NumericUpDown();
            this.DrinkAtLabel = new System.Windows.Forms.Label();
            this.CorpseLoot = new System.Windows.Forms.CheckBox();
            this.CorpseSkin = new System.Windows.Forms.CheckBox();
            this.ProtectedItemsLabel = new System.Windows.Forms.Label();
            this.NinjaSkin = new System.Windows.Forms.CheckBox();
            this.CreatureInput = new System.Windows.Forms.TextBox();
            this.CreatureLabel = new System.Windows.Forms.Label();
            this.Harvest = new System.Windows.Forms.CheckBox();
            this.VendorAtLabel = new System.Windows.Forms.Label();
            this.VendorAtInput = new System.Windows.Forms.NumericUpDown();
            this.Vendor = new System.Windows.Forms.CheckBox();
            this.PulseRateInput = new System.Windows.Forms.NumericUpDown();
            this.PulseRateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EatAtInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrinkAtInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendorAtInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PulseRateInput)).BeginInit();
            this.SuspendLayout();
            // 
            // KeepGreens
            // 
            this.KeepGreens.AutoSize = true;
            this.KeepGreens.Location = new System.Drawing.Point(372, 118);
            this.KeepGreens.Name = "KeepGreens";
            this.KeepGreens.Size = new System.Drawing.Size(88, 17);
            this.KeepGreens.TabIndex = 1;
            this.KeepGreens.Text = "Keep Greens";
            this.KeepGreens.UseVisualStyleBackColor = true;
            this.KeepGreens.CheckedChanged += new System.EventHandler(this.KeepGreens_CheckedChanged);
            // 
            // KeepBlues
            // 
            this.KeepBlues.AutoSize = true;
            this.KeepBlues.Location = new System.Drawing.Point(372, 141);
            this.KeepBlues.Name = "KeepBlues";
            this.KeepBlues.Size = new System.Drawing.Size(80, 17);
            this.KeepBlues.TabIndex = 2;
            this.KeepBlues.Text = "Keep Blues";
            this.KeepBlues.UseVisualStyleBackColor = true;
            this.KeepBlues.CheckedChanged += new System.EventHandler(this.KeepBlues_CheckedChanged);
            // 
            // KeepPurples
            // 
            this.KeepPurples.AutoSize = true;
            this.KeepPurples.Location = new System.Drawing.Point(373, 164);
            this.KeepPurples.Name = "KeepPurples";
            this.KeepPurples.Size = new System.Drawing.Size(89, 17);
            this.KeepPurples.TabIndex = 3;
            this.KeepPurples.Text = "Keep Purples";
            this.KeepPurples.UseVisualStyleBackColor = true;
            this.KeepPurples.CheckedChanged += new System.EventHandler(this.KeepPurples_CheckedChanged);
            // 
            // ProtectedItems
            // 
            this.ProtectedItems.Location = new System.Drawing.Point(192, 24);
            this.ProtectedItems.Multiline = true;
            this.ProtectedItems.Name = "ProtectedItems";
            this.ProtectedItems.Size = new System.Drawing.Size(175, 150);
            this.ProtectedItems.TabIndex = 8;
            this.ProtectedItems.TextChanged += new System.EventHandler(this.ProtectedItems_TextChanged);
            // 
            // EatAtInput
            // 
            this.EatAtInput.Location = new System.Drawing.Point(70, 180);
            this.EatAtInput.Name = "EatAtInput";
            this.EatAtInput.Size = new System.Drawing.Size(40, 20);
            this.EatAtInput.TabIndex = 9;
            this.EatAtInput.ValueChanged += new System.EventHandler(this.EatAtInput_ValueChanged);
            // 
            // EatAtLabel
            // 
            this.EatAtLabel.AutoSize = true;
            this.EatAtLabel.Location = new System.Drawing.Point(10, 182);
            this.EatAtLabel.Name = "EatAtLabel";
            this.EatAtLabel.Size = new System.Drawing.Size(46, 13);
            this.EatAtLabel.TabIndex = 10;
            this.EatAtLabel.Text = "Eat at %";
            // 
            // DrinkAtInput
            // 
            this.DrinkAtInput.Location = new System.Drawing.Point(70, 205);
            this.DrinkAtInput.Name = "DrinkAtInput";
            this.DrinkAtInput.Size = new System.Drawing.Size(40, 20);
            this.DrinkAtInput.TabIndex = 11;
            this.DrinkAtInput.ValueChanged += new System.EventHandler(this.DrinkAtInput_ValueChanged);
            // 
            // DrinkAtLabel
            // 
            this.DrinkAtLabel.AutoSize = true;
            this.DrinkAtLabel.Location = new System.Drawing.Point(10, 205);
            this.DrinkAtLabel.Name = "DrinkAtLabel";
            this.DrinkAtLabel.Size = new System.Drawing.Size(55, 13);
            this.DrinkAtLabel.TabIndex = 12;
            this.DrinkAtLabel.Text = "Drink at %";
            // 
            // CorpseLoot
            // 
            this.CorpseLoot.AutoSize = true;
            this.CorpseLoot.Location = new System.Drawing.Point(372, 24);
            this.CorpseLoot.Name = "CorpseLoot";
            this.CorpseLoot.Size = new System.Drawing.Size(83, 17);
            this.CorpseLoot.TabIndex = 13;
            this.CorpseLoot.Text = "Corpse Loot";
            this.CorpseLoot.UseVisualStyleBackColor = true;
            this.CorpseLoot.CheckedChanged += new System.EventHandler(this.CorpseLoot_CheckedChanged);
            // 
            // CorpseSkin
            // 
            this.CorpseSkin.AutoSize = true;
            this.CorpseSkin.Location = new System.Drawing.Point(372, 48);
            this.CorpseSkin.Name = "CorpseSkin";
            this.CorpseSkin.Size = new System.Drawing.Size(83, 17);
            this.CorpseSkin.TabIndex = 14;
            this.CorpseSkin.Text = "Corpse Skin";
            this.CorpseSkin.UseVisualStyleBackColor = true;
            this.CorpseSkin.CheckedChanged += new System.EventHandler(this.CorpseSkin_CheckedChanged);
            // 
            // ProtectedItemsLabel
            // 
            this.ProtectedItemsLabel.AutoSize = true;
            this.ProtectedItemsLabel.Location = new System.Drawing.Point(190, 7);
            this.ProtectedItemsLabel.Name = "ProtectedItemsLabel";
            this.ProtectedItemsLabel.Size = new System.Drawing.Size(78, 13);
            this.ProtectedItemsLabel.TabIndex = 15;
            this.ProtectedItemsLabel.Text = "ProtectedItems";
            // 
            // NinjaSkin
            // 
            this.NinjaSkin.AutoSize = true;
            this.NinjaSkin.Location = new System.Drawing.Point(372, 72);
            this.NinjaSkin.Name = "NinjaSkin";
            this.NinjaSkin.Size = new System.Drawing.Size(74, 17);
            this.NinjaSkin.TabIndex = 16;
            this.NinjaSkin.Text = "Ninja Skin";
            this.NinjaSkin.UseVisualStyleBackColor = true;
            this.NinjaSkin.CheckedChanged += new System.EventHandler(this.NinjaSkin_CheckedChanged);
            // 
            // CreatureInput
            // 
            this.CreatureInput.Location = new System.Drawing.Point(12, 24);
            this.CreatureInput.Multiline = true;
            this.CreatureInput.Name = "CreatureInput";
            this.CreatureInput.Size = new System.Drawing.Size(175, 150);
            this.CreatureInput.TabIndex = 17;
            this.CreatureInput.MouseLeave += new System.EventHandler(this.Creatures_MouseLeave);
            // 
            // CreatureLabel
            // 
            this.CreatureLabel.AutoSize = true;
            this.CreatureLabel.Location = new System.Drawing.Point(10, 7);
            this.CreatureLabel.Name = "CreatureLabel";
            this.CreatureLabel.Size = new System.Drawing.Size(52, 13);
            this.CreatureLabel.TabIndex = 18;
            this.CreatureLabel.Text = "Creatures";
            // 
            // Harvest
            // 
            this.Harvest.AutoSize = true;
            this.Harvest.Location = new System.Drawing.Point(372, 95);
            this.Harvest.Name = "Harvest";
            this.Harvest.Size = new System.Drawing.Size(83, 17);
            this.Harvest.TabIndex = 19;
            this.Harvest.Text = "Herb / Mine";
            this.Harvest.UseVisualStyleBackColor = true;
            this.Harvest.CheckedChanged += new System.EventHandler(this.Harvest_CheckedChanged);
            // 
            // VendorAtLabel
            // 
            this.VendorAtLabel.AutoSize = true;
            this.VendorAtLabel.Location = new System.Drawing.Point(190, 182);
            this.VendorAtLabel.Name = "VendorAtLabel";
            this.VendorAtLabel.Size = new System.Drawing.Size(53, 13);
            this.VendorAtLabel.TabIndex = 20;
            this.VendorAtLabel.Text = "Vendor at";
            // 
            // VendorAtInput
            // 
            this.VendorAtInput.Location = new System.Drawing.Point(249, 180);
            this.VendorAtInput.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.VendorAtInput.Name = "VendorAtInput";
            this.VendorAtInput.Size = new System.Drawing.Size(40, 20);
            this.VendorAtInput.TabIndex = 21;
            this.VendorAtInput.ValueChanged += new System.EventHandler(this.VendorAtInput_ValueChanged);
            // 
            // Vendor
            // 
            this.Vendor.AutoSize = true;
            this.Vendor.Location = new System.Drawing.Point(373, 187);
            this.Vendor.Name = "Vendor";
            this.Vendor.Size = new System.Drawing.Size(60, 17);
            this.Vendor.TabIndex = 22;
            this.Vendor.Text = "Vendor";
            this.Vendor.UseVisualStyleBackColor = true;
            this.Vendor.CheckedChanged += new System.EventHandler(this.Vendor_CheckedChanged);
            // 
            // PulseRateInput
            // 
            this.PulseRateInput.Location = new System.Drawing.Point(249, 206);
            this.PulseRateInput.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PulseRateInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PulseRateInput.Name = "PulseRateInput";
            this.PulseRateInput.Size = new System.Drawing.Size(40, 20);
            this.PulseRateInput.TabIndex = 23;
            this.PulseRateInput.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.PulseRateInput.ValueChanged += new System.EventHandler(this.PulseRateInput_ValueChanged);
            // 
            // PulseRateLabel
            // 
            this.PulseRateLabel.AutoSize = true;
            this.PulseRateLabel.Location = new System.Drawing.Point(190, 205);
            this.PulseRateLabel.Name = "PulseRateLabel";
            this.PulseRateLabel.Size = new System.Drawing.Size(59, 13);
            this.PulseRateLabel.TabIndex = 24;
            this.PulseRateLabel.Text = "Pulse Rate";
            // 
            // CMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 244);
            this.Controls.Add(this.PulseRateLabel);
            this.Controls.Add(this.PulseRateInput);
            this.Controls.Add(this.Vendor);
            this.Controls.Add(this.VendorAtInput);
            this.Controls.Add(this.VendorAtLabel);
            this.Controls.Add(this.Harvest);
            this.Controls.Add(this.CreatureLabel);
            this.Controls.Add(this.CreatureInput);
            this.Controls.Add(this.NinjaSkin);
            this.Controls.Add(this.ProtectedItemsLabel);
            this.Controls.Add(this.CorpseSkin);
            this.Controls.Add(this.CorpseLoot);
            this.Controls.Add(this.DrinkAtLabel);
            this.Controls.Add(this.DrinkAtInput);
            this.Controls.Add(this.EatAtLabel);
            this.Controls.Add(this.EatAtInput);
            this.Controls.Add(this.ProtectedItems);
            this.Controls.Add(this.KeepPurples);
            this.Controls.Add(this.KeepBlues);
            this.Controls.Add(this.KeepGreens);
            this.Name = "CMD";
            this.Text = "Grinder";
            ((System.ComponentModel.ISupportInitialize)(this.EatAtInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrinkAtInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendorAtInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PulseRateInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        System.Windows.Forms.CheckBox KeepGreens;
        System.Windows.Forms.CheckBox KeepBlues;
        System.Windows.Forms.CheckBox KeepPurples;
        System.Windows.Forms.TextBox ProtectedItems;
        NumericUpDown EatAtInput;
        Label EatAtLabel;
        NumericUpDown DrinkAtInput;
        Label DrinkAtLabel;
        CheckBox CorpseLoot;
        CheckBox CorpseSkin;
        Label ProtectedItemsLabel;
        CheckBox NinjaSkin;
        private TextBox CreatureInput;
        private Label CreatureLabel;
        private CheckBox Harvest;
        private Label VendorAtLabel;
        private NumericUpDown VendorAtInput;
        private CheckBox Vendor;
        private NumericUpDown PulseRateInput;
        private Label PulseRateLabel;
    }
}
