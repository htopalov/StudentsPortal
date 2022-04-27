using StudentsPortal.Data.DataModels;

namespace StudentsPortal.Data.Repositories.Student
{
    public interface IStudentRepository
    {
        Task<List<DataModels.Student>> GetStudentsAsync();

        Task<DataModels.Student> GetStudentAsync(Guid studentId);

        Task<List<Gender>> GetGendersAsync();
    }
}
