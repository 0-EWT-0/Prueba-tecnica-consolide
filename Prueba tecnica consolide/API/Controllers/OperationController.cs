using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OperationController : ControllerBase
    {

        private readonly IOperation _operationService;

        public OperationController(IOperation operationService)
        {
            _operationService = operationService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<CreateOperationResponse>> CreateOperation([FromBody] CreateOperationDTO operationDTO)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            bool couldConvert = int.TryParse(userIdClaim, out int userId);

            if (operationDTO == null || couldConvert == false)
            {
                return BadRequest(new CreateOperationResponse(false, 400, null, "Invalid request"));
            }

            var dto = operationDTO with { UserId = userId};

            var result = await _operationService.CreateOperationAsync(dto);

            return StatusCode(result.Code, result);
        }


        [Authorize]
        [HttpGet("getAll")]
        public async Task<ActionResult<GetAllOperationHistoryResponse>> GetAllOperationHistory(GetAllOperationHistoryDTO getAllOperationHistoryDTO)
        {
            if (getAllOperationHistoryDTO == null)
            {
                return new GetAllOperationHistoryResponse(false, 400, null, "Invalid request");
            }

            var result = await _operationService.GetAllOperationHitoryAsync(getAllOperationHistoryDTO);

            return StatusCode(result.Code, result);
        }
    };
}