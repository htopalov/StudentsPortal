using AutoMapper;
using StudentsPortal.Data.DataModels;
using StudentsPortal.Models.Address;
using StudentsPortal.Models.Gender;
using StudentsPortal.Models.Student;

namespace StudentsPortal.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Student, StudentExportDto>();

            this.CreateMap<Gender, GenderExportDto>();

            this.CreateMap<Address, AddressExportDto>();

            this.CreateMap<StudentImportDto, Student>();
        }
    }
}
