using ESADS.Code.EBCS_1995;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ESADS.Code;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for eServiceabilityTest and is intended
    ///to contain all eServiceabilityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class eServiceabilityTest
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
        ///A test for Get_β_a
        ///</summary>
        [TestMethod()]
        public void Get_β_aTest()
        {
            eStructureType TypeOfStructure = new eStructureType(); // TODO: Initialize to an appropriate value
            eSpanType TypeOfSpan = new eSpanType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eServiceability.Get_β_a(TypeOfStructure, TypeOfSpan);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMinEffDepth
        ///</summary>
        [TestMethod()]
        public void GetMinEffDepthTest()
        {
            eSteelGrade SteelGrade = new eSteelGrade(); // TODO: Initialize to an appropriate value
            double EffectiveSpan = 0F; // TODO: Initialize to an appropriate value
            eSpanType TypeOfSpan = new eSpanType(); // TODO: Initialize to an appropriate value
            eStructureType TypeOfStructure = new eStructureType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eServiceability.GetMinEffDepth(SteelGrade, EffectiveSpan, TypeOfSpan, TypeOfStructure);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
