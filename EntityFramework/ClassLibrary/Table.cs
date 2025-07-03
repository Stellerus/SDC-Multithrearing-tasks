namespace ClassLibrary
{
    public class Table
    {
        public Manufacturer Facturer { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public int Id { get; set; }


        public Table(){}
        public Table(string name, Manufacturer facturer)
        {
            Facturer = facturer;
            Name = name;
        }
    }
}
