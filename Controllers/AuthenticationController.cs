using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController
    {
        private readonly AuthenticationService _service;
        
        public AuthenticationController(AuthenticationService service)
        {
            _service = service;
        }
        
        // POST: api/Authentication
        [HttpPost]
        public async Task<ActionResult<string>> LoginWithEmailPassword(LoginDto dto)
        {
            return await _service.LoginWithEmailPasswordAsync(dto);
        }
        
        // PUT: api/Authentication
        [HttpPut]
        public async Task<ActionResult<GoogleLoginDto>> LoginWithGoogle(GoogleLoginDto dto)
        {
            return await _service.LoginWithGoogle(dto);
        }
        
        // PATCH: api/Authentication
        [HttpPatch]
        public async Task<ActionResult<PatientDto>> AuthenticatePatientToken(GoogleLoginDto dto)
        {
            return await _service.AuthenticatePatientToken(dto);
        }
    }
}