using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.IntegrationTests.Utils;

namespace SimpleXmlSerializer.IntegrationTests.Tests
{
    [TestClass]
    public class TestsBase
    {
        private XmlSerializer serializer = new XmlSerializer(GetSettingsBuilder().GetSettings());

        public string AssetsDirectory
        {
            get
            {
                return string.Format("Assets\\{0}", GetType().Name.Replace("Tests", string.Empty));
            }
        }

        public XmlSerializer Serializer
        {
            get { return serializer; }
            set { serializer = value; }
        }

        protected void ActAndAssert(object obj, string fileName)
        {
            SerializeAndAssert(obj, fileName);
            DeserializeAndAssert(obj, fileName);
        }

        protected void SerializeAndAssert(object obj, string fileName)
        {
            var path = GetXmlFilePath(fileName);
            Serializer.SerializeAndAssert(obj, path);
        }

        protected void DeserializeAndAssert(object obj, string fileName)
        {
            var path = GetXmlFilePath(fileName);
            Serializer.DeserializeAndAssert(obj, path);
        }

        protected void ActAndAssert(object obj)
        {
            ActAndAssert(obj, obj.GetType().Name.ToLowerInvariant());
        }

        protected string GetXmlFilePath(string fileName)
        {
            return Path.Combine(AssetsDirectory, fileName.ToLowerInvariant() + ".xml");
        }

        protected static XmlSerializerSettingsBuilder GetSettingsBuilder()
        {
            return new XmlSerializerSettingsBuilder().SetDecapitalizeNamingConvention();
        }
    }
}