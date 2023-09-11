using API.Dto;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly StudyBuddyDbContext _context;
        public UserController(StudyBuddyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IQueryable<UserDto> GetUsers()
        {
            var users = _context.Users.Where(x => x.UserName != null).Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            });
            if (users != null)
            {
                return users;
            }
            else return null;
        }
        [HttpGet]
        [Route("{id}")]
        public UserDto GetUserById([FromRoute]  int id) 
        {
           var user =  _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
                return new UserDto(user.Id, user.UserName, user.Email);
            else return null;

        }
    }
    
}
