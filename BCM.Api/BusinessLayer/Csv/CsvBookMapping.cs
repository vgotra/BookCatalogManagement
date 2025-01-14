using BCM.Api.DataAccess.Entities;
using TinyCsvParser.Mapping;

namespace BCM.Api.BusinessLayer.Csv;

public class CsvBookMapping : CsvMapping<Book>
{
    public CsvBookMapping()
    {
        MapProperty(0, x => x.Author);
        MapProperty(1, x => x.Title);
        MapProperty(2, x => x.Genre);
    }
}