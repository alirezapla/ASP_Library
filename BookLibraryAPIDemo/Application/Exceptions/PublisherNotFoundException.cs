namespace BookLibraryAPIDemo.Application.Exceptions
{
    public class PublisherNotFoundException : NotFoundException
    {
        public PublisherNotFoundException(string PublisherId) : base($" Publisher with id : {PublisherId} is not found")
        {
        }
    }
}