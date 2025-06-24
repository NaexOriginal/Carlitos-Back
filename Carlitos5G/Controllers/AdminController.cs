using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Carlitos5G.Data;
using Carlitos5G.Models;
using Carlitos5G.Services;
using Carlitos5G.Commons;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminService;

        public AdminController(IAdminServices adminService)
        {
            _adminService = adminService;
        }

        // Obtener todos los administradores
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminService.GetAllAdminsAsync();
            return Ok(admins);
        }

        // Obtener un administrador por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(string id)
        {
            var admin = await _adminService.GetAdminByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        // Crear un nuevo administrador
        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] Admin adminDto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new ServiceResponse<Admin>
                {
                    Success = false,
                    Message = "Datos inválidos. Revisa los errores.",
                    ErrorDetails = string.Join(" | ", errorMessages)
                };

                return BadRequest(response);
            }

            try
            {
                var admin = new Admin
                {
                    Id = adminDto.Id,
                    Name = adminDto.Name,
                    Email = adminDto.Email,
                    Password = adminDto.Password
                };

                var result = await _adminService.CreateAdminAsync(admin);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse<Admin>
                {
                    Success = false,
                    Message = "Ocurrió un error al crear el administrador.",
                    ErrorDetails = ex.Message
                });
            }
        }


        // Actualizar un administrador existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(string id, [FromBody] Admin admin)
        {
            if (id != admin.Id.ToString())
            {
                return BadRequest();
            }

            var updatedAdmin = await _adminService.UpdateAdminAsync(admin);
            if (updatedAdmin == null)
            {
                return NotFound();
            }

            return Ok(updatedAdmin);
        }

        // Eliminar un administrador
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            var success = await _adminService.DeleteAdminAsync(id);
            if (success == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
