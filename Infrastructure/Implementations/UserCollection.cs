using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helpers;
using Dapper;
using System.Data;

namespace Infrastructure.Implementations
{
    public class UserCollection : IUserCollection
    {
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public UserCollection(
            IMapper mapper, 
            IDbConnection dbConnection, 
            IDbTransaction dbTransaction)
        {
            _mapper = mapper;
            _dbConnection = dbConnection;
            _dbTransaction = dbTransaction;
        }
       

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdentificationAsync(string identification)
        {          
            try
            {             
                var parameters = new DynamicParameters(new {Identification = identification });                             
                var sql = "SELECT id, name, last_name, email, phone, identification FROM Users WHERE identification = @Identification;";
               
                User user = await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, parameters, _dbTransaction);               
                
                if (user is null)
                {
                    return null;
                }
              
                return user;              
            }
            catch (Exception ex)
            {
                throw;
            }            
        }


        public async Task<int> InsertUser(User user)
        {
            try
            {
                var sql = "INSERT INTO Users (name,last_name,identification,email,phone) " +
                     "OUTPUT INSERTED.Id " +
                    "VALUES (@Name, @LastName, @Identification, @Email, @Phone)";                           

                int result = await _dbConnection.QuerySingleAsync<int>(sql, user, _dbTransaction);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
