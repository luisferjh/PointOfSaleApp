using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class ProductCategoryCollection : IProductCategoryCollection
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public ProductCategoryCollection(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }

        public Task<List<ProductCategory>> GetAllProductCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
        {
            var parameters = new DynamicParameters(new { IdProductCategory = id });
            var sql = "SELECT id, name, id_rate FROM ProductCategory WHERE id = @IdProductCategory;";

            ProductCategory productCategory = await _dbConnection.QuerySingleOrDefaultAsync<ProductCategory>(sql, parameters, _dbTransaction);
            return productCategory;
        }
    }
}
