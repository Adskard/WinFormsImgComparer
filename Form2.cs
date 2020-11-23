using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Image_Comparer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            setImages();
        }
        private void setImages()
        {
            foreach (String name in Form1.conflictingImages)
            {
                imageList1.Images.Add(Image.FromFile(name));
            }
            //imageList1.ImageSize = new Size(listView1.Size.Width / Form1.conflictingImages.Count, listView1.Size.Height / Form1.conflictingImages.Count);
            for (int i = 0; i < Form1.conflictingImages.Count; i++)
            {
                ListViewItem img = new ListViewItem { ImageIndex = i, Text= Form1.conflictingImages[i]};
                listView1.Items.Add(img);
            }
        }

        private void select_Click(object sender, EventArgs e)
        {
            Form1.conflictingImages.Clear();
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                Form1.conflictingImages.Add(listView1.SelectedItems[i].Text);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }
    }
}
