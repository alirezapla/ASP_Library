namespace BookLibraryAPIDemo.Application.Exceptions
{
    public class AuthorNotFoundException : NotFoundException
    {

        public AuthorNotFoundException(string authorId) : base($"Author with  id :{authorId} is not found  ")
        {

        }
    }
}