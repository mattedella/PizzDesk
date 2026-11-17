# PizzDesk

**PizzDesk** is a desktop application designed to help pizzerias manage
their orders and day-to-day operations. It provides tools to create
orders, customize pizzas, manage the menu and ingredients, print
receipts, and generate daily reports.

## Features

-   **Order management** -- create and manage customer orders, with
    products organized by category, quantity, and price.
-   **Menu management** -- add and edit available products and their
    prices.
-   **Pizza customization** -- choose a pizza base and its ingredients,
    with the price updated accordingly.
-   **Ingredient management** -- add, edit, and remove ingredients and
    their prices.
-   **Data persistence** -- store menu items, orders, and related
    information in an SQLite database.
-   **Printing** -- print order receipts and daily reports.
-   **Daily reports** -- generate and view a summary of the day's
    orders.

## Built With

-   C#
-   .NET 10
-   Windows Forms
-   SQLite
-   `Microsoft.Data.Sqlite`

## Requirements

To build and run the project, you need:

-   Windows
-   .NET 10 SDK
-   Visual Studio with the **.NET desktop development** workload, or
    another environment that supports .NET 10 and Windows Forms.

## Getting Started

1.  Clone the repository:

    ``` bash
    git clone https://github.com/USERNAME/REPOSITORY.git
    ```

2.  Open the solution or the `Prenotazione_PIzze.csproj` project file in
    Visual Studio.

3.  Restore the NuGet packages and build the project.

4.  Run the application from Visual Studio, or use the terminal on
    Windows:

    ``` bash
    dotnet restore
    dotnet build
    dotnet run
    ```

## Database

The application uses SQLite. The `pizzeria.db` database is automatically
created and initialized at startup, inside the `Database` folder next to
the executable.

This database stores the application's data. When deploying the
application to another computer, make sure the user has write
permissions for the directory in which the application runs.

## Project Structure

``` text
Prenotazione_PIzze/
└── Prenotazione_PIzze/
    ├── Database/
    │   └── DatabaseManager.cs
    ├── Form1.cs
    ├── Form1.Designer.cs
    ├── GestioneIngredienti.cs
    ├── GestioneIngredienti.Designer.cs
    ├── PersonalizzaPizza.cs
    ├── PersonalizzaPizza.Designer.cs
    ├── SchedaPizza.cs
    ├── Program.cs
    └── Prenotazione_PIzze.csproj
```

-   `Form1.cs` -- main application window and order workflow.
-   `Database/DatabaseManager.cs` -- SQLite database access and
    operations.
-   `GestioneIngredienti.cs` -- ingredient management screen.
-   `PersonalizzaPizza.cs` -- pizza customization screen.
-   `SchedaPizza.cs` -- product card UI component and related logic.

## Project Status

This project is currently under development. Features, the user
interface, and the project structure may change over time.

## Contributing

Suggestions, bug reports, and improvement ideas are welcome. For major
changes, please open an issue first to discuss what you would like to
change.
