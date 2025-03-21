using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.BookDetails;

public class UpdateBookDetail : IRequest<BookDetailDTO>
{
    public UpdateBookDetail BookDetail { get; set; }
    public string BookDetailId { get; set; }
}

public class UpdateBookDetailHandler : IRequestHandler<UpdateBookDetail, BookDetailDTO>
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<BookDetail> _repository;
    
    public UpdateBookDetailHandler(IMapper mapper, IBaseRepository<BookDetail> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<BookDetailDTO> Handle(UpdateBookDetail request, CancellationToken cancellationToken)
    {
        var existingBookDetail = await _repository.GetByIdAsync(request.BookDetailId);

        if (existingBookDetail == null)
        {
            throw new KeyNotFoundException($"Book Detail with id {request.BookDetailId} not found");
        }

        _mapper.Map(request.BookDetail, existingBookDetail);
        await _repository.UpdateAsync(existingBookDetail);
        return _mapper.Map<BookDetailDTO>(existingBookDetail);

    }
}