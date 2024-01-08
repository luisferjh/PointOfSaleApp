using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkAdapter _unitOfWorkAdapter;
       
        public UserService(IUnitOfWorkAdapter unitOfWorkAdapter)
        {
            _unitOfWorkAdapter = unitOfWorkAdapter;          
        }

        public async Task<UserDTO> GetUserByIdentification(string id)
        {
            User user;
            using (var context = _unitOfWorkAdapter.Create())
            {               
                user = await context.UnitOfWorkRepositories.UserCollection.GetUserByIdentificationAsync(id);         
            }

            return user.Adapt<UserDTO>();
        }


        //public async Task PostUser(UserRegisterDTO userRegisterDTO)
        //{
        //    try
        //    {
        //        using (var context = _unitOfWorkAdapter.Create())
        //        {
        //            User user = userRegisterDTO.Adapt<User>();
        //            user.State = true;
        //            context.BeginTransaction();
                    
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        await _unitOfWork.Rollback();
        //    }

        //}


    }
}
