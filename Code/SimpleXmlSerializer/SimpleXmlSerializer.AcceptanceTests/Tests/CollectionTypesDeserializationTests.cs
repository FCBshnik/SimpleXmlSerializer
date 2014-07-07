using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;
using SimpleXmlSerializer.AcceptanceTests.Utils;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class CollectionTypesDeserializationTests
    {
        private const string AssetsDirectory = "Assets\\Collections";

        private static readonly XmlSerializer serializer = new XmlSerializer();

        [TestMethod]
        public void Deserialize_ListOfPrimitives()
        {
            var collection = new List<string> { "One", "Two" };

            DeserializeAndAssert(collection, "collectionOfPrimitives");
        }
        
        [TestMethod]
        public void Deserialize_ArrayOfPrimitives()
        {
            var collection = new[] { "One", "Two" };

            DeserializeAndAssert(collection, "collectionOfPrimitives");
        }
        
        [TestMethod]
        public void Deserialize_DictionaryOfPrimitives()
        {
            var collection = new Dictionary<int, string> { {1, "One"}, {2,"Two"} };

            DeserializeAndAssert(collection, "dictionaryOfPrimitives");
        }

        [TestMethod]
        public void Deserialize_ListOfComplexes()
        {
            var players = new List<Player>
                {
                    Players.Xavi,
                    Players.Iniesta,
                };

            DeserializeAndAssert(players, "collectionOfComplexes");
        }

        [TestMethod]
        public void Deserialize_ArrayOfComplexes()
        {
            var collection = new[] { Players.Xavi, Players.Iniesta };

            DeserializeAndAssert(collection, "collectionOfComplexes");
        }

        [TestMethod]
        public void Deserialize_DictionaryOfComplexes()
        {
            var players = new Dictionary<string, Player>
                {
                    { "Xavi", Players.Xavi }, 
                    { "Iniesta", Players.Iniesta }
                };

            DeserializeAndAssert(players, "dictionaryOfComplexes");
        }


        [TestMethod]
        public void Deserialize_ListOfCollections()
        {
            var collection = new List<IEnumerable<string>>
                {
                    new List<string> { "One", "Two" },
                    new List<string> { "Three", "Four" }
                };

            DeserializeAndAssert(collection, "collectionOfCollections");
        }

        [TestMethod]
        public void Deserialize_ArrayOfCollections()
        {
            var collection = new IEnumerable<string>[]
                {
                    new List<string> { "One", "Two" },
                    new List<string> { "Three", "Four" }
                };

            DeserializeAndAssert(collection, "collectionOfCollections");
        }

        [TestMethod]
        public void Deserialize_DictionaryOfCollections()
        {
            var collection = new Dictionary<int, IEnumerable<string>>
                {
                    { 1, new List<string> { "One", "Two" } }, 
                    { 2, new List<string> { "Three", "Four" } }
                };

            DeserializeAndAssert(collection, "dictionaryOfCollections");
        }

        [TestMethod]
        public void Deserialize_CollectionOfDictionaries()
        {
            var collection = new[]
            {
                new Dictionary<int, string>
                    {
                        { 1, "One" }, 
                        { 2, "Two" }
                    },
                new Dictionary<int, string>
                    {
                        { 2, "Two" }, 
                        { 3, "Three" }
                    }
            };

            DeserializeAndAssert(collection, "collectionOfDictionaries");
        }

        [TestMethod]
        public void Deserialize_DictionaryOfDictionaries()
        {
            var dictionary = new Dictionary<string, Dictionary<int, string>>
                {
                    { "first", new Dictionary<int, string> { { 1, "One" },  { 2, "Two" } } },
                    { "second", new Dictionary<int, string> { { 3, "Three" },  { 4, "Four" } } },
                };

            DeserializeAndAssert(dictionary, "dictionaryOfDictionaries");
        }

        private void DeserializeAndAssert(object expected, string fileName)
        {
            var path = Path.Combine(AssetsDirectory, fileName.ToLowerInvariant() + ".xml");

            serializer.DeserializeAndAssertObject(expected, path);
        }
    }
}