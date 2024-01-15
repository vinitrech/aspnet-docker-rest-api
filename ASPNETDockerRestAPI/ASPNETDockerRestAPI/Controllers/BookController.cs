using Asp.Versioning;
using ASPNETDockerRestAPI.Business;
using Microsoft.AspNetCore.Mvc;
using ASPNETDockerRestAPI.Dtos;
using ASPNETDockerRestAPI.Hypermedia.Filters;

namespace ASPNETDockerRestAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/books")]
    public class BookController(
        ILogger<BookController> logger,
        IBookBusiness bookBusiness
        ) : ControllerBase
    {
        [HttpGet]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult GetAll()
        {
            return Ok(bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
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
