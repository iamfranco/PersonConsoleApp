using PersonApp.Models;

namespace PersonApp.Contexts;
public abstract class PersonContextBase
{
    public abstract List<Person> People { get; }

    public abstract void AddPeople(List<Person> people);
}
