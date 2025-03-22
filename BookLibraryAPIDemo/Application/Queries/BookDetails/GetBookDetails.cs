using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.BookDetails;

public class GetBookDetails : IRequest<BookDetailDTO>
{
    public string BookId { get; set; }
}

public class GetBookDetailsHandler : IRequestHandler<GetBookDetails, BookDetailDTO>
{
    private readonly IBaseRepository<Book> _repository;
    private readonly IMapper _mapper;

    public GetBookDetailsHandler(IBaseRepository<Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookDetailDTO> Handle(GetBookDetails request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.BookId,(Book book) => book.BookDetail);
        if (book == null)
        {
            throw new BookNotFoundException(request.BookId);
        }

        return _mapper.Map<BookDetailDTO>(book.BookDetail);
    }
}