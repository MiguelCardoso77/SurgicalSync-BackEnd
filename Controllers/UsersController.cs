using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    
   
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        
        public UsersController(UserService service)
        {
            _service = service;
        }
        
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }
        
        // GET: api/Users/U1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(String id)
        {
            var user = await _service.GetByIdAsync(new UserId(id));
            
            if (user == null)
            {
                return NotFound();
            }
            
            return user;
        }
        
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(UserDto dto)
        {
            var user = await _service.AddAsync(dto);
            
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        
        // PUT: api/Users/U5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(String id, UserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            
            try
            {
                var user = await _service.UpdateAsync(dto);
                
                if (user == null)
                {
                    return NotFound();
                }
                
                return user;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE: api/Users/U5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(String id)
        {
            try
            {
                var fam = await _service.DeleteAsync(new UserId(id));

                if (fam == null)
                {
                    return NotFound();
                }

                return Ok(fam);
            }
            catch(Exception ex)
            {
               return BadRequest(new {Message = ex.Message});
            }
        }
        
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (ModelState.IsValid)
                
            {
                var user = await _service.GetUserByEmail(new UserEmail(dto.UserEmail));

                if (user == null)
                {
                    return BadRequest(new { message = "User not found." });
                }
                
                

                // Gera o link de redefinição de senha no Firebase
                var resetLink = await FirebaseService.GeneratePasswordResetLink(user.UserEmail);

                // Envia o e-mail de redefinição de senha
                var smtpEmailService = new EmailService();
                var emailContent = $"Hello {user.UserEmail}! \n Here is the link to reset your password: {resetLink} \n ";
                var email = new Email(emailContent, user.UserEmail.ToString(), "Change your password");
                await smtpEmailService.SendEmailAsync(email);

                return Ok(new { message = "Password reset link sent successfully." });
            }

            return BadRequest(new { message = "Invalid request." });
        }

       
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto user)
        {
            if (string.IsNullOrEmpty(user.UserEmail) || string.IsNullOrEmpty(user.NewPassword) || string.IsNullOrEmpty(user.Token))
            {
                return BadRequest(new { message = "Email, new password, and token are required." });
            }

            try
            {
                await FirebaseService.ResetUserPasswordAsync(user.UserEmail.ToString(), user.NewPassword, user.Token);
                return Ok(new { message = "Password successfully reset." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error resetting password: {ex.Message}" });
            }
        }

        
    }
}