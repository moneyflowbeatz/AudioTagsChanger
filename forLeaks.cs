﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;

namespace WindowsFormsApp1
{
    public partial class forLeaks : Form
    {
        public forLeaks()
        {
            InitializeComponent();
        }
        private string filePath;
        private string archiveDescription;

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    try
                    {
                        string fileContent = File.ReadAllText(selectedFile);
                        textBox1.Text = selectedFile;
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при чтении файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string sourceDirectory = textBox2.Text;
            string archiveName = textBox3.Text;
            string destinationArchive = textBox2.Text + @"\" + archiveName + ".zip";
            //string archiveDescription = 
            //    "Leaked by @affectlab\n\nt.me/affectlab";
            string textFilePath = textBox1.Text; // Получаем путь к текстовому файлу из textBox1

            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(sourceDirectory);

                    if (!string.IsNullOrEmpty(textFilePath) && File.Exists(textFilePath)) // Проверяем, что путь к файлу указан и файл существует
                    {
                        zip.AddFile(textFilePath, ""); // Добавляем текстовый файл к архиву
                    }

                    ProgressBar progressBar = new ProgressBar
                    {
                        Minimum = 0,
                        Maximum = 100,
                        Dock = DockStyle.Bottom,
                        Style = ProgressBarStyle.Continuous
                    };

                    Controls.Add(progressBar);

                   

                    await Task.Run(() =>
                    {
                        zip.SaveProgress += (o, args) =>
                        {
                            // Проверяем, что EntriesTotal не равен нулю, чтобы избежать ошибки деления на ноль
                            if (args.EntriesTotal != 0)
                            {
                                // Обновляем значение ProgressBar в главном потоке
                                Invoke(new MethodInvoker(() =>
                                {
                                    progressBar.Value = (int)((args.EntriesSaved * 100) / args.EntriesTotal);
                                }));
                            }
                        };


                        zip.Comment = archiveDescription;
                        zip.Save(destinationArchive);
                    });

                    

                    
                }

                MessageBox.Show("Архив успешно создан.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании архива: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedDirectory = folderBrowserDialog.SelectedPath; // Получаем путь к выбранной директории
                    textBox2.Text = selectedDirectory; // Отправляем путь к директории в текстовое поле
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName; // Получаем путь к выбранному текстовому файлу
                    archiveDescription = ReadTextFromFile(filePath); // Читаем текст из файла

                    if (!string.IsNullOrEmpty(archiveDescription)) // Проверяем, что текст был успешно считан
                    {
                        label5.Visible = true; // Отображаем Label5
                    }
                }
            }

        }

        private string ReadTextFromFile(string filePath)
        {
            string fileContent = "";
            try
            {
                // Чтение текста из файла
                fileContent = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                // Обработка ошибки чтения файла
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fileContent;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Main main = new Main();
            main.Show();
        }
    }
}
