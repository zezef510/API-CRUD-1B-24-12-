using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_CRUD_1B.Models;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace API_CRUD_1B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(TodoContext context, ILogger<StudentsController> logger)
        {
            _context = context;
            _logger = logger;


        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetSinhVien()
        {
            return await _context.SinhVien.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.SinhVien.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int ID, [FromBody] Student student)
        {
            try
            {
                if (ID != student.Id)
                {
                    _logger.LogError("Mismatched ID in the URL and the request body.");
                    return BadRequest("Mismatched ID in the URL and the request body.");
                }

                // Kiểm tra sự tồn tại của sinh viên với ID tương ứng
                var existingStudent = await _context.SinhVien.FindAsync(ID);
                if (existingStudent == null)
                {
                    _logger.LogError("Student not found.");
                    return NotFound("Student not found.");
                }

                // Kiểm tra header "Content-Type"
               

                // Cập nhật thông tin sinh viên
                existingStudent.HoTen = student.HoTen;
                existingStudent.GioiTinh = student.GioiTinh;
                existingStudent.Tuoi = student.Tuoi;
                existingStudent.NgaySinh = student.NgaySinh;

                existingStudent.Luong = student.Luong;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw; // Rethrow the exception to propagate it further if needed.
            }
        }





        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.SinhVien.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.SinhVien.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.SinhVien.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.SinhVien.Any(e => e.Id == id);
        }
    }
}
