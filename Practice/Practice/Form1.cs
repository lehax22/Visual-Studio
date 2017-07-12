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
            stateOfLoad.Visible = false;
        }

        //load
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            textBox1.Text = filename;
            stateOfLoad.Visible = true;

            SchemaExp.CreateDataBaseSchema();

            stateOfLoad.Text = "Data from the file loaded...";
            XMLParser.parse(filename);

            stateOfLoad.Text = "Completed";
            MessageBox.Show("Данные загружены");
            logger.Info("Xml file is found");
            stateOfLoad.Visible = false;
            
            studentList = srt.ReadAll();

            viewing(studentList);
            groupnumberbox(studentList);
            
        }

        private void groupnumberbox(List<Student> studentList)
        {
            foreach (var s in (from t in studentList select t.GroupNumber).Distinct())
            {
                if (!comboBox1.Items.Contains(s))
                {
                    comboBox1.Items.Add(s);
                }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gr = comboBox1.Text;

            List<Student> list = (from t in studentList where t.GroupNumber.Equals(gr) select t).ToList();

            viewing(list);
        }

    }
}
