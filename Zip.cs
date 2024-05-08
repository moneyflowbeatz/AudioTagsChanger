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
using System.IO.Compression;
using TagLib;
using TagLib.Mpeg;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Zip : Form
    {
        public Zip()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Архивы (*.zip)|*.zip"; // Фильтр для отображения только ZIP-архивов
                openFileDialog.RestoreDirectory = true; // Восстановить текущий каталог при закрытии диалога

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName; // Установка пути к архиву в textBox1
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedArchive = textBox1.Text;
            string newDescription = textBox2.Text;

            if (!string.IsNullOrEmpty(selectedArchive) && !string.IsNullOrEmpty(newDescription))
            {
                List<string> audioExtensions = new List<string> { ".mp3", ".wav", ".flac", ".aac", ".ogg", ".m4a" };

                try
                {
                    using (var zipStream = new FileStream(selectedArchive, FileMode.Open))
                    using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                    {
                        foreach (var entry in zipArchive.Entries)
                        {
                            string entryExtension = Path.GetExtension(entry.FullName).ToLower();
                            if (audioExtensions.Contains(entryExtension))
                            {
                                using (var entryStream = entry.Open())
                                {
                                    ProcessAudioFile(entryStream, newDescription);
                                }
                            }
                        }
                    }

                    MessageBox.Show("Метаданные аудиофайлов в архиве изменены.", "Успешно");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при изменении метаданных аудиофайла в архиве: " + selectedArchive + "\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ProcessAudioFile(Stream audioStream, string newDescription)
        {
            try
            {
                using (TagLib.File audioFile = TagLib.File.Create(new StreamFileAbstraction("", audioStream, null)))
                {
                    audioFile.Tag.Title = newDescription;
                    audioFile.Tag.Lyrics = newDescription;
                    audioFile.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при изменении метаданных аудиофайла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
