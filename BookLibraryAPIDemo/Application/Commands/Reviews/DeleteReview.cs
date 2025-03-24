using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Reviews;

public class DeleteReview : IRequest<DeleteDTO>
{
    public string ReviewId { get; set; }
    public string BookId { get; set; }
}

public class DeleteReviewHandler : IRequestHandler<DeleteReview, DeleteDTO>
{
    private readonly IBaseRepository<Review> _repository;

    public DeleteReviewHandler(IBaseRepository<Review> repository)
    {
        _repository = repository;
    }

    public async Task<DeleteDTO> Handle(DeleteReview request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.ReviewId);
        if (review == null || review.IsDeleted)
        {
            throw new KeyNotFoundException(request.ReviewId);
        }

        await _repository.SoftDeleteAsync(review);
        return new DeleteDTO()
        {
            Message = $"Review with Id {request.ReviewId} has been deleted"
        };
    }
}