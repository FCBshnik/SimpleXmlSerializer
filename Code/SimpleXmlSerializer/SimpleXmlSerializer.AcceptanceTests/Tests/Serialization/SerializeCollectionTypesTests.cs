using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.AcceptanceTests.Tests.Serialization
{
    [TestClass]
    public class SerializeCollectionTypesTests
    {
        private const string AssetsDirectory = "Assets\\Collections";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Serialize_ListOfPrimitives()
        {
            var collection = new List<string> { "One", "Two" };

            SerializeAndAssert(collection, "numbersCollection");
        }
        
        [TestMethod]
        public void Serialize_ArrayOfPrimitives()
        {
            var collection = new[] { "One", "Two" };

            SerializeAndAssert(collection, "numbersCollection");
        }
        
        [TestMethod]
        public void Serialize_DictionaryOfPrimitives()
        {
            var collection = new Dictionary<int, string> { {1, "One"}, {2,"Two"} };

            SerializeAndAssert(collection, "numbersDictionary");
        }

        [TestMethod]
        public void Serialize_ListOfComplexes()
        {
            var players = new List<Player> { Players.Xavi, Players.Iniesta, };

            SerializeAndAssert(players, "playersCollection");
        }

        [TestMethod]
        public void Serialize_ArrayOfComplexes()
        {
            var players = new[] { Players.Xavi, Players.Iniesta };

            SerializeAndAssert(players, "playersCollection");
        }

        [TestMethod]
        public void Serialize_DictionaryOfComplexes()
        {
            var players = new Dictionary<string, Player>
                {
                    { "Xavi", Players.Xavi }, 
                    { "Iniesta", Players.Iniesta }
                };

            SerializeAndAssert(players, "playersDictionary");
        }

        [TestMethod]
        public void Serialize_ListOfCollections()
        {
            var collection = new List<IEnumerable<string>>
                {
                    new List<string> { "One", "Two" },
                    new List<string> { "Three", "Four" }
                };

            SerializeAndAssert(collection, "numbersCollectionsCollection");
        }

        [TestMethod]
        public void Serialize_ArrayOfCollections()
        {
            var collection = new IEnumerable<string>[]
                {
                    new List<string> { "One", "Two" },
                    new List<string> { "Three", "Four" }
                };

            SerializeAndAssert(collection, "numbersCollectionsCollection");
        }

        [TestMethod]
        public void Serialize_DictionaryOfCollections()
        {
            var collection = new Dictionary<int, IEnumerable<string>>
                {
                    { 1, new List<string> { "One", "Two" } }, 
                    { 2, new List<string> { "Three", "Four" } }
                };

            SerializeAndAssert(collection, "numbersCollectionsDictionary");
        }

        private void SerializeAndAssert(object collection, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName + ".xml");

            serializer.SerializeAndAssertObject(collection, path);
        }
    }
}