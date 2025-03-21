using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.BookDetails;

public class DeleteReview : IRequest<string>
{
    public string ReviewId { get; set; }
}

public class DeleteReviewHandler : IRequestHandler<DeleteReview, string>
{
    private readonly IBaseRepository<Review> _repository;

    public DeleteReviewHandler(IBaseRepository<Review> repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(DeleteReview request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.ReviewId);
        if (review == null)
        {
            throw new KeyNotFoundException(request.ReviewId);
        }

        await _repository.DeleteAsync(review);

        return $"Review with Id {request.ReviewId} has been deleted";
    }
}