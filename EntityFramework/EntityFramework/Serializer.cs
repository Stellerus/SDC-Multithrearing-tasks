using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExamTask
{
    public static class Serializer
    {
        // Files naming
        public const string ManufacturerSerializeFile = "manufacturers.xml";
        public const string TableSerializeFile = "tables.xml";

        /// <summary>
        /// Serializes both Manufacturers and Tables to XML
        /// </summary>
        /// <param name="tables"> List of tables for Serialization </param>
        /// <param name="manufacturers"> List of manufacturers for Serialization </param>
        public static void SerializeAllXML(List<Table> tables, List<Manufacturer> manufacturers)
        {
            SerializerManufacturersXML(manufacturers);
            SerializeTablesXML(tables);

        }

        /// <summary>
        /// Serializes Manufacturers to XML
        /// </summary>
        /// <param name="tables"> List of manufacturers for Serialization </param>
        public static void SerializerManufacturersXML(List<Manufacturer> manufacturers)
        {
            var manufacturerSerializer = new XmlSerializer(typeof(List<Manufacturer>));
            using (var writer = new StreamWriter(ManufacturerSerializeFile))
            {
                manufacturerSerializer.Serialize(writer, manufacturers);
            }
        }


        /// <summary>
        /// Serializes and Tables to XML
        /// </summary>
        /// <param name="tables"> List of tables for Serialization </param>
        public static void SerializeTablesXML(List<Table> tables)
        {
            var productSerializer = new XmlSerializer(typeof(List<Table>));
            using (var writer = new StreamWriter(TableSerializeFile))
            {
                productSerializer.Serialize(writer, tables);
            }
        }

        public static List<T> Deserialize<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using var reader = new StreamReader(path);

            return (List<T>)serializer.Deserialize(reader);
        }

    }
}
