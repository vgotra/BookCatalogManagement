﻿namespace BCM.Api.BusinessLayer;

public interface IBookBulkImportService
{
    Task ImportBooksAsync(IFormFile file, CancellationToken cancellationToken = default);
}