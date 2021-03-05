using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Restaurant.Axios.Utility
{
    public static class FileUploader
    {
        public static async Task<string> UploadAsync(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueId = Guid.NewGuid().ToString();

            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "foods");

            if (Directory.Exists(uploadPath) is false)
            {
                Directory.CreateDirectory(uploadPath);
            }
            string fileName = $"{uniqueId}-{formFile.FileName}";
            string finalPath = Path.Combine(uploadPath, fileName);

            using FileStream fileStream = new(finalPath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);
            await fileStream.DisposeAsync();

            return fileName;
        }
    }
}
