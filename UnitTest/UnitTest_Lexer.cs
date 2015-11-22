using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

using MathStatementParser.Lexer;

namespace UnitTest
{
    /// <summary>
    /// UnitTest_Lexer の概要の説明
    /// </summary>
    [TestClass]
    public class UnitTest_Lexer
    {
        /// <summary>
        /// Makes the lexical array.
        /// 入力文字列を字句解析する。
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>字句解析結果</returns>
        private Token[] MakeLexicalArray(string input)
        {
            Lexer lexer = new MathLexer(input);
            List<Token> list = new List<Token>();

            Token token = lexer.NextToken();
            while (token.Type != Lexer.TYPE_EOF)
            {
                list.Add(token);
                Trace.WriteLine(token.ToString());
                token = lexer.NextToken();
            }
            //EOFを追加
            list.Add(token);

            return list.ToArray();
        }
        /// <summary>
        /// Tests the token empty.
        /// 空文字列からなる入力文字列に対する字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenEmpty()
        {
            string input = "";
            var ar = MakeLexicalArray(input);

            int index = 0;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF] , ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the integer.
        /// 整数のみの字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenInteger()
        {
            string input = "123";

            var ar = MakeLexicalArray(input);

            Assert.AreEqual(2, ar.Length);

            int index = 0;
            Assert.AreEqual("123", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_NUM, ar[index].Type);
            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the real.
        /// 実数のみからなる字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenReal()
        {
            string input = "345.123";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(2, ar.Length);

            int index = 0;
            Assert.AreEqual("345.123",ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_REAL,ar[index].Type);
            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the operator plus.
        /// 加算演算子('+')のみからなる字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenOperatorPlus()
        {
            string input = "+";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(2, ar.Length);

            int index = 0;
            Assert.AreEqual("+", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_OPE_ADD, ar[index].Type);
            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the operator minus.
        /// 減算演算子('-')のみからなる字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenOperatorMinus()
        {
            string input = "-";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(2, ar.Length);

            int index = 0;
            Assert.AreEqual("-", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_OPE_SUB, ar[index].Type);
            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the operator multiplication.
        /// 乗算演算子('*')のみからなる字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenOperatorMultiplication()
        {
            string input = "*";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(2, ar.Length);

            int index = 0;
            Assert.AreEqual("*", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_OPE_MUL, ar[index].Type);
            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the operator division.
        /// 除算演算子('/')のみからなる字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenOperatorDivision()
        {
            string input = "/";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(2, ar.Length);

            int index = 0;
            Assert.AreEqual("/", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_OPE_DIV, ar[index].Type);
            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the left paren.
        /// 丸括弧の開始('(')のみからなる字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenLeftParen()
        {
            string input = "(";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(2, ar.Length);

            int index = 0;
            Assert.AreEqual("(", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_LPAREN, ar[index].Type);
            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the right paren.
        /// 丸括弧の終了(')')のみからなる字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestTokenRightParen()
        {
            string input = ")";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(2, ar.Length);

            int index = 0;
            Assert.AreEqual(")", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_RPAREN, ar[index].Type);
            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
        /// <summary>
        /// Tests the lexer exception number.
        /// </summary>
        /// <remarks>\d+\.の後ろには数字が一文字以上入らなければならない。</remarks>
        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void TestLexerExceptionNumber()
        {
            // 字句解析上での書式例外
            string input = "123.";
            var ar = MakeLexicalArray(input);
        }
        /// <summary>
        /// Tests the lexer invalid exception.
        /// </summary>
        /// <remarks>字句解析できない無効な文字が入力されたら例外とする。</remarks>
        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void TestLexerInvalidException()
        {
            // 解釈不可能な文字が入っていると例外を吐く
            string input = "@";
            var ar = MakeLexicalArray(input);
        }

        /// <summary>
        /// Tests the real.
        /// 実数と演算子からなる数式文字列の字句解析テスト
        /// </summary>
        [TestMethod]
        public void TestLexerRealStatement()
        {
            string input = "1.2/123.0+654.321";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(6, ar.Length);

            int index = 0;
            Assert.AreEqual(MathLexer.TYPE_REAL, ar[index].Type);
            Assert.AreEqual("1.2", ar[index].Text);

            index++;
            Assert.AreEqual(MathLexer.TYPE_OPE_DIV, ar[index].Type);
            Assert.AreEqual("/", ar[index].Text);

            index++;
            Assert.AreEqual(MathLexer.TYPE_REAL, ar[index].Type);
            Assert.AreEqual("123.0", ar[index].Text);

            index++;
            Assert.AreEqual(MathLexer.TYPE_OPE_ADD, ar[index].Type);
            Assert.AreEqual("+", ar[index].Text);

            index++;
            Assert.AreEqual(MathLexer.TYPE_REAL, ar[index].Type);
            Assert.AreEqual("654.321", ar[index].Text);

            index++;
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
        }
        /// <summary>
        /// Tests the basic.
        /// 整数、+-*/演算子,()が含まれる入力文字列の字句解析試験
        /// </summary>
        [TestMethod]
        public void TestLexerBasicStatement()
        {
            string input = "123 +456*(19- 20)    /7890123456";
            var ar = MakeLexicalArray(input);

            Assert.AreEqual(12, ar.Length);
            int index = 0;
            Assert.AreEqual("123", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual("+", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_OPE_ADD, ar[index].Type);

            index++;
            Assert.AreEqual("456", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual("*", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_OPE_MUL, ar[index].Type);

            index++;
            Assert.AreEqual("(", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_LPAREN, ar[index].Type);

            index++;
            Assert.AreEqual("19", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual("-", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_OPE_SUB, ar[index].Type);

            index++;
            Assert.AreEqual("20", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual(")", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_RPAREN, ar[index].Type);

            index++;
            Assert.AreEqual("/", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_OPE_DIV, ar[index].Type);

            index++;
            Assert.AreEqual("7890123456", ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_NUM, ar[index].Type);

            index++;
            Assert.AreEqual(MathLexer.tokenNames[MathLexer.TYPE_EOF], ar[index].Text);
            Assert.AreEqual(MathLexer.TYPE_EOF, ar[index].Type);
        }
    }
}
