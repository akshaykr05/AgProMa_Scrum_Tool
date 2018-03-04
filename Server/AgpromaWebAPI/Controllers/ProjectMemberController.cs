using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgProMa.Services;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;

namespace AgProMa.Controllers
{
    [Produces("application/json")]
    public class ProjectmembersController : Controller
    {
        private IProjectmemberservice _context;
        public ProjectmembersController(IProjectmemberservice context)
        {

            _context = context;
        }
        // GET api/values
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            return Ok("oidsjicd");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public void PostMemberDetails([FromBody]Projectmembers user)
        {
            _context.Add_MemberDetails(user);
        }

       
    }
}
