using PersonApp.Contexts;
using PersonApp.Models;

namespace PersonApp.Tests.Contexts;
public class PersonContext : PersonContextBase
{
    public override List<Person> People { get; } = new List<Person>();

    public override void AddPeople(List<Person> people)
    {
        if (people is null)
            throw new ArgumentNullException(null, "people should not be null");

        People.AddRange(people);
    }
}