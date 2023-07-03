using LibraryWebAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    //### GET ####

    [HttpGet("authors")]
    public IActionResult GetAuthors()
    {
        var authors = _authorRepository.GetAuthors();

        if (authors == null)
        {
            ModelState.AddModelError("Server", "Null collection");
            return BadRequest(ModelState);
        }

        if (authors.Count == 0)
        {
            return NotFound();
        }

        return Ok(authors);
    }

    [HttpGet("{authorId:int}")]
    public IActionResult GetAuthor([FromRoute] int authorId)
    {
        var author = _authorRepository.GetAuthor(authorId);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpGet("{name}")]
    public IActionResult GetAuthorByName([FromRoute] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Should provide name");
            return BadRequest();
        }

        var author = _authorRepository.GetAuthorsByName(name);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    //### POST ####

    [HttpPost]
    public IActionResult CreateAuthor([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Should provide name");
            return BadRequest();
        }

        if (!_authorRepository.CreateAuthor(name))
        {
            return StatusCode(500);
        }

        return Ok();
    }

    //### PATCH ###

    [HttpPatch("update_name/{authorId:int}")]
    public IActionResult ChangeAuthorName([FromRoute] int authorId, [FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Should provide name");
            return BadRequest();
        }

        var author = _authorRepository.GetAuthor(authorId);

        if (author == null)
        {
            return NotFound();
        }

        if (!_authorRepository.ChangeAuthorName(author, name))
        {
            return StatusCode(500);
        }

        return Ok();
    }

    //### DELETE ###
    [HttpDelete("{authorId:int}", Name = nameof(DeleteAuthor))]
    public IActionResult DeleteAuthor([FromRoute] int authorId)
    {
        var author = _authorRepository.GetAuthor(authorId);

        if (author == null)
        {
            return NotFound();
        }

        if (!_authorRepository.DeleteAuthor(author))
        {
            return StatusCode(500);
        }

        return Ok();
    }
}
