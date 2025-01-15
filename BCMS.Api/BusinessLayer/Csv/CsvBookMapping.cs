using BCMS.Api.DataAccess.Entities;
using TinyCsvParser.Mapping;

namespace BCMS.Api.BusinessLayer.Csv;

public class CsvBookMapping : CsvMapping<Book>
{
    public CsvBookMapping()
    {
        MapProperty(0, x => x.Author);
        MapProperty(1, x => x.Title);
        MapProperty(2, x => x.Genre);
    }
}