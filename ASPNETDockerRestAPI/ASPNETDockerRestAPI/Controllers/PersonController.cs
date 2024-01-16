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
        IPersonBusiness personBusiness
        ) : ControllerBase
    {
        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<PersonDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult GetAll([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return Ok(personBusiness.FindAllPaged(name, sortDirection, pageSize, page));
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

        [HttpGet("findPersonsByName")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult GetByNames([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var persons = personBusiness.FindByName(firstName, lastName);

            if (persons.Count == 0)
            {
                return NotFound();
            }

            return Ok(persons);
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

        [HttpPatch("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Patch(long id)
        {
            return Ok(personBusiness.Disable(id));
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
