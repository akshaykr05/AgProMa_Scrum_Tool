using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgpromaWebAPI.Service;
using AgpromaWebAPI.model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgpromaWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EfficiencyController : Controller
    {
        public IEfficiencyService _service;
        public EfficiencyController(IEfficiencyService service)
        {
            _service = service;
        }

        // GET api/values/5
        [HttpGet("{UserId}")]
        //get efficiency for a user.
        public float Get(int userId)
        {
           return _service.GetEfficiencyForUser(userId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        //update the checklist item for a task
        public void Put(int id, [FromBody]ChecklistBacklog checklist)
        {
            _service.Update(id, checklist);
        }
    }
}
