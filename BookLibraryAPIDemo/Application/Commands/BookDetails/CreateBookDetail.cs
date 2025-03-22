using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.BookDetails;

public class CreateBookDetail : IRequest<BookDetailDTO>
{
    public CreateBookDetailDTO BookDetail { get; set; }
    public string BookId { get; set; }
}

public class CreateBookDetailHandler : IRequestHandler<CreateBookDetail, BookDetailDTO>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBaseRepository<BookDetail> _bookDetailRepository;

    public CreateBookDetailHandler(IBaseRepository<Book> repository, IMapper mapper, IBaseRepository<BookDetail> bookDetailRepository)
    {
        _bookRepository = repository;
        _mapper = mapper;
        _bookDetailRepository = bookDetailRepository;
    }

    public async Task<BookDetailDTO> Handle(CreateBookDetail request, CancellationToken cancellationToken)
    {
        var existingBook = await _bookRepository.GetByIdAsync(request.BookId,(Book book) => book.BookDetail);

        if (existingBook == null)
        {
            throw new KeyNotFoundException($"Book with id {request.BookId} not found");
        }

        if (existingBook.BookDetail != null)
        {
            throw new BadRequestException($"Book with id {request.BookId} already has detail");
            ;
        }
        var bookDetail = _mapper.Map<BookDetail>(request.BookDetail);
        bookDetail.BookId = request.BookId;
        await _bookDetailRepository.CreateAsync(bookDetail);
        return _mapper.Map<BookDetailDTO>(bookDetail);
    }
}

