using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace Movie4U.Utilities
{
    public class CSVUtility
    {
        /// <summary>
        /// Load from CSV file using a class map for custom mapping.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TClassMap">The type of the ClassMap.</typeparam>
        /// <param name="path">The local path of the CSV file.</param>
        /// <returns>An array of TEntity.</returns>
        public static TEntity[] LoadFromCSV<TEntity, TClassMap>(string path)
            where TClassMap: ClassMap<TEntity>
        {
            using(var streamReader = new StreamReader(path))
            {
                using(var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<TClassMap>();

                    var records = csvReader.GetRecords<TEntity>().ToArray();

                    return records;
                }
            }
        }

        /// <summary>
        /// Load from CSV file using the default mapping.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="path">The local path of the CSV file.</param>
        /// <returns>An array of TEntity.</returns>
        public static TEntity[] LoadFromCSV<TEntity>(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<TEntity>().ToArray();

                    return records;
                }
            }
        }

    }
}
