using Asp.Versioning;
using ASPNETDockerRestAPI.Business;
using Microsoft.AspNetCore.Mvc;
using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/persons")]
    public class PersonController(
        ILogger<PersonController> logger,
        IPersonBusiness personBusiness
        ) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var person = personBusiness.FindById(id);

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

            return Ok(personBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] PersonDto person)
        {
            if (person is null)
            {
                return BadRequest();
            }

            return Ok(personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var person = personBusiness.FindById(id);

            if (person is null)
            {
                return NotFound();
            }

            personBusiness.Delete(id);

            return NoContent();
        }
    }
}