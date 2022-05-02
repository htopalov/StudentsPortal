using Microsoft.EntityFrameworkCore;
using StudentsPortal.Data.DataModels;
using StudentsPortal.Models.Student;

namespace StudentsPortal.Data.Repositories.Student
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentsPortalDbContext context;

        public StudentRepository(StudentsPortalDbContext context) 
            => this.context = context;

        public async Task<List<DataModels.Student>> GetStudentsAsync()
            => await context.Students
                .Include(s=>s.Address)
                .Include(s=>s.Gender)
                .ToListAsync();

        public async Task<DataModels.Student> GetStudentAsync(Guid studentId)
            => await this.context.Students
                .Include(s => s.Address)
                .Include(s => s.Gender)
                .FirstOrDefaultAsync(s => s.Id == studentId);

        public async Task<List<Gender>> GetGendersAsync() 
            => await this.context.Genders
                .ToListAsync();

        public async Task<DataModels.Student> UpdateStudentAsync(Guid studentId, StudentImportDto importedStudent)
        {
            var existingStudent = await GetStudentAsync(studentId);
            existingStudent.FirstName = importedStudent.FirstName;
            existingStudent.LastName = importedStudent.LastName;
            existingStudent.BirthDate = DateTime.Parse(importedStudent.BirthDate);
            existingStudent.Email = importedStudent.Email;
            existingStudent.Phone = importedStudent.Phone;
            existingStudent.Address.PhysicalAddress = importedStudent.PhysicalAddress;
            existingStudent.Address.PostalAddress = importedStudent.PostalAddress;
            existingStudent.GenderId = importedStudent.GenderId;

            this.context.Students.Update(existingStudent);
            await this.context.SaveChangesAsync();
            return existingStudent;
        }
    }
}
