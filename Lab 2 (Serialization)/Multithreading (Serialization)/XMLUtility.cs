using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

public static class XmlUtility
{
    private const string BikePath = "bikes.xml";

    public static void ShowBikeValuesXDocument()
    {
        if (!File.Exists(BikePath))
        {
            Console.WriteLine("bikes.xml not found.");
            return;
        }

        XDocument doc = XDocument.Load(BikePath);
        var models = doc.Descendants("Bike");

        Console.WriteLine("\n-- Bikes (XDocument) --");
        foreach (var model in models)
        {
            Console.WriteLine(model.Value);
        }
    }

    public static void ShowBikeValuesXmlDocument()
    {
        if (!File.Exists(BikePath))
        {
            Console.WriteLine("bikes.xml not found.");
            return;
        }

        XmlDocument doc = new XmlDocument();
        doc.Load(BikePath);

        var nodes = doc.GetElementsByTagName("Bike");

        Console.WriteLine("\n-- Bikes (XmlDocument) --");
        foreach (XmlNode node in nodes)
        {
            Console.WriteLine(node.InnerText);
        }
    }

    public static void ModifyWithXDocument(string elementName, int index, string newValue)
    {
        if (!File.Exists(BikePath))
        {
            Console.WriteLine("bikes.xml not found.");
            return;
        }

        var doc = XDocument.Load(BikePath);
        var bikes = doc.Descendants("Bike").ToList();

        if (index < 0 || index >= bikes.Count)
        {
            Console.WriteLine("Invalid index.");
            return;
        }

        var element = bikes[index].Element(elementName);
        if (element != null)
        {
            element.Value = newValue;
            doc.Save(BikePath);
            Console.WriteLine("Value updated using XDocument.");
        }
        else
        {
            Console.WriteLine("Element not found.");
        }
    }

    public static void ModifyWithXmlDocument(string elementName, int index, string newValue)
    {
        if (File.Exists(BikePath))
        {
            Console.WriteLine("bikes.xml not found.");
            return;
        }

        XmlDocument doc = new XmlDocument();
        doc.Load(BikePath);
        var bikeNodes = doc.GetElementsByTagName("Bike");

        if (index < 0 || index >= bikeNodes.Count)
        {
            Console.WriteLine("Invalid index.");
            return;
        }

        var bike = bikeNodes[index];
        var element = bike[elementName];
        if (element != null)
        {
            element.InnerText = newValue;
            doc.Save(BikePath);
            Console.WriteLine("Value updated using XmlDocument.");
        }
        else
        {
            Console.WriteLine("Element not found.");
        }
    }
}
