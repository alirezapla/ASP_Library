📚 ASP Library Project

This project is a simple library management system built using ASP.NET Core and Entity Framework Core. It provides functionalities to manage books, authors, publishers, categories, and book details.

🚀 Features

    Book Management: Add, edit, delete, and view books.

    Author Management: Add, edit, delete, and view authors.

    Publisher Management: Add, edit, delete, and view publishers.

    Category Management: Add, edit, delete, and view categories.

    Book Details Management: Add, edit, delete, and view book details.

    Request Validation: Use of Data Annotations for request validation.

    Error Handling: Middleware for handling server errors and invalid requests.

    Database: SQL Server as the database and Entity Framework Core for data management.

🛠️ Technologies Used

    ASP.NET Core: For building the API.

    Entity Framework Core: For database operations and ORM.

    SQL Server: As the relational database.

    AutoMapper: For mapping between entities and DTOs.

    Swagger: For API documentation and testing.

    Dependency Injection: For managing dependencies and services.

    Middleware: For global exception handling and request validation.

🚀 Getting Started

Follow these steps to set up and run the project locally.
Prerequisites

    .NET 8 SDK

    SQL Server

Installation


    Set Up the Database:

        Update the connection string in appsettings.json to point to your SQL Server instance.

        Run the following commands to apply migrations and create the database:
        bash
        Copy

        dotnet ef database update

    Run the Application:

        In Visual Studio:

            Open the solution file (ASP_Library.sln).

        dotnet run

    Access the API:

        The API will be available at https://localhost:5001 (or http://localhost:5000).

        Use Swagger UI to explore and test the API endpoints: https://localhost:5001/swagger.

📂 Project Structure
Copy

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

📄 API Endpoints
Books

    GET /api/books - Get all books.

    GET /api/books/{id} - Get a book by ID.

    POST /api/books - Add a new book.

    PUT /api/books/{id} - Update a book.

    DELETE /api/books/{id} - Delete a book.

Authors

    GET /api/authors - Get all authors.

    GET /api/authors/{id} - Get an author by ID.

    POST /api/authors - Add a new author.

    PUT /api/authors/{id} - Update an author.

    DELETE /api/authors/{id} - Delete an author.

Publishers

    GET /api/publishers - Get all publishers.

    GET /api/publishers/{id} - Get a publisher by ID.

    POST /api/publishers - Add a new publisher.

    PUT /api/publishers/{id} - Update a publisher.

    DELETE /api/publishers/{id} - Delete a publisher.

Categories

    GET /api/categories - Get all categories.

    GET /api/categories/{id} - Get a category by ID.

    POST /api/categories - Add a new category.

    PUT /api/categories/{id} - Update a category.

    DELETE /api/categories/{id} - Delete a category.

Book Details

    GET /api/books/{id}/details - Get book details by book ID.

    POST /api/books/{id}/details - Add book details.

    PUT /api/books/{id}/details - Update book details.

    DELETE /api/books/{id}/details - Delete book details.

🛠️ Error Handling

The project includes a global exception handler middleware (GlobalExceptionHandlerMiddleware) to handle errors gracefully. It returns structured error responses for:

    400 Bad Request: Invalid input data.

    404 Not Found: Resource not found.

 
