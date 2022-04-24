using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsPortal.Data.Repositories.Student;
using StudentsPortal.Models.Student;

namespace StudentsPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepo;
        private readonly IMapper mapper;

        public StudentController(IStudentRepository studentRepo, IMapper mapper)
        {
            this.studentRepo = studentRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll() 
            => Ok(this.mapper
                .Map<List<StudentExportDto>>(await this.studentRepo
                .GetStudentsAsync()));
    }
}
