using System.IO;
using System.Text;

namespace SimpleXmlSerializer.Utils
{
    public class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override System.Text.Encoding Encoding
        {
            get
            {
                return encoding;
            }
        }
    }
}