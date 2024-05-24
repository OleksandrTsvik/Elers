using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Services;
using Domain.Shared;
using Infrastructure.Files;
using Microsoft.Extensions.Options;

namespace Infrastructure.SupabaseSetup;

public class SupabaseFileService : IFileService
{
    private readonly Supabase.Client _client;
    private readonly string _bucketName;
    private readonly IFileValidator _fileValidator;

    public SupabaseFileService(
        Supabase.Client client,
        IOptions<SupabaseSettingsOptions> supabaseSettingsOptions,
        IFileValidator fileValidator)
    {
        _client = client;
        _bucketName = supabaseSettingsOptions.Value.BucketName;
        _fileValidator = fileValidator;
    }

    public async Task<Result<FileUploadResult>> AddAsync(IFile file)
    {
        Result validationResult = _fileValidator.Validate(file);

        if (validationResult.IsFailure)
        {
            return validationResult.Error;
        }

        string fileName = file.FileName;
        string fileExtension = Path.GetExtension(fileName);
        string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        await _client.Storage
            .From(_bucketName)
            .Upload(memoryStream.ToArray(), uniqueFileName);

        return new FileUploadResult
        {
            FileName = fileName,
            UniqueFileName = uniqueFileName
        };
    }

    public async Task RemoveAsync(string uniqueFileName)
    {
        await _client.Storage
            .From(_bucketName)
            .Remove(uniqueFileName);
    }

    public async Task RemoveRangeAsync(IEnumerable<string> uniqueFileNames)
    {
        await _client.Storage
            .From(_bucketName)
            .Remove(uniqueFileNames.ToList());
    }
}
