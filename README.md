# Request-and-Action-Management-System-with-Authorized-User-Relations
# Request and Action Management System with Authorized User Relations

This is a Request and Action Management System designed for managing requests and actions with authorized user roles and relations. The system allows users to create requests, assign actions to users, and track the progress of those actions. The system also includes role-based access control for managing user permissions.

## Features

- **Request Management**: Create and manage requests with descriptions, titles, and statuses.
- **Action Management**: Define actions related to requests, including description, assignee, deadline, and status.
- **Role-Based Access Control**: Different users can have different access levels based on roles (e.g., Admin, User).
- **Authorization**: Use of JWT (JSON Web Tokens) for secure authentication and authorization.
- **Database Integration**: Utilizes SQL-based database (e.g., SQL Server, PostgreSQL) for storing requests, actions, and user data.
- **Entity Relationships**: Requests can have multiple associated actions, and actions are assigned to users.
- **Unit of Work**: Implements the Unit of Work pattern to manage database transactions efficiently and consistently.

## Technologies Used

- **Backend**: 
  - ASP.NET Core
  - Entity Framework Core
  - JWT Authentication
  - Unit of Work Pattern
- **Database**: SQL Server, PostgreSQL (based on your setup)
- **API**: RESTful APIs for managing requests and actions.

## Requirements

- .NET 6.0 or higher
- SQL Server or PostgreSQL
- Visual Studio or Visual Studio Code (for development)
- Postman or any API testing tool (for testing APIs)


