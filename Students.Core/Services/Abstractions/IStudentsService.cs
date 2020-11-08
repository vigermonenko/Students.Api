using System.Collections.Generic;
using System.Threading.Tasks;

using Students.Core.Common;
using Students.Core.Models;


namespace Students.Core.Services.Abstractions
{
    public interface IStudentsService
    {
        Task<ApiView<StudentDetailsModel>> GetStudentsDetailsAsync(int id);

        Task<ListApiView<IReadOnlyCollection<StudentThumbnailModel>>> GetStudentsListAsync(Paging paging);

        Task<ApiView<StudentDetailsModel>> AddStudentAsync(StudentDetailsModel student);

        Task<ApiView<object>> RemoveStudentAsync(int id);

        Task<ApiView<AbsenteeismModel>> AddStudentAbsenAsync(AbsenteeismModel absent);
    }
}