using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class LyricsManager
    {
        public static string SongLyrics { get; set; }
        public static string GetLyricsFromAudioFile(string audioFilePath)
        {
            try
            {
                TagLib.File audioFile = TagLib.File.Create(audioFilePath);
                return audioFile.Tag.Lyrics;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при получении текста песни из аудиофайла: " + ex.Message);
                return null;
            }
        }
    }


}
