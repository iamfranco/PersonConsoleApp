using PersonApp.Contexts;
using PersonApp.CsvParsers;
using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonApp.Controllers;
public class PersonController
{
    private PersonContextBase _personContext;
    private IPersonCsvParser _personCsvParser;

    public PersonController(PersonContextBase personContext, IPersonCsvParser personCsvParser)
    {
        if (personContext is null)
            throw new ArgumentNullException(null, "personContext should not be null");

        if (personCsvParser is null)
            throw new ArgumentNullException(null, "personCsvParser should not be null");

        _personContext = personContext;
        _personCsvParser = personCsvParser;
    }

    public void LoadPeopleFromCsvFile(string filePath)
    {
        if (filePath is null)
            throw new ArgumentNullException(null, "filePath should not be null");

        List<Person> people = _personCsvParser.Parse(filePath);
        _personContext.AddPeople(people);
    }

    public List<Person> GetPeopleWithCompanyNameContainingEsq()
    {
        return _personContext.People.Where(
            person => person.Company.Contains("Esq")).ToList();
    }

    public List<Person> GetPeopleWithCountyDerbyshire()
    {
        return _personContext.People.Where(
            person => person.County == "Derbyshire").ToList();
    }

    public List<Person> GetPeopleWithThreeDigitHouseNumber()
    {
        return _personContext.People.Where(
            person => Regex.IsMatch(person.Address, @"^\d{3}\s")).ToList();
    }

    public List<Person> GetPeopleWithUrlLongerThan35Characters()
    {
        return _personContext.People.Where(
            person => person.Web.Length > 35).ToList();
    }

    public List<Person> GetPeopleWithSingleDigitPostcodeArea()
    {
        return _personContext.People.Where(
            person => Regex.IsMatch(person.Postal, @"^[a-zA-Z]+\d\s")).ToList();
    }

    public List<Person> GetPeopleWithFirstPhoneNumberLargerThanSecondPhoneNumber()
    {
        return _personContext.People.Where(
            person => PhoneNumberToLong(person.Phone1) > PhoneNumberToLong(person.Phone2)).ToList();

        long PhoneNumberToLong(string phoneNumber) => long.Parse(phoneNumber.Replace("-", ""));
    }
}
