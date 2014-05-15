using System.Xml;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Extensions
{
    public static class XmlReaderExtensions
    {
        public static bool ReadToDescendant(this XmlReader xmlReader)
        {
            Preconditions.NotNull(xmlReader, "xmlReader");

            var depth = xmlReader.Depth;

            while (xmlReader.Read() && !(xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Depth == depth))
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Depth == depth)
                {
                    return false;
                }

                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Depth == depth + 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ReadToNextSibling(this XmlReader xmlReader)
        {
            Preconditions.NotNull(xmlReader, "xmlReader");

            var depth = xmlReader.Depth;

            while (xmlReader.Read() && !(xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Depth == depth - 1))
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Depth == depth)
                {
                    return true;
                }
            }

            return false;
        }
    }
}