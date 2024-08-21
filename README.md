# InventoryIt

**InventoryIt** is an inventory management system developed with **Blazor Server** and **.NET**. This system enables efficient management of employees, computers, phone lines, phones, and peripherals, providing a comprehensive solution for asset management within your organization.

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
  - [Manual Installation](#manual-installation)
  - [Installation with Docker](#installation-with-docker)
- [Usage](#usage)
- [Contributions](#contributions)
- [License](#license)

## Features

- **Store Information**: Allows you to input and store detailed information about employees, computers, phones, and peripherals.
- **Modify Data**: Enables updating of the information stored in the system.
- **Archive Assets**: Allows you to mark assets as archived when they are no longer in use, keeping the inventory up to date.
- **Assignment and Change History**: Maintains a detailed record of all assignments and modifications made to assets.
- **Document Management**: Supports uploading and storing PDF files directly in the database, as well as downloading them.
- **PDF Generation**: Creates PDF documents to formalize the handover of devices to employees.

## Requirements

- **Database**: SQL Server 2019 or higher
- **.NET SDK**: Version 6.0 or higher
- **Docker** (optional, for deployment in containers)

## Installation

### Manual Installation

1. **Clone this repository**:
    
    ```bash
    
    git clone https://github.com/yourusername/InventoryIt.git
    
    ```
    
2. **Configure the SQL Server connection string** in the `appsettings.json` file.
3. **Run migrations** to create the database:
    
    ```bash
   
    dotnet ef database update
    
    ```
    
4. **Run the application**:
    
    ```bash
    
    dotnet run
    
    ```
    
5. **Access the application** in your browser at http://localhost:5000.

### Installation with Docker

1. **Clone this repository**:
    
    ```bash
    
    git clone https://github.com/yourusername/InventoryIt.git
    
    ```
    
2. **Configure Docker Hub secrets** in your GitHub repository:
    - **DOCKER_USERNAME**: Your Docker Hub username.
    - **DOCKER_PASSWORD**: Your Docker Hub password or access token.
3. **Build and start the containers** with Docker Compose:
    
    ```bash
    bashCopiar código
    docker-compose up --build
    
    ```
    
4. **Access the application** at [http://localhost:8080](http://localhost:8080/).

### Example of `docker-compose.yml`

```yaml

version: '3.8'

services:
  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    container_name: sqlserver
    environment:
      SA_PASSWORD: "YourStrong!Passw0rd"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    networks:
      - blazor_network

  app:
    image: stevengazo/inventoryit:latest
    container_name: blazor_app
    environment:
      ASPNETCORE_ENVIRONMENT: "Development"
      ConnectionStrings__DefaultConnection: "Server=db;Database=YourDatabaseName;User Id=sa;Password=YourStrong!Passw0rd;"
      ConnectionStrings__Inventory: "Server=db;Database=InventoryDB;User Id=sa;Password=YourStrong!Passw0rd;"
    ports:
      - "8080:80"
    depends_on:
      - db
    networks:
      - blazor_network

networks:
  blazor_network:
    driver: bridge

```

## Usage

To use **InventoryIt**, follow the installation and deployment instructions. Once the application is running, you will be able to access the admin panel where you can:

- **Register new employees and assets**.
- **Modify existing information**.
- **Archive assets that are no longer in use**.
- **Generate PDFs for device handover**.
- **Upload and download documents related to assets**.

## Contributions

Contributions are welcome. If you would like to contribute to this project, please follow these steps:

1. **Fork this repository**.
2. **Create a branch** for your new feature or bug fix:
    
    ```bash
    
    git checkout -b feature/new-feature
    
    ```
    
3. **Make your changes** and write descriptive commits.
4. **Submit a pull request** for review and integration into the main project.

## License

This project is licensed under the MIT License. You can review the full terms in the [LICENSE](notion://www.notion.so/LICENSE) file.
