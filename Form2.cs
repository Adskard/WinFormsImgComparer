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
            
            
        }
        public Form2(List<String> con)
        {
            InitializeComponent();
            setImages();
        }
        private void setImages()
        {
            Image img=default(Image);
            ListViewItem item=default(ListViewItem);
            for (int i = 0; i < Form1.conflictingImages.Count; i++)
            {
            img = Image.FromFile(Form1.conflictingImages[i]);
            imageList1.Images.Add(img);
            item = new ListViewItem { ImageIndex = i, Text= Form1.conflictingImages[i]};
            listView1.Items.Add(item);
            }
        }

        private void select_Click(object sender, EventArgs e)
        {
            Form1.conflictingImages.Clear();
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                Form1.conflictingImages.Add(listView1.SelectedItems[i].Text);
            }
            GC.Collect();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
