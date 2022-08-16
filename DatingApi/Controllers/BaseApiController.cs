using DatingApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DatingApi.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController:ControllerBase
    {       
    }
}