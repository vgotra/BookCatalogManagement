using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BCMS.Web.Pages;

public partial class UploadCsv(HttpClient http) : ComponentBase
{
    private UploadResult? _uploadResult;
    private bool _shouldRender;

    protected override bool ShouldRender() => _shouldRender;

    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        _shouldRender = false;

        using var content = new MultipartFormDataContent();
        try
        {
            var fileContent = new StreamContent(e.File.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(e.File.ContentType);
            content.Add(content: fileContent, name: "\"file\"", fileName: e.File.Name);

            var response = await http.PostAsync("/api/books/upload", content);
            response.EnsureSuccessStatusCode();

            _uploadResult = new UploadResult { Uploaded = true, FileName = e.File.Name };
        }
        catch
        {
            _uploadResult = null;
        }

        _shouldRender = true;
    }
}