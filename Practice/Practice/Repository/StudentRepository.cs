using Practice.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Repository
{
    public interface StudentRepository
    {
        void Create(Student student);
        void AddRange(List<Student> studentList);
        Student Read(int ID);
        List<Student> ReadAll();
        void Update(Student student);
        void Delete(int ID);
        //void DeleteAll();
    }
}
