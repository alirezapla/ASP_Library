using System.Text;
using BookLibraryAPIDemo.Application.Models;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;

namespace BookLibraryAPIDemo.Application.Queries.Books;

public class ExportAllBooksAsCsv : IRequest<string>
{
    public List<string> Columns { get; set; }
    public QueryParams QueryParams { get; set; }
}

public class ExportAllBooksAsCsvHandler : IRequestHandler<ExportAllBooksAsCsv, string>
{
    private readonly IBaseRepository<Book> _bookRepository;

    public ExportAllBooksAsCsvHandler(IBaseRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<string> Handle(ExportAllBooksAsCsv request, CancellationToken cancellationToken)
    {
        var (books, _) =
            await _bookRepository.GetAllAsync(BuildSpec(request.QueryParams), request.QueryParams.SortParams);

        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine(string.Join(",", request.Columns));
        if (request.Columns.Count != 0)
        {
            csvBuilder.AppendLine(string.Join(',', request.Columns));
        }
        else
        {
            csvBuilder.AppendLine("Id,Title,AuthorId,PublisherId,CategoryId,CreatedDate,Description,Price,PageCount");
            request.Columns =
            [
                "Id", "Title", "AuthorId", "PublisherId", "CategoryId", "CreatedDate", "Description", "Price",
                "PageCount"
            ];
        }

        foreach (var book in books)
        {
            var values = new List<string>();
            if (request.Columns?.Contains("Id") == true)
                values.Add(book.Id);
            if (request.Columns?.Contains("Title") == true)
                values.Add(book.Title);
            if (request.Columns?.Contains("AuthorId") == true)
                values.Add(book.AuthorId);
            if (request.Columns?.Contains("PublisherId") == true)
                values.Add(book.PublisherId);
            if (request.Columns?.Contains("CategoryId") == true)
                values.Add(book.CategoryId);
            if (request.Columns?.Contains("Description") == true)
                values.Add(book.BookDetail?.Description);
            if (request.Columns?.Contains("Price") == true)
                values.Add(book.BookDetail?.Price.ToString());
            if (request.Columns?.Contains("PageCount") == true)
                values.Add(book.BookDetail?.PageCount.ToString());
            if (request.Columns?.Contains("CreatedDate") == true)
                values.Add(book.CreatedDate.ToString("yyyy-MM-dd"));

            csvBuilder.AppendLine(string.Join(",", values));
        }

        return csvBuilder.ToString();
    }


    private static IRichSpecification<Book> BuildSpec(QueryParams queryParams)
    {
        var builder = new SpecificationBuilder<Book>();

        foreach (var filter in queryParams.Filters)
        {
            builder.Where(filter);
        }

        return builder.Include(book => book.BookDetail)
            .Build();
    }
}