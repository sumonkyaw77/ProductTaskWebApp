# TaskProductWebApplication

A simple ASP.NET Core MVC web application for managing products using Dapper and SQL Server.

## Features

- **Homepage:** Displays a list of products with search functionality (by name or description).
- **Add Product:** Create a new product.
- **Edit Product:** Update product details.
- **Delete Product:** Remove a product.
- **View Details:** See product details in a modal popup.
- **Responsive UI:** Built with Bootstrap.

## Project Structure

- `Controllers/ProductsController.cs` — Handles CRUD and search actions for products.
- `Models/Product.cs` — Product entity definition.
- `Data/ProductRepository.cs` — Dapper-based data access for products.
- `Views/Products/` — MVC views for listing, creating, editing, deleting, and viewing product details.
- `Views/Shared/_Layout.cshtml` — Main layout and navigation.
- `appsettings.json` — Configuration, including database connection string.
- `Program.cs` — Application startup and routing.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio 2022 or later

## Setup & Run

1. **Clone the repository:**



2. **Configure the database:**
- Create a SQL Server database.
- Add your connection string in `appsettings.json`:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;"
  }
  ```
- Create the `Products` table:
  ```sql
  CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Created DATETIME NOT NULL
  );
  ```

3. **Restore NuGet packages:**

4. **Build and run the project:**

Or use Visual Studio: press `F5` to run.

5. **Access the app:**
- Open your browser and go to `https://localhost:5001` (or the port shown in the console).
- The homepage will show the product list. Use the navigation bar to access other pages.

## Usage

- **Search:** Use the search bar to filter products by name or description.
- **Add/Edit/Delete:** Use the buttons in the product list to manage products.
- **Details:** Click the details icon to view product details in a popup.

## Dependencies

- ASP.NET Core MVC
- Dapper
- Bootstrap (for UI)

## Notes

- The default route is set to show the product list on the homepage.
- All data access is handled via Dapper in `ProductRepository`.

---

Feel free to customize this README for your specific needs!
