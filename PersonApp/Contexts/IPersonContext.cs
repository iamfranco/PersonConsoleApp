﻿using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonApp.Contexts;
public interface IPersonContext
{
    void AddPeople(List<Person> people);
}
