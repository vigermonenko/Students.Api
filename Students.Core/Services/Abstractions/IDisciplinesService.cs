using System.Collections.Generic;

using Students.Core.Common;
using Students.Core.Models;


namespace Students.Core.Services.Abstractions
{
    public interface IDisciplinesService
    {
        ListApiView<IReadOnlyCollection<DisciplineModel>> GetDisciplinesListAsync(Paging paging);

        ApiView<DisciplineModel> AddDisciplineAsync(DisciplineModel discipline);
    }
}