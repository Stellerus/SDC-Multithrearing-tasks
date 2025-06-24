using BikeLibrary;
using System.Xml.Serialization;

public class BikeSerializer
{
    public void Serialize(List<Bike> bikes, string filePath)
    {
        var serializer = new XmlSerializer(typeof(List<Bike>));
        using var writer = new StreamWriter(filePath);
        serializer.Serialize(writer, bikes);
    }

    public List<Bike> Deserialize(string filePath)
    {
        var serializer = new XmlSerializer(typeof(List<Bike>));
        using var reader = new StreamReader(filePath);
        return (List<Bike>)serializer.Deserialize(reader);
    }
}