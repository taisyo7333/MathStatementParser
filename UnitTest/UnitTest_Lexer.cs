using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

using MathStatementParser.Lexer;

namespace UnitTest
{
    [TestClass]
    public class UnitTest_Lexer
    {
        private Token[] MakeLexicalArray(string input)
        {
            Lexer lexer = new ListLexer(input);
            List<Token> list = new List<Token>();

            Token token = lexer.NextToken();
            while (token.Type != Lexer.TYPE_EOF)
            {
                list.Add(token);
                Trace.WriteLine(token.ToString());
                token = lexer.NextToken();
            }
            //EOF
            list.Add(token);

            return list.ToArray();
        }

        [TestMethod]
        public void TestReal()
        {
            string input = "1.2/123.0+654.321";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(6, ar.Length);

            int index = 0;
            Assert.AreEqual(ListLexer.TYPE_REAL, ar[index].Type);
            Assert.AreEqual("1.2", ar[index].Text);

            index++;
            Assert.AreEqual(ListLexer.TYPE_OPE, ar[index].Type);
            Assert.AreEqual("/", ar[index].Text);

            index++;
            Assert.AreEqual(ListLexer.TYPE_REAL, ar[index].Type);
            Assert.AreEqual("123.0", ar[index].Text);

            index++;
            Assert.AreEqual(ListLexer.TYPE_OPE, ar[index].Type);
            Assert.AreEqual("+", ar[index].Text);

            index++;
            Assert.AreEqual(ListLexer.TYPE_REAL, ar[index].Type);
            Assert.AreEqual("654.321", ar[index].Text);

            index++;
            Assert.AreEqual(ListLexer.TYPE_EOF, ar[index].Type);
        }
        [TestMethod]
        public void TestBasic()
        {
            string input = "123 +456*(19- 20)    /7890123456";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(12, ar.Length);
            int index = 0;
            Assert.AreEqual("123", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual("+", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_OPE, ar[index].Type);

            index++;
            Assert.AreEqual("456", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual("*", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_OPE, ar[index].Type);

            index++;
            Assert.AreEqual("(", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_LPAREN, ar[index].Type);

            index++;
            Assert.AreEqual("19", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual("-", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_OPE, ar[index].Type);

            index++;
            Assert.AreEqual("20", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual(")", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_RPAREN, ar[index].Type);

            index++;
            Assert.AreEqual("/", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_OPE, ar[index].Type);

            index++;
            Assert.AreEqual("7890123456", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual("<EOF>", ar[index].Text);
            Assert.AreEqual(ListLexer.TYPE_EOF, ar[index].Type);
        }
    }
}
