using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample1.Services;
using MongoExample1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoExample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestController : Controller
    {

        private readonly MongoDBService _mongoDBService;

        public UserTestController(MongoDBService mongoDBService)
        {

            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<UserTest>> Get()
        {

            return await _mongoDBService.GetAsync();

        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserTest usertest)
        {

            await _mongoDBService.CreateAsync(usertest);
            return CreatedAtAction(nameof(Get), new { id = usertest.Id }, usertest);
        }

        //update operations
        [HttpPut("{id}")]
        public async Task<IActionResult> AddToUserTest(string id, [FromBody] string uid)
        {

            await _mongoDBService.AddToUserTestAsync(id, uid);
            return NoContent();
        }






        // delete operation
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

    }
}
