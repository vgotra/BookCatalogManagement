using BCMS.Api.BusinessLayer.Models.Books;
using BCMS.Api.DataAccess.Entities;

namespace BCMS.Api.BusinessLayer.Mappers;

public interface IBookMapper
{
    Book? ToEntity(CreateBookRequest? createModel);

    BookResponse ToResponse(Book entity);
}