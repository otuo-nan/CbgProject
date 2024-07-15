# CbgProject 
This project contains the following applications

- Taxi 24 API - enables other companies can use to manage their fleet of drivers and allocate drivers to passengers
- Blazor Application - serving as a UI to showcase API use

## Setup
To make it easier to start project, a docker compose configuration is in the root folder of the project solution

**Requirements** 
-  [Docker Desktop](https://www.docker.com/products/docker-desktop/) <sup(optional)</sup>
- [Microsoft Sql Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) *
- [Visual Studio](https://visualstudio.microsoft.com/downloads/)/ [VSCode](https://code.visualstudio.com/download) *
- [.Net 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) *

** required when projects are started without Docker

Project can be run by: 
1. **Docker**
   - Open a terminal in the root of the project solution then Run the command, `docker compose up -d`
   - API swagger can be found at http://localhost:5134/swagger/index.html
   - Blazor app can be located at http://localhost:5135
   - Other project settings can be found in the compose file

2. **Individual project**
   - Navigate to the root folder of CbgTaxi24.API and/or CbgTaxi24.Blazor then run the command `dotnet run -v q`
   - API swagger can be located at http://localhost:5081/swagger/index.html
   - Blazor app can be located at http://localhost:5207
   
## Usage
To make request to API endpoints you can make use of the **CbgTaxi24.API.http** file found in the root of **CbgTaxi24.API** project folder (NB: code editor required)
