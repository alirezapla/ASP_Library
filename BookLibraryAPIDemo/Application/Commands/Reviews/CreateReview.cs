using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;

namespace BookLibraryAPIDemo.Application.Commands.Reviews;

public class CreateReview : IRequest<ReviewDTO>
{
    public CreateReviewDTO Review { get; set; }
    public string BookId { get; set; }
}

public class CreateReviewHandler : IRequestHandler<CreateReview, ReviewDTO>
{
    private readonly IBaseRepository<Review> _repository;
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public CreateReviewHandler(IBaseRepository<Review> repository, IMapper mapper, IBaseRepository<Book> bookRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<ReviewDTO> Handle(CreateReview request, CancellationToken cancellationToken)
    {
        var existingBook = await _bookRepository.GetByIdAsync(request.BookId);

        if (existingBook == null)
        {
            throw new KeyNotFoundException($"Book with id {request.BookId} not found");
        }
        var review = _mapper.Map<Review>(request.Review);
        review.Book = existingBook;
        review.BookId = request.BookId;
        await _repository.CreateAsync(review);
        return _mapper.Map<ReviewDTO>(review);
    }
}