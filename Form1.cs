using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TagLib;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string selectedFolder = folderBrowser.SelectedPath;
                textBox1.Text = selectedFolder;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedFolder = textBox1.Text; 
            string newDescription = textBox2.Text;
            string newPerformer = textBox3.Text;
            string AlbumName = textBox1.Text;

            if (!string.IsNullOrEmpty(selectedFolder) && !string.IsNullOrEmpty(newDescription))
            {
                string[] audioFiles = Directory.GetFiles(selectedFolder, "*.*", SearchOption.AllDirectories); 
                List<string> audioExtensions = new List<string> { ".mp3", ".wav", ".flac", ".aac", ".ogg", ".m4a" }; 

                foreach (string file in audioFiles)
                {
                    string extension = Path.GetExtension(file).ToLower();
                    if (audioExtensions.Contains(extension))
                    {
                        try
                        {
                            TagLib.File audioFile = TagLib.File.Create(file);
                            audioFile.Tag.Title = newDescription;
                            audioFile.Tag.Comment = newDescription;
                            audioFile.Tag.Conductor = newDescription;
                            audioFile.Tag.Copyright = newDescription;
                            audioFile.Tag.Genres = new string[] { newDescription };
                            audioFile.Tag.Lyrics = newDescription;
                            audioFile.Tag.Publisher = newDescription;
                            audioFile.Tag.RemixedBy = newDescription;
                            audioFile.Tag.Subtitle = newDescription;
                            audioFile.Tag.Year = 2024;
                            audioFile.Tag.Performers = new string[] { newPerformer }; 
                            audioFile.Tag.Composers = new string[] { AlbumName };
                            audioFile.Tag.AlbumArtists = new string[] { newPerformer };
                            audioFile.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при переименовании описания: " + file + "\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                MessageBox.Show("Описание аудиофайлов изменено.", "Успешно"); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Тут появится путь к вашей папке после нажатия на кнопку ниже.");
            toolTip1.SetToolTip(button1, "Выборите каталог в появившемся меню после нажатия на кнопку");
        }

        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(toolTip1.GetToolTip(textBox1), textBox1, textBox1.Width, 0);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(textBox1);
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(toolTip1.GetToolTip(button1), button1, button1.Width, 0);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button1);
        }
    }
}
