@ECHO OFF
:: This file can now be deleted!
:: It was used when setting up the package solution (using https://github.com/LottePitcher/opinionated-package-starter)

:: set up git
git init
git branch -M main
git remote add origin https://github.com/stevetemple/Umbraco.Community.ApplicationInsightsLogging.git

:: ensure latest Umbraco templates used
dotnet new install Umbraco.Templates --force

:: use the umbraco-extension dotnet template to add the package project
cd src
dotnet new umbraco-extension -n "ApplicationInsightsLogging" --site-domain 'https://localhost:44383' --include-example --allow-scripts Yes

:: replace package .csproj with the one from the template so has nuget info
cd ApplicationInsightsLogging
del ApplicationInsightsLogging.csproj
ren ApplicationInsightsLogging_nuget.csproj ApplicationInsightsLogging.csproj

:: add project to solution
cd..
dotnet sln add "ApplicationInsightsLogging"

:: add reference to project from test site
dotnet add "ApplicationInsightsLogging.TestSite/ApplicationInsightsLogging.TestSite.csproj" reference "ApplicationInsightsLogging/ApplicationInsightsLogging.csproj"