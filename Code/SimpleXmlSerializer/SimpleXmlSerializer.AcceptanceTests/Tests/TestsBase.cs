using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class TestsBase
    {
        protected XmlSerializer serializer = new XmlSerializer();

        public string AssetsDirectory
        {
            get
            {
                return string.Format("Assets\\{0}", GetType().Name.Replace("Tests", string.Empty));
            }
        }

        protected void ActAndAssert(object obj, string fileName)
        {
            SerializeAndAssert(obj, fileName);
            DeserializeAndAssert(obj, fileName);
        }

        protected void SerializeAndAssert(object obj, string fileName)
        {
            var path = GetXmlFilePath(fileName);
            serializer.SerializeAndAssert(obj, path);
        }

        protected void DeserializeAndAssert(object obj, string fileName)
        {
            var path = GetXmlFilePath(fileName);
            serializer.DeserializeAndAssert(obj, path);
        }

        protected void ActAndAssert(object obj)
        {
            ActAndAssert(obj, obj.GetType().Name.ToLowerInvariant());
        }

        private string GetXmlFilePath(string fileName)
        {
            return Path.Combine(AssetsDirectory, fileName.ToLowerInvariant() + ".xml");
        }
    }
}