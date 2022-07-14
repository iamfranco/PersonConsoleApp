using PersonApp.CsvParsers;
using PersonApp.Models;
using System.Text.RegularExpressions;

namespace PersonApp.Tests.CsvParsers;
public class SimplePersonCsvParser : IPersonCsvParser
{
    private static string _delimiter = ",";

    public List<Person> Parse(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("No file found matching filepath");

        IEnumerable<string> csvLines = File.ReadLines(filePath);

        string csvHeaderLine = csvLines.First();
        IEnumerable<string> csvBodyLines = csvLines.Skip(1);

        List<Person> people = ParsePeopleFromCsvBodyLines(csvBodyLines, csvHeaderLine);

        return people;
    }

    private List<Person> ParsePeopleFromCsvBodyLines(IEnumerable<string> csvBodyLines, string csvHeaderLine)
    {
        List<string> headerColumnStrings = csvHeaderLine.Split(_delimiter).ToList();

        List<Person> people = new();
        foreach (string csvBodyLine in csvBodyLines)
        {
            if (string.IsNullOrWhiteSpace(csvBodyLine))
                continue;

            Person newPerson = ParseSingleLine(csvBodyLine, headerColumnStrings);

            people.Add(newPerson);
        }

        return people;
    }

    private Person ParseSingleLine(string csvBodyLine, List<string> headerColumns)
    {
        List<string> values = SplitLineByComma(csvBodyLine);

        Person person = new()
        {
            FirstName = GetValueByHeaderLabel("first_name"),
            LastName = GetValueByHeaderLabel("last_name"),
            Company = GetValueByHeaderLabel("company_name"),
            Address = GetValueByHeaderLabel("address"),
            City = GetValueByHeaderLabel("city"),
            County = GetValueByHeaderLabel("county"),
            Postal = GetValueByHeaderLabel("postal"),
            Phone1 = GetValueByHeaderLabel("phone1"),
            Phone2 = GetValueByHeaderLabel("phone2"),
            Email = GetValueByHeaderLabel("email"),
            Web = GetValueByHeaderLabel("web")
        };

        return person;

        string GetValueByHeaderLabel(string headerLabel)
        {
            return values.ElementAt(headerColumns.IndexOf(headerLabel));
        }
    }

    private static List<string> SplitLineByComma(string csvBodyLine)
    {
        string commaSubstitute = "___COMMA___";
        csvBodyLine = Regex.Replace(csvBodyLine, "(?<=\".+),(?=.+\")", commaSubstitute);

        List<string> values = csvBodyLine.Split(_delimiter)
            .Select(x => x.Replace(commaSubstitute, ","))
            .Select(x => x.Replace("\"", ""))
            .ToList();

        return values;
    }
}