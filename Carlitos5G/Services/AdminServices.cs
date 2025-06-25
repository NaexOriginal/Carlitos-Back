 using Carlitos5G.Commons;
using Carlitos5G.Data;
using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuditService _auditService;

        public AdminServices(ApplicationDbContext context, IAuditService auditService)
        {
            _context = context;
            _auditService = auditService;
        }

        public async Task<ServiceResponse<IEnumerable<Admin>>> GetAllAdminsAsync()
        {
            var response = new ServiceResponse<IEnumerable<Admin>>();
            try
            {
                response.Data = await _context.Admins.ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener la lista de administradores.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Admin>> GetAdminByIdAsync(Guid id)
        {
            var response = new ServiceResponse<Admin>();
            try
            {
                var admin = await _context.Admins.FindAsync(id);
                if (admin == null)
                {
                    response.Success = false;
                    response.Message = "Administrador no encontrado.";
                    return response;
                }

                response.Data = admin;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el administrador.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Admin>> CreateAdminAsync(Admin admin)
        {
            var response = new ServiceResponse<Admin>();
            try
            {
                admin.Password = SHA256Helper.ComputeSHA256Hash(admin.Password);

                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();

                var newData = System.Text.Json.JsonSerializer.Serialize(admin);
                await _auditService.LogAsync("Admins", "Create", admin.Id.ToString(), newData, "system");

                response.Data = admin;
                response.Message = "Administrador creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el administrador.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Admin>> UpdateAdminAsync(Admin admin)
        {
            var response = new ServiceResponse<Admin>();
            try
            {
                var existingAdmin = await _context.Admins.AsNoTracking().FirstOrDefaultAsync(a => a.Id == admin.Id);
                if (existingAdmin == null)
                {
                    response.Success = false;
                    response.Message = "Administrador no encontrado.";
                    return response;
                }

                var oldData = System.Text.Json.JsonSerializer.Serialize(existingAdmin);
                admin.Password = SHA256Helper.ComputeSHA256Hash(admin.Password);

                _context.Admins.Update(admin);
                await _context.SaveChangesAsync();

                var newData = System.Text.Json.JsonSerializer.Serialize(admin);
                var changes = $"ANTES: {oldData} \nDESPUÉS: {newData}";

                await _auditService.LogAsync("Admins", "Update", admin.Id.ToString(), changes, "system");

                response.Data = admin;
                response.Message = "Administrador actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el administrador.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAdminAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var admin = await _context.Admins.FindAsync(id);
                if (admin == null)
                {
                    response.Success = false;
                    response.Message = "Administrador no encontrado.";
                    response.Data = false;
                    return response;
                }

                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();

                await _auditService.LogAsync("Admins", "Delete", id.ToString(), null, "system");

                response.Data = true;
                response.Message = "Administrador eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el administrador.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }
            return response;
        }
    }
}
