using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Image_Comparer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //structures and variables
        private class photoForComparison
        {
            public List<bool> hash;
            public List<int> photoSize= new List<int>();
            public List<String> name = new List<string>();
            public photoForComparison(List<bool> hash, int photoSize, String name)
            {
                this.hash = hash;
                this.photoSize.Add(photoSize);
                this.name.Add(name);
            }
        }
        private List<photoForComparison> hashingTable = new List<photoForComparison>();
        public static int errorMargin = 1;
        public static int hashSize = 16;
        public static List<String> conflictingImages = new List<string>();
        //Aplication specific functions
        public static bool compareImg(Bitmap Img1, Bitmap Img2, int errMargin)
        {
            List<bool> Hash1 = getHash(Img1, hashSize);
            List<bool> Hash2 = getHash(Img2, hashSize);
            if (Hash1.Count() == Hash2.Count())
            {
                for (int i = 0; i < Hash1.Count(); i++)
                {
                    if (Hash1[i] != Hash2[i])
                    {
                        errMargin--;
                    }
                }
                if (errMargin > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool compareHash(List<bool> hash1, List<bool> hash2, int errMargin)
        {
            if (hash1.Count() == hash2.Count())
            {
                for (int i = 0; i < hash1.Count(); i++)
                {
                    if (hash1[i] != hash2[i])
                    {
                        errMargin--;
                    }
                }
                if (errMargin > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void addPicturesToSelection()
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = "c:\\";
                fileDialog.Title = "Prohlížeč obrázků";
                fileDialog.Filter = "Obrázky (*.BMP; *.JPG; *.JPEG; *.PNG; *.GIF)|*.BMP; *.JPG; *.JPEG; *.PNG; *.GIF";
                fileDialog.Multiselect = true;
                fileDialog.RestoreDirectory = true;
                DialogResult result = fileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (String fileName in fileDialog.FileNames)
                    {
                        try
                        {
                            inputPictures.Items.Add(fileName);
                            //MessageBox.Show("adding item: " + fileName);
                            int size = (int)new System.IO.FileInfo(fileName).Length;
                            Bitmap img = (Bitmap)Image.FromFile(fileName);
                            List<bool> hash = getHash(img, hashSize);
                            bool unique = true;
                            foreach (photoForComparison photo in hashingTable)
                            {
                                if (compareHash(photo.hash, hash, errorMargin) && unique)
                                {
                                    //MessageBox.Show("adding to existing hash");
                                    photo.photoSize.Add(size);
                                    photo.name.Add(fileName);
                                    unique = false;
                                }
                            }
                            if (unique)
                            {
                                //MessageBox.Show("unique");
                                hashingTable.Add(new photoForComparison(hash, size, fileName));
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Soubor " + fileName.Substring(fileName.LastIndexOf("\\")) + " nemůže být zobrazen. Nastal Error" + ex.Message);
                        }
                    }
                }
            }
        }

        private void addGaleryToSelection()
        {
            using (FolderBrowserDialog fileDialog = new FolderBrowserDialog())
            {
                String[] pictureExtensions = { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
                fileDialog.Description = "Prohlížeč obrázků";
                DialogResult result = fileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    var files = Directory.GetFiles(fileDialog.SelectedPath, "*.*", SearchOption.AllDirectories).Where(str => pictureExtensions.Contains(Path.GetExtension(str).ToLower()));
                    foreach (String fileName in files)
                    {
                        try
                        {
                            inputPictures.Items.Add(fileName);
                            //MessageBox.Show("adding item: " + fileName);
                            int size = (int)new System.IO.FileInfo(fileName).Length;
                            Bitmap img = (Bitmap)Image.FromFile(fileName);
                            List<bool> hash = getHash(img, hashSize);
                            bool unique = true;
                            foreach (photoForComparison photo in hashingTable)
                            {
                                if (compareHash(photo.hash, hash, errorMargin) && unique)
                                {
                                    //MessageBox.Show("adding to existing hash");
                                    photo.photoSize.Add(size);
                                    photo.name.Add(fileName);
                                    unique = false;
                                }
                            }
                            if (unique)
                            {
                                //MessageBox.Show("unique");
                                hashingTable.Add(new photoForComparison(hash, size, fileName));
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Soubor " + fileName.Substring(fileName.LastIndexOf("\\")) + " nemůže být zobrazen. Nastal Error" + ex.Message);
                        }
                    }
                }
            }
        }
        public static List<bool> getHash(Bitmap Sourcebmp, int newBmpDim)
        {
            List<bool> result = new List<bool>();
            Bitmap newBmp = new Bitmap(Sourcebmp, new Size(newBmpDim, newBmpDim));
            for (int i = 0; i < newBmpDim; i++)
            {
                for (int j = 0; j < newBmpDim; j++)
                {
                    result.Add(newBmp.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return result;
        }

        private void compareAll()
        {
            outputPictures.Items.Clear();
            for (int i = 0; i < inputPictures.Items.Count; i++)
            {
                Bitmap image1 = (Bitmap)Image.FromFile(inputPictures.Items[i].ToString());
                for (int j = i + 1; j < inputPictures.Items.Count; j++)
                {
                    Bitmap image2 = (Bitmap)Image.FromFile(inputPictures.Items[j].ToString());
                    //MessageBox.Show("comparing " +inputPictures.Items[i].ToString()+ " and " + inputPictures.Items[j].ToString());
                    if (compareImg(image1, image2, errorMargin))
                    {
                        //MessageBox.Show(inputPictures.Items[i].ToString() + " is not unique");
                        break;
                    }
                    if (j == inputPictures.Items.Count - 1)
                    {
                        //MessageBox.Show(inputPictures.Items[i].ToString() + " is unique");
                        outputPictures.Items.Add(inputPictures.Items[i]);
                    }
                }
                if (i == inputPictures.Items.Count - 1)
                {
                    //MessageBox.Show(inputPictures.Items[i].ToString() + " is unique");
                    outputPictures.Items.Add(inputPictures.Items[i]);
                }
            }
        }
        private void betterCompareAll()
        {
            outputPictures.Items.Clear();
            foreach (photoForComparison hash in hashingTable)
            {
                if (hash.name.Count()==1)
                {
                    outputPictures.Items.Add(hash.name[0]);
                }
                else
                {
                    conflictingImages.Clear();
                    foreach (String name in hash.name)
                    {
                        conflictingImages.Add(name);
                    }
                    Form conflictImages = new Form2();
                    DialogResult result = conflictImages.ShowDialog();
                    if (result==DialogResult.OK)
                    {
                        foreach (String img in conflictingImages)
                        {
                            outputPictures.Items.Add(img);
                        }
                    }
                }
            }
        }
        //Form handlers
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
        private void Compare_Click(object sender, EventArgs e)
        {
            //compareAll();
            betterCompareAll();
        }

        private void addPictures_Click(object sender, EventArgs e)
        {
            addPicturesToSelection();
        }

        private void addGallery_Click(object sender, EventArgs e)
        {
            addGaleryToSelection();
        }

        private void inputPictures_MouseDown(object sender, MouseEventArgs e)
        {
            if (inputPictures.SelectedItem == null)
            {
                return;
            }
            previewPicture.Load(inputPictures.SelectedItem.ToString());
            previewPicture.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void clear_Click(object sender, EventArgs e)
        {
            inputPictures.Items.Clear();
            outputPictures.Items.Clear();
            hashingTable.Clear();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name=="options")
            {

            }
            if (e.ClickedItem.Name=="saveImages")
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "Uložit fotky";
                if (save.ShowDialog()==DialogResult.OK)
                {

                }
            }
        }
    }
}
