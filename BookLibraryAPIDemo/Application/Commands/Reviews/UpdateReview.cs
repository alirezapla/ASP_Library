using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Reviews;

public abstract class UpdateReview : IRequest<ReviewDTO>
{
    public UpdateReviewDTO Review { get; set; }
    public string Id { get; set; }
}

public class UpdateReviewHandler : IRequestHandler<UpdateReview, ReviewDTO>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Review> _repository;

    public UpdateReviewHandler(IBaseRepository<Review> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReviewDTO> Handle(UpdateReview request, CancellationToken cancellationToken)
    {
        var existingReview = await _repository.GetByIdAsync(request.Id);

        if (existingReview == null)
        {
            throw new KeyNotFoundException($"Review with id {request.Id} not found");
        }

        _mapper.Map(request.Review, existingReview);
        await _repository.UpdateAsync(existingReview);
        return _mapper.Map<ReviewDTO>(existingReview);
    }
}