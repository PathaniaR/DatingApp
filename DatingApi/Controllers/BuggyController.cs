using DatingApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApi.Controllers
{
    public class BuggyController:BaseApiController
    {
        private readonly DatingDataContext  context;
        public BuggyController(DatingDataContext _context){
            this.context=_context;
        }
  
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
           return "Secret Text";
        }

        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound()
        {
           var thing =  context.AppUsers.Find(-2);
           if(thing == null)
           {
               return NotFound();
           }
           return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
           var thing= context.AppUsers.Find(-3);
           var thingToReturn=thing.ToString();

           return thingToReturn;
        }


        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
           return BadRequest("This was not a bad request");
        }
    }
}