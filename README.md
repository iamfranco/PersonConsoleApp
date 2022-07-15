# Mars Rover Challenge

This repository contains a C# console application that allows user to read data of people from a CSV file (for example `input.csv`), and then print a "filtered" list of people on the following filtering options:

1. Every person who has “Esq” in their company name.
2. Every person who lives in “Derbyshire”.
3. Every person whose house number is exactly three digits.
4. Every person whose website URL is longer than 35 characters.
5. Every person who lives in a postcode area with a single-digit value.
6. Every person whose first phone number is numerically larger than their second phone number.

Here we have 3 folders:

1. The `PersonApp` folder contains the C# solution to the challenge
2. The `PersonApp.Tests` folder contains the unit tests for the solution
3. The `diagrams` folder contains diagrams related to the solution

# Instructions

**Prerequisite**: The machine running the application should have [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) (or above) installed.

To run the application:

1. clone the repository to your computer
2. then navigate to the `Person` folder (with `cd` command or otherwise)
3. then run the following command

```c#
dotnet run
```
