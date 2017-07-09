using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using NHibernate;
using NLog;
using Practice.Entity;
using Practice.Helper;
using Practice.Helper.XMLParser;
using Practice.IRepository;
using Practice.Repository;

namespace Practice
{
    public partial class Form1 : Form
    {

        private List<Student> studentList;
        private Logger logger;
        private StudentRepository srt;

        public Form1()
        {
            InitializeComponent();

            logger = LogManager.GetCurrentClassLogger();
            srt = new IStudentRepository();
        }

        //load
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            textBox1.Text = filename;

            XMLParser.parse(filename);
            logger.Info("Xml file is found");
            
            studentList = srt.ReadAll();

            viewing(studentList);
            groupnumberbox(studentList);
            
        }

        private void groupnumberbox(List<Student> studentList)
        {
            foreach (var s in (from t in studentList select t.GroupNumber).Distinct())
            {
                comboBox1.Items.Add(s);
            }
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
