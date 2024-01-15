using Asp.Versioning;
using ASPNETDockerRestAPI.Business;
using Microsoft.AspNetCore.Mvc;
using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Hypermedia.Filters;

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
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult GetAll()
        {
            return Ok(personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HypermediaFilter))]
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
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Post([FromBody] PersonDto person)
        {
            if (person is null)
            {
                return BadRequest();
            }

            return Ok(personBusiness.Create(person));
        }

        [HttpPut]
        [TypeFilter(typeof(HypermediaFilter))]
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
