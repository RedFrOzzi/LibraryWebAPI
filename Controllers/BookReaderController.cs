using LibraryWebAPI.Models;
using LibraryWebAPI.Repository.Implementation;
using LibraryWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReaderController : ControllerBase
    {
        private readonly IBookReaderRepository _bookReaderRepository;
        private readonly IBookRepository _bookRepository;

        public BookReaderController(IBookReaderRepository bookReaderRepository, IBookRepository bookRepository)
        {
            _bookReaderRepository = bookReaderRepository;
            _bookRepository = bookRepository;
        }

        //### GET ###

        [HttpGet("readers")]
        public IActionResult GetReaders()
        {
            var readers = _bookReaderRepository.GetReaders();

            if (readers == null)
            {
                ModelState.AddModelError("Server", "Null collection");
                return BadRequest(ModelState);
            }

            if (readers.Count == 0)
            {
                return NotFound();
            }

            return Ok(readers);
        }

        [HttpGet("{readerId:int}")]
        public IActionResult GetReader([FromRoute] int readerId)
        {
            var reader = _bookReaderRepository.GetReader(readerId);

            if (reader == null)
            {
                return NotFound();
            }

            return Ok(reader);
        }

        [HttpGet("{name}")]
        public IActionResult GetReaderByName([FromRoute] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError("Name", "Should provide name");
                return BadRequest();
            }

            var reader = _bookReaderRepository.GetReaders(name);

            if (reader == null)
            {
                return NotFound();
            }

            return Ok(reader);
        }

        //### POST ####

        [HttpPost]
        public IActionResult CreateReader([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError("Name", "Should provide name");
                return BadRequest();
            }

            if (!_bookReaderRepository.CreateReader(name))
            {
                return StatusCode(500);
            }

            return Ok();
        }

        //### PATCH ###

        [HttpPatch("update_name/{readerId:int}")]
        public IActionResult ChangeAuthorName([FromRoute] int readerId, [FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError("Name", "Should provide name");
                return BadRequest();
            }

            var reader = _bookReaderRepository.GetReader(readerId);

            if (reader == null)
            {
                return NotFound();
            }

            if (!_bookReaderRepository.ChangeReaderName(reader, name))
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPatch("add_books/{readerId:int}")]
        public IActionResult AddBooks([FromRoute] int readerId, [FromQuery] int[] bookIds)
        {
            var reader = _bookReaderRepository.GetReader(readerId);

            if (reader == null)
            {
                return NotFound();
            }

            var books = new Book[bookIds.Length];

            for (int i = 0; i < bookIds.Length; i++)
            {
                var book = _bookRepository.GetBook(bookIds[i]);

                if (book == null)
                {
                    ModelState.AddModelError("Entity", $"Book with {bookIds[i]} was not found");

                    continue;
                }

                books[i] = book;
            }

            if (!_bookReaderRepository.AddBorrowedBooks(reader, books))
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPatch("remove_books/{readerId:int}")]
        public IActionResult RemoveBooks([FromRoute] int readerId, [FromQuery] int[] bookIds)
        {
            var reader = _bookReaderRepository.GetReader(readerId);

            if (reader == null)
            {
                return NotFound();
            }

            var books = new Book[bookIds.Length];

            for (int i = 0; i < bookIds.Length; i++)
            {
                var book = _bookRepository.GetBook(bookIds[i]);

                if (book == null)
                {
                    ModelState.AddModelError("Entity", $"Book with {bookIds[i]} was not found");

                    continue;
                }

                books[i] = book;
            }

            if (books.All(b => b == null))
            {
                return NotFound(ModelState);
            }

            if (!_bookReaderRepository.RemoveBorrowedBooks(reader, books))
            {
                return StatusCode(500);
            }

            return Ok(ModelState);
        }

        //### DELETE ###
        [HttpDelete("{readerId:int}")]
        public IActionResult DeleteReader([FromRoute] int readerId)
        {
            var reader = _bookReaderRepository.GetReader(readerId);

            if (reader == null)
            {
                return NotFound();
            }

            if (!_bookReaderRepository.DeleteReader(reader))
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
