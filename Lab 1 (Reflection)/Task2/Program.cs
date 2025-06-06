﻿using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;



Console.WriteLine("Enter Assembly path");
string? assemblyPath = Console.ReadLine();
if (assemblyPath == null)
{
    throw new NullReferenceException("Assembly not found");
}
assemblyPath = assemblyPath.Trim();

//@"C:\Users\petro\Documents\GitHub\SDC-Multithrearing-tasks\Lab 1 (Reflection)\ClassLib\bin\Debug\net6.0\ClassLib.dll" for debug
Assembly assembly = Assembly.LoadFrom(assemblyPath);




//Task 2
ShowInfo();

void ShowInfo()
{
    foreach (var type in assembly.GetTypes())
    {
        if (type.IsClass)
        {
            Console.WriteLine($"Class named {type.Name}");

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            string getAccess = string.Empty;
            string setAccess = string.Empty;

            foreach (var prop in properties)
            {
                getAccess = HasGetter(prop);
                setAccess = HasSetter(prop);

                Console.WriteLine($" ({getAccess} Get/{setAccess} Set)  property: {prop.Name} ({prop.PropertyType.Name})");

                Console.WriteLine();
            }

            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            string access = "private ";
            string staticity = string.Empty;

            Console.WriteLine($"Methods ");
            foreach (var method in methods)
            {
                if (method.IsPublic)
                {
                    access = "public ";
                }
                if (method.IsStatic)
                {
                    staticity = "static ";
                }

                Console.WriteLine($"    {access}{staticity} {method.ReturnType} {method.Name}:  method");
                Console.WriteLine($"        Parameters:");
                foreach (var param in method.GetParameters())
                {
                    Console.WriteLine($"        {param.ParameterType} {param.Name}: parameter");
                }

                Console.WriteLine();
            }
        }

    }
}

string HasGetter(PropertyInfo prop)
{
    string getAccess;
    if (prop.GetMethod == null)
    {
        getAccess = "Missing";
    }
    else if (prop.GetMethod.IsPublic == true)
    {
        getAccess = "Public";
    }
    else
    {
        getAccess = "Private";
    }
    return getAccess;
}

string HasSetter(PropertyInfo prop)
{
    string setAccess;
    if (prop.SetMethod == null)
    {
        setAccess = "Missing";
    }
    else if (prop.SetMethod.IsPublic == true)
    {
        setAccess = "Public";
    }
    else
    {
        setAccess = "Private";
    }
    return setAccess;
}