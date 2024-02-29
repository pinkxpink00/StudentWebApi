using Microsoft.EntityFrameworkCore;
using StudentWebApi.Data;
using StudentWebApi.Models;

namespace StudentWebApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Student> AddStudent(Student student)
        {
            var result = await appDbContext.Students.AddAsync(student);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteStudent(int id)
        {
            var result = await appDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if(result != null)
            {
                appDbContext.Students.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }

        public Task<Student> GetStudend(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> Search(string name, Gender? gender)
        {
            throw new NotImplementedException();
        }

        public Task<Student> UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
