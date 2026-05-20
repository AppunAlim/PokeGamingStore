using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokeGamingStore.Catalog;
using System;

namespace PokeGamingStore.Tests
{
    [TestClass]
    public class ProductCatalogTests
    {
        private ProductCatalog<Product> catalog;

        [TestInitialize]
        public void Setup()
        {
            catalog = ProductCatalog<Product>.Instance;
        }

        [TestMethod]
        public void AddProduct_ValidProduct_AddsSuccessfully()
        {
            var product = new Product { Id = "P001", Name = "Elden Ring", Category = "Game", Price = 600000 };

            catalog.AddProduct(product);

            Assert.AreEqual(1, catalog.GetAllProducts().Count);
        }

        [TestMethod]
        public void AddProduct_NullProduct_ThrowsException()
        {
            try
            {
            catalog.AddProduct(null);
            Assert.Fail("Test gagal: Sistem seharusnya menolak produk bernilai null.");
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void SearchProduct_ByName_ReturnsCorrectResult()
        {
            catalog.AddProduct(new Product { Id = "P001", Name = "Elden Ring", Category = "Game" });
            catalog.AddProduct(new Product { Id = "P002", Name = "PS5 Controller", Category = "Accessory" });

            var results = catalog.SearchProduct("Name", "Elden");

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Elden Ring", results[0].Name);
        }
    }
}