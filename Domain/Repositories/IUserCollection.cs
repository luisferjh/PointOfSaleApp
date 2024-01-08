using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserCollection
    {
        Task<int> InsertUser(User user);        
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdentificationAsync(string identification);
    }
}
