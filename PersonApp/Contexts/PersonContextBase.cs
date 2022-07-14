using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApp.Contexts;
public abstract class PersonContextBase
{
    public abstract List<Person> People { get; }

    public abstract void AddPeople(List<Person> people);
}
