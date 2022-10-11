using SampleWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApplication.Services.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductItemModel>> GetAllItems();
        Task<ProductItemModel> Add(AddProductItemModel newItem);
        Task<ProductItemModel> Update(int id, string name, decimal price);
        Task<ProductItemModel> GetById(int id);
        Task Remove(int id);
    }
}
