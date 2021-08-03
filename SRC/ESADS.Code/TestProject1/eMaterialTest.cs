using ESADS.Code.EBCS_1995;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ESADS.Code;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for eMaterialTest and is intended
    ///to contain all eMaterialTest Unit Tests
    ///</summary>
    [TestClass()]
    public class eMaterialTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Get_f_yk
        ///</summary>
        [TestMethod()]
        public void Get_f_ykTest()
        {
            eSteelGrade Grade = new eSteelGrade(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eMaterial.Get_f_yk(Grade);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_ctk
        ///</summary>
        [TestMethod()]
        public void Get_f_ctkTest()
        {
            eConcreteGrade Grade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eMaterial.Get_f_ctk(Grade);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_ctk
        ///</summary>
        [TestMethod()]
        public void Get_f_ctkTest1()
        {
            eConcreteGrade Grade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            eConcreteTestSpecimenType TypeOfTestSpecimen = new eConcreteTestSpecimenType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eMaterial.Get_f_ctk(Grade, TypeOfTestSpecimen);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_ck
        ///</summary>
        [TestMethod()]
        public void Get_f_ckTest()
        {
            eConcreteGrade Grade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eMaterial.Get_f_ck(Grade);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_ck
        ///</summary>
        [TestMethod()]
        public void Get_f_ckTest1()
        {
            eConcreteGrade Grade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            eConcreteTestSpecimenType TypeOfTestSpecimen = new eConcreteTestSpecimenType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eMaterial.Get_f_ck(Grade, TypeOfTestSpecimen);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_ConcUnitWeight
        ///</summary>
        [TestMethod()]
        public void Get_ConcUnitWeightTest()
        {
            eConcreteType TypeOfConcrete = new eConcreteType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eMaterial.Get_ConcUnitWeight(TypeOfConcrete);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_ConcModOfElasticity
        ///</summary>
        [TestMethod()]
        public void Get_ConcModOfElasticityTest()
        {
            eConcreteGrade ConcreteGrade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eMaterial.Get_ConcModOfElasticity(ConcreteGrade);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
