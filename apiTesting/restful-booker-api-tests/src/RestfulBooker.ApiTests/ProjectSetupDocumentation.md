1. Install packages dependencies for project:

dotnet add package Microsoft.Data.Sqlite --version 8.0.0
dotnet add package Microsoft.Extensions.Configuration --version 10.0.2
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables --version 10.0.2
dotnet add package Microsoft.Extensions.Configuration.Json --version 10.0.2
dotnet add package Microsoft.NET.Test.Sdk --version 17.12.0
dotnet add package Microsoft.Playwright --version 1.57.0
dotnet add package Microsoft.Playwright.MSTest --version 1.57.0
dotnet add package MSTest --version 3.6.4
dotnet add package MSTest.TestAdapter --version 3.6.4
dotnet add package MSTest.TestFramework --version 3.6.4
dotnet add package Newtonsoft.Json --version 13.0.3
dotnet add package Newtonsoft.Json.Schema --version 3.0.16
dotnet add package SolidToken.SpecFlow.DependencyInjection --version 3.9.3
dotnet add package SpecFlow --version 3.9.74
dotnet add package SpecFlow.MsTest --version 3.9.74
dotnet add package SpecFlow.Tools.MsBuild.Generation --version 3.9.74

2. Clean, Restore and Build the solution:
   dotnet clean
   dotnet restore
   dotnet build

3. Install Database:
   Go to Extensions -> Search DbCode database -> Install and Setup

4. Run Tests
   -> Open the terminal
   -> Go inside project folder by using this command: cd ./Team-3PO/restful-booker-api-tests
   -> Run the command: dotet test
