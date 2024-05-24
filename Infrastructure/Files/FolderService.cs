using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Files;

public class FolderService : IFolderService
{
    private const string FilesFolder = "UploadedData";
    private readonly IWebHostEnvironment _appEnvironment;

    public FolderService(IWebHostEnvironment appEnvironment)
    {
        _appEnvironment = appEnvironment;
    }

    public string GetFolderPath(string folder)
    {
        string folderPath = Path.Combine(_appEnvironment.ContentRootPath, FilesFolder, folder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        return folderPath;
    }
}
