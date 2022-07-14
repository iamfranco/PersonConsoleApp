using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApp.CsvParsers;
public interface IPersonCsvParser
{
    List<Person> Parse(string filePath);
}
