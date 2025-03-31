using BikeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassExtensions
{
    public static class ClassExtensions
    {
        public static void PrintObject(this object targetClass)
        {
            Type type = targetClass.GetType();
            if (type.IsClass)
            {
                Console.WriteLine($"Class named {type.Name}");

                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    string getAccess = HasGetter(prop);

                    string setAccess = HasSetter(prop);

                    Console.WriteLine($" ({getAccess} Get/{setAccess} Set)  property: {prop.Name} ({prop.PropertyType.Name})");
                }

                //    foreach (var field in type.GetFields())
                //    {
                //        string access;

                //        if (field.IsPublic == true)
                //        {
                //            access = "Public";
                //        }
                //        else
                //        {
                //            access = "Private";
                //        }

                //        Console.WriteLine($" {access} field: {field.Name} ({field.FieldType.Name})");
                //    }

                //    foreach (var ctor in type.GetConstructors())
                //    {
                //        Console.WriteLine($" Constructor: {ctor.Name}");

                //        foreach (var parameter in ctor.GetParameters())
                //        {
                //            Console.WriteLine($" Parameter: ({parameter.ParameterType.Name}) {parameter.Name} ");
                //        }

                //    }
                //    Console.WriteLine();

                //    foreach (var method in type.GetMethods())
                //    {
                //        string access;

                //        if (method.IsPublic == true)
                //        {
                //            access = "Public";
                //        }
                //        else
                //        {
                //            access = "Private";
                //        }

                //        Console.WriteLine($" {access} Method: {method.ReturnParameter.ParameterType.Name} {method.Name}");

                //        foreach (var parameter in method.GetParameters())
                //        {
                //            Console.WriteLine($" Parameter: ({parameter.ParameterType.Name}) {parameter.Name} ");
                //        }

                //    }
                //    Console.WriteLine();
            }

            string HasGetter(PropertyInfo? prop)
            {
                string getAccess;
                if (prop == null)
                {
                    throw new NullReferenceException();
                }
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

            string HasSetter(PropertyInfo? prop)
            {
                string setAccess;
                if (prop == null)
                {
                    throw new NullReferenceException();
                }
                if (prop.GetMethod == null)
                {
                    setAccess = "Missing";
                }
                else if (prop.GetMethod.IsPublic == true)
                {
                    setAccess = "Public";
                }
                else
                {
                    setAccess = "Private";
                }
                return setAccess;
            }
        }
    }
}
