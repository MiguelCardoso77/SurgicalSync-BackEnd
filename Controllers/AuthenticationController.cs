using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    /**
     * Controller for handling authentication-related operations.
     * Provides endpoints for user login and token authentication.
     */
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController
    {
        private readonly AuthenticationService _service;

        /**
         * Initializes a new instance of the AuthenticationController class.
         *
         * @param service The authentication service used for login and token operations.
         */
        public AuthenticationController(AuthenticationService service)
        {
            _service = service;
        }

        /**
         * Logs in a user using email and password.
         *
         * @param dto The login data transfer object containing user credentials.
         * @return An ActionResult containing the login response as a string.
         */
        // POST: api/Authentication
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> LoginWithEmailPassword(LoginDto dto)
        {
            return await _service.LoginWithEmailPasswordAsync(dto);
        }

        /**
         * Logs in a user using Google authentication.
         *
         * @param dto The Google login data transfer object containing necessary information.
         * @return An ActionResult containing the GoogleLoginDto object.
         */
        // PUT: api/Authentication
        [HttpPut]
        public async Task<ActionResult<GoogleLoginDto>> LoginWithGoogle(GoogleLoginDto dto)
        {
            return await _service.LoginWithGoogle(dto);
        }

        /**
         * Authenticates a patient using a Google token.
         *
         * @param dto The Google login data transfer object containing the authorization code.
         * @return An ActionResult containing the authenticated PatientDto object.
         */
        // PATCH: api/Authentication
        [HttpPatch]
        public async Task<ActionResult<PatientDto>> AuthenticatePatientToken(GoogleLoginDto dto)
        {
            return await _service.AuthenticatePatientToken(dto);
        }
    }
}