using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.BookDetails;

public class GetAllBookDetails : IRequest<PagedResult<BookDetailDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetAllBookDetailsHandler : IRequestHandler<GetAllBookDetails, PagedResult<BookDetailDTO>>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<BookDetail> _repository;
    
    public GetAllBookDetailsHandler(IMapper mapper, IBaseRepository<BookDetail> repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public async Task<PagedResult<BookDetailDTO>> Handle(GetAllBookDetails request, CancellationToken cancellationToken)
    {
        var (allBookDetails, totalCount) = await _repository.GetAllAsync(request.PageNumber, request.PageSize);
        var results = _mapper.Map<List<BookDetailDTO>>(allBookDetails);
        return new PagedResult<BookDetailDTO>
        {
            Items = results,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}