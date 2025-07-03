using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    public static class Generator
    {
        //Generator naming constants
        const string TablesBasicName = "Table ";
        const string ManufacturersBasicName = "Manufacturers ";

        /// <summary>
        /// Generates Tables with random Manufacturer from collection and basic naming
        /// </summary>
        /// <param name="n"> number of tables </param>
        /// <param name="manufacturers"> List of manufacturers assigned to tables randomly </param>
        /// <returns> Generated Tables List </returns>
        public static List<Table> GenerateTables(int n, List<Manufacturer> manufacturers)
        {
            var tables = new List<Table>();
            var random = new Random();

            for (int i = 1; i <= n; i++)
            {
                var manufacturer = manufacturers[random.Next(manufacturers.Count)];
                tables.Add(new Table
                    (
                        $"{TablesBasicName}{i}",
                        manufacturer
                    )
                );
            }

            return tables;
        }

        /// <summary>
        /// Generates Manufacturers with basic naming
        /// </summary>
        /// <param name="n"> number of manufacturers </param>
        /// <returns> Generated Manufacturers List </returns>
        public static List<Manufacturer> GenerateManufacturers(int n)
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            for (int i = 1; i <= n; i++)
            {
                manufacturers.Add(new Manufacturer($"{ManufacturersBasicName}{i}"));
            }

            return manufacturers;
        }
    }
}
