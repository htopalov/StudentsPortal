using StudentsPortal.Data.DataModels;
using StudentsPortal.Models.Student;

namespace StudentsPortal.Data.Repositories.Student
{
    public interface IStudentRepository
    {
        Task<List<DataModels.Student>> GetStudentsAsync();

        Task<DataModels.Student> GetStudentAsync(Guid studentId);

        Task<List<Gender>> GetGendersAsync();

        Task<DataModels.Student> UpdateStudentAsync(Guid studentId, StudentImportDto importedStudent);

        Task<DataModels.Student> DeleteStudentAsync(Guid studentId);

        Task<DataModels.Student> CreateStudentAsync(StudentImportDto importedStudent);

        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);
    }
}
