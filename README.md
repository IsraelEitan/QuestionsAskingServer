Poll and Trivia Question Server

Description
  This project is a web server built with .NET Core and Entity Framework Core, designed to handle poll and trivia questions.
  It includes RESTful API endpoints for creating and managing questions, with support for future question types expansion.

Features
  Poll Questions: Questions without correct answers.
  Trivia Questions: Questions with one correct answer, including the correct one in the response.
  Flexible Architecture: Designed for easy addition of new question types.

API Endpoints

  List All Questions: Supports search, filtering, and pagination.
  Output: Questions and answers.
  
  Get Specific Question: Retrieves a single question and its answers.
  Output: Question and answers.
  
  Create New Question: Allows adding a new question.
  Output: Created question ID.
  
  Vote on an Answer: For trivia, indicates if the answer is correct.
  Output: Vote count per answer.
  
Principles and Style
  Caching: For efficient data retrieval.
  Dependency Injection (DI): For decoupling components.
  Object-Oriented Programming (OOP) and Encapsulation: For a maintainable and scalable codebase.
  SOLID Principles and Best Practices: Ensuring robust and efficient code structure.
  Clean Controller API Service: Well-structured and readable code.
  
Running the Program
  Migrations
  To apply the initial database migration, use the following command:

In shell / Package Manager Console
  Copy this code:
    dotnet ef migrations add 20240114140601_InitialCreate
    dotnet ef database update

Starting the Server
  run the program

Testing with Swagger UI
  To test the API endpoints using Swagger UI:

Start the server.
  Navigate to http://localhost:<port>/swagger in your web browser.
  The Swagger UI interface will display all available API endpoints.
  You can test each endpoint directly from the interface by sending requests and viewing responses.
