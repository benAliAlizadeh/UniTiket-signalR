# UniTiket-signalR

## Online Ticketing System

This repository contains an online ticketing system developed as a university project. It leverages modern .NET technologies to provide a real-time ticketing experience. Built with .NET 6, SignalR for real-time communication, and Entity Framework Core for database management, this system allows users to create, manage, and track tickets efficiently. It also includes a real-time chat feature for user interaction.

## Features

- Real-time ticket creation and updates using SignalR
- Real-time chat functionality for user communication
- Database management with Entity Framework Core
- User authentication and authorization
- Responsive web interface
- Support for SQL Server as the database

## Technologies

- **.NET 6**: A cross-platform framework for building modern applications
- **ASP.NET Core**: For building the web application and APIs
- **SignalR**: Enables real-time web functionality for ticket updates and chat
- **Entity Framework Core**: An ORM for database operations
- **SQL Server**: The database used for storing tickets and user data

## Project Purpose

This project was developed as part of a university assignment to explore real-time web applications. The goal was to create an online ticketing system that demonstrates the use of SignalR for real-time updates and chat, alongside Entity Framework Core for efficient database management. It showcases the ability to handle concurrent updates and provide a seamless user experience.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any other database supported by EF Core)
- Visual Studio 2022 or any preferred IDE

## Setup

Follow these steps to set up and run the project:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/benAliAlizadeh/UniTiket-signalR.git
   ```

2. **Restore dependencies**:
   ```bash
   cd UniTiket-signalR
   dotnet restore
   ```

3. **Configure the database**:
   - Ensure SQL Server is running.
   - Update the connection string in `appsettings.json` with your SQL Server details:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=your_server;Database=UniTiketDb;Trusted_Connection=True;"
       }
     }
     ```
   - Run migrations to create the database:
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

4. **Build and run the project**:
   ```bash
   dotnet build
   dotnet run
   ```

5. **Access the application**:
   - Open your browser and navigate to `https://localhost:5001` (or the port specified in your launch settings).
   - Register a user, create tickets, and use the real-time chat feature.

## Usage

- **Ticketing**: After logging in, users can create, view, and manage tickets. Updates to tickets are reflected in real-time across all connected clients.
- **Chat**: The system includes a real-time chat feature, allowing users to communicate instantly using SignalR.
- **Admin Features**: Authorized users can manage tickets and monitor user activity.

## Contributing

Contributions are welcome! Feel free to submit Issues or Pull Requests to improve the project.

## License

This project is licensed under the [MIT License](LICENSE).

## Persian Description (توضیحات فارسی)

این پروژه یه سیستم بلیط‌گذاری آنلاینه که برای یه پروژه دانشگاهی با .NET 6 ساخته شده. از SignalR برای به‌روزرسانی‌های بلادرنگ و چت، و از Entity Framework Core برای مدیریت پایگاه داده استفاده می‌کنه. با SQL Server کار می‌کنه و رابط کاربری پاسخگویی داره. می‌تونید بلیط بسازید، مدیریت کنید و به‌صورت بلادرنگ با بقیه کاربرا چت کنید. برای اطلاعات بیشتر، بخش‌های بالا رو بخونید یا با من تماس بگیرید.

---
Developed by [Ali Alizadeh](https://alizadeh82.ir/)