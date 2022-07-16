using PersonApp.Models;

namespace PersonApp.StringFormatters;
public class PersonStringFormatter : IPersonStringFormatter
{
    public string GetPersonFormattedString(Person person)
    {
        if (person is null)
            throw new ArgumentNullException(null, "person should not be null");

        return $"{person.Id} - {person.FirstName} {person.LastName} - {person.Company}";
    }
}
