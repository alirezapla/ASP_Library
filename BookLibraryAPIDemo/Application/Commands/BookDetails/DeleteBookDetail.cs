using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.BookDetails;

public class DeleteBookDetail : IRequest<string>
{
    public string BookDetailId { get; set; }
}

public class DeleteBookDetailHandler : IRequestHandler<DeleteBookDetail, string>
{
    private readonly IBaseRepository<BookDetail> _repository;

    public DeleteBookDetailHandler(IBaseRepository<BookDetail> repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(DeleteBookDetail request, CancellationToken cancellationToken)
    {
        var bookDetail = await _repository.GetByIdAsync(request.BookDetailId);
        if (bookDetail == null)
        {
            throw new KeyNotFoundException(request.BookDetailId);
        }

        await _repository.DeleteAsync(bookDetail);

        return $"Book Detail with Id {request.BookDetailId} has been deleted";
    }
}