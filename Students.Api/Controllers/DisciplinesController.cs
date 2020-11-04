using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Students.Core.Common;
using Students.Core.Models;
using Students.Core.Services.Abstractions;


namespace Students.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DisciplinesController : Controller
    {
        private readonly IDisciplinesService _disciplinesService;


        public DisciplinesController(IDisciplinesService disciplinesService)
        {
            _disciplinesService = disciplinesService;
        }


        [HttpGet("")]
        public async Task<ActionResult<ListApiView<IReadOnlyCollection<DisciplineModel>>>> GetDisciplinesListAsync([FromQuery] Paging paging)
        {
            var result = await _disciplinesService.GetDisciplinesListAsync(paging);
            return StatusCode(result.Status, result);
        }

        [HttpPost("")]
        public async Task<ActionResult<ApiView<DisciplineModel>>> AddDiscilineAsync([FromBody] DisciplineModel model)
        {
            var result = await _disciplinesService.AddDisciplineAsync(model);
            return StatusCode(result.Status, result);
        }
    }
}