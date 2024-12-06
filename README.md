# Asset Tracker
## Description
Asset Tracker is a console application designed to help businesses keep track of their assets, such as laptops, stationary computers, phones, and other products. By maintaining an organized database, companies can monitor the status of their assets throughout their lifecycle. The application provides insights into the condition and age of products, helping businesses to make informed decisions about maintenance and replacements.

## Key Features:
Asset Tracking: Keep track of various company assets, including laptops, computers, phones, and more.
End-of-Life Notification: Products that have reached or are near their end of life are marked clearly. Assets that have reached the end of their lifecycle are highlighted in red, while assets that are about to turn three years old are highlighted in yellow, indicating that the customer should be informed about the product's age.
Currency Conversion: The local prices of assets across different offices are displayed, with prices converted to EURO using an API that fetches the daily conversion rates.

## Tools Used
C#: The primary programming language used for developing the application.
Entity Framework: A powerful Object-Relational Mapping (ORM) framework used for managing the asset tracking database.
SQL Server: The relational database management system used to store the asset data.
Visual studio: The source code is created in Visual studio.

## Database Features
The asset database stores detailed information about each asset, including the asset type, purchase date, status (active or inactive), location (office), and price in the local currency.
End of Life Marking: Products that have exceeded their useful life are highlighted in red for immediate attention, and products that are six months away from being three years old are marked in yellow to notify users to monitor their age.
Currency Conversion: The local price of assets is converted to EURO. The currency conversion is updated daily by fetching real-time rates through an API.

## Getting Started
###Prerequisites
To get started with the Asset Tracker project, you will need the following installed:

Visual Studio (or any C# compatible IDE)
SQL Server or a compatible database system for the project
.NET Core SDK (if not included in your IDE)

### Installation
Clone this repository to your local machine:
  git clone https://github.com/yourusername/asset-tracker.git

Open the solution file (AssetTracker.sln) in Visual Studio.

Restore the project dependencies using NuGet Package Manager.

Set up the SQL Server database using the provided scripts or use Entity Framework to auto-generate the database schema.

Build and run the console application.

### Usage
After starting the console application, the program will display the asset database with the following functionalities:

List all assets with their prices in local currencies.
Highlight products that are approaching their end of life or are already expired.
Convert and display product prices in EURO using real-time exchange rates fetched from an API.

### Future Improvements
Web Interface: Implement a web interface for easier access to the asset database.
Email Notifications: Add email notifications to alert users when products reach their end of life.
Search and Filter: Enhance the system to allow users to search and filter assets based on different criteria (e.g., asset type, office location, age).
