using PersonApp.Models;

namespace PersonApp.Contexts;
public class PersonContext : IPersonContext
{
    public List<Person> People { get; } = new List<Person>();

    public void AddPeople(List<Person> people)
    {
        if (people is null)
            throw new ArgumentNullException(null, "people should not be null");

        int lastId = 0;
        if (People.Count > 0)
            lastId = People.Last().Id;

        List<Person> peopleWithId = people.Select((x, index) =>
        {
            Person y = x.Clone();
            y.Id = lastId + index + 1;
            return y;
        }).ToList();

        People.AddRange(peopleWithId);
    }
}
