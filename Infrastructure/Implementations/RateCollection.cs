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
    public class RateCollection : IRateCollection
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public RateCollection(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }

        public async Task<Rates> GetRateByIdAsync(int id)
        {
            var parameters = new DynamicParameters(new { IdRate = id });
            var sql = "SELECT id, tax, rate FROM Rate WHERE id = @IdRate;";

            Rates rate = await _dbConnection.QuerySingleOrDefaultAsync<Rates>(sql, parameters, _dbTransaction);
            return rate;
        }

        public async Task<List<Rates>> GetRatesByProductCategory(int idProductCategory)
        {
            var parameters = new DynamicParameters(new { IdProductCategory = idProductCategory });
            var sql = "SELECT id, id_product_category, tax, rate FROM Rate WHERE id_product_category = @IdProductCategory;";

            var rates = await _dbConnection.QueryAsync<Rates>(sql, parameters, _dbTransaction);
            return rates.ToList();
        }

        public async Task<List<Rates>> GetRatesForAllProductCategories()
        {            
            var sql = "SELECT id, id_product_category, tax, rate FROM Rate WHERE id_product_category IS null;";

            var rates = await _dbConnection.QueryAsync<Rates>(sql, transaction: _dbTransaction);
            return rates.ToList();
        }
    }
}
