namespace GamesApi.Api;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public string UploadImage(IFormFile file)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream())
        };

        var uploadResult = _cloudinary.Upload(uploadParams);

        return uploadResult.SecureUri.ToString();
    }
}