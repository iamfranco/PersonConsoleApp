using PersonApp.Models;

namespace PersonApp.CsvParsers;
public interface IPersonCsvParser
{
    List<Person> Parse(string filePath);
}
