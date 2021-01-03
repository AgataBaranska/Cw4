using Cw4.Models;
using System.Collections.Generic;


namespace Cw4.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public Student GetStudent(string index);

        public int AddStudent(Student studentToAdd);

        public int RemoveStudent(string index);

        public int UpdateStudent(Student studentToUpdate);

        public StudentsEnrollment StudentsEnrollments(string index);

    }
}
