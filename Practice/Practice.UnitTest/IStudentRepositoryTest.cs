using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practice.IRepository;
using System.Collections.Generic;
using Practice.Entity;

namespace Practice.UnitTest
{
    [TestClass]
    public class IStudentRepositoryTest
    {
        [TestMethod]
        public void TestReadCase()
        {
            var rep = new IStudentRepository();
            Assert.IsNotNull(rep.Read(2));
        }

        [TestMethod]
        public void TestAddRangeCase()
        {
            var rep = new IStudentRepository();

            try
            {
                rep.AddRange(new List<Student> { new Student() { FirstName = "Дмитрий", MiddleName = "Андреевич", LastName = "Kevlev", GroupNumber = "11-508", Email = "axe@mail.ru" } });
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, "Error in open session");
            }
        }
    }
}
