namespace BookLibraryAPIDemo.Application.Exceptions
{
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(string bookId) : base($"Book with id: {bookId} not found.")
        {
        }
    }

}