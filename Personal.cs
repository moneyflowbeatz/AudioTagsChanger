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
using TagLib;

namespace WindowsFormsApp1
{
   
    public partial class Personal : Form
    {
        
        public Personal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Аудиофайлы (*.mp3;*.wav;*.flac;*.aac;*.ogg;*.m4a)|*.mp3;*.wav;*.flac;*.aac;*.ogg;*.m4a|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    textBox1.Text = selectedFile; 
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedFile = textBox1.Text;
            string newDescription = textBox2.Text;
            string newPerformer = textBox3.Text;
            string AlbumName = textBox4.Text;
            string Label = textBox12.Text;
            string Genre = textBox8.Text;
            string Year = textBox6.Text;
            string imagePath = textBox4.Text;

            if (System.IO.File.Exists(selectedFile) && !string.IsNullOrEmpty(newDescription))
            {
                try
                {
                    TagLib.File audioFile = TagLib.File.Create(selectedFile);
                    audioFile.Tag.Title = newDescription;
                    audioFile.Tag.AlbumArtists = new string[] { newPerformer };
                    audioFile.Tag.Composers = new string[] { AlbumName };
                    audioFile.Tag.Publisher = Label;
                    audioFile.Tag.Copyright = "@affectlab";
                    if (!string.IsNullOrEmpty(imagePath)) {
                        TagLib.Picture picture = new TagLib.Picture(imagePath);
                        audioFile.Tag.Pictures = new TagLib.IPicture[] { picture };
                    }
                    audioFile.Tag.Genres = new string[] { Genre };
                    audioFile.Tag.Lyrics = LyricsManager.SongLyrics;
                    audioFile.Tag.Year = Convert.ToUInt16(Year);
                    audioFile.Tag.Performers = new string[] { newPerformer };
                    audioFile.Save();

                    MessageBox.Show("Метаданные аудиофайла изменены.", "Успешно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при изменении метаданных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите аудиофайл и введите новое описание.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void button5_Click(object sender, EventArgs e)
        {
            string currentAudioFilePath = textBox1.Text; // Получаем путь к текущему аудиофайлу из textBox1

            if (!string.IsNullOrEmpty(currentAudioFilePath))
            {
                // Удаляем текст песни из текущего аудиофайла
                LyricsManager.RemoveLyricsFromAudioFile(currentAudioFilePath);
                MessageBox.Show("Текст песни успешно удален из аудиофайла.", "Успешно");
            }
            else
            {
                MessageBox.Show("Не указан путь к аудиофайлу.", "Ошибка");
            }
        }
    }
}
