namespace Image_Comparer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.souborToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPictures = new System.Windows.Forms.ToolStripMenuItem();
            this.addGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.options = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImages = new System.Windows.Forms.ToolStripMenuItem();
            this.previewPicture = new System.Windows.Forms.PictureBox();
            this.Compare = new System.Windows.Forms.Button();
            this.inputPictures = new System.Windows.Forms.ListBox();
            this.outputPictures = new System.Windows.Forms.ListBox();
            this.clear = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.souborToolStripMenuItem,
            this.options,
            this.saveImages});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1199, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // souborToolStripMenuItem
            // 
            this.souborToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPictures,
            this.addGallery});
            this.souborToolStripMenuItem.Name = "souborToolStripMenuItem";
            this.souborToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.souborToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.souborToolStripMenuItem.Text = "Soubor";
            // 
            // addPictures
            // 
            this.addPictures.Name = "addPictures";
            this.addPictures.Size = new System.Drawing.Size(329, 26);
            this.addPictures.Text = "Přidat jednotlivé fotky k porovnání...";
            this.addPictures.Click += new System.EventHandler(this.addPictures_Click);
            // 
            // addGallery
            // 
            this.addGallery.Name = "addGallery";
            this.addGallery.Size = new System.Drawing.Size(329, 26);
            this.addGallery.Text = "Přidat celé složky s fotkami...";
            this.addGallery.Click += new System.EventHandler(this.addGallery_Click);
            // 
            // options
            // 
            this.options.Name = "options";
            this.options.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.options.Size = new System.Drawing.Size(88, 24);
            this.options.Text = "Nastavení";
            // 
            // saveImages
            // 
            this.saveImages.Name = "saveImages";
            this.saveImages.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveImages.Size = new System.Drawing.Size(183, 24);
            this.saveImages.Text = "Uložit porovnané fotky...";
            // 
            // previewPicture
            // 
            this.previewPicture.Location = new System.Drawing.Point(457, 385);
            this.previewPicture.Name = "previewPicture";
            this.previewPicture.Size = new System.Drawing.Size(293, 208);
            this.previewPicture.TabIndex = 1;
            this.previewPicture.TabStop = false;
            // 
            // Compare
            // 
            this.Compare.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Compare.Location = new System.Drawing.Point(457, 48);
            this.Compare.Name = "Compare";
            this.Compare.Size = new System.Drawing.Size(293, 75);
            this.Compare.TabIndex = 3;
            this.Compare.Text = "Porovnej!";
            this.Compare.UseVisualStyleBackColor = true;
            this.Compare.Click += new System.EventHandler(this.Compare_Click);
            // 
            // inputPictures
            // 
            this.inputPictures.FormattingEnabled = true;
            this.inputPictures.HorizontalScrollbar = true;
            this.inputPictures.ItemHeight = 20;
            this.inputPictures.Location = new System.Drawing.Point(15, 48);
            this.inputPictures.Name = "inputPictures";
            this.inputPictures.Size = new System.Drawing.Size(377, 544);
            this.inputPictures.TabIndex = 5;
            this.inputPictures.MouseDown += new System.Windows.Forms.MouseEventHandler(this.inputPictures_MouseDown);
            // 
            // outputPictures
            // 
            this.outputPictures.FormattingEnabled = true;
            this.outputPictures.HorizontalScrollbar = true;
            this.outputPictures.ItemHeight = 20;
            this.outputPictures.Location = new System.Drawing.Point(804, 48);
            this.outputPictures.Name = "outputPictures";
            this.outputPictures.Size = new System.Drawing.Size(377, 544);
            this.outputPictures.TabIndex = 6;
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(457, 144);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(293, 61);
            this.clear.TabIndex = 8;
            this.clear.Text = "Vyčisti";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 615);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.outputPictures);
            this.Controls.Add(this.inputPictures);
            this.Controls.Add(this.Compare);
            this.Controls.Add(this.previewPicture);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem souborToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem options;
        private System.Windows.Forms.ToolStripMenuItem addPictures;
        private System.Windows.Forms.PictureBox previewPicture;
        private System.Windows.Forms.Button Compare;
        private System.Windows.Forms.ToolStripMenuItem addGallery;
        private System.Windows.Forms.ListBox inputPictures;
        private System.Windows.Forms.ListBox outputPictures;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.ToolStripMenuItem saveImages;
    }
}

