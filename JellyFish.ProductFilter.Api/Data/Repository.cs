using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace ProductFilter.Api.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Repository(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;

            string filePath = _hostingEnvironment.ContentRootPath + _configuration["DataDirectory"];
            dynamic serializedProducts = JsonConvert.DeserializeObject(File.ReadAllText(filePath));

            List<T> products = serializedProducts.Products.ToObject<List<T>>();           
            GetAll = products.AsQueryable();          
        }

        public IQueryable<T> GetAll { get; }

        public IQueryable<T> Get(Expression<Func<T, bool>> queryExpression)
        {
            return GetAll.Where(queryExpression);
        }
    }
}
