using Dapper;
using Dapper.Contrib.Extensions;
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
    public class InvoiceCollection : IInvoiceCollection
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public InvoiceCollection(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }
  
        public async Task<long> InsertInvoiceAsync(Invoice invoice)
        {
            try
            {
                string sql = "INSERT INTO Invoice (id_client, name_client, identification, num_invoice, date, total, state) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@IdClient, @NameClient, @Identification, @NumInvoice, @Date, @Total, @State)";
                var result = await _dbConnection.QuerySingleAsync<int>(sql, invoice, _dbTransaction);
               
                return result <= 0 ? -1: result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> InsertInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            try
            {
                string sql = "INSERT INTO InvoiceDetail (id_invoice, id_product, id_rate, amount, unit_price, imp, subtotal, state) " +                     
                    "VALUES (@IdInvoice, @IdProduct, @IdRate, @Amount, @UnitPrice, @Imp, @Subtotal, @State)";
                var result = await _dbConnection.ExecuteAsync(sql, invoiceDetail, _dbTransaction);
               
                return result <= 0 ? -1 : result;
            }
            catch (Exception ex)
            {                
                throw;
            }
        }
    }
}
