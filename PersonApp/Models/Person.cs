using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApp.Models;
public class Person
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Company { get; init; }
    public string Address { get; init; }
    public string City { get; init; }
    public string County { get; init; }
    public string Postal { get; init; }
    public string Phone1 { get; init; }
    public string Phone2 { get; init; }
    public string Email { get; init; }
    public string Web { get; init; }
}
