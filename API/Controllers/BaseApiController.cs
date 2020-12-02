using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] //this class is a controller type, inside of the mvc
    [Route("api/[controller]")] //to get info of this controller, the client needs to search api/users
    public class BaseApiController : ControllerBase //the controller have to derive from ControllerBase
    {
        
    }
}