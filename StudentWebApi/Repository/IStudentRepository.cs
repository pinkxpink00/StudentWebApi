using StudentWebApi.Models;

namespace StudentWebApi.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> Search (string name,Gender? gender);
        Task<Student> GetStudend(int id);
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentByEmail (string email);
        Task<Student> AddStudent (Student student);
        Task<Student> UpdateStudent (Student student);
        Task<Student> DeleteStudent (int id);
    }
}
