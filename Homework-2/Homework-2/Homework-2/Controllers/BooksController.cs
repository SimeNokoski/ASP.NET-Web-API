using Homework_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBook()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        [HttpGet("queryString")]
        public IActionResult GetBook([FromQuery] int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Index can not be negative");
                }
                if (index >= StaticDb.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Index {index} does not exist");
                }
                return Ok(StaticDb.Books[index]);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        [HttpGet("filter")]
        public IActionResult FilterBookFromQuery([FromQuery] string author,[FromQuery] string title)
        {
            if(string.IsNullOrEmpty(author) || string.IsNullOrEmpty(title))
            {
                return BadRequest("Bad request");
            }
            List<Book> filterBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())
                                                          && x.Title.ToLower().Contains(title.ToLower())).ToList();
            return Ok(filterBooks);
        }

        [HttpPost("postBook")]
        public IActionResult Post([FromBody] Book book)
        {
            try
            {
                if(string.IsNullOrEmpty(book.Author) || string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest();
                }
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created,"Book was added");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }
        [HttpPost("listOfTitles")]
        public IActionResult BooksTitle([FromBody] List<Book> books)
        {
            try
            {
                List<string> titles = books.Select(x => x.Title).ToList();
                return Ok(titles);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }
    }
}
