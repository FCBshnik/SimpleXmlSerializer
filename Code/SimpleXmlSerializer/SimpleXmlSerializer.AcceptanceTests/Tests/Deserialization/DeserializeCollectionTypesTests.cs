using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.Core;

namespace SimpleXmlSerializer.AcceptanceTests.Tests.Deserialization
{
    [TestClass]
    public class DeserializeCollectionTypesTests
    {
        private const string AssetsDirectory = "Assets\\Collections";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Deserialize_ListOfPrimitives()
        {
            var collection = new List<string> { "One", "Two" };

            DeserializeAndAssert(collection, "numbersCollection");
        }
        
        [TestMethod]
        public void Deserialize_ArrayOfPrimitives()
        {
            var collection = new[] { "One", "Two" };

            DeserializeAndAssert(collection, "numbersCollection");
        }
        
        [TestMethod]
        public void Deserialize_DictionaryOfPrimitives()
        {
            var collection = new Dictionary<int, string> { {1, "One"}, {2,"Two"} };

            DeserializeAndAssert(collection, "numbersDictionary");
        }

        [TestMethod]
        public void Deserialize_ListOfComplexes()
        {
            var players = new List<Player>
                {
                    Players.Xavi,
                    Players.Iniesta,
                };

            DeserializeAndAssert(players, "playersCollection");
        }

        [TestMethod]
        public void Deserialize_ArrayOfComplexes()
        {
            var collection = new[] { Players.Xavi, Players.Iniesta };

            DeserializeAndAssert(collection, "playersCollection");
        }

        [TestMethod]
        public void Deserialize_DictionaryOfComplexes()
        {
            var players = new Dictionary<string, Player>
                {
                    { "Xavi", Players.Xavi }, 
                    { "Iniesta", Players.Iniesta }
                };

            DeserializeAndAssert(players, "playersDictionary");
        }


        [TestMethod]
        public void Deserialize_ListOfCollections()
        {
            var collection = new List<IEnumerable<string>>
                {
                    new List<string> { "One", "Two" },
                    new List<string> { "Three", "Four" }
                };

            DeserializeAndAssert(collection, "numbersCollectionsCollection");
        }

        [TestMethod]
        public void Deserialize_ArrayOfCollections()
        {
            var collection = new IEnumerable<string>[]
                {
                    new List<string> { "One", "Two" },
                    new List<string> { "Three", "Four" }
                };

            DeserializeAndAssert(collection, "numbersCollectionsCollection");
        }

        [TestMethod]
        public void Deserialize_DictionaryOfCollections()
        {
            var collection = new Dictionary<int, IEnumerable<string>>
                {
                    { 1, new List<string> { "One", "Two" } }, 
                    { 2, new List<string> { "Three", "Four" } }
                };

            DeserializeAndAssert(collection, "numbersCollectionsDictionary");
        }

        private void DeserializeAndAssert(object expected, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName.ToLowerInvariant() + ".xml");

            serializer.DeserializeAndAssertObject(expected, path);
        }
    }
}