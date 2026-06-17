using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokeGamingStore;
using System;

namespace PokeGamingStore.Tests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void AddToCart_OverCapacity_ThrowsException()
        {
            StockManager manager = new StockManager();
            Cart cart = new Cart(manager, 5);
            Item item = new Item { Id = "I001" };
            manager.AddCatalogItem(item, 10);
            try
            {
                cart.AddToCart(item, 6);
                Assert.Fail("Sistem seharusnya menolak penambahan melebihi kapasitas.");
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}