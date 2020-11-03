using System.Threading.Tasks;

using Students.Core.Common;
using Students.Core.Models;


namespace Students.Core.Services.Abstractions
{
    public interface IAbsenteeismService
    {
        Task<ApiView<AbsenteeismModel>> AddAbsenteeismAsync(AbsenteeismModel absenteeism);
    }
}