ventHub 🎉

A modern event management platform developed with ASP.NET MVC.

Features

User Management: Registration, login, and profile management

Event Creation: Users can create their own events

Event Categories: Organize events under different categories

Event Participation: Join events and manage participants

Messaging System: User-to-user messaging

Map Integration: Display event locations on a map

Admin Panel: Administrator control dashboard

Scoring System: User rating functionality

Technologies

Framework: ASP.NET MVC 5

ORM: Entity Framework 6.5

Frontend: HTML5, CSS3, JavaScript

Database: SQL Server

Package Management: NuGet

Installation

Requirements:

Visual Studio 2017 or later

.NET Framework 4.7.2+

SQL Server 2014 or later

IIS Express

Installation Steps:

Clone the project: git clone https://github.com/ecemy3/EventHub.git

Open eventhub.sln in Visual Studio

Restore NuGet packages (Solution → Restore NuGet Packages)

Configure the database connection in Web.config

Apply migrations using: Update-Database

Run the project (F5)

Project Structure

Controllers (MVC controllers)
Models (Database models)
Views (Razor views)
Assets (CSS, JS, images)
Migrations (Entity Framework migrations)
App_Start (Application startup configuration)

Core Controllers

HomeController – Homepage and general pages
AuthenticationController – Login & registration
EventController – Event creation and management
ProfileController – User profile
MessageController – Messaging
AdminController – Admin dashboard
MapsController – Map functionalities

Database Models

User
Event
EventCategory
EventMember
Message
MessageDetail
Score

Usage

Start the application

Register or log in

Browse or create events

Join events and message other users

Contributing

Fork → Create feature branch → Commit → Push → Open Pull Request

License

This project was created for educational purposes.
