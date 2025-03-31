using System.Reflection;
using System.Transactions;



while (true)
{

    Console.WriteLine("Enter class name");

    //stated the class name including namespace
    string className = "Furniture." + Console.ReadLine();

    Type? classType = Type.GetType(className);
    Console.WriteLine(classType);
    if (classType == null)
    {
        Console.WriteLine("There is no such class with that name. Try again");
        continue;
    }

    var typeInstance = CreateInstance(classType);


    Console.WriteLine("Enter method name");
    string? methodName = Console.ReadLine();
    if (methodName == null)
    {
        throw new NullReferenceException();
    }

    MethodInfo? classMethod = classType.GetMethod(methodName);

    if (classMethod == null)
    {
        Console.WriteLine("No such method in chosen class. Try again");
        continue;
    }
    else
    {
        foreach (var param in classMethod.GetParameters())
        {
            Console.WriteLine(param);
        }
        classMethod.Invoke(null, null);
    }
}





object? CreateInstance(Type classType)
{
    ConstructorInfo? ctorChoice = ChooseCtor(classType);
    if (ctorChoice == null)
    {
        throw new NullReferenceException();
    }
    ParameterInfo[] ctorParams = ctorChoice.GetParameters();
    object?[]? parameters = ReturnParams(ctorParams);

    return ctorChoice.Invoke(parameters);
}

ConstructorInfo? ChooseCtor(Type classType)
{
    List<ConstructorInfo> ctors = new List<ConstructorInfo>();

    Console.WriteLine("Now Let's create an instance\n Here are all constructors");
    for(int i = 0; i < classType.GetConstructors().Length; i++)
    {
        ConstructorInfo[] ctorArr = classType.GetConstructors();
        Console.WriteLine($"{i+1}. {ctorArr[i]}");
        ctors.Add(ctorArr[i]);
    }

        Console.WriteLine("Enter the number of the one you want");
    while (true)
    {
        string? choice = Console.ReadLine();
        if (String.IsNullOrEmpty(choice))
        {
            throw new NullReferenceException();
        }
        int choiceInt = int.Parse(choice);

        if (choiceInt < classType.GetConstructors().Length + 1 || choiceInt > 0)
        {
            var ctorChoice = ctors[choiceInt - 1];

            return ctorChoice;
        }
        else
        {
            Console.WriteLine("Choose only present options. Try again");
            continue;
        }
    }
    
}



object?[]? ReturnParams(ParameterInfo[] ctorParams)
{
    List<string> choicesStr = new List<string>();
    List<object> objList = new List<object>();

    for (int i = 0; i < ctorParams.Length; i++)
    {
        Console.WriteLine("Enter " + ctorParams[i].Name);
        string? choice = Console.ReadLine();
        if (String.IsNullOrEmpty(choice))
        {
            return null;
        }
        //converts string to simple ParameterType and adds to return array
        objList.Add(Convert.ChangeType(choice, ctorParams[i].ParameterType));
    }

    return objList.ToArray();
}