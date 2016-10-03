using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SecuritiesService.Controllers
{
    [Route("[controller]")]
    public class SecurityController : Controller
    {
        [HttpGet]
        public string Get(int securityId, string property)
        {
            return "Getting Property " + property + " of security ID " + securityId;
        }

        [HttpGet("{id}")]
        public string Get(int securityId)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
