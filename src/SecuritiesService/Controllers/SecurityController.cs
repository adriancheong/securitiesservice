﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecuritiesService.Helpers;
using StackExchange.Redis;

namespace SecuritiesService.Controllers
{
    [Route("[controller]")]
    public class SecurityController : Controller
    {
        [HttpGet]
        public object Get(string securityId, string property)
        {
            Console.WriteLine("Securities Service is called to retrieve Property: {0} of Security ID: {1}", property, securityId);
            IDatabase redisdb = RedisFactory.GetRedisDatabase();
            var ret = redisdb.HashGet(securityId, property);
            return ret.ToString();
        }

        [HttpGet("{securityId}")]
        public object Get(string securityId)
        {
            Console.WriteLine("Securities Service is called to retrieve Security ID: " + securityId);
            try
            {
                IDatabase redisdb = RedisFactory.GetRedisDatabase();
                var ret = redisdb.HashGetAll(securityId).ToStringDictionary();
                Console.WriteLine("Retrieved value: " + ret);
                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception encountered: " + e.Message);
                return null;
            }
            
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
