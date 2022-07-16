using PersonApp.Models;

namespace PersonApp.StringFormatters;
public interface IPersonStringFormatter
{
    string GetPersonFormattedString(Person person);
}
