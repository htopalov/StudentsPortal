using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsPortal.Data.Repositories.Student;
using StudentsPortal.Models.Gender;

namespace StudentsPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IStudentRepository studentRepo;
        private readonly IMapper mapper;

        public GenderController(IStudentRepository studentRepo,IMapper mapper)
        {
            this.studentRepo = studentRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
            => Ok(this.mapper
                .Map<List<GenderExportDto>>(await this.studentRepo
                    .GetGendersAsync()));
    }
}
