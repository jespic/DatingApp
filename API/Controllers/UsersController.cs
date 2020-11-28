using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController] //this class is a controller type, inside of the mvc
    [Route("api/[controller]")] //to get info of this controller, the client needs to search api/users
    public class UsersController : ControllerBase //the controller have to derive from ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context) //getting data from db
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() //the function is going to return something of type ActionResult, which is IEnumerable<AppUser> type
        { 
            return await _context.Users.ToListAsync();    
        }

        // api/users/3
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) 
        { 
            return await _context.Users.FindAsync(id);    
        }
    }
}