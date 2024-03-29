﻿using ESADS.Code.EBCS_1995;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ESADS.Code;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for eDetailingTest and is intended
    ///to contain all eDetailingTest Unit Tests
    ///</summary>
    [TestClass()]
    public class eDetailingTest
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
        ///A test for Get_ρ_min
        ///</summary>
        [TestMethod()]
        public void Get_ρ_minTest()
        {
            eStructureType TypeOfStructure = new eStructureType(); // TODO: Initialize to an appropriate value
            eSteelGrade SteelGrade = new eSteelGrade(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eDetailing.Get_ρ_min(TypeOfStructure, SteelGrade);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMinSpacing
        ///</summary>
        [TestMethod()]
        public void GetMinSpacingTest()
        {
            double BiggestBarDia = 0F; // TODO: Initialize to an appropriate value
            double MaxAggSize = 0F; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eDetailing.GetMinSpacing(BiggestBarDia, MaxAggSize);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
