using Microsoft.AspNetCore.Http;

namespace StudentsPortal.Data.Repositories.Image
{
    public class ImageRepository : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using FileStream stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return GetRelativePath(fileName);
        }

        private string GetRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}
