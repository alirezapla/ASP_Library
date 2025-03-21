using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.BookDetails;

public class CreateReview : IRequest<ReviewDTO>
{
    public CreateReviewDTO? Review { get; set; }
}

public class CreateReviewHandler : IRequestHandler<CreateReview, ReviewDTO>
{
    private readonly IBaseRepository<Review> _repository;
    private readonly IMapper _mapper;

    public CreateReviewHandler(IBaseRepository<Review> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReviewDTO> Handle(CreateReview request, CancellationToken cancellationToken)
    {
        var review = _mapper.Map<Review>(request.Review);
        await _repository.CreateAsync(review);
        return _mapper.Map<ReviewDTO>(review);
    }
}

