﻿using System;
using System.Xml;
using SimpleXmlSerializer.Utils;

namespace SimpleXmlSerializer.Extensions
{
    public static class XmlReaderExtensions
    {
        public static bool ReadToDescendant(this XmlReader xmlReader)
        {
            if (xmlReader == null)
            {
                throw new ArgumentNullException("xmlReader");
            }

            if (xmlReader.NodeType != XmlNodeType.Element)
            {
                throw new InvalidOperationException(string.Format("Current node type must be '{0}'", XmlNodeType.Element));
            }

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
            if (xmlReader == null)
            {
                throw new ArgumentNullException("xmlReader");
            }

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