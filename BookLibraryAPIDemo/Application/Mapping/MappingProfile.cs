using AutoMapper;
using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Domain.Entities;

namespace BookLibraryAPIDemo.Application.Mapping
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from Book entity to BookDTO and back
            CreateMap<Book, AllBooksDTO>()
   .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
   .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
   .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
   .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
   .ForMember(dest => dest.BookDetail, opt => opt.MapFrom(src => src.BookDetail))
   .ReverseMap();
            
            CreateMap<Book, BookDTO>()
   .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
   .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
   .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
   .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
   .ForMember(dest => dest.BookDetailId, opt => opt.MapFrom(src => src.BookDetail))
   .ReverseMap();

            CreateMap<CreateBookDTO, Book>();
            CreateMap<Book, IncludeBooksDTO>();
            
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<CreateAuthorDTO, Author>();
            CreateMap<UpdateAuthorDTO, Author>();
            
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryWithBooksDTO>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books)) 
                .ReverseMap();

            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
            
            CreateMap<Publisher, PublisherDTO>().ReverseMap();
            CreateMap<CreatePublisherDTO, Publisher>();
            CreateMap<UpdatePublisherDTO, Publisher>();
            
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<CreateReviewDTO, Review>();
            CreateMap<UpdateReviewDTO, Review>();
            
            CreateMap<BookDetail, BookDetailDTO>().ReverseMap();
            CreateMap<CreateBookDetailDTO, BookDetail>();
            CreateMap<UpdateBookDetailDto, BookDetail>();

        }
    }



}