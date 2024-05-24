using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Files;

public class FormFileProxy : IFile
{
    private readonly IFormFile _formFile;

    public string ContentType => _formFile.ContentType;

    public long Length => _formFile.Length;

    public string FileName => _formFile.FileName;

    public FormFileProxy(IFormFile formFile)
    {
        ArgumentNullException.ThrowIfNull(formFile);

        _formFile = formFile;
    }

    public void CopyTo(Stream target)
    {
        _formFile.CopyTo(target);
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        return _formFile.CopyToAsync(target, cancellationToken);
    }

    public Stream OpenReadStream()
    {
        return _formFile.OpenReadStream();
    }
}
