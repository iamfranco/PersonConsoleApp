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

    public Person Clone()
    {
        return new Person()
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Company = Company,
            Address = Address,
            City = City,
            County = County,
            Postal = Postal,
            Phone1 = Phone1,
            Phone2 = Phone2,
            Email = Email,
            Web = Web
        };
    }
}
