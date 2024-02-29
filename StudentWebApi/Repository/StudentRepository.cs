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

        public async Task<Student> GetStudend(int id)
        {
             return await appDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Student> GetStudentByEmail(string email)
        {
            return await appDbContext.Students.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await appDbContext.Students.ToListAsync();
        }

        public async Task<IEnumerable<Student>> Search(string name, Gender? gender)
        {
            IQueryable<Student> query = appDbContext.Students;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e=>e.FirstName.Contains(name)
                || e.LastName.Contains(name));
            }

            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }

        public Task<Student> UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
