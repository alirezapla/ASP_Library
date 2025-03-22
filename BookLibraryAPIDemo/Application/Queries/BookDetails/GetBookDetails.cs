using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Application.Queries.Books;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.BookDetails;

public class GetBookDetails : IRequest<BookDetailDTO>
{
    public string BookDetailId { get; set; }
}

public class GetBookDetailsByIdHandler : IRequestHandler<GetBookDetails, BookDetailDTO>
{
    private readonly IBaseRepository<BookDetail> _repository;
    private readonly IMapper _mapper;

    public GetBookDetailsByIdHandler(IBaseRepository<BookDetail> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookDetailDTO> Handle(GetBookDetails request, CancellationToken cancellationToken)
    {
        var bookDetail = await _repository.GetByIdAsync(request.BookDetailId);
        if (bookDetail == null)
        {
            throw new BookNotFoundException(request.BookDetailId);
        }

        return _mapper.Map<BookDetailDTO>(bookDetail);
    }
}