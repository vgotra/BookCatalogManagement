using BCMS.Api.BusinessLayer.Models.Books;
using BCMS.Api.DataAccess.Entities;
using Riok.Mapperly.Abstractions;

namespace BCMS.Api.BusinessLayer.Mappers;

/// <remarks>
/// Riok.Mapperly will generate the implementation for those methods, can be also extensions
/// </remarks>>
[Mapper]
public partial class BookMapper : IBookMapper
{
    [MapperIgnoreTarget(nameof(Book.Id))]
    public partial Book? ToEntity(CreateBookRequest? createModel);

    public partial BookResponse? ToResponse(Book? entity);
}