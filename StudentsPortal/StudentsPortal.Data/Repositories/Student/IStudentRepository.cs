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
    }
}
