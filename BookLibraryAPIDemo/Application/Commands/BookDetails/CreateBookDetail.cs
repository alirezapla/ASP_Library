using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.BookDetails;

public class CreateBookDetail : IRequest<BookDetailDTO>
{
    public CreateBookDetailDTO BookDetail { get; set; }
}

public class CreateBookDetailHandler : IRequestHandler<CreateBookDetail, BookDetailDTO>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<BookDetail> _repository;

    public CreateBookDetailHandler(IBaseRepository<BookDetail> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookDetailDTO> Handle(CreateBookDetail request, CancellationToken cancellationToken)
    {
        var bookDetail = _mapper.Map<BookDetail>(request.BookDetail);
        await _repository.CreateAsync(bookDetail);
        return _mapper.Map<BookDetailDTO>(bookDetail);
    }
}

