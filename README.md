# Project Name: Linked Gates Device Management System

## Overview

This project is a web application designed to manage and track devices within an IT department. It allows users to list devices, assign properties, and modify properties dynamically based on device categories (e.g., Printer, Laptop, Switch). The project is built using ASP.NET MVC with a focus on the following architectural patterns:

- **Onion Architecture**: For separating layers and ensuring loose coupling between the application core and external dependencies.
- **Unit of Work Pattern**: To manage the consistency and coordination of operations across multiple repositories.
- **Generic Repository Pattern**: To provide a common data access interface that is flexible and reusable for various types of data entities.
- **Searching and Paging**: For efficient device management, the application includes search and pagination features to navigate and filter large datasets.
- **Database Seeding**: Automatically populates the database with initial data upon setup, ensuring the system has default data to work with.

## Features

- **Dynamic Property Assignment**: Device properties are determined by the device category, such as Processor, IP Address, and Brand for Laptops, or Ports and Speed for Switches.
- **Device Management**: Add, edit, and delete devices, along with the ability to assign properties dynamically.
- **Category-Based Management**: Organize devices into categories, each with unique properties.
- **User Management**: Assign devices to specific users.
- **Search and Pagination**: Easily search for devices by name or category and navigate through large datasets using pagination.
- **Database Seeding**: Automatically inserts default categories, properties, and some sample devices into the database on first run, making setup easier and faster.

## Technologies Used

- **ASP.NET MVC**: For implementing the Model-View-Controller design pattern.
- **Entity Framework (EF)**: For object-relational mapping (ORM) with SQL Server.
- **SQL Server**: The backend database for storing device data.
- **C#**: The programming language used for the backend logic.
- **Unit of Work and Generic Repository Pattern**: For managing database transactions and interactions.
- **Onion Architecture**: For organizing the project into layers that promote loose coupling and testability.
- **Database Seeding**: To initialize the database with default values for easy testing and setup.

## Project Structure

The project is organized into several key layers following the Onion Architecture:

1. **Domain Layer**: Contains the core business logic and domain models (e.g., `Device`, `Category`, `Properties`).
2. **Data Access Layer**: Implements the repository pattern and interacts with the database using Entity Framework.
3. **Service Layer**: Contains the business logic services that coordinate data access and apply business rules.
4. **Presentation Layer**: The MVC project responsible for the user interface and controllers.

### Folder Structure

- **Controllers**: Houses the MVC controllers that manage the web requests and route them to appropriate views or services.
- **Models**: Contains the data models used within the MVC framework.
- **Views**: Stores Razor views for the front-end UI.
- **Data**: Holds the Entity Framework configurations, repository implementations, Unit of Work class, and database seeding scripts.
- **Domain**: Contains the business logic, DTOs, Enums, and core models.

## Patterns Used

### Onion Architecture

- Promotes separation of concerns, making the application flexible and easy to maintain.
- Core layers (Domain and Application) are independent of infrastructure concerns (like databases or external services).

### Unit of Work

- Ensures that multiple repository operations are executed under a single transaction, making the database access consistent and reliable.

### Generic Repository

- Provides reusable data access methods (e.g., Add, Update, Delete, GetById, GetAll) that can be applied to any entity in the system.

### Searching and Paging

- **Searching**: Allows users to search for devices by name or category.
- **Paging**: Implements pagination to improve performance and usability when dealing with large datasets, ensuring that the user can navigate through records page by page.

### Database Seeding

- Upon the first run of the application, the database is automatically populated with default values such as:
  - Device categories (e.g., Laptops, Printers, Switches).
  - Default property definitions (e.g., IP Address, Processor, RAM, Ports).
  - Some example devices to quickly test functionality.

## How to Run the Project

1. **Prerequisites**:
   - Visual Studio 2017 or higher.
   - SQL Server 2016 or higher.

2. **Setup**:
   - Clone the repository to your local machine.
   - Open the solution in Visual Studio.
   - Update the connection string in `appsettings.json` to point to your SQL Server instance.
   - Run the database migrations to create the required tables: 
     ```sh
     Update-Database
     ```
   - The database will be seeded with initial data upon the first run.

3. **Running the Application**:
   - Start the application via Visual Studio (Ctrl+F5 or F5).
   - Navigate to the device management page to begin adding and managing devices.
   - Use the search functionality to filter devices, and pagination to browse through the records.

## Entity Models

### Device Model

- `ID`: Unique identifier for each device.
- `Name`: The name of the device.
- `Category`: The category (e.g., Laptop, Printer, Router).
- `AcquisitionDate`: The date the device was acquired.
- `Memo`: A memo field for additional notes.
- `Properties`: A collection of dynamic properties associated with the device.

### Category Model

- `ID`: Unique identifier for each category.
- `Name`: The name of the category (e.g., Printer, Laptop).
- `Properties`: A collection of properties assigned to the category.

### Property Model

- `ID`: Unique identifier for each property.
- `Description`: A brief description of the property (e.g., IP Address, RAM, Processor).

## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Push the branch and submit a pull request.

## License

This project is licensed under the MIT License.

