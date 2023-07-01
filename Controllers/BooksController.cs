using LibraryWebAPI.Models;
using LibraryWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookStackRepository _bookStackRepository;

        public BooksController(IBookStackRepository bookStackRepository)
        {
            _bookStackRepository = bookStackRepository;
        }

        [HttpGet("books_collection")]
        [ProducesResponseType(200, Type = typeof(ICollection<BookStack>))]
        [ProducesResponseType(404)]
        public IActionResult GetBookStacks()
        {
            var bookStacks = _bookStackRepository.GetBookStacks();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (bookStacks.Count == 0)
            {
                return NotFound();
            }

            return Ok(bookStacks);
        }

        [HttpGet("title")]
        [ProducesResponseType(200, Type = typeof(ICollection<BookStack>))]
        [ProducesResponseType(404)]
        public IActionResult GetBookStacksByTitle([FromRoute] string title)
        {
            var stacks = _bookStackRepository.GetBookStacksByTitle(title);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (stacks.Count == 0)
            {
                return NotFound();
            }

            return Ok(stacks);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateBookStack([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("No title provided");
            }

            if (!_bookStackRepository.CreateBookStack(title))
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPut("update_author/{bookStackId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult AddAuthor([FromRoute] int bookStackId, [FromQuery] int authorId)
        {
            if (!_bookStackRepository.AddAuthor(bookStackId, authorId))
            {
                return BadRequest(500);
            }

            return Ok();
        }

        [HttpPut("update_books/{bookStackId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult AddBooks([FromRoute] int bookStackId, int bookAmount)
        {
            if (!_bookStackRepository.AddBooks(bookStackId, bookAmount))
            {
                return BadRequest(500);
            }

            return Ok();
        }
    }
}
