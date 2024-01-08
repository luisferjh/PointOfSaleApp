using Domain.Repositories;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Helpers
{
    public static class Extensions
    {
        private static SqlConnection _sqlConnection;     
        private static SqlTransaction _transaction;     
        public static void SetSqlConnection<T>(
            this T obj, 
            SqlConnection connection,           
            string who) where T : IUnitOfWork
        {
            _sqlConnection = connection;                   
        }

        public static SqlConnection GetSqlConnection<T>(this T obj) where T : ISqlConnection
        {
            return _sqlConnection;
        }
        
        public static void SetSqlTransaction<T>(this T obj, SqlTransaction transaction) where T : IDbTransaction
        {
            _transaction = transaction;
        }
        
        public static SqlTransaction GetSqlTransaction<T>(this T obj) where T : ISqlConnection
        {
            return _transaction;
        }
      
    }
}
