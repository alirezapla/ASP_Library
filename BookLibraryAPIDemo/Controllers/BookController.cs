using BookLibraryAPIDemo.Application.Commands.BookDetails;
using BookLibraryAPIDemo.Application.Commands.Books;
using BookLibraryAPIDemo.Application.Commands.Reviews;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.DTO.Book;
using BookLibraryAPIDemo.Application.Queries.BookDetails;
using BookLibraryAPIDemo.Application.Queries.Books;
using BookLibraryAPIDemo.Application.Queries.Reviews;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    [Route("books")]
    public class BookController : BaseApiController
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await Mediator.Send(new CreateBook {Book = model}));
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<BookDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBooksAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await Mediator.Send(new GetAllBook {PageNumber = pageNumber, PageSize = pageSize}));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "CanRead")]
        public async Task<IActionResult> GetBookByIdAsync(string id)
        {
            return Ok(await Mediator.Send(new GetBookById() {BookId = id}));
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteBook {BookId = id}));
        }

        [HttpPost("{id}/detail")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBookDetailAsync([FromBody] CreateBookDetailDTO model,
            [FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Mediator.Send(new CreateBookDetail() {BookDetail = model, BookId = id});
            return Created(id, "created");
        }

        [HttpGet("{id}/detail")]
        [Authorize(Policy = "CanRead")]
        public async Task<IActionResult> GetBookDetailsAsync([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetBookDetails() {BookId = id}));
        }

        [HttpPut("{id}/detail")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBookDetailsAsync([FromRoute] string id,
            [FromBody] UpdateBookDetailDto model)
        {
            return Ok(await Mediator.Send(new UpdateBookDetail() {BookDetail = model, BookId = id}));
        }

        [HttpGet("{id}/reviews")]
        [Authorize(Policy = "CanRead")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ReviewDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllReviews([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromRoute] string id)
        {
            return Ok(await Mediator.Send(new GetAllReviews() {PageNumber = pageNumber, PageSize = pageSize}));
        }

        [HttpGet(("{id}/reviews/{reviewId}"))]
        [Authorize(Policy = "CanRead")]
        public async Task<IActionResult> GetReviewById([FromRoute] string id, [FromRoute] string reviewId)
        {
            return Ok(await Mediator.Send(new GetReviewById()));
        }

        [HttpPost("{id}/reviews")]
        [Authorize(Policy = "CanWrite")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO model, [FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await Mediator.Send(new CreateReview() {Review = model, BookId = id});
            return Created(review.Id, review);
        }

        [HttpDelete("{id}/reviews/{reviewId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateReview([FromRoute] string id, [FromRoute] string reviewId)
        {
            return Ok(await Mediator.Send(new DeleteReview() {ReviewId = reviewId, BookId = id}));
        }
    }
}