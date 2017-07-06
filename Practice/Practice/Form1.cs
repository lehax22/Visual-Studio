using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using NHibernate;
using NLog;

namespace Practice
{
    public partial class Form1 : Form
    {

        private List<Student> studentList;

        private Logger logger;

        public Form1()
        {
            InitializeComponent();

            logger = LogManager.GetCurrentClassLogger();

            comboBox1.Items.Add("11-501");
            comboBox1.Items.Add("11-502");
            comboBox1.Items.Add("11-503");
            comboBox1.Items.Add("11-504");
            comboBox1.Items.Add("11-505");
            comboBox1.Items.Add("11-506");
            comboBox1.Items.Add("11-507");
            comboBox1.Items.Add("11-508");
        }

        //load
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            textBox1.Text = filename;

            try
            {
                var doc = XDocument.Load(filename);

                logger.Debug("XML File has been uploaded");

                studentList = doc.Descendants("Student").Select(d =>
                new Student
                {
                    FirstName = d.Element("FirstName").Value,
                    MiddleName = d.Element("MiddleName").Value,
                    LastName = d.Element("LastName").Value,
                    GroupNumber = d.Element("GroupNumber").Value,
                    Email = d.Element("Email").Value
                }).ToList();

                saivingOnBD(studentList);

                viewing(studentList);
            } catch (FileNotFoundException ex)
            {
                logger.Debug(ex.Message);
            } 
            
        }

        //saivingOnBD
        private void saivingOnBD(List<Student> st)
        {
            ISession session = NHibertnateSession.OpenSession();

            logger.Debug("Open session");
            
            foreach(var s in st)
            {
                session.Save(s);
            }
            session.Close();
        }

        //viewing
        private void viewing(List<Student> st)
        {
            listBox1.Items.Clear();
            foreach (var i in st)
            {
                listBox1.Items.Add(i.Email);
            }
        }

        //emails
        private void button3_Click(object sender, EventArgs e)
        {
            string gr = comboBox1.Text;

            List<Student> list = (from t in studentList where t.GroupNumber.Equals(gr) select t).ToList();

            viewing(list);
        }

    }
}
