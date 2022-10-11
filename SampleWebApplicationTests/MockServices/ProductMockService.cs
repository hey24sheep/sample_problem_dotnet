using SampleWebApplication.Models;
using SampleWebApplication.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApplicationTests.MockServices
{
    public class ProductMockService : IProductService
    {
        private List<ProductItemModel> _productItems;

        public ProductMockService()
        {
            _productItems = new List<ProductItemModel>()
            {
                new ProductItemModel() { Id = 1,
                    Name = "Sentry Club Fan", Manufacturer="Oak Tree Industries", Price = 79.99M },
                new ProductItemModel() { Id = 2,
                    Name = "Almond Milk", Manufacturer="Blue Roasters", Price = 4.00M },
                new ProductItemModel() { Id = 3,
                    Name = "Medium Roasted Coffee Beans", Manufacturer="Blue Roasters", Price = 12.00M },
                new ProductItemModel() { Id = 4,
                    Name = "French Press (Medium)", Manufacturer="Blue Roasters", Price = 119.99M },
                new ProductItemModel() { Id = 5,
                    Name = "Siphon Press", Manufacturer="Blue Roasters", Price = 219.99M }
            };
        }

        public async Task<ProductItemModel> Add(AddProductItemModel newItem)
        {
            var item = new ProductItemModel()
            {
                Id = _productItems.Max(i => i.Id) + 1,
                Name = newItem.Name,
                Price = newItem.Price,
                Manufacturer = newItem.Manufacturer
            };
            _productItems.Add(item);
            return item;
        }

        public async Task<IEnumerable<ProductItemModel>> GetAllItems()
        {
            return _productItems;
        }

        public async Task<ProductItemModel> GetById(int id)
        {
            var item = _productItems.Where(i => i.Id == id).FirstOrDefault();
            return item;
        }

        public async Task Remove(int id)
        {
            var existing = _productItems.First(i => i.Id == id);
            _productItems.Remove(existing);
        }

        public async Task<ProductItemModel> Update(int id, string name, decimal price)
        {
            var existing = _productItems.First(i => i.Id == id);
            existing.Name = name;
            existing.Price = price;
            return existing;
        }
    }
}
