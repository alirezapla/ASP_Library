using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Reviews;

public class GetAllReviews : IRequest<PagedResult<ReviewDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAllReviewsHandler : IRequestHandler<GetAllReviews, PagedResult<ReviewDTO>>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Review> _repository;

    public GetAllReviewsHandler(IBaseRepository<Review> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResult<ReviewDTO>> Handle(GetAllReviews request, CancellationToken cancellationToken)
    {
        var (allReviews, totalCount) = await _repository.GetAllAsync(request.PageNumber, request.PageSize);
        var results = _mapper.Map<List<ReviewDTO>>(allReviews);
        return new PagedResult<ReviewDTO>
        {
            Items = results,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}