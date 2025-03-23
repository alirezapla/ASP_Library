using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Books
{
    public class UpdateBook : IRequest<BookDTO>
    {
        public UpdateBookDTO Book { get; set; }
        public string BookId { get; set; }
    }

    public class UpdateBookHandler : IRequestHandler<UpdateBook, BookDTO>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public UpdateBookHandler(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(UpdateBook request, CancellationToken cancellationToken)
        {
            var existingBook = await _repository.GetByIdAsync(request.BookId);

            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with id {request.BookId} not found");
            }

            _mapper.Map(request.Book, existingBook);
            await _repository.UpdateAsync(existingBook);
            return _mapper.Map<BookDTO>(existingBook);
        }
    }
}