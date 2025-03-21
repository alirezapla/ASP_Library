using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Reviews;

public class GetReviewById : IRequest<ReviewDTO>
{
    public string ReviewId { get; set; }
}

public class GetReviewByIdHandler : IRequestHandler<GetReviewById, ReviewDTO>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Review> _repository;

    public GetReviewByIdHandler(IMapper mapper, IBaseRepository<Review> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ReviewDTO> Handle(GetReviewById request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.ReviewId);
        if (review == null)
        {
            throw new KeyNotFoundException(request.ReviewId);
        }

        return _mapper.Map<ReviewDTO>(review);
    }
}