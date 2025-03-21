namespace BookLibraryAPIDemo.Application.DTO
{



    public class BookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int PublisherId { get; set; }
        public string Publisher { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }





}