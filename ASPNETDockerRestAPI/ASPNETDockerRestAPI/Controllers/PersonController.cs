using Asp.Versioning;
using ASPNETDockerRestAPI.Models;
using ASPNETDockerRestAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETDockerRestAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController(
        ILogger<PersonController> logger,
        IPersonService personService
        ) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(personService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var person = personService.FindById(id);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person is null)
            {
                return BadRequest();
            }

            return Ok(personService.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person is null)
            {
                return BadRequest();
            }

            return Ok(personService.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var person = personService.FindById(id);

            if (person is null)
            {
                return NotFound();
            }

            personService.Delete(id);

            return NoContent();
        }
    }
}
