using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Students.Core.Common;
using Students.Core.Services.Abstractions;

using StudentsView = Students.Core.Common.ListApiView<System.Collections.Generic.IReadOnlyCollection<Students.Core.Models.StudentThumbnailModel>>;



namespace Students.Api.Controllers
{
    [ApiController, Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;


        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }


        [HttpGet("")]
        public async Task<ActionResult<StudentsView>> GetStudentsListAsync([FromQuery] Paging paging)
        {
            var result = await _studentsService.GetStudentsListAsync(paging);
            return StatusCode(result.Status, result);
        }
    }
}