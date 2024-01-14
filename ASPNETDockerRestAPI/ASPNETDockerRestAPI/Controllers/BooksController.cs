using Asp.Versioning;
using ASPNETDockerRestAPI.Business;
using Microsoft.AspNetCore.Mvc;
using ASPNETDockerRestAPI.Dtos;

namespace ASPNETDockerRestAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController(
        ILogger<BooksController> logger,
        IBooksBusiness booksBusiness
        ) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(booksBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var book = booksBusiness.FindById(id);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookDto book)
        {
            if (book is null)
            {
                return BadRequest();
            }

            return Ok(booksBusiness.Create(book));
        }

        [HttpPut]
        public IActionResult Put([FromBody] BookDto book)
        {
            if (book is null)
            {
                return BadRequest();
            }

            return Ok(booksBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var book = booksBusiness.FindById(id);

            if (book is null)
            {
                return NotFound();
            }

            booksBusiness.Delete(id);

            return NoContent();
        }
    }
}
