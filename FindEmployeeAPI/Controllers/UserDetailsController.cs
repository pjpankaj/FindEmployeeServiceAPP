using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindEmployeeAPI.DAL;
using FindEmployeeAPI.Models;
using FindEmployeeAPI.DAL.Interface;

namespace FindEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserInfo _context;
        private readonly DemoUserInfoDBContext _demouser;       
        public UserDetailsController(IUserInfo context, DemoUserInfoDBContext demouser)
        {
            _context = context;
            _demouser = demouser;
        }

        // GET: api/UserDetails
        [HttpGet]
        public  ActionResult<IEnumerable<UserInfoViewModel>> GetUserProfiles()
        {
            List<UserInfoViewModel> UserList = new List<UserInfoViewModel>();
            var ListOfUser=  _context.GetALLUsers().Result;
            foreach (var item in ListOfUser)
            {
                var user = new UserInfoViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    EmailId = item.EmailId,
                    Experience = item.Experience,
                    Qualification = _demouser.Qualifications.Find(item.QualificationId).Qualif,
                    CurrentCompany = item.CurrentCompany,
                    PreferredLocation = item.PreferredLocation,
                    ImageName=item.ImageName                    
                };
                UserList.Add(user);
            }
            return UserList;
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoViewModel>> GetUserProfile(int id)
        {
            var userProfile = await _context.GetUserById(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            var user = new UserInfoViewModel()
            {
                Id = userProfile.Id,
                Name = userProfile.Name,
                EmailId = userProfile.EmailId,
                Experience = userProfile.Experience,
                Qualification = _demouser.Qualifications.Find(userProfile.QualificationId).Qualif,
                CurrentCompany = userProfile.CurrentCompany,
                PreferredLocation = userProfile.PreferredLocation,
                ImageName = userProfile.ImageName
            };
            return user;
        }

        // PUT: api/UserDetails/5       
        [HttpPut("{id}")]
        public IActionResult PutUserProfile(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest();
            }            
                 _context.UpdateUserInfo(userProfile);            
            return NoContent();
        }

        // POST: api/UserDetails
        [HttpPost]
        public ActionResult<UserProfile> PostUserProfile(UserProfile userProfile)
        {            
             _context.SaveUserInfo(userProfile);
            return CreatedAtAction("GetUserProfile", new { id = userProfile.Id }, userProfile);
        }

        // DELETE: api/UserProfiles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUserProfile(int id)
        {
             _context.DeleteUserInfo(id);            
            return NoContent();
        }
    }
}
