using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsPortal.Data.Repositories.Image;
using StudentsPortal.Data.Repositories.Student;
using StudentsPortal.Models.Student;

namespace StudentsPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepo;
        private readonly IImageRepository imageRepo;
        private readonly IMapper mapper;

        public StudentController(
            IStudentRepository studentRepo,
            IImageRepository imageRepo,
            IMapper mapper)
        {
            this.studentRepo = studentRepo;
            this.imageRepo = imageRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
            => Ok(this.mapper
                .Map<List<StudentExportDto>>(await this.studentRepo
                .GetStudentsAsync()));

        [HttpGet]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> GetStudent(Guid studentId)
        {
            var student = await this.studentRepo
                .GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<StudentExportDto>(student));
        }

        [HttpPut("{studentId:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid studentId, [FromBody] StudentImportDto updateStudentDto)
        {
            var existingStudent = await this.studentRepo
                .GetStudentAsync(studentId);

            if (existingStudent == null)
            {
                return NotFound();
            }

            return Ok(this.mapper
                .Map<StudentExportDto>(
                    await this.studentRepo
                        .UpdateStudentAsync(studentId, updateStudentDto)));
        }

        [HttpDelete]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid studentId)
        {
            var existingStudent = await this.studentRepo
                .GetStudentAsync(studentId);

            if (existingStudent == null)
            {
                return NotFound();
            }

            return Ok(this.mapper
                .Map<StudentExportDto>(
                    await this.studentRepo
                        .DeleteStudentAsync(studentId)));
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddStudent(StudentImportDto student)
        {
            var createdStudent = await this.studentRepo
                .CreateStudentAsync(student);

            return CreatedAtAction(
                nameof(GetStudent),
                new { studentId = createdStudent.Id },
                this.mapper.Map<StudentExportDto>(createdStudent));
        }

        [HttpPost]
        [Route("{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadStudentImage(Guid studentId, IFormFile profileImage)
        {
            if (await this.studentRepo.GetStudentAsync(studentId) == null)
            {
                return NotFound();
            }

            var imagePath = await this.imageRepo
                .Upload(profileImage, Guid.NewGuid() + Path.GetExtension(profileImage.FileName));

            if (await this.studentRepo.UpdateProfileImage(studentId, imagePath))
            {
                return Ok(imagePath);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");

        }
    }
}
