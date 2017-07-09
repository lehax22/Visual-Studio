using NHibernate;
using Practice.Entity;
using Practice.Helper;
using Practice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice.IRepository
{
    public class IStudentRepository : StudentRepository
    {
        //Add list of students
        public void AddRange(List<Student> studentList)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                try
                {
                    foreach (var s in studentList)
                    {
                        session.Save(s);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }

        //Add student
        public void Create(Student student)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(student);
            }
        }

        //Delete student
        public void Delete(int ID)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Delete(Read(ID));
            }
        }

        public void DeleteAll()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.CreateQuery("DELETE FROM Student").ExecuteUpdate();
            }
        }

        //Read student
        public Student Read(int ID)
        {
            List<Student> studentList = ReadAll();

            return studentList.Find(t => t.ID == ID);
        }

        //Read all of students in list
        public List<Student> ReadAll()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria<Student>();
                IList<Student> studentlist = criteria.List<Student>();

                return (List<Student>)studentlist;
            }
        }

        //Update student
        public void Update(Student student)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.SaveOrUpdate(student);
            }
        }

        public String GetNameStudent(int ID)
        {
            Student st = Read(ID);

            return (st != null) ? st.FirstName : null;
        }
    }
}
