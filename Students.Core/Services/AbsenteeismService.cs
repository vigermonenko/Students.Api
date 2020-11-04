using System.Net;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Students.Core.Common;
using Students.Core.Models;
using Students.Core.Services.Abstractions;
using Students.Infrastructure;
using Students.Infrastructure.Entities;


namespace Students.Core.Services
{
    public class AbsenteeismService : IAbsenteeismService
    {
        private CoreContext CoreContext => new CoreContext(_contextSettings);

        private readonly ICoreContextSettings _contextSettings;


        public AbsenteeismService(ICoreContextSettings contextSettings)
        {
            _contextSettings = contextSettings;
        }


        public async Task<ApiView<AbsenteeismModel>> AddAbsenteeismAsync(AbsenteeismModel absenteeism)
        {
            await using CoreContext context = CoreContext;

            if (!context.Students.AsNoTracking().Any(s => absenteeism.StudentId == s.Id))
            {
                return new ApiView<AbsenteeismModel>(HttpStatusCode.NotFound)
                {
                    Message = "Student not found.",
                };
            }

            if (!context.Disciplines.AsNoTracking().Any(d => d.Id == absenteeism.DisciplineId))
            {
                return new ApiView<AbsenteeismModel>(HttpStatusCode.NotFound)
                {
                    Message = "Discipline not found."
                };
            }

            Absenteeism absenteeismDto = new Absenteeism
            {
                StudentId = absenteeism.StudentId,
                DisciplineId = absenteeism.DisciplineId,
                Date = absenteeism.Date,
            };

            await context.Absenteeism.AddAsync(absenteeismDto);
            await context.SaveChangesAsync();

            absenteeism.Id = absenteeismDto.Id;

            return new ApiView<AbsenteeismModel>(HttpStatusCode.Created)
            {
                Message = "Ok.",
                Payload = absenteeism,
            };
        }
    }
}