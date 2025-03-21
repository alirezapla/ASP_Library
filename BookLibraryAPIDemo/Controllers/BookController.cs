using BookLibraryAPIDemo.Application.Commands.Books;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Queries.Books;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.API.Controllers
{
    [Authorize]
    public class BookController : BaseApiController
    {


        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO model)
        {
            return Ok(await Mediator.Send(new CreateBook { Book = model }));
        }

        [HttpGet("GetAllBook")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<BookDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBooksAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await Mediator.Send(new GetAllBook{ PageNumber = pageNumber, PageSize = pageSize }));
        }

        [HttpGet("books/{id}")]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            return Ok(await Mediator.Send(new GetBookById() { BookId = id }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookAsync([FromBody] BookDTO model)
        {
            return Ok(await Mediator.Send(new UpdateBook { Book = model }));
        }

        [HttpDelete("books/{id}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteBook { BookId = id }));
        }
    }
}
