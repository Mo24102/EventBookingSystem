
Event Booking System Backend
Setup Instructions

Prerequisites:

.NET 8 SDK
SQL Server
Visual Studio


Configuration:

Update appsettings.json with your SQL Server connection string.
Ensure the JWT Key is secure and unique.


Database Setup:

Run dotnet ef migrations add EventBookingSystem.
Run dotnet ef database update to create the database.


Running the API:

Run dotnet run from the project root.
The API will be available at https://localhost:7221.


API Endpoints:

Auth: POST /api/auth/register, POST /api/auth/login
Events: GET /api/events, POST /api/events
Bookings: POST /api/bookings



=======
# EventBookingSystem
>>>>>>> d79381b03d92883b2dfd7e0ef1d6c019f4c17fa0
