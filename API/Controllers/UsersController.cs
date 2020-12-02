using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController //the controller inherits the attributes of BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context) //getting data from db
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous] //to access to this method, authorization is not required
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() //the function is going to return something of type ActionResult, which is IEnumerable<AppUser> type
        { 
            return await _context.Users.ToListAsync();    
        }

        // api/users/3
        [Authorize] //to protect this endpoint
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) 
        { 
            return await _context.Users.FindAsync(id);    
        }
    }
}