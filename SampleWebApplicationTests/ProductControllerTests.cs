using Microsoft.AspNetCore.Mvc;
using SampleWebApplication.Controllers;
using SampleWebApplication.Models;
using SampleWebApplication.Services.Product;
using SampleWebApplicationTests.MockServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SampleWebApplicationTests
{
    public class ProductControllerTests
    {

        private readonly ProductController _productController;
        private readonly IProductService _productService;

        public ProductControllerTests()
        {
            _productService = new ProductMockService();
            _productController = new ProductController(_productService);
        }

        #region Public Tests

        [Fact]
        [Trait("Category", "Public")]
        public async Task Get_NegativeId_Test()
        {
            var id = -12;
            var res = await _productController.Get(id);

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact]
        [Trait("Category", "Public")]
        public async Task Get_NotFound_Test()
        {
            var id = 92;
            var res = await _productController.Get(id);

            Assert.IsType<NotFoundResult>(res.Result);
        }

        [Fact]
        [Trait("Category", "Public")]
        public async Task Add_InvalidName_ReturnsBadRequest()
        {
            var nameMissingItem = new AddProductItemModel()
            {
                Manufacturer = "Victory Knox",
                Price = 152.00M
            };

            _productController.ModelState.AddModelError("Name", "Required");

            var badResponse = await _productController.Post(nameMissingItem);

            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        #endregion Public Tests

        #region Private Tests

        [Fact]
        [Trait("Category", "Private")]
        public async Task Add_NegativePrice_ReturnsBadRequest()
        {
            var nameMissingItem = new AddProductItemModel()
            {
                Name = "Swiss Knife",
                Manufacturer = "Victory Knox",
                Price = -152.00M
            };

            _productController.ModelState.AddModelError("Price", "Range");

            var badResponse = await _productController.Post(nameMissingItem);

            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }

        [Fact]
        [Trait("Category", "Private")]
        public async Task Update_NotFound_Invalid_Test()
        {
            var updateItem = new ProductItemModel()
            {
                Id = 99,
                Name = "Auro Pure",
                Price = 1200M,
                Manufacturer = null
            };

            var res = await _productController.Put(updateItem);

            Assert.IsType<NotFoundResult>(res.Result);
        }

        [Fact]
        [Trait("Category", "Private")]
        public async Task Update_Invalid_Test()
        {
            var updateItem = new ProductItemModel()
            {
                Id = 1,
                Name = null,
                Price = 1200M,
                Manufacturer = null
            };

            _productController.ModelState.AddModelError("Name", "Required");

            var res = await _productController.Put(updateItem);

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact]
        [Trait("Category", "Private")]
        public async Task Remove_NotFound_Invalid_Test()
        {
            var res = await _productController.Delete(112);

            Assert.IsType<NotFoundResult>(res);
        }

        [Fact]
        [Trait("Category", "Private")]
        public async Task Remove_NegatieId_Invalid_Test()
        {
            var res = await _productController.Delete(-112);

            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        [Trait("Category", "Private")]
        public async Task Remove_Test()
        {
            var item = await _productService.GetById(1);
            var existingItem = (ProductItemModel)item.Clone();

            var res = await _productController.Delete(1);
            Assert.IsType<OkResult>(res);

            var res1 = await _productController.Get(1);
            Assert.IsType<NotFoundResult>(res1.Result);
        }

        #endregion Private Tests

        #region Public Tests (Will allways pass, so disable them?)

        //[Fact]
        //[Trait("Category", "Public")]
        //public async Task GetTest()
        //{
        //    var id = 2;
        //    var res = await _productController.Get(id);
        //    var item = (res.Result as ObjectResult).Value as ProductItemModel;

        //    Assert.IsType<OkObjectResult>(res.Result);

        //    var allItems = await _productService.GetAllItems();
        //    var expectedItem = allItems.ToList().First(i => i.Id == id);
        //    Assert.Equal(expectedItem.Name, item.Name);
        //}

        //[Fact]
        //[Trait("Category", "Public")]
        //public async Task GetAllTest()
        //{
        //    var result = await _productController.GetAll();
        //    var allItems = (result.Result as ObjectResult).Value as List<ProductItemModel>;

        //    Assert.NotNull(allItems);
        //    Assert.Equal(5, allItems.Count());
        //}

        //[Fact]
        //[Trait("Category", "Public")]
        //public async Task AddTest()
        //{
        //    var newItem = new AddProductItemModel()
        //    {
        //        Name = "Chocolate Cookies",
        //        Price = 4.99M,
        //        Manufacturer = "Bakers Bite"
        //    };

        //    var res = await _productController.Post(newItem);
        //    var addedItem = (res.Result as ObjectResult).Value as ProductItemModel;

        //    Assert.IsType<CreatedAtActionResult>(res.Result);
        //    Assert.NotNull(addedItem);
        //    Assert.Equal(newItem.Name, addedItem.Name);
        //    Assert.Equal(newItem.Price, addedItem.Price);
        //    Assert.Equal(newItem.Manufacturer, addedItem.Manufacturer);
        //}

        //[Fact]
        //[Trait("Category", "Public")]
        //public async Task UpdateTest()
        //{
        //    var item = await _productService.GetById(1);
        //    var existingItem = (ProductItemModel)item.Clone();

        //    var res = await _productController.Put(new ProductItemModel()
        //    {
        //        Id = 1,
        //        Name = "Training Sneakers XLR",
        //        Price = 600M,
        //        Manufacturer = null // you cannot update manufacturer
        //    });

        //    var updatedItem = (res.Result as ObjectResult).Value as ProductItemModel;

        //    Assert.IsType<CreatedAtActionResult>(res.Result);
        //    Assert.NotNull(updatedItem);
        //    Assert.NotEqual(existingItem.Name, updatedItem.Name);
        //    Assert.NotEqual(existingItem.Price, updatedItem.Price);

        //    // manufacturer cannot be updated, so it should be the same
        //    Assert.Equal(existingItem.Manufacturer, updatedItem.Manufacturer);
        //}

        #endregion

    }
}
