using AutoMapper;
using Northwind.Data.Context;
using Northwind.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Mapper
{
    public static class MapperFactory
    {
        private static MapperConfiguration mapperConfiguration;
        private static bool _isInitialized;
        private static object lck = new object();

        public static K Map<T, K>(T input)
        {
            Init();

            IMapper mapper = mapperConfiguration.CreateMapper();

            return input != null ? mapper.Map<T, K>(input) : default(K);
        }

        private static void Init()
        {
            lock (lck)
            {
                if (_isInitialized) return;

                mapperConfiguration = new MapperConfiguration(p =>
                {
                    p.CreateMap<Product, ProductModel>().MaxDepth(1).ReverseMap();
                });
            }
            _isInitialized = true;
        }
    }
}