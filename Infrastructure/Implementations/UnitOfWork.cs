using Domain.Repositories;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Infrastructure.Helpers;
using System.Data;
using Mapster;

namespace Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {      
        private readonly IUnitOfWorkRepositories _unitOfWorkRepositories;
        private readonly IDbTransaction _dbTransaction;
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public UnitOfWork(
            IConfiguration configuration,
            IUnitOfWorkRepositories unitOfWorkRepositories,
            IDbTransaction dbTransaction)
        {
            _configuration = configuration;
            _unitOfWorkRepositories = unitOfWorkRepositories;
            _dbTransaction = dbTransaction;                    
        }

        public IUnitOfWorkRepositories UnitOfWorkRepositories { get => _unitOfWorkRepositories; }   

        public void Dispose()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Dispose();
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
      
        public void Save()
        {
            try
            {
                _dbTransaction.Commit();                                
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
        }

       
    }
}
