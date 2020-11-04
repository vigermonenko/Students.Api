using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Students.Core.Common;
using Students.Core.Models;
using Students.Core.Services.Abstractions;


namespace Students.Api.Controllers
{
    [ApiController, Route("api/absenteeism")]
    public class AbsenteeismConroller : Controller
    {
        private readonly IAbsenteeismService _absenteeismService;


        public AbsenteeismConroller(IAbsenteeismService absenteeismService)
        {
            _absenteeismService = absenteeismService;
        }


        [HttpPost("")]
        public async Task<ActionResult<ApiView<AbsenteeismModel>>> AddAbsenteeismAsync([FromBody] AbsenteeismModel model)
        {
            var result = await _absenteeismService.AddAbsenteeismAsync(model);
            return StatusCode(result.Status, result);
        }
    }
}