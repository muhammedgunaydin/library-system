# library system

Library management system is written in [.NET](https://learn.microsoft.com/tr-tr/dotnet/welcome) MVC model and [PostgreSql](https://www.postgresql.org/) database.

## Requirements
- [.NET](https://learn.microsoft.com/tr-tr/dotnet/welcome)
- [PostgreSql](https://www.postgresql.org/)
- Code Editor

## Instructions

- Clone the repository and move to the project directory:
  ```bash
    git clone https://github.com/muhammedgunaydin/library-system.git
    cd library-system
  ```
- To load the dependencies:
  ```bash
    dotnet restore
  ```
- To load the migrations:
  ```bash
    dotnet ef migrations add InitialCreate
  ```
- And:
  ```bash
    dotnet ef database update
  ```
- Start:
  ```bash
    dotnet run
  ```
  
## Usage Examples
-You can access other endpoints from comment lines in controllers.

-  Get All Books
  ```bash
    curl -X GET localhost:XXXX/api/book
  ```
-  Get Book (by id)
  ```bash
    curl -X GET localhost:XXXX/api/book/:id
  ```
-  Add Book
  ```bash
    curl -X POST localhost:XXXX/api/book
      '{
          "id" : "3",
          "name": "Test Book",
          "page : 151,
          "isExist": true,
          "authorId":"1"
          "rackId":"1"
         }'
  ```
-  Update Book (by id)
  ```bash
    curl -X PUT localhost:XXXX/api/book/:id
       '{
          "id" : "3",
          "name": "Test Book",
          "page : 210,
          "isExist": true,
          "authorId":"1"
          "rackId":"4"
         }'
  ```
-  Delete Book (by id)
  ```bash
    curl -X DELETE localhost:XXXX/api/book/:id
  ```

-Also you can test with postman instead of curl
