using BikeLibrary;

public class BikeGenerator
{
    public List<Bike> Generate(int count)
    {
        const string manufacturerName = "Bike Co.";
        const string manufacturerAddress = "123 Street";
        var manufacturer = Manufacturer.Create(manufacturerName, manufacturerAddress, false);

        return Enumerable.Range(1, count)
            .Select(i => Bike.Create(
                i,
                $"Bike_{i}",
                $"SN{i}:0000",
                "Mountain",
                manufacturer))
            .ToList();
    }
}
