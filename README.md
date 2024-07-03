# InventoryIt

InventoryIt is an inventory management system developed with Blazor Server and .NET. This system allows the entry and management of employees, computers, phone lines, phones, and peripherals.

## Features

- **Store basic information**: Allows you to enter and store detailed information about employees, computers, phones, and peripherals.
- **Modify information**: Allows updating the data stored in the system.
- **Archive assets**: Allows marking assets as archived when they are no longer in use.
- **Generate assignment and change history**: Maintains a detailed record of all assignments and changes made.
- **Upload PDF files directly to the database**: Facilitates uploading documents in PDF format.
- **Download stored files**: Allows downloading files stored in the database.
- **Generate PDF for device handover**: Generates PDF documents to formalize the handover of devices to employees.

## Requirements

- **Database**: SQL Server

## Installation

1. Clone this repository:
    ```bash
    git clone https://github.com/yourusername/InventoryIt.git
    ```
2. Configure your SQL Server database connection string in the `appsettings.json` file.
3. Run the migrations to create the database:
    ```bash
    dotnet ef database update
    ```
4. Run the application:
    ```bash
    dotnet run
    ```

## Contributions

Contributions are welcome. Please open an issue or a pull request to discuss any changes you wish to make.

## License

This project is licensed under the MIT License. For more information, see the [LICENSE](LICENSE) file.
