using Microsoft.AspNetCore.Http;

namespace StudentsPortal.Data.Repositories.Image
{
    public interface IImageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
