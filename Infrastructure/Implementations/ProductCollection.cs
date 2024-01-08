using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using MapsterMapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class ProductCollection : IProductCollection
    {

        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public ProductCollection(       
            IDbConnection dbConnection,
            IDbTransaction dbTransaction)
        {           
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }

        public Task<List<Product>> GetAllProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetStockProductAsync(int idProduct)
        {
            try
            {
                var parameters = new DynamicParameters(new { IdProduct = idProduct });
                var sql = "SELECT stock FROM Product WHERE id = @IdProduct;";

                int stock = await _dbConnection.QuerySingleOrDefaultAsync<int>(sql, parameters, _dbTransaction);
                
                return stock;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStockProductAsync(int idProduct, int stockSubstract)
        {
            var parametersProduct = new DynamicParameters(new { IdProduct = idProduct });
            var sqlProduct = "Select id, id_category, name, stock, unit_price, state FROM Product WHERE id = @IdProduct;";
            Product product = await _dbConnection.QuerySingleOrDefaultAsync<Product>(sqlProduct, parametersProduct, _dbTransaction);

            int newStock = product.Stock - stockSubstract;
            var parameters = new DynamicParameters(new { NewStock = newStock, IdProduct = idProduct });
            var sql = "Update Product SET stock = @NewStock WHERE id = @IdProduct;";

            int result = await _dbConnection.ExecuteAsync(sql, parameters, _dbTransaction);

            return result > 0 ? true : false;
        }

        public Task InsertProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

    }
}
