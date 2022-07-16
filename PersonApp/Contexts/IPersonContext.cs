using PersonApp.Models;

namespace PersonApp.Contexts;
public interface IPersonContext
{
    List<Person> People { get; }

    void AddPeople(List<Person> people);
}
