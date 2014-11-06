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
            var path = Path.Combine(AssetsDirectory, fileName.ToLowerInvariant() + ".xml");
            serializer.SerializeAndAssert(obj, path);
            serializer.DeserializeAndAssert(obj, path);
        }

        protected void ActAndAssert(object obj)
        {
            ActAndAssert(obj, obj.GetType().Name.ToLowerInvariant());
        }
    }
}