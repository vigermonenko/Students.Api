using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.Core.Common;
using Students.Core.Models;
using Students.Core.Services.Abstractions;
using Students.Infrastructure;
using Students.Infrastructure.Entities;


namespace Students.Core.Services
{
    public class DisciplinesService : IDisciplinesService
    {
        private CoreContext CoreContext => new CoreContext(_contextSettings);

        private readonly ICoreContextSettings _contextSettings;


        public DisciplinesService(ICoreContextSettings contextSettings)
        {
            _contextSettings = contextSettings;
        }


        public async Task<ListApiView<IReadOnlyCollection<DisciplineModel>>> GetDisciplinesListAsync(Paging paging)
        {
            await using CoreContext context = CoreContext;

            IReadOnlyCollection<DisciplineModel> disciplines = await context.Disciplines.AsNoTracking()
                .Select(d => new DisciplineModel
                {
                    Id = d.Id,
                    Name = d.Name,
                })
                .Skip(paging.Offset).Take(paging.Size)
                .ToListAsync();

            int totalDisciplines = context.Disciplines.AsNoTracking().Count();

            return new ListApiView<IReadOnlyCollection<DisciplineModel>>(HttpStatusCode.OK)
            {
                Message = "Ok.",
                Payload = disciplines,
                Page = paging.Page,
                PageSize = paging.Size,
                TotalResults = totalDisciplines,
            };
        }

        public async Task<ApiView<DisciplineModel>> AddDisciplineAsync(DisciplineModel discipline)
        {
            await using CoreContext context = CoreContext;

            if (context.Disciplines.AsNoTracking().Any(d => d.Id == discipline.Id))
            {
                return new ApiView<DisciplineModel>(HttpStatusCode.BadRequest)
                {
                    Message = "Discipline already exists.",
                };
            }

            Discipline disciplineDto = new Discipline
            {
                Name = discipline.Name,
            };

            await context.Disciplines.AddAsync(disciplineDto);
            await context.SaveChangesAsync();

            discipline.Id = disciplineDto.Id;

            return new ApiView<DisciplineModel>(HttpStatusCode.Created)
            {
                Message = "Discipline added.",
                Payload = discipline,
            };
        }
    }
}