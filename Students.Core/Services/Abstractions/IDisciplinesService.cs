using System.Collections.Generic;
using System.Threading.Tasks;
using Students.Core.Common;
using Students.Core.Models;


namespace Students.Core.Services.Abstractions
{
    public interface IDisciplinesService
    {
        Task<ListApiView<IReadOnlyCollection<DisciplineModel>>> GetDisciplinesListAsync(Paging paging);

        Task<ApiView<DisciplineModel>> AddDisciplineAsync(DisciplineModel discipline);
    }
}