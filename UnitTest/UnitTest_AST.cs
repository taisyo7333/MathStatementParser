using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MathStatementParser;
using MathStatementParser.Tree;
using MathStatementParser.Lexer;

namespace UnitTest
{
    /// <summary>
    /// UnitTest_AST の概要の説明
    /// </summary>
    [TestClass]
    public class UnitTest_AST
    {
        public UnitTest_AST()
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
        /// Tests single operater node and number nodes.
        /// </summary>
        [TestMethod]
        public void TestAST_Single_Add()
        {
            AbstractSyntaxTree root = new AbstractSyntaxTree(null);

            AbstractSyntaxTree tree = new AbstractSyntaxTree(MathLexer.TYPE_OPE_ADD, "+");
            tree.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_NUM, "123"));
            tree.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_REAL, "456.7"));

            var result = tree.ToStringTree();

            Assert.AreEqual("(+ 123 456.7)", result);
        }
        /// <summary>
        /// Tests two operator nodes and number nodes.
        /// </summary>
        [TestMethod]
        public void TestAST_Double_AddDiv()
        {
            AbstractSyntaxTree root = new AbstractSyntaxTree(null);

            AbstractSyntaxTree tree = new AbstractSyntaxTree(MathLexer.TYPE_OPE_ADD, "+");
            tree.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_NUM, "123"));

            root.AddChild(tree);

            AbstractSyntaxTree tree_child = new AbstractSyntaxTree(MathLexer.TYPE_OPE_DIV, "/");
            tree_child.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_NUM, "678.9"));
            tree_child.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_REAL, "1"));

            tree.AddChild(tree_child);

            var result = tree.ToStringTree();

            Assert.AreEqual("(+ 123 (/ 678.9 1))", result);
        }
    }
}
