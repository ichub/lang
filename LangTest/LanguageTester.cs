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
            Regex statement = new Regex(Lang.Script.StatementPattern);

            Assert.IsFalse(statement.Match("").Success, "1");
            Assert.IsFalse(statement.Match("()").Success, "2");
            Assert.IsFalse(statement.Match("(a s () d f").Success, "3");

            Assert.IsTrue(statement.Match("(asdf(a)").Success, "4");
            Assert.IsTrue(statement.Match("(a s d f)").Success, "5");
            Assert.IsTrue(statement.Match("(a s d (a) f").Success, "6");
        }
    }
}
