using BookLibraryAPIDemo.API.Controllers;
using BookLibraryAPIDemo.Application.Commands.BookDetails;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Queries.BookDetails;
using BookLibraryAPIDemo.Application.Queries.Books;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers;

[Route("book-details")]
public class BookDetailController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateBookDetailAsync([FromBody] CreateBookDetailDTO model)
    {
        var detail = await Mediator.Send(new CreateBookDetail() {BookDetail = model});
        return Created(detail.Id, detail);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<BookDetailDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBookDetailsAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        return Ok(await Mediator.Send(new GetAllBookDetails(){PageNumber = pageNumber,PageSize = pageSize}));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookDetailsByIdAsync(string id)
    {
        return Ok(await Mediator.Send(new GetBookById() {BookId = id}));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookDetailsAsync([FromRoute] string id, [FromBody] UpdateBookDetailDto model)
    {
        using var reader = new StreamReader(Request.Body);
        return Ok(await Mediator.Send(new UpdateBookDetail() {BookDetail = model, BookDetailId = id}));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookDetailsAsync([FromRoute] string id)
    {
        return Ok(await Mediator.Send(new DeleteBookDetail() {BookDetailId = id}));
    }
}