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


        public async Task<ApiView<StudentDetailsModel>> GetStudentsDetailsAsync(int id)
        {
            await using CoreContext coreContext = CoreContext;

            Student student = await coreContext.Students.AsNoTracking()
                .Include(s => s.Absenteeism)
                .ThenInclude(a => a.Discipline)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return new ApiView<StudentDetailsModel>(HttpStatusCode.NotExtended) { Message = "Student not found." };
            }

            StudentDetailsModel model = new StudentDetailsModel
            {
                Id = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Group = student.Group,
                StudentAbsenteeism = student.Absenteeism
                    .OrderByDescending(a => a.Date)
                    .Select(a => new AbsenteeismModel
                {
                    Id = a.Id,
                    StudentId = id,
                    DisciplineId = a.DisciplineId,
                    Date = a.Date,
                    StudentFullName = $"{student.FirstName} {student.LastName}",
                    DisciplineName = a.Discipline.Name,
                }).ToArray()
            };

            return new ApiView<StudentDetailsModel>(HttpStatusCode.OK)
            {
                Message = "Ok",
                Payload = model,
            };
        }

        public async Task<ListApiView<IReadOnlyCollection<StudentThumbnailModel>>> GetStudentsListAsync(Paging paging)
        {
            await using CoreContext coreContext = CoreContext;

            IReadOnlyCollection<StudentThumbnailModel> students = await coreContext.Students.AsNoTracking()
                .Skip(paging.Offset).Take(paging.Size)
                .Select(s => new StudentThumbnailModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Group = s.Group,
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

        public async Task<ApiView<AbsenteeismModel>> AddStudentAbsenAsync(AbsenteeismModel absent)
        {
            await using CoreContext coreContext = CoreContext;

            Absenteeism absentDto = new Absenteeism
            {
                Date = absent.Date,
                StudentId = absent.StudentId,
                DisciplineId = absent.DisciplineId,
            };

            await coreContext.Absenteeism.AddAsync(absentDto);
            await coreContext.SaveChangesAsync();

            absent.Id = absentDto.Id;

            return new ApiView<AbsenteeismModel>(HttpStatusCode.Created)
            {
                Message = "Absnet was sucessfully added.",
                Payload = absent,
            };
        }

        public async Task<ApiView<object>> RemoveStudentAsync(int id)
        {
            await using CoreContext coreContext = CoreContext;
            Student student = await coreContext.Students.FindAsync(id);

            if (student == null)
            {
                return new ApiView<object>(HttpStatusCode.NotFound)
                {
                    Message = "Student not fount.",
                };
            }

            coreContext.Students.Remove(student);
            await coreContext.SaveChangesAsync();

            return new ApiView<object>(HttpStatusCode.OK)
            {
                Message = "Student was successfully deleted."
            };
        }
    }
}