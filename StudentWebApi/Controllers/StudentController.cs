using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentWebApi.Models;
using StudentWebApi.Repository;

namespace StudentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly ILogger logger;

        public StudentController(IStudentRepository studentRepository, ILogger<StudentController>logger)
        {
            this.studentRepository = studentRepository;
            this.logger = logger;
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Student>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await studentRepository.Search(name, gender);

                if(result.Any())
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetSudents()
        {
            try
            {
                logger.LogInformation("GetStudent was work");
                return Ok(await studentRepository.GetStudents());
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var result = await studentRepository.GetStudent(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    return NotFound();
                }

                var stu = await studentRepository.GetStudentByEmail(student.Email);

                if (stu != null)
                {
                    ModelState.AddModelError("Email", "Student email already is use");
                    return BadRequest(ModelState);
                }

                var createStudent = studentRepository.AddStudent(student);
                return CreatedAtAction(nameof(CreateStudent), new {id = createStudent.Id} , createStudent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Student>> UpdateStudent(Student student, int id)
        {
            try
            {
                if(id != student.Id)
                {
                    return BadRequest("Student ID mismatch");
                }
                
                var studentToUpdate = studentRepository.GetStudent(id);

                if (studentToUpdate == null)
                {
                    return NotFound($"Student with id: {id} not found...");
                }

                return await studentRepository.UpdateStudent(student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating student record");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                var studentToDelete = await studentRepository.GetStudent(id);

                if (studentToDelete == null)
                {
                    return NotFound($"Student with id: {id} not found...");
                }

                await studentRepository.DeleteStudent(id);

                return Ok($"Student with id:{id} was delete");
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting Student record");
            }
        }
        //[HttpGet("{email:string}")]
        //public async Task<ActionResult<Student>> GetStudentByEmail(string email)
        //{
        //    try
        //    {
        //        var result = await studentRepository.GetStudentByEmail(email);

        //        if (result == null)
        //        {
        //            return NotFound();
        //        }

        //        return result;  
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        //    }
        //}
    }
}
