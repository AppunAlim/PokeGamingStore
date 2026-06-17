using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokeGamingStore;
using System;

namespace PokeGamingStore.Tests
{
    [TestClass]
    public class StockManagerTests
    {
        [TestMethod]
        public void ReduceStock_ValidQuantity_StockReduced()
        {
            StockManager manager = new StockManager();
            Item item = new Item { Id = "I001" };
            manager.AddCatalogItem(item, 10);
            manager.ReduceStock("I001", 3);
            Assert.AreEqual(7, manager.GetStock("I001"));
        }
    }
}