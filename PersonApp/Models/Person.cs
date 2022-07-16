namespace PersonApp.Models;
public class Person
{
    public int Id { get; set; }
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
