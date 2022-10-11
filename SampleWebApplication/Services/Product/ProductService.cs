using SampleWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Services.Product
{
    public class ProductService : IProductService
    {
        public Task<ProductItemModel> Add(AddProductItemModel newItem)
        {
            throw new NotImplementedException();
        }

        public Task<ProductItemModel> Update(int id, string name, decimal price)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductItemModel>> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Task<ProductItemModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
