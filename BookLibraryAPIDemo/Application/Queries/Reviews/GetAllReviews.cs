using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Reviews;

public class GetAllReviews : IRequest<PagedResult<ReviewDTO>>
{
    public QueryParams QueryParams { get; set; }
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
        var (allReviews, totalCount) =
            await _repository.GetAllAsync(BuildSpecification(request.QueryParams), request.QueryParams.SortParams);
        var results = _mapper.Map<List<ReviewDTO>>(allReviews);
        return new PagedResult<ReviewDTO>(results, totalCount, request.QueryParams.PaginationParams);
    }

    private static IRichSpecification<Review> BuildSpecification(QueryParams queryParams)
    {
        var builder = new SpecificationBuilder<Review>();

        foreach (var filter in queryParams.Filters)
        {
            builder.Where(filter);
        }

        return builder.ApplyPaging(queryParams.PaginationParams)
            .Build();
    }
}