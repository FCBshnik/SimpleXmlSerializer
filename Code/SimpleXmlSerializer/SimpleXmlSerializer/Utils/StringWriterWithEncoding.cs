using System.IO;
using System.Text;

namespace SimpleXmlSerializer.Utils
{
    internal class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return encoding;
            }
        }
    }
}