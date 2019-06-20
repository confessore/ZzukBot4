using Grinder.Settings;
using System.Linq;
using System.Windows.Forms;

namespace Grinder.GUI
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
            this.LoadProfile = new System.Windows.Forms.Button();
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
            this.SearchRadiusInput = new System.Windows.Forms.NumericUpDown();
            this.SearchRadiusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EatAtInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrinkAtInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchRadiusInput)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadProfile
            // 
            this.LoadProfile.Location = new System.Drawing.Point(263, 370);
            this.LoadProfile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoadProfile.Name = "LoadProfile";
            this.LoadProfile.Size = new System.Drawing.Size(100, 28);
            this.LoadProfile.TabIndex = 0;
            this.LoadProfile.Text = "Load Profile";
            this.LoadProfile.UseVisualStyleBackColor = true;
            this.LoadProfile.Click += new System.EventHandler(this.LoadProfile_Click);
            // 
            // KeepGreens
            // 
            this.KeepGreens.AutoSize = true;
            this.KeepGreens.Location = new System.Drawing.Point(16, 100);
            this.KeepGreens.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.KeepGreens.Name = "KeepGreens";
            this.KeepGreens.Size = new System.Drawing.Size(114, 21);
            this.KeepGreens.TabIndex = 1;
            this.KeepGreens.Text = "Keep Greens";
            this.KeepGreens.UseVisualStyleBackColor = true;
            this.KeepGreens.CheckedChanged += new System.EventHandler(this.KeepGreens_CheckedChanged);
            // 
            // KeepBlues
            // 
            this.KeepBlues.AutoSize = true;
            this.KeepBlues.Location = new System.Drawing.Point(16, 128);
            this.KeepBlues.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.KeepBlues.Name = "KeepBlues";
            this.KeepBlues.Size = new System.Drawing.Size(102, 21);
            this.KeepBlues.TabIndex = 2;
            this.KeepBlues.Text = "Keep Blues";
            this.KeepBlues.UseVisualStyleBackColor = true;
            this.KeepBlues.CheckedChanged += new System.EventHandler(this.KeepBlues_CheckedChanged);
            // 
            // KeepPurples
            // 
            this.KeepPurples.AutoSize = true;
            this.KeepPurples.Location = new System.Drawing.Point(16, 156);
            this.KeepPurples.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.KeepPurples.Name = "KeepPurples";
            this.KeepPurples.Size = new System.Drawing.Size(115, 21);
            this.KeepPurples.TabIndex = 3;
            this.KeepPurples.Text = "Keep Purples";
            this.KeepPurples.UseVisualStyleBackColor = true;
            this.KeepPurples.CheckedChanged += new System.EventHandler(this.KeepPurples_CheckedChanged);
            // 
            // ProtectedItems
            // 
            this.ProtectedItems.Location = new System.Drawing.Point(16, 214);
            this.ProtectedItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ProtectedItems.Multiline = true;
            this.ProtectedItems.Name = "ProtectedItems";
            this.ProtectedItems.Size = new System.Drawing.Size(232, 184);
            this.ProtectedItems.TabIndex = 8;
            this.ProtectedItems.TextChanged += new System.EventHandler(this.ProtectedItems_TextChanged);
            // 
            // EatAtInput
            // 
            this.EatAtInput.Location = new System.Drawing.Point(309, 11);
            this.EatAtInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EatAtInput.Name = "EatAtInput";
            this.EatAtInput.Size = new System.Drawing.Size(53, 22);
            this.EatAtInput.TabIndex = 9;
            this.EatAtInput.ValueChanged += new System.EventHandler(this.EatAtInput_ValueChanged);
            // 
            // EatAtLabel
            // 
            this.EatAtLabel.AutoSize = true;
            this.EatAtLabel.Location = new System.Drawing.Point(240, 14);
            this.EatAtLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EatAtLabel.Name = "EatAtLabel";
            this.EatAtLabel.Size = new System.Drawing.Size(61, 17);
            this.EatAtLabel.TabIndex = 10;
            this.EatAtLabel.Text = "Eat at %";
            // 
            // DrinkAtInput
            // 
            this.DrinkAtInput.Location = new System.Drawing.Point(309, 39);
            this.DrinkAtInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DrinkAtInput.Name = "DrinkAtInput";
            this.DrinkAtInput.Size = new System.Drawing.Size(53, 22);
            this.DrinkAtInput.TabIndex = 11;
            this.DrinkAtInput.ValueChanged += new System.EventHandler(this.DrinkAtInput_ValueChanged);
            // 
            // DrinkAtLabel
            // 
            this.DrinkAtLabel.AutoSize = true;
            this.DrinkAtLabel.Location = new System.Drawing.Point(228, 42);
            this.DrinkAtLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DrinkAtLabel.Name = "DrinkAtLabel";
            this.DrinkAtLabel.Size = new System.Drawing.Size(73, 17);
            this.DrinkAtLabel.TabIndex = 12;
            this.DrinkAtLabel.Text = "Drink at %";
            // 
            // CorpseLoot
            // 
            this.CorpseLoot.AutoSize = true;
            this.CorpseLoot.Location = new System.Drawing.Point(16, 15);
            this.CorpseLoot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CorpseLoot.Name = "CorpseLoot";
            this.CorpseLoot.Size = new System.Drawing.Size(107, 21);
            this.CorpseLoot.TabIndex = 13;
            this.CorpseLoot.Text = "Corpse Loot";
            this.CorpseLoot.UseVisualStyleBackColor = true;
            this.CorpseLoot.CheckedChanged += new System.EventHandler(this.CorpseLoot_CheckedChanged);
            // 
            // CorpseSkin
            // 
            this.CorpseSkin.AutoSize = true;
            this.CorpseSkin.Location = new System.Drawing.Point(16, 43);
            this.CorpseSkin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CorpseSkin.Name = "CorpseSkin";
            this.CorpseSkin.Size = new System.Drawing.Size(106, 21);
            this.CorpseSkin.TabIndex = 14;
            this.CorpseSkin.Text = "Corpse Skin";
            this.CorpseSkin.UseVisualStyleBackColor = true;
            this.CorpseSkin.CheckedChanged += new System.EventHandler(this.CorpseSkin_CheckedChanged);
            // 
            // ProtectedItemsLabel
            // 
            this.ProtectedItemsLabel.AutoSize = true;
            this.ProtectedItemsLabel.Location = new System.Drawing.Point(12, 194);
            this.ProtectedItemsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ProtectedItemsLabel.Name = "ProtectedItemsLabel";
            this.ProtectedItemsLabel.Size = new System.Drawing.Size(102, 17);
            this.ProtectedItemsLabel.TabIndex = 15;
            this.ProtectedItemsLabel.Text = "ProtectedItems";
            // 
            // NinjaSkin
            // 
            this.NinjaSkin.AutoSize = true;
            this.NinjaSkin.Location = new System.Drawing.Point(16, 71);
            this.NinjaSkin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NinjaSkin.Name = "NinjaSkin";
            this.NinjaSkin.Size = new System.Drawing.Size(93, 21);
            this.NinjaSkin.TabIndex = 16;
            this.NinjaSkin.Text = "Ninja Skin";
            this.NinjaSkin.UseVisualStyleBackColor = true;
            this.NinjaSkin.CheckedChanged += new System.EventHandler(this.NinjaSkin_CheckedChanged);
            // 
            // SearchRadiusInput
            // 
            this.SearchRadiusInput.Location = new System.Drawing.Point(309, 71);
            this.SearchRadiusInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SearchRadiusInput.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SearchRadiusInput.Name = "SearchRadiusInput";
            this.SearchRadiusInput.Size = new System.Drawing.Size(53, 22);
            this.SearchRadiusInput.TabIndex = 17;
            this.SearchRadiusInput.ValueChanged += new System.EventHandler(this.SearchRadiusInput_ValueChanged);
            // 
            // SearchRadiusLabel
            // 
            this.SearchRadiusLabel.AutoSize = true;
            this.SearchRadiusLabel.Location = new System.Drawing.Point(199, 74);
            this.SearchRadiusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SearchRadiusLabel.Name = "SearchRadiusLabel";
            this.SearchRadiusLabel.Size = new System.Drawing.Size(101, 17);
            this.SearchRadiusLabel.TabIndex = 18;
            this.SearchRadiusLabel.Text = "Search Radius";
            // 
            // CMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 414);
            this.Controls.Add(this.SearchRadiusLabel);
            this.Controls.Add(this.SearchRadiusInput);
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
            this.Controls.Add(this.LoadProfile);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CMD";
            this.Text = "Grinder";
            ((System.ComponentModel.ISupportInitialize)(this.EatAtInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrinkAtInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchRadiusInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        System.Windows.Forms.Button LoadProfile;
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
        NumericUpDown SearchRadiusInput;
        Label SearchRadiusLabel;
    }
}
