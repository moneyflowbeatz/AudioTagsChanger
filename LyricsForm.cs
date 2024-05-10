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
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApp1
{
    public partial class LyricsForm : Form
    {
        public LyricsForm()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lyricsText = richTextBox1.Text;

            
            LyricsManager.SongLyrics = lyricsText;

            MessageBox.Show("Текст песни сохранен.", "Сохранение");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }
    }
}
