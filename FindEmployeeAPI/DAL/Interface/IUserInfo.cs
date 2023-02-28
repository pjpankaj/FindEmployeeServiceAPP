using FindEmployeeAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindEmployeeAPI.DAL.Interface
{
   public interface IUserInfo
    {
        public  Task<List<UserProfile>> GetALLUsers();
        public  Task<UserProfile> GetUserById(int Id);
        public Task SaveUserInfo(UserProfile user);
        public Task UpdateUserInfo(UserProfile user);
        public Task DeleteUserInfo(int Id);       

    }
}
