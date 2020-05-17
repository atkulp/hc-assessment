# Assessment Exercise

Sample _People Finder_ project for pre-employment assessment.

## Setting up

Database will be created and seeded if it's not present and the application is run in Debug configuration.
Seeding occurs using several arrays of name and address parts with randomization.
It's kinda ugly, but it works well enough for the purpose.
It uses a single table, _Assessment_, in a SQL Server Express LocalDB database.
Load the sln into Visual Studio 2019+ and press F5.
You may need to accept the developer HTTPS certificate and approve the network exception for node on first run.

## Trying it out

In the search box, you can search on names as composed in **MockPersonRepository**.
The Search button is disabled if the field is blank or solely whitespace.

To test slow response, include the term "slow".
To test a failure, include the term "err".
Both terms can be present to simulate a slow search that ultimately fails.

Sample first names to try:

- John
- Bob
- Chris
- Paul
- Cindy
- Janet
- Julie
- Diane
- Wilma
- Betty

Sample last names to try:

- Smith
- Johnson
- Jones
- Peters
- Anderson
- Brown
- Gonzalez
- Davis
- Wilson
- Miller

## Testing
A minimal set of tests are present at three levels.

1. Xunit tests for .NET Core code (Visual Studio Test Runner or "dotnet test")
2. Integration (e2e) tests for Angular code ("ng e2e")
3. Unit tests for Angular code ("ng test")

## Contact info
Arian Kulp \
ariankulp@gmail.com \
541-394-0078
