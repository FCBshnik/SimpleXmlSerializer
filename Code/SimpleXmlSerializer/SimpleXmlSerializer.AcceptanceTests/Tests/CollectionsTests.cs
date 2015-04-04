using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleXmlSerializer.AcceptanceTests.Dto;

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
        public void ListOfNulls()
        {
            var collection = new List<string> { null, null };

            ActAndAssert(collection, "collectionOfNulls");
        }

        [TestMethod]
        public void ListOfEmpties()
        {
            var collection = new List<string> { string.Empty, string.Empty };

            ActAndAssert(collection, "collectionOfEmpties");
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
            var collection = new Dictionary<int, string> { { 1, "One" }, { 2, "Two" } };

            ActAndAssert(collection, "dictionaryOfPrimitives");
        }

        [TestMethod]
        public void ListOfComposites()
        {
            var collection = new List<CompositeWithPrimitives> { CompositeWithPrimitives.One, CompositeWithPrimitives.Two };

            ActAndAssert(collection, "collectionOfComposites");
        }

        [TestMethod]
        public void ArrayOfComposites()
        {
            var collection = new[] { CompositeWithPrimitives.One, CompositeWithPrimitives.Two };

            ActAndAssert(collection, "collectionOfComposites");
        }

        [TestMethod]
        public void DictionaryOfComposites()
        {
            var dictionary = new Dictionary<string, CompositeWithPrimitives>
                {
                    { "One", CompositeWithPrimitives.One }, 
                    { "Two", CompositeWithPrimitives.Two }
                };

            ActAndAssert(dictionary, "dictionaryOfComposites");
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
        public void ListOfEmptyCollections()
        {
            var collection = new List<IEnumerable<string>>
                {
                    new List<string>(),
                    new List<string>()
                };

            ActAndAssert(collection, "collectionOfEmptyCollections");
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