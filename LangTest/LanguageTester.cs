using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace LangTest
{
    [TestClass]
    public class LanguageTester
    {
        [TestMethod]
        public void TestStatementMatching()
        {
            string[] variables = Lang.LangSpec.DivideExpressions("(1,2,3,4)");

            Assert.AreEqual("1", variables[0]);
            Assert.AreEqual("2", variables[1]);
            Assert.AreEqual("3", variables[2]);
            Assert.AreEqual("4", variables[3]);
        }
    }
}
