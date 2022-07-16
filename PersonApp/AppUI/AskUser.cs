using PersonApp.Controllers;
using PersonApp.CsvParsers;
using PersonApp.Models;
using PersonApp.StringFormatters;
using Spectre.Console;

namespace PersonApp.AppUI;
public class AskUser
{
    private PersonController _personController;
    private IPersonStringFormatter _personStringFormatter;
    private Dictionary<string, IPersonCsvParser> _csvParserDictionary;

    public AskUser(PersonController personController, 
        IPersonStringFormatter personStringFormatter,
        Dictionary<string, IPersonCsvParser> csvParserDictionary)
    {
        _personController = personController;
        _personStringFormatter = personStringFormatter;
        _csvParserDictionary = csvParserDictionary;
    }

    public void AskUserToLoadCsvFileOrChangeCsvParser()
    {
        Dictionary<string, Action> optionsActions = new()
        {
            {"Load People from CSV file", AskUserForCsvFilePath },
            {"Change CSV Parser", AskUserToChooseCsvParser },
        };

        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select one of the following options:")
                .AddChoices(optionsActions.Keys)
            );

        Console.Clear();
        AnsiConsole.Markup($"[green]{option}[/]\n");

        optionsActions[option]();
    }

    public void AskUserToChooseCsvParser()
    {
        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select one of the following CSV Parser:")
                .AddChoices(_csvParserDictionary.Keys)
            );

        Console.Clear();
        AnsiConsole.MarkupLine($"Selected CSV Parser: [green]{option}[/]\n");

        IPersonCsvParser personCsvParser = _csvParserDictionary[option];
        _personController.SetPersonCsvParser(personCsvParser);

        AskUserToLoadCsvFileOrChangeCsvParser();
    }

    public void AskUserForCsvFilePath()
    {
        string csvFilePath = AnsiConsole.Ask<string>("Enter .csv file path to load (full path): ");

        try
        {
            _personController.LoadPeopleFromCsvFile(csvFilePath);
        }
        catch (FileLoadException ex)
        {
            AnsiConsole.MarkupLine($"\n[red]PROBLEM: {ex.Message}[/]\n");
            AskUserForCsvFilePath();
        }

        AnsiConsole.MarkupLine("\n.csv file [green]loaded[/].\n");

        AskUserToSelectPeopleFilterOption();
    }

    public void AskUserToSelectPeopleFilterOption()
    {
        Dictionary<string, Func<List<Person>>> optionsFuncs = new Dictionary<string, Func<List<Person>>>()
        {
            {"Get people with company name that has \"Esq\"",
                _personController.GetPeopleWithCompanyNameContainingEsq },
            {"Get people who lives in \"Derbyshire\"",
                _personController.GetPeopleWithCountyDerbyshire },
            {"Get people with three digit house number",
                _personController.GetPeopleWithThreeDigitHouseNumber },
            {"Get people with URL longer than 35 characters",
                _personController.GetPeopleWithUrlLongerThan35Characters },
            {"Get people with single digit postcode area code",
                _personController.GetPeopleWithSingleDigitPostcodeArea },
            {"Get people with first phone number larger than second phone number",
                _personController.GetPeopleWithFirstPhoneNumberLargerThanSecondPhoneNumber }
        };

        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select one of the filtering options below:")
                .AddChoices(optionsFuncs.Keys)
            );

        Console.Clear();
        AnsiConsole.Markup($"[green]{option}[/]\n");

        List<Person> people = optionsFuncs[option]();

        PrintPeople(people);
        AskUserToLoadMoreCsvOrSelectAnotherFilterOption();
    }

    public void AskUserToLoadMoreCsvOrSelectAnotherFilterOption()
    {
        Dictionary<string, Action> optionsActions = new Dictionary<string, Action>()
        {
            {"Select another filter option", AskUserToSelectPeopleFilterOption},
            {"Load more people from .csv file", AskUserForCsvFilePath},
            {"Exit", () => Environment.Exit(0) }
        };

        Console.WriteLine();
        string option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[blue]Select[/] one of the following options to continue:")
                .AddChoices(optionsActions.Keys)
            );

        Console.Clear();

        optionsActions[option]();
    }

    public void PrintPeople(List<Person> people)
    {
        Console.WriteLine($"\nCount: {people.Count}\n");

        foreach (Person person in people)
        {
            Console.WriteLine(_personStringFormatter.GetPersonFormattedString(person));
        }
    }
}
