using FindEmployeeAPI.DAL;
using FindEmployeeAPI.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindEmployeeAPI.DAL
{
    public class UserInfo : IUserInfo
    {
        private readonly DemoUserInfoDBContext _context;
        public UserInfo(DemoUserInfoDBContext context)
        {
            _context = context;
        }
        public async Task<List<UserProfile>> GetALLUsers()
        {
            return await _context.UserProfiles.ToListAsync();
        }
        public async Task<UserProfile> GetUserById(int Id)
        {
           return await _context.UserProfiles.FindAsync(Id);
        }
        public async Task SaveUserInfo(UserProfile user)
        {
            _context.UserProfiles.Add(user);
           await  _context.SaveChangesAsync();
        }
        public async Task UpdateUserInfo(UserProfile user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();            
        }
        public async Task DeleteUserInfo(int Id)
        {
            var user =  _context.UserProfiles.Find(Id);         
            if(user!=null)
            {
                _context.UserProfiles.Remove(user);
                await _context.SaveChangesAsync();
            }            
        }
    }
}
