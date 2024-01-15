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
    [Route("api/v{version:apiVersion}/books")]
    public class BookController(
        ILogger<BookController> logger,
        IBookBusiness bookBusiness
        ) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult GetAll()
        {
            return Ok(bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult GetById(long id)
        {
            var book = bookBusiness.FindById(id);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Post([FromBody] BookDto book)
        {
            if (book is null)
            {
                return BadRequest();
            }

            return Ok(bookBusiness.Create(book));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Put([FromBody] BookDto book)
        {
            if (book is null)
            {
                return BadRequest();
            }

            return Ok(bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            var book = bookBusiness.FindById(id);

            if (book is null)
            {
                return NotFound();
            }

            bookBusiness.Delete(id);

            return NoContent();
        }
    }
}
