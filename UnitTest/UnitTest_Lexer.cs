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
        [TestMethod]
        public void TestBasic()
        {
            string input = "123 +456*(19- 20)    /7890123456";
            Lexer lexer = new ListLexer(input);

            var tokenList = new List<Token>();

            Token token = lexer.NextToken();

            while (token.Type != Lexer.TYPE_EOF)
            {
                tokenList.Add(token);
                Console.WriteLine(token.ToString());
                token = lexer.NextToken();
            }
            // EOF
            tokenList.Add(token);

            var ar = tokenList.ToArray();

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
