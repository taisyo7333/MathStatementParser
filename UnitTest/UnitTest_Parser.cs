using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MathStatementParser;
using MathStatementParser.Lexer;
using MathStatementParser.Parser;

namespace UnitTest
{
    /// <summary>
    /// UnitTest_Parser の概要の説明
    /// </summary>
    [TestClass]
    public class UnitTest_Parser
    {
        public UnitTest_Parser()
        {
            //
            // TODO: コンストラクター ロジックをここに追加します
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
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

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Test_s the parse.
        /// 先読みチェック
        /// </summary>
        [TestMethod]
        public void TestParseMathStatement()
        {
            Lexer lex = new MathLexer("123+(456.0*5)/1.000000007");
            Parser parser = new MathParser(lex);

            Assert.AreEqual(MathLexer.TYPE_NUM, parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_NUM);

            Assert.AreEqual(MathLexer.TYPE_OPE_ADD, parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_OPE_ADD);

            Assert.AreEqual(MathLexer.TYPE_LPAREN,parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_LPAREN);

            Assert.AreEqual(MathLexer.TYPE_REAL, parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_REAL);

            Assert.AreEqual(MathLexer.TYPE_OPE_MUL, parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_OPE_MUL);

            Assert.AreEqual(MathLexer.TYPE_NUM, parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_NUM);

            Assert.AreEqual(MathLexer.TYPE_RPAREN, parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_RPAREN);

            Assert.AreEqual(MathLexer.TYPE_OPE_DIV, parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_OPE_DIV);

            Assert.AreEqual(MathLexer.TYPE_REAL, parser.LOOK_AHEAD.Type);
            parser.Match(MathLexer.TYPE_REAL);

            Assert.AreEqual(MathLexer.TYPE_EOF, parser.LOOK_AHEAD.Type);
        }
        /// <summary>
        /// Tests the parse exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void TestParseException()
        {
            Lexer lex = new MathLexer("123");
            Parser parser = new MathParser(lex);

            // 一致しない字句型を指定すると例外を発行する。
            parser.Match(MathLexer.TYPE_REAL);
        }
        /// <summary>
        /// Tests the parse success.
        /// 数式構文解析の成功試験
        /// </summary>
        [TestMethod]
        public void TestParseSuccess()
        {
            Lexer lex = new MathLexer("123+(456.0*5)/1.000000007");
            Parser parser = new MathParser(lex);

            var result = parser.Test();
            Assert.AreEqual(null, result);
        }
        /// <summary>
        /// 構文解析エラーパターン：構文解析終了後にEOFになっていない場合。
        /// DSLをコードに起こしただけでは検出できない文法エラーを捕捉する。
        /// </summary>
        [TestMethod]
        public void TestParserErrorExit()
        {
            Lexer lex = new MathLexer("123(+5");
            Parser parser = new MathParser(lex);

            var result = parser.Test();
            Assert.AreNotEqual(null, result);
        }
        /// <summary>
        /// Tests the parser error empty.
        /// 構文解析エラーパターン：空文字入力
        /// </summary>
        [TestMethod]
        public void TestParserErrorEmpty()
        {
            Lexer lex = new MathLexer("");
            Parser parser = new MathParser(lex);

            var result = parser.Test();
            Assert.AreNotEqual(null, result);
        }
        /// <summary>
        /// Tests the parser syntax error.
        /// 構文解析エラーパターン：文法エラー
        /// </summary>
        [TestMethod]
        public void TestParserSyntaxError()
        {
            Lexer lex = new MathLexer("123+)");
            Parser parser = new MathParser(lex);

            var result = parser.Test();
            Assert.AreNotEqual(null, result);
        }
        /// <summary>
        /// Tests the parser abstruct syntax tree_ empty.
        /// 空文字列入力テスト
        /// </summary>
        [TestMethod]
        public void TestParserAST_NG_Empty()
        {
            Lexer lexer = new MathLexer("");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreEqual(null, result);
        }
        /// <summary>
        /// Tests the parser Abstruct Syntax Tree occurs syntax error.
        /// </summary>
        [TestMethod]
        public void TestParserAST_NG_SyntaxError()
        {
            Lexer lexer = new MathLexer("");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreEqual(null, result);

        }
        /// <summary>
        /// Tests the parser Abstruct Syntax Tree success in case of addition.
        /// 加算演算子のみを使用した数式をテストする
        /// </summary>
        [TestMethod]
        public void TestParserAST_OK_Add()
        {
            Lexer lexer = new MathLexer("1+23+456+7890");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreNotEqual(null, result);
            var ast = result.ToStringTree();
            Assert.AreEqual("(+ 1 (+ 23 (+ 456 7890)))",ast);
        }
        /// <summary>
        /// Tests the parser Abstruct Syntax Tree success in case of subtraction.   
        /// 減算演算子のみを使用した数式をテストする
        /// </summary>
        [TestMethod]
        public void TestParserAST_OK_Sub()
        {
            Lexer lexer = new MathLexer("1-23-456-7890");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreNotEqual(null, result);
            var ast = result.ToStringTree();
            Assert.AreEqual("(- 1 (- 23 (- 456 7890)))",ast);
        }
        /// <summary>
        /// Tests the parser Abstruct Syntax Tree success in case of multiplication.
        /// 乗算演算子のみを使用した数式をテストする
        /// </summary>
        [TestMethod]
        public void TestParserAST_OK_Mul()
        {
            Lexer lexer = new MathLexer("1*23*456*7890");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreNotEqual(null, result);
            var ast = result.ToStringTree();
            Assert.AreEqual("(* 1 (* 23 (* 456 7890)))", ast);
        }
        /// <summary>
        /// Tests the parser Abstruct Syntax Tree success in case of division.
        /// 除算演算子のみを使用した数式をテストする
        /// </summary>
        [TestMethod]
        public void TestParserAST_OK_Div()
        {
            Lexer lexer = new MathLexer("1/23/456/7890");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreNotEqual(null, result);
            var ast = result.ToStringTree();
            Assert.AreEqual("(/ 1 (/ 23 (/ 456 7890)))", ast);
        }
        /// <summary>
        /// Tests the parser Abstruct Syntax Tree success in case of complex situation.
        /// 複数の優先度が異なる演算子を使用した数式をテストする。
        /// その１。
        /// </summary>
        [TestMethod]
        public void TestParserAST_OK_Priority1()
        {
            Lexer lexer = new MathLexer("1+23*456");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreNotEqual(null, result);
            var ast = result.ToStringTree();
            Assert.AreEqual("(+ 1 (* 23 456))", ast);
        }
        /// <summary>
        /// Tests the parser Abstruct Syntax Tree success in case of complex situation.
        /// 複数の優先度が異なる演算子を使用した数式をテストする。
        /// その２。
        /// </summary>
        [TestMethod]
        public void TestParserAST_OK_Priority2()
        {
            Lexer lexer = new MathLexer("1*23+456");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreNotEqual(null, result);
            var ast = result.ToStringTree();
            Assert.AreEqual("(+ (* 1 23) 456)", ast);
        }
        /// <summary>
        /// Tests the parser Abstruct Syntax Tree success in case of using parenthesis.
        /// 複数の優先度が異なる演算子と()を使用した数式をテストする。
        /// その３。
        /// </summary>
        [TestMethod]
        public void TestParserAST_OK_Priority_Paren()
        {
            Lexer lexer = new MathLexer("1*(23+456)");
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();
            Assert.AreNotEqual(null, result);
            var ast = result.ToStringTree();
            Assert.AreEqual("(* 1 (+ 23 456))", ast);
        }


    }
}
