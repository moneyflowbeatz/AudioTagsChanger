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
            string newPerformer = textBox5.Text;
            string AlbumName = textBox1.Text;
            string Label = textBox12.Text;
            string Genre = textBox8.Text;
            string Year = textBox6.Text;
            string imagePath = textBox4.Text;


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
                            audioFile.Tag.AlbumArtists = new string[] { newPerformer };
                            audioFile.Tag.Composers = new string[] { AlbumName };
                            audioFile.Tag.Publisher = Label;
                            audioFile.Tag.Copyright = "@affectlab";
                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                TagLib.Picture picture = new TagLib.Picture(imagePath);
                                audioFile.Tag.Pictures = new TagLib.IPicture[] { picture };
                            }
                            audioFile.Tag.Genres = new string[] { Genre };
                            audioFile.Tag.Lyrics = newDescription;
                            audioFile.Tag.Year = Convert.ToUInt16(Year);
                            audioFile.Tag.Performers = new string[] { newPerformer }; 
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
            textBox4.Visible = false;
            button4.Enabled = false;
            button5.Enabled = false;
            toolTip1.SetToolTip(textBox1, "Тут появится путь к вашей папке после нажатия на кнопку ниже.");
            toolTip1.SetToolTip(button1, "Выборите каталог в появившемся меню после нажатия на кнопку");
            toolTip1.SetToolTip(button3, "Недоступно для изменения текста всех аудиофайлов сразу, возможны конфликты");
            toolTip1.SetToolTip(button4, "Недоступно для изменения текста всех аудиофайлов сразу, возможны конфликты");
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

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(toolTip1.GetToolTip(button1), button1, button1.Width, 0);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button1);
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show(toolTip1.GetToolTip(button1), button1, button1.Width, 0);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(button1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPhotoPath = openFileDialog.FileName;
                textBox4.Visible = true;
                textBox4.Text = selectedPhotoPath;
                label10.Visible = false;
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Main main = new Main();
            main.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var lyricsForm = new LyricsForm())
            {
                if (lyricsForm.ShowDialog() == DialogResult.OK)
                {
                    LyricsManager.SongLyrics = lyricsForm.richTextBox1.Text;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string currentAudioFilePath = textBox1.Text; // Получаем путь к текущему аудиофайлу из textBox1

            if (!string.IsNullOrEmpty(currentAudioFilePath))
            {
                // Получаем текст песни из текущего аудиофайла
                string currentLyrics = LyricsManager.GetLyricsFromAudioFile(currentAudioFilePath);

                if (!string.IsNullOrEmpty(currentLyrics))
                {

                    // Отображаем текст песни в RichTextBox на форме LyricsForm


                    // Открываем форму LyricsForm для изменения текста песни
                    using (var lyricsForm = new LyricsForm())
                    {
                        if (lyricsForm.ShowDialog() == DialogResult.OK)
                        {
                            lyricsForm.richTextBox1.Text = currentLyrics;
                            // Получаем измененный текст песни
                            string editedLyrics = lyricsForm.richTextBox1.Text;

                            // Сохраняем измененный текст песни обратно в LyricsManager
                            LyricsManager.SongLyrics = editedLyrics;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось получить текст песни из аудиофайла.", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Не указан путь к аудиофайлу.", "Ошибка");
            }
        }

       
    }
}
