using Asp.Versioning;
using ASPNETDockerRestAPI.Business;
using Microsoft.AspNetCore.Mvc;
using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonsController(
        ILogger<PersonsController> logger,
        IPersonsBusiness personsBusiness
        ) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(personsBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var person = personsBusiness.FindById(id);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonDto person)
        {
            if (person is null)
            {
                return BadRequest();
            }

            return Ok(personsBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] PersonDto person)
        {
            if (person is null)
            {
                return BadRequest();
            }

            return Ok(personsBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var person = personsBusiness.FindById(id);

            if (person is null)
            {
                return NotFound();
            }

            personsBusiness.Delete(id);

            return NoContent();
        }
    }
}
