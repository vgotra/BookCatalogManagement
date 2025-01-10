using BCM.Api.BusinessLayer.Models.Books;
using BCM.Api.DataAccess.Entities;

namespace BCM.Api.BusinessLayer.Mappers;

public interface IBookMapper
{
    Book? ToEntity(CreateBookRequest? createModel);

    BookResponse? ToResponse(Book? entity);
}