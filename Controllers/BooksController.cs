using FluentValidation;
using LibraryWebAPI.Models;
using LibraryWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookStackRepository _bookStackRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<BookStack> _validator;

    public BooksController(
        IBookStackRepository bookStackRepository,
        IAuthorRepository authorRepository,
        IBookRepository bookRepository,
        IValidator<BookStack> validator)
    {
        _bookStackRepository = bookStackRepository;
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
        _validator = validator;
    }

    //### GET ###

    [HttpGet("BooksCollection")]
    [ProducesResponseType(200, Type = typeof(ICollection<BookStack>))]
    [ProducesResponseType(404)]
    public IActionResult GetBookStacks()
    {
        var bookStacks = _bookStackRepository.GetBookStacks();

        if (bookStacks == null)
        {
            ModelState.AddModelError("Server", "Null collection");
            return BadRequest(ModelState);
        }

        if (bookStacks.Count == 0)
        {
            return NotFound();
        }

        return Ok(bookStacks);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ICollection<BookStack>))]
    [ProducesResponseType(404)]
    public IActionResult GetBookStack([FromRoute] int bookStackId)
    {
        var bookStacks = _bookStackRepository.GetBookStack(bookStackId);

        if (bookStacks == null)
        {
            return NotFound();
        }

        return Ok(bookStacks);
    }

    [HttpGet("{title}")]
    [ProducesResponseType(200, Type = typeof(ICollection<BookStack>))]
    [ProducesResponseType(404)]
    public IActionResult GetBookStacksByTitle([FromRoute] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            ModelState.AddModelError("Title", "Should provide title");
            return BadRequest(ModelState);
        }

        var bookStacks = _bookStackRepository.GetBookStacksByTitle(title);

        if (bookStacks == null)
        {
            ModelState.AddModelError("Server", "Null collection");
            return BadRequest(ModelState);
        }

        if (bookStacks.Count == 0)
        {
            return NotFound();
        }

        return Ok(bookStacks);
    }

    [HttpGet("individual_book")]
    public IActionResult GetIndividualBooks()
    {
        var books = _bookRepository.GetBooks();

        if (books.Count == 0)
        {
            return NotFound();
        }

        return Ok(books);
    }

    [HttpGet("individual_book/{bookId:int}")]
    public IActionResult GetIndividualBook([FromRoute] int bookId)
    {
        var book = _bookRepository.GetBook(bookId);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    //### POST ####

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult CreateBookStack([FromQuery] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            ModelState.AddModelError("Title", "Should provide title");
            return BadRequest(ModelState);
        }

        var bookStack = new BookStack() { Title = title };

        if (!_bookStackRepository.CreateBookStack(bookStack))
        {                
            return StatusCode(500);
        }

        return Ok();
    }

    //### PATCH ###

    [HttpPatch("update_author/{bookStackId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult AddAuthorToBook([FromRoute] int bookStackId, [FromQuery] int authorId)
    {
        var bookStack = _bookStackRepository.GetBookStack(bookStackId);

        if (bookStack == null)
        {
            ModelState.AddModelError("Entity", "Book is not found");
            return NotFound(ModelState);
        }

        var author = _authorRepository.GetAuthor(authorId);

        if (author == null)
        {
            ModelState.AddModelError("Entity", "Author is not found");
            return NotFound(ModelState);
        }

        if (!_bookStackRepository.AddAuthor(bookStack, author))
        {
            return StatusCode(500);
        }

        return Ok();
    }

    [HttpPatch("update_books/{bookStackId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult AddBooks([FromRoute] int bookStackId, int bookAmount)
    {
        if (bookAmount <= 0)
        {
            ModelState.AddModelError("Entity", "Books amount is required");
            return BadRequest(ModelState);
        }

        var bookStack = _bookStackRepository.GetBookStack(bookStackId);

        if (bookStack == null)
        {
            ModelState.AddModelError("Entity", "Book is not found");
            return NotFound(ModelState);
        }

        if (!_bookStackRepository.AddBooks(bookStack, bookAmount))
        {
            return StatusCode(500);
        }

        return Ok();
    }

    //### DELETE ###

    [HttpDelete("delete/{bookStackId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult RemoveBook([FromRoute] int bookStackId)
    {
        var bookStack = _bookStackRepository.GetBookStack(bookStackId);

        if (bookStack == null)
        {
            ModelState.AddModelError("Entity", "Book is not found");
            return NotFound(ModelState);
        }

        if (!_bookStackRepository.DeleteBookStack(bookStack))
        {
            return StatusCode(500);
        }

        return Ok();
    }

    [HttpDelete("individual_book/{bookId:int}")]
    public IActionResult RemoveIndividualBook([FromRoute] int bookId)
    {
        var book = _bookRepository.GetBook(bookId);

        if (book == null)
        {
            return NotFound();
        }

        if (!_bookRepository.RemoveBook(book))
        {
            return StatusCode(500);
        }

        return Ok();
    }
}
