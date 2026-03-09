using Patterns_Lab3_Part2;
using Patterns_Lab3_Part2.Expressions;
using System;
using System.Collections.Generic;


string roman = "MCMXLIV";  //1944

var context = new Context(roman);

var tree = new List<RomanExpression>
        {
            new ThousandsExpression(),
            new HundredsExpression(),
            new TensExpression(),
            new OnesExpression()
        };

foreach (var exp in tree)
    exp.Interpret(context);

Console.WriteLine($"{roman} = {context.Output}");
