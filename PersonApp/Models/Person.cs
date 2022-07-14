using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApp.Models;
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postal { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Email { get; set; }
    public string Web { get; set; }
}
