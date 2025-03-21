namespace BookLibraryAPIDemo.Application.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(string categoryId) : base($"Category with id: {categoryId} not found.")
        {
        }

    }
}