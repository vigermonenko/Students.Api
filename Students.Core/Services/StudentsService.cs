using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Students.Core.Common;
using Students.Core.Models;
using Students.Infrastructure;
using Students.Infrastructure.Entities;
using Students.Core.Services.Abstractions;


namespace Students.Core.Services
{
    public class StudentsService : IStudentsService
    {
        private CoreContext CoreContext => new CoreContext(_coreContextSettings);

        private readonly ICoreContextSettings _coreContextSettings;


        public StudentsService(ICoreContextSettings contextSettings)
        {
            _coreContextSettings = contextSettings;
        }


        public async Task<ListApiView<IReadOnlyCollection<StudentThumbnailModel>>> GetStudentsListAsync(Paging paging)
        {
            await using CoreContext coreContext = CoreContext;

            IReadOnlyCollection<StudentThumbnailModel> students = await coreContext.Students.AsNoTracking()
                .Skip(paging.Offset).Take(paging.Size)
                .Select(s => new StudentThumbnailModel
                {

                })
                .ToListAsync();

            int totalStudents = coreContext.Students.AsNoTracking().Count();

            return new ListApiView<IReadOnlyCollection<StudentThumbnailModel>>(HttpStatusCode.OK)
            {
                Message = "Ok.",
                Payload = students,
                TotalResults = totalStudents,
                Page = paging.Page,
                PageSize = paging.Size,

            };
        }

        public async Task<ApiView<StudentDetailsModel>> AddStudentAsync(StudentDetailsModel student)
        {
            await using CoreContext coreContext = CoreContext;

            Student studentDto = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Group = student.Group,
            };

            await coreContext.Students.AddAsync(studentDto);
            await coreContext.SaveChangesAsync();

            student.Id = studentDto.Id;

            return new ApiView<StudentDetailsModel>(HttpStatusCode.OK)
            {
                Message = "New student was successfully added.",
                Payload = student,
            };
        }
    }
}