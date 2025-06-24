using Carlitos5G.Commons;
using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _tutorService;
        private readonly ApplicationDbContext _dbContext;

        public TutorController(ITutorService tutorService, ApplicationDbContext dbContext)
        {
            _tutorService = tutorService;
            _dbContext = dbContext;
        }



        // Obtener todos los tutores
        [HttpGet]
        public async Task<IActionResult> GetAllTutors()
        {
            var tutors = await _tutorService.GetAllTutorsAsync();
            return Ok(tutors);
        }

        // Obtener un tutor por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTutorById(string id)
        {
            var tutor = await _tutorService.GetTutorByIdAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            return Ok(tutor);
        }

        // Crear un nuevo tutor
        [HttpPost]
        public async Task<IActionResult> CreateTutor([FromForm] TutorDto tutorDto)
        {
            try
            {
                var createdTutor = await _tutorService.CreateTutorAsync(tutorDto);
                return CreatedAtAction(nameof(GetTutorById), new { id = createdTutor.Data.Id }, createdTutor.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }


        // Actualizar un tutor
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTutor(string id, [FromForm] TutorDto tutorDto)
        {
            try
            {
                
                tutorDto.IsEdit = true;


                var updatedTutor = await _tutorService.UpdateTutorAsync(id, tutorDto);
                if (updatedTutor == null)
                {
                    return NotFound();
                }
                return Ok(updatedTutor);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }

        // Eliminar un tutor
        [HttpDelete("{id}")]
        public async Task<ServiceResponse<bool>> DeleteTutorAsync(string id)
        {
            var response = new ServiceResponse<bool>();

            if (!Guid.TryParse(id, out Guid tutorId))
            {
                response.Data = false;
                response.Success = false;
                response.Message = "ID de tutor inválido";
                return response;
            }


            var tutor = await _dbContext.Tutors.FindAsync(tutorId);
            if (tutor == null)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "Tutor no encontrado";
                return response;
            }

            _dbContext.Tutors.Remove(tutor);
            await _dbContext.SaveChangesAsync();
            response.Data = true;
            return response;
        }


        // buscar tutores
        [HttpGet("search")]
        public async Task<IActionResult> SearchTutors([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return await GetAllTutors();
            }

            var tutors = await _tutorService.SearchTutorsAsync(term);
            return Ok(tutors);
        }

        [HttpGet("{id}/stats")]
        public async Task<IActionResult> GetTutorStats(string id)
        {
            var stats = await _tutorService.GetTutorStatsAsync(id);
            if (stats == null)
            {
                return NotFound();
            }
            return Ok(stats);
        }
        
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetTutorCourses(string id)
        {
            var courses = await _tutorService.GetTutorCoursesAsync(id);
            return Ok(courses);
        }

        [HttpGet("available-tutors")]
        public async Task<IActionResult> GetAvailableTutors()
        {
            var response = new ServiceResponse<IEnumerable<TutorSimpleDto>>();

            try
            {
                var tutors = await _dbContext.Tutors
                    .Select(t => new TutorSimpleDto
                    {
                        TutorId = t.Id,
                        Name = t.Name,
                        Email = t.Email,
                        Profession = t.Profession
                    })
                    .ToListAsync();

                response.Data = tutors;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener tutores";
                response.ErrorDetails = ex.Message;
                return StatusCode(500, response);
            }
        }

    }
}
