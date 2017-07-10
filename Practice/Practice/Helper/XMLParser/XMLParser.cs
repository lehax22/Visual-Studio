using Practice.Entity;
using Practice.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Practice.IRepository;

namespace Practice.Helper.XMLParser
{
    public class XMLParser
    {
        public static void parse(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
            if (fileInfo.Exists)
            {
                if (fileInfo.Name.EndsWith(".xml"))
                {
                    XDocument xdoc = XDocument.Load(filename);

                    List<Student> studentList = xdoc.Descendants("Student").Select(d =>
                     new Student
                     {
                         FirstName = d.Element("FirstName").Value,
                         MiddleName = d.Element("MiddleName").Value,
                         LastName = d.Element("LastName").Value,
                         GroupNumber = d.Element("GroupNumber").Value,
                         Email = d.Element("Email").Value
                     }).ToList();

                    StudentRepository st = new IStudentRepository();
                    //st.DeleteAll();
                    st.AddRange(studentList);

                }
                else
                {
                    MessageBox.Show("No correct name of file");
                    log.Error("No correct name of file");
                }
            }
            else
            {
                MessageBox.Show("File not found");
                log.Error("File not found");
            }

        }
    }
}
