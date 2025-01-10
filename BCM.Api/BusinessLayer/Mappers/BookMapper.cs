using BCM.Api.BusinessLayer.Models.Books;
using BCM.Api.DataAccess.Entities;
using Riok.Mapperly.Abstractions;

namespace BCM.Api.BusinessLayer.Mappers;

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