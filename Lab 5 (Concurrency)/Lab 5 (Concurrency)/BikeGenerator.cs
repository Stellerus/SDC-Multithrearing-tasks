using BikeLibrary;

public class BikeGenerator
{
    // Bike naming constants
    const string BikeNamePrefix = "Bike_";
    const string BikeSerialNumberPrefix = "SN";
    const string BikeSerialNumberPostfix = ":0000";
    const string BikeType = "Mountain";

    // Manufacturer naming constants
    const string manufacturerName = "Bike Co.";
    const string manufacturerAddress = "123 Street";

    public List<Bike> Generate(int count)
    {

        var manufacturer = 
            Manufacturer.Create(manufacturerName, manufacturerAddress, false);

        return Enumerable.Range(1, count)
            .Select(i => Bike.Create(
                i,
                $"{BikeNamePrefix}{i}",
                $"{BikeSerialNumberPrefix}{i}{BikeSerialNumberPostfix}",
                BikeType,
                manufacturer))
            .ToList();
    }
}
