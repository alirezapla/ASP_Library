using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.BookDetails;

public class UpdateBookDetail : IRequest<BookDetailDTO>
{
    public UpdateBookDetailDto BookDetail { get; set; }
    public string BookId { get; set; }
}

public class UpdateBookDetailHandler : IRequestHandler<UpdateBookDetail, BookDetailDTO>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Book> _repository;
    
    public UpdateBookDetailHandler(IMapper mapper, IBaseRepository<Book> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<BookDetailDTO> Handle(UpdateBookDetail request, CancellationToken cancellationToken)
    {
        var existingBook = await _repository.GetByIdAsync(request.BookId,(Book book) => book.BookDetail);

        if (existingBook == null)
        {
            throw new KeyNotFoundException($"Book with id {request.BookId} not found");
        }

        _mapper.Map(request.BookDetail, existingBook.BookDetail);
        await _repository.UpdateAsync(existingBook);
        return _mapper.Map<BookDetailDTO>(existingBook.BookDetail);

    }
}