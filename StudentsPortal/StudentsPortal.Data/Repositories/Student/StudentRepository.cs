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

        public async Task<DataModels.Student> DeleteStudentAsync(Guid studentId)
        {
            var student = await GetStudentAsync(studentId);
            this.context
                .Students
                .Remove(student);

            await this.context.SaveChangesAsync();

            return student;
        }

        public async Task<DataModels.Student> CreateStudentAsync(StudentImportDto importedStudent)
        {
            var student = new DataModels.Student
            {
                FirstName = importedStudent.FirstName,
                LastName = importedStudent.LastName,
                BirthDate = DateTime.Parse(importedStudent.BirthDate),
                Email = importedStudent.Email,
                Phone = importedStudent.Phone,
                GenderId = importedStudent.GenderId
            };

            await this.context.Students.AddAsync(student);

            var studentAddress = new Address
            {
                StudentId = student.Id,
                PhysicalAddress = importedStudent.PhysicalAddress,
                PostalAddress = importedStudent.PostalAddress,
            };
            student.Address = studentAddress;

            await context.SaveChangesAsync();

            return student;
        }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetStudentAsync(studentId);

            if (student == null)
            {
                return false;
            }

            student.ProfileImageUrl = profileImageUrl;
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
