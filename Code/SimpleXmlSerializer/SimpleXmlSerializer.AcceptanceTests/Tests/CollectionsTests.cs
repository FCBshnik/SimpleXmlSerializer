using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto.Football;

namespace SimpleXmlSerializer.AcceptanceTests.Tests
{
    [TestClass]
    public class CollectionsTests : TestsBase
    {
        [TestMethod]
        public void ListOfPrimitives()
        {
            var collection = new List<string> { "One", "Two" };

            ActAndAssert(collection, "collectionOfPrimitives");
        }
        
        [TestMethod]
        public void ArrayOfPrimitives()
        {
            var collection = new[] { "One", "Two" };

            ActAndAssert(collection, "collectionOfPrimitives");
        }
        
        [TestMethod]
        public void DictionaryOfPrimitives()
        {
            var collection = new Dictionary<int, string> { {1, "One"}, {2, "Two"} };

            ActAndAssert(collection, "dictionaryOfPrimitives");
        }

        [TestMethod]
        public void ListOfComplexes()
        {
            var players = new List<Player> { Players.Xavi, Players.Iniesta, };

            ActAndAssert(players, "collectionOfComplexes");
        }

        [TestMethod]
        public void ArrayOfComplexes()
        {
            var players = new[] { Players.Xavi, Players.Iniesta };

            ActAndAssert(players, "collectionOfComplexes");
        }

        [TestMethod]
        public void DictionaryOfComplexes()
        {
            var players = new Dictionary<string, Player>
                {
                    { "Xavi", Players.Xavi }, 
                    { "Iniesta", Players.Iniesta }
                };

            ActAndAssert(players, "dictionaryOfComplexes");
        }

        [TestMethod]
        public void ListOfCollections()
        {
            var collection = new List<IEnumerable<string>>
                {
                    new List<string> { "One", "Two" },
                    new List<string> { "Three", "Four" }
                };

            ActAndAssert(collection, "collectionOfCollections");
        }

        [TestMethod]
        public void ArrayOfCollections()
        {
            var collection = new IEnumerable<string>[]
                {
                    new List<string> { "One", "Two" },
                    new List<string> { "Three", "Four" }
                };

            ActAndAssert(collection, "collectionOfCollections");
        }

        [TestMethod]
        public void DictionaryOfCollections()
        {
            var collection = new Dictionary<int, IEnumerable<string>>
                {
                    { 1, new List<string> { "One", "Two" } }, 
                    { 2, new List<string> { "Three", "Four" } }
                };

            ActAndAssert(collection, "dictionaryOfCollections");
        }

        [TestMethod]
        public void CollectionOfDictionaries()
        {
            var collection = new []
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

            ActAndAssert(collection, "collectionOfDictionaries");
        }

        [TestMethod]
        public void DictionaryOfDictionaries()
        {
            var dictionary = new Dictionary<string, Dictionary<int, string>>
                {
                    { "first", new Dictionary<int, string> { { 1, "One" },  { 2, "Two" } } },
                    { "second", new Dictionary<int, string> { { 3, "Three" },  { 4, "Four" } } },
                };

            ActAndAssert(dictionary, "dictionaryOfDictionaries");
        }
    }
}