using Northwind.Data.Context;
using Northwind.Data.UnitOfWork;
using Northwind.Models.Models;
using Northwind.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Service
{
    public interface IProductService
    {
        Product Add(ProductModel dto);
    }
    public class ProductService : IProductService
    {
        public Product Add(ProductModel dto)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var entity = MapperFactory.Map<ProductModel, Product>(dto);
                uow.ProductRepository.Add(entity);
                uow.SaveChanges();
                return entity;
            }
        }
    }
}
