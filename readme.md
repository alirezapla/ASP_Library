# 📚 ASP Library Project

This project is a simple library management system built using ASP.NET Core and Entity Framework Core. It provides functionalities to manage books, authors, publishers, categories, and book details.


## 🚀 Features


*   **Book Management:**
    *   Add new books with details like title, ISBN, author, category, publisher, publication date, and description.
    *   Edit and update existing book information.
    *   Search for books by title, author, category, or ISBN.
    *   Display a list of all books or filter by category or availability.
*   **Author Management:**
    *   Add new authors with their biography.
    *   Edit author details.
    *   List all authors and their associated books.
*   **Category Management:**
    *   Create, edit, and delete book categories.
    *   Assign categories to books.
*   **Publisher Management:**
    *   Add new publishers with their contact information.
    *   Edit publisher details.
    *   Associate publishers with books.
*   **User Management:**
    *   User registration and login.
    *   Role-based access control (e.g., Admin, Librarian, User).
    *   Manage user profiles.
*   **Borrowing and Returning:**
    *   Users can borrow available books.
    *   Track borrowing history for each user.
    *   Automatic due date calculation.
    *   Process book returns.
*   **Reviews and Ratings:**
    *   Users can submit reviews and ratings for books.
    *   Display average ratings for each book.
*   **Reporting:** (Optional - Include if implemented)
    *   Generate reports on popular books, borrowing trends, overdue books, etc.
*   **Search Functionality:**
    * Robust search capabilities across all entities.
    
    
## 🛠️ Technologies Used

    ASP.NET Core: For building the API.

    Entity Framework Core: For database operations and ORM.

    SQL Server: As the relational database.

    AutoMapper: For mapping between entities and DTOs.

    Swagger: For API documentation and testing.

    Dependency Injection: For managing dependencies and services.

    Middleware: For global exception handling and request validation.

## Getting Started

**Restore NuGet packages:**

    dotnet restore

**Update the database connection string:**

Open the `appsettings.json` file.

Locate the `ConnectionStrings` section.

Modify the `DefaultConnection` value to point to your SQL Server (or other database) instance.  Ensure the database server is running and accessible.


    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server;Database=LibraryDB;User Id=your_user_id;Password=your_password;"
    }

**Apply database migrations:**


    dotnet ef database update

manual script for migration:
```sql
CREATE UNIQUE INDEX IX_Books_Author_Title_Publisher
    ON Books (AuthorId,Title,PublisherId)
    WHERE IsDeleted = 0;

CREATE UNIQUE INDEX IX_PublisherName
    ON Publishers (PublisherName)
    WHERE IsDeleted = 0;

CREATE UNIQUE INDEX IX_CategoryName
    ON Categories (Name)
    WHERE IsDeleted = 0;
```
**Build the project:**


    dotnet build

**Run the application:**

    
    dotnet run

**Access the application:**

        The API will be available at https://localhost:5001 (or http://localhost:5000).

        Use Swagger UI to explore and test the API endpoints: https://localhost:5001/swagger.

## Configuration

**appsettings.json:**  The primary configuration file for the application.  You can configure database connection strings, logging settings, and other application-specific parameters in this file.


## 📂 Project Structure

```
ASP_Library/
├── Controllers/          # API Controllers
├── Domain/               # Domain Models and Entities
├── Application/          # Application Layer (DTOs, Services, Mappings)
├── Infrastructure/       # Database Context and Repositories
├── MiddleWares/          # Custom Middleware (e.g., Exception Handling)
├── Migrations/           # Entity Framework Migrations
├── appsettings.json      # Configuration File
├── Program.cs            # Main Entry Point
└── README.md             # Project Documentation
```

## 📄 API Endpoints
### Books

    GET /api/books - Get all books.

    GET /api/books/{id} - Get a book by ID.

    POST /api/books - Add a new book.

    PUT /api/books/{id} - Update a book.

    DELETE /api/books/{id} - Delete a book.

### Authors

    GET /api/authors - Get all authors.

    GET /api/authors/{id} - Get an author by ID.

    POST /api/authors - Add a new author.

    PUT /api/authors/{id} - Update an author.

    DELETE /api/authors/{id} - Delete an author.

### Publishers

    GET /api/publishers - Get all publishers.

    GET /api/publishers/{id} - Get a publisher by ID.

    POST /api/publishers - Add a new publisher.

    PUT /api/publishers/{id} - Update a publisher.

    DELETE /api/publishers/{id} - Delete a publisher.

### Categories

    GET /api/categories - Get all categories.

    GET /api/categories/{id} - Get a category by ID.

    POST /api/categories - Add a new category.

    PUT /api/categories/{id} - Update a category.

    DELETE /api/categories/{id} - Delete a category.

### Book Details

    GET /api/books/{id}/details - Get book details by book ID.

    POST /api/books/{id}/details - Add book details.

    PUT /api/books/{id}/details - Update book details.

    DELETE /api/books/{id}/details - Delete book details.

### Reviews

    GET /api/reviews/{id} - Get review by review ID.

    POST /api/reviews/{id} - Add review.

    PUT /api/reviews/{id} - Update review.

    DELETE /api/reviews/{id} - Delete review.


## 🛠️ Error Handling

The project includes a global exception handler middleware (GlobalExceptionHandlerMiddleware) to handle errors gracefully. It returns structured error responses for:

    400 Bad Request: Invalid input data.

    404 Not Found: Resource not found.

 
## ERD
