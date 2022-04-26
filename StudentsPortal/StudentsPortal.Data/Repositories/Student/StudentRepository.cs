using Microsoft.EntityFrameworkCore;

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
    }
}
