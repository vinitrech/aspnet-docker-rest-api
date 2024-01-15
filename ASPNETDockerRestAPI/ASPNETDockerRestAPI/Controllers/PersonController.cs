using Asp.Versioning;
using ASPNETDockerRestAPI.Business;
using Microsoft.AspNetCore.Mvc;
using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;

namespace ASPNETDockerRestAPI.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/persons")]
    public class PersonController(
        ILogger<PersonController> logger,
        IPersonBusiness personBusiness
        ) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PersonDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult GetAll()
        {
            return Ok(personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
