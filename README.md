
# SO88822195

**SO88822195** is a Software Operations (SO) project built with .NET 8, demonstrating a hierarchical department structure and an email reminder module. 

## Department Hierarchy Management

### Features

- Hierarchical department structure management.
- Display of sub-departments and parent departments for any selected department.
- Handling of invalid department IDs with appropriate error messages.

### Technologies Used

- .NET 8
- Entity Framework Core
- Bootstrap (default .NET design)

### Getting Started

1. **Clone the repository**

    ```bash
    git clone https://github.com/kyree-henry/SO88822195.git
    cd SO88822195
    ```

2. **Run the application**

    ```bash
    dotnet run --project DepartmentHierarchyManagement
    ```

### Usage

#### Viewing Departments

- The home page displays a list of all top-level departments.
- Click on a department to view its sub-departments and parent departments.

#### Handling Invalid IDs

- If an invalid department ID is provided in the URL, an error message "Invalid department ID" will be displayed.

## Email Reminder Module

### Features

- Implements a `ReminderHub` with a method `SendReminder` that triggers a `ReceiveReminder` event on all connected clients.
- Handles potential circular dependencies between `ReminderHub` and `reminderService`.

### Technologies Used

- .NET 8
- SignalR for real-time communication

### Getting Started

1. **Run the Email Reminder Module**

    ```bash
    dotnet run --project EmailReminderModule
    ```

### Usage

- The `ReminderHub` allows clients to connect and receive reminders.
- Use the `SendReminder` method to trigger the `ReceiveReminder` event on all connected clients.

## Project Structure

- **Department Hierarchy Management**
  - `Controllers/DepartmentsController.cs`: Contains the logic to handle department selection and hierarchy display.
  - `Models/Department.cs`: Defines the `Department` entity.
  - `Services/DepartmentService.cs`: Provides methods to interact with the database.
  - `Views/Departments/Index.cshtml`: The main view for displaying departments and their hierarchy.

- **Email Reminder Module**
  - `Hubs/ReminderHub.cs`: Implements the `ReminderHub` for real-time reminders.
  - `Services/ReminderService.cs`: Provides methods to send reminders and interact with the `ReminderHub`.
