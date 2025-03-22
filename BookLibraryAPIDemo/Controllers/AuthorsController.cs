using BookLibraryAPIDemo.API.Controllers;
using BookLibraryAPIDemo.Application.Commands.Authors;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Queries.Authors;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    //[Authorize]
    [Route("authors")]
    public class AuthorsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(new CreateAuthor {Author = model}));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<AuthorDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthorsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await Mediator.Send(new GetAllAuthors() {PageNumber = pageNumber, PageSize = pageSize}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorByIdAsync(string id)
        {
            return Ok(await Mediator.Send(new GetAuthorById() {AuthorId = id}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthorAsync([FromRoute] string id, [FromBody] UpdateAuthorDTO model)
        {
            return Ok(await Mediator.Send(new UpdateAuthor {Id = id, Author = model}));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteAuthor {AuthorId = id}));
        }
    }
}