using Medinexus.Application.Models;
using Medinexus.Application.Models.Chemist;
using Medinexus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Medinexus.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/chemist")]
    public class ChemistController : ControllerBase
    {
        private readonly IChemistService _chemistService;

        public ChemistController(IChemistService chemistService)
        {
            _chemistService = chemistService;
        }

        #region Chemist APIs

        /// <summary>
        /// Registers a new chemist.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("registerchemist")]
        public async Task<IActionResult> RegisterChemist([FromBody] ChemistRequestModel request)
        {
            try
            {
                ChemistResponseModel? result = await _chemistService.AddNewChemistDetailAsync(request);

                if (result == null)
                    return Ok(CommonResponseModel.ErrorResponse("Failed to register chemist.", statusCode: HttpStatusCode.BadRequest));

                return Ok(CommonResponseModel.SuccessResponse(result, "Chemist registered successfully.", HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return Ok(CommonResponseModel.ExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Gets chemist details by ID.
        /// </summary>
        [HttpGet("getbyid/{chemistId:int}")]
        public async Task<IActionResult> GetChemistById(int chemistId)
        {
            try
            {
                ChemistResponseModel? result = await _chemistService.GetChemistDetailByIdAsync(chemistId);

                if (result == null)
                    return Ok(CommonResponseModel.ErrorResponse($"Chemist not found with ID {chemistId}.", statusCode: HttpStatusCode.NotFound));

                return Ok(CommonResponseModel.SuccessResponse(result, "Chemist details retrieved successfully."));
            }
            catch (Exception ex)
            {
                return Ok(CommonResponseModel.ExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Updates existing chemist details.
        /// </summary>
        [HttpPut("updatechemist")]
        public async Task<IActionResult> UpdateChemist([FromBody] ChemistRequestModel request)
        {
            try
            {
                ChemistResponseModel? result = await _chemistService.UpdateChemistDetailAsync(request);

                if (result == null)
                    return Ok(CommonResponseModel.ErrorResponse($"Failed to update chemist with ID {request.Id}.", statusCode: HttpStatusCode.BadRequest));

                return Ok(CommonResponseModel.SuccessResponse(result, "Chemist details updated successfully."));
            }
            catch (Exception ex)
            {
                return Ok(CommonResponseModel.ExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Deletes a chemist by ID.
        /// </summary>
        [HttpDelete("deletebyid/{chemistId:int}")]
        public async Task<IActionResult> DeleteChemist(int chemistId)
        {
            try
            {
                bool isDeleted = await _chemistService.DeleteChemistDetailAsync(chemistId);

                if (!isDeleted)
                    return Ok(CommonResponseModel.ErrorResponse($"Chemist not found with ID {chemistId}.", statusCode: HttpStatusCode.NotFound));

                return Ok(CommonResponseModel.SuccessResponse(true, "Chemist deleted successfully."));
            }
            catch (Exception ex)
            {
                return Ok(CommonResponseModel.ExceptionResponse(ex));
            }
        }

        #endregion
    }
}
