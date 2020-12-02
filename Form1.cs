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
using System.Diagnostics;

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
            public bool[] hash;
            public List<String> name = new List<string>();
            public photoForComparison(bool[] hash, int photoSize, String name)
            {
                this.hash = hash;
                this.name.Add(name);
            }
        }
        private List<photoForComparison> hashingTable = new List<photoForComparison>();
        public static int errorMargin = 1;
        public static int hashSize = 16;
        public static bool del = false;
        public static List<String> conflictingImages = new List<string>();
        //Aplication specific functions
        public static bool compareHash(bool[] hash1, bool[] hash2, int errMargin)
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
                List<String> errFiles = new List<string>();
                Exception ex = default(Exception);
                DialogResult result = fileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (String fileName in fileDialog.FileNames)
                    {
                        try
                        {
                            inputPictures.Items.Add(fileName);
                            int size = (int)new System.IO.FileInfo(fileName).Length;
                            Bitmap img = (Bitmap)Image.FromFile(fileName);
                            bool[] hash = getHash(img, hashSize);
                            img.Dispose();
                            img = null;
                            bool unique = true;
                            foreach (photoForComparison photo in hashingTable)
                            {
                                if (compareHash(photo.hash, hash, errorMargin) && unique)
                                {
                                    photo.name.Add(fileName);
                                    unique = false;
                                }
                            }
                            if (unique)
                            {
                                hashingTable.Add(new photoForComparison(hash, size, fileName));
                            }
                        }
                        catch (Exception err)
                        {
                            if (err!=ex)
                            {
                                MessageBox.Show("Soubory " + string.Join(", ", errFiles) + " nemůžou být přidány. Nastal Error " + ex.Message);
                                errFiles = null;
                                errFiles= new List<string>();
                            }
                            ex = err;
                            errFiles.Add(fileName.Substring(fileName.LastIndexOf("\\")));
                        }
                    }
                    MessageBox.Show("Soubory " + string.Join(", ", errFiles) + " nemůžou být přidány. Nastal Error " + ex.Message);
                }
                fileDialog.Dispose();
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
                    String[] files = Directory.GetFiles(fileDialog.SelectedPath, "*.*", SearchOption.AllDirectories).Where(str => pictureExtensions.Contains(Path.GetExtension(str).ToLower())).ToArray();
                    File.WriteAllText("./files.txt", string.Join("\n", files));
                    int lines = File.ReadLines("./files.txt").Count();
                    File.WriteAllText("./lines.txt", lines.ToString());
                    foreach (String fileName in files)
                    {
                        try
                        {
                            inputPictures.Items.Add(fileName);
                            int size = (int)new System.IO.FileInfo(fileName).Length;
                            Bitmap img = (Bitmap)Image.FromFile(fileName);
                            bool[] hash = getHash(img, hashSize);
                            img.Dispose();
                            img = null;
                            bool unique = true;
                            foreach (photoForComparison photo in hashingTable)
                            {
                                if (compareHash(photo.hash, hash, errorMargin) && unique)
                                {
                                    photo.name.Add(fileName);
                                    unique = false;
                                }
                            }
                            if (unique)
                            {
                                hashingTable.Add(new photoForComparison(hash, size, fileName));
                            }
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("Soubor " + fileName + " nemůže být přidán. Nastal Error " + err.Message);
                        }
                    }
                }
                fileDialog.Dispose();
            }
        }
        public static bool[] getHash(Bitmap Sourcebmp, int newBmpDim)
        {
            bool[] result = new bool[newBmpDim*newBmpDim];
            Bitmap newBmp = new Bitmap(Sourcebmp, new Size(newBmpDim, newBmpDim));
            for (int i = 0; i < newBmpDim; i++)
            {
                for (int j = 0; j < newBmpDim; j++)
                {
                    result[i*newBmpDim+j]=(newBmp.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return result;
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
                    Form resolveConflict = new Form2(conflictingImages);
                    resolveConflict.ShowDialog();
                    if (resolveConflict.DialogResult == DialogResult.OK)
                    {
                        foreach (String img in conflictingImages)
                        {
                            outputPictures.Items.Add(img);
                        }
                    }
                }
            }
            conflictingImages.Clear();
        }
        //Form handlers
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
        private void Compare_Click(object sender, EventArgs e)
        {
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
            else
            {

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
                FolderBrowserDialog save = new FolderBrowserDialog();
                if (save.ShowDialog()==DialogResult.OK)
                {
                    foreach (String name in outputPictures.Items)
                    {
                        String fileName = Path.GetFileName(name);
                        try
                        {
                            System.IO.File.Copy(name, Path.Combine(save.SelectedPath, fileName));
                            if (del)
                            {
                                System.IO.File.Delete(name);
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("soubor " + fileName + " nelze přemístit do cílové složky " + save.SelectedPath);
                        } 
                    }
                    MessageBox.Show("Obrázky úspěšně zkopírovány");
                }
            }
        }
    }
}
