using BookLibraryAPIDemo.API.Controllers;
using BookLibraryAPIDemo.Application.Commands.BookDetails;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Queries.Reviews;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers;

[Route("reviews")]
public class ReviewController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ReviewDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllReviews([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(await Mediator.Send(new GetAllReviews() {PageNumber = pageNumber, PageSize = pageSize}));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewById([FromRoute] string id)
    {
        return Ok(await Mediator.Send(new GetReviewById()));
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO model)
    {
        var review = await Mediator.Send(new CreateReview() {Review = model});
        return Created(review.Id, review);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReviewById([FromRoute] string id)
    {
        return Ok(await Mediator.Send(new DeleteBookDetail() {BookDetailId = id}));
    }
}