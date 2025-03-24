using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Books
{
    public class DeleteBook : IRequest<DeleteDTO>
    {
        public string BookId { get; set; }


        public class DeleteBookHandler : IRequestHandler<DeleteBook, DeleteDTO>
        {
            private readonly IBaseRepository<Book> _repository;

            public DeleteBookHandler(IBaseRepository<Book> repository)
            {
                _repository = repository;
            }

            public async Task<DeleteDTO> Handle(DeleteBook request, CancellationToken cancellationToken)
            {
                var book = await _repository.GetByIdAsync(request.BookId);
                if (book == null || book.IsDeleted)
                {
                    throw new BookNotFoundException(request.BookId);
                }

                await _repository.SoftDeleteAsync(book);

                return new DeleteDTO()
                {
                    Message = $"Book with Id {request.BookId} has been deleted"
                };
            }
        }
    }
}