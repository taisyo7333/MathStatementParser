using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MathStatementParser;
using MathStatementParser.Lexer;
using MathStatementParser.Parser;
using MathStatementParser.Tree;

namespace UnitTest
{
    [TestClass]
    public class UnitTest_Interpreter
    {
        private AstInterpreter GenInterpreter(string statement)
        {
            Lexer lexer = new MathLexer(statement);
            Parser parser = new MathParser(lexer);
            AstInterpreter interpreter = new AstInterpreter(parser);

            return interpreter;
        }
        [TestMethod]
        public void TestInterp_Integer()
        {
            string statement = "0";

            var ip = GenInterpreter(statement);

            var result = ip.Exec();

            Assert.AreEqual(0.0, result);
        }
        [TestMethod]
        public void TestInterp_Real()
        {
            string statement = "1.0";

            var ip = GenInterpreter(statement);

            var result = ip.Exec();

            Assert.AreEqual(1.0, result);
        }
        [TestMethod]
        public void TestInterp_Add_Int()
        {
            string statement = "2+5";

            var ip = GenInterpreter(statement);

            var result = ip.Exec();

            Assert.AreEqual(7.0, result);
        }
        [TestMethod]
        public void TestInterp_Sub_Int()
        {
            string statement = "2-5";

            var ip = GenInterpreter(statement);

            var result = ip.Exec();

            Assert.AreEqual(-3.0, result);
        }
        [TestMethod]
        public void TestInterp_Mul_Int()
        {
            string statement = "2*5";

            var ip = GenInterpreter(statement);

            var result = ip.Exec();

            Assert.AreEqual(10.0, result);
        }
        [TestMethod]
        public void TestInterp_Div_Int()
        {
            string statement = "2/5";

            var ip = GenInterpreter(statement);

            var result = ip.Exec();

            Assert.AreEqual(0.4, result);
        }
        [TestMethod]
        public void TestInterp_MixOp_Int1()
        {
            string statement = "2*3+4";

            var ip = GenInterpreter(statement);

            var result = ip.Exec();
            Assert.AreEqual(10.0, result);
        }
        [TestMethod]
        public void TestInterp_MixOp_Int2()
        {
            string statement = "2*(3+4)";

            var ip = GenInterpreter(statement);

            var result = ip.Exec();
            Assert.AreEqual(14.0, result);
        }
    }
}
