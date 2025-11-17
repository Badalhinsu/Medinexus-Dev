using Medinexus.Application.Models;
using Medinexus.Application.Models.Token;
using Medinexus.Application.Models.User;
using Medinexus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Medinexus.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region User APIs

        /// <summary>
        /// Register a new user
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestModel request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(CommonResponseModel.ErrorResponse(
                        "Invalid request payload.",
                        statusCode: HttpStatusCode.BadRequest));
                }

                UserResponseModel response = await _userService.AddNewUserAsync(request);
                return Ok(CommonResponseModel.SuccessResponse(response, "User created successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, CommonResponseModel.ExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        [Authorize]
        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                UserResponseModel? user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(CommonResponseModel.ErrorResponse("User not found.", statusCode: HttpStatusCode.NotFound));
                }

                return Ok(CommonResponseModel.SuccessResponse(user, "User retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, CommonResponseModel.ExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Update user details
        /// </summary>
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequestModel request)
        {
            try
            {
                if (request == null || request.Id <= 0)
                {
                    return BadRequest(CommonResponseModel.ErrorResponse(
                        "Invalid request data. 'Id' must be provided.",
                        statusCode: HttpStatusCode.BadRequest));
                }

                UserResponseModel? updatedUser = await _userService.UpdateUserDetailAsync(request);
                if (updatedUser == null)
                {
                    return NotFound(CommonResponseModel.ErrorResponse("User not found.", statusCode: HttpStatusCode.NotFound));
                }

                return Ok(CommonResponseModel.SuccessResponse(updatedUser, "User updated successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, CommonResponseModel.ExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Delete a user by ID
        /// </summary>
        [Authorize]
        [HttpDelete("deletebyid/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                bool result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound(CommonResponseModel.ErrorResponse("User not found or could not be deleted.", statusCode: HttpStatusCode.NotFound));
                }

                return Ok(CommonResponseModel.SuccessResponse(true, "User deleted successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, CommonResponseModel.ExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Login user (username or email + password)
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(CommonResponseModel.ErrorResponse(
                        "Username/email and password are required.",
                        statusCode: HttpStatusCode.BadRequest));
                }

                TokenResponseModel? token = await _userService.LoginAsync(request.UserName, request.Password);
                if (token == null)
                {
                    return Unauthorized(CommonResponseModel.ErrorResponse("Invalid credentials.", statusCode: HttpStatusCode.Unauthorized));
                }

                return Ok(CommonResponseModel.SuccessResponse(token, "Login successful."));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, CommonResponseModel.ExceptionResponse(ex));
            }
        }
        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> GetByRefreshToken([FromBody] RefereshTokenRequestModel request)
        {
            var token = await _userService.GetByRefreshTokenAsync(request.RefreshToken);
            if (token == null) return Unauthorized();
            return Ok(token);
        }


        #endregion
    }
}
