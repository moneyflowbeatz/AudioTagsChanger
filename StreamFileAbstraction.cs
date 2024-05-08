using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace WindowsFormsApp1
{
    public class StreamFileAbstraction : TagLib.File.IFileAbstraction
    {
        private string name;
        private Stream readStream;
        private Stream writeStream;

        public StreamFileAbstraction(string name, Stream readStream, Stream writeStream)
        {
            this.name = name;
            this.readStream = readStream;
            this.writeStream = writeStream;
        }

        public string Name
        {
            get { return name; }
        }

        public Stream ReadStream
        {
            get { return readStream; }
        }

        public Stream WriteStream
        {
            get { return writeStream; }
        }

        public void CloseStream(Stream stream)
        {
            // Не закрываем потоки, так как это может быть обработано вне этого класса.
        }
    }
}
