using ESADS.Code.EBCS_1995;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ESADS.Code;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for eBasisOfDesignTest and is intended
    ///to contain all eBasisOfDesignTest Unit Tests
    ///</summary>
    [TestClass()]
    public class eBasisOfDesignTest
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
        ///A test for Get_f_yd
        ///</summary>
        [TestMethod()]
        public void Get_f_ydTest()
        {
            eSteelGrade Grade = new eSteelGrade(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_yd(Grade, ClassWork);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_yd
        ///</summary>
        [TestMethod()]
        public void Get_f_ydTest1()
        {
            eSteelGrade Grade = new eSteelGrade(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_yd(Grade, ClassWork, DesignSituation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_ctd
        ///</summary>
        [TestMethod()]
        public void Get_f_ctdTest()
        {
            eConcreteGrade Grade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            eConcreteTestSpecimenType TypeOfTestSpecimen = new eConcreteTestSpecimenType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_ctd(Grade, ClassWork, TypeOfTestSpecimen);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_ctd
        ///</summary>
        [TestMethod()]
        public void Get_f_ctdTest1()
        {
            eConcreteGrade Grade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            eConcreteTestSpecimenType TypeOfTestSpecimen = new eConcreteTestSpecimenType(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_ctd(Grade, ClassWork, TypeOfTestSpecimen, DesignSituation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_ctd
        ///</summary>
        [TestMethod()]
        public void Get_f_ctdTest2()
        {
            eConcreteGrade Grade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_ctd(Grade, ClassWork);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_cd
        ///</summary>
        [TestMethod()]
        public void Get_f_cdTest()
        {
            eConcreteGrade ConcreteGrade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_cd(ConcreteGrade, ClassWork, DesignSituation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_cd
        ///</summary>
        [TestMethod()]
        public void Get_f_cdTest1()
        {
            eConcreteGrade ConcreteGrade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            eConcreteTestSpecimenType TypeOfTestSpecimen = new eConcreteTestSpecimenType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_cd(ConcreteGrade, ClassWork, DesignSituation, TypeOfTestSpecimen);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_cd
        ///</summary>
        [TestMethod()]
        public void Get_f_cdTest2()
        {
            eConcreteGrade ConcreteGrade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_cd(ConcreteGrade, ClassWork);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_c
        ///</summary>
        [TestMethod()]
        public void Get_f_cTest()
        {
            eConcreteGrade ConcreteGrade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            double AmountOfStrain = 0F; // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_c(ConcreteGrade, AmountOfStrain, ClassWork);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_c
        ///</summary>
        [TestMethod()]
        public void Get_f_cTest1()
        {
            eConcreteGrade ConcreteGrade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            double AmountOfStrain = 0F; // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_c(ConcreteGrade, AmountOfStrain, ClassWork, DesignSituation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_c
        ///</summary>
        [TestMethod()]
        public void Get_f_cTest2()
        {
            eConcreteGrade ConcreteGrade = new eConcreteGrade(); // TODO: Initialize to an appropriate value
            double AmountOfStrain = 0F; // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            eConcreteTestSpecimenType TypeOfTestSpecimen = new eConcreteTestSpecimenType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_c(ConcreteGrade, AmountOfStrain, ClassWork, DesignSituation, TypeOfTestSpecimen);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Get_f_c
        ///</summary>
        [TestMethod()]
        public void Get_f_cTest3()
        {
            double AmountOfStrain = 0F; // TODO: Initialize to an appropriate value
            double f_cd = 0F; // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.Get_f_c(AmountOfStrain, f_cd);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMaterialPartialSafetyFactor
        ///</summary>
        [TestMethod()]
        public void GetMaterialPartialSafetyFactorTest()
        {
            eLimitStateType TypeOfDesign = new eLimitStateType(); // TODO: Initialize to an appropriate value
            eMaterialType Material = new eMaterialType(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.GetMaterialPartialSafetyFactor(TypeOfDesign, Material, DesignSituation, ClassWork);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMaterialPartialSafetyFactor
        ///</summary>
        [TestMethod()]
        public void GetMaterialPartialSafetyFactorTest1()
        {
            eMaterialType Material = new eMaterialType(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            eClassWork ClassWork = new eClassWork(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.GetMaterialPartialSafetyFactor(Material, DesignSituation, ClassWork);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetActionPartialSafetyFactor
        ///</summary>
        [TestMethod()]
        public void GetActionPartialSafetyFactorTest()
        {
            eActionType ActionType = new eActionType(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.GetActionPartialSafetyFactor(ActionType, DesignSituation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetActionPartialSafetyFactor
        ///</summary>
        [TestMethod()]
        public void GetActionPartialSafetyFactorTest1()
        {
            eActionType ActionType = new eActionType(); // TODO: Initialize to an appropriate value
            eActionCondition ActionCondition = new eActionCondition(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.GetActionPartialSafetyFactor(ActionType, ActionCondition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetActionPartialSafetyFactor
        ///</summary>
        [TestMethod()]
        public void GetActionPartialSafetyFactorTest2()
        {
            eActionType ActionType = new eActionType(); // TODO: Initialize to an appropriate value
            eActionCondition ActionCondition = new eActionCondition(); // TODO: Initialize to an appropriate value
            eDesignSituation DesignSituation = new eDesignSituation(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.GetActionPartialSafetyFactor(ActionType, ActionCondition, DesignSituation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetActionPartialSafetyFactor
        ///</summary>
        [TestMethod()]
        public void GetActionPartialSafetyFactorTest3()
        {
            eActionType ActionType = new eActionType(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = eBasisOfDesign.GetActionPartialSafetyFactor(ActionType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
