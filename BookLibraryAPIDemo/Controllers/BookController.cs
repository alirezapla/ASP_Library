using BookLibraryAPIDemo.Application.Commands.BookDetails;
using BookLibraryAPIDemo.Application.Commands.Books;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Queries.BookDetails;
using BookLibraryAPIDemo.Application.Queries.Books;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.API.Controllers
{
    // [Authorize]
    [Route("books")]
    public class BookController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(new CreateBook {Book = model}));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<BookDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBooksAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await Mediator.Send(new GetAllBook {PageNumber = pageNumber, PageSize = pageSize}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(string id)
        {
            return Ok(await Mediator.Send(new GetBookById() {BookId = id}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync([FromBody] BookDTO model)
        {
            return Ok(await Mediator.Send(new UpdateBook {Book = model}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteBook {BookId = id}));
        }


        [HttpPost("{id}/detail")]
        public async Task<IActionResult> CreateBookDetailAsync([FromBody] CreateBookDetailDTO model,
            [FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Mediator.Send(new CreateBookDetail() {BookDetail = model, BookId = id});
            return Created(id, "created");
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetBookDetailsAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetBookDetails() {BookId = id}));
        }


        [HttpPut("{id}/detail")]
        public async Task<IActionResult> UpdateBookDetailsAsync([FromRoute] string id,
            [FromBody] UpdateBookDetailDto model)
        {
            using var reader = new StreamReader(Request.Body);
            return Ok(await Mediator.Send(new UpdateBookDetail() {BookDetail = model, BookId = id}));
        }
    }
}