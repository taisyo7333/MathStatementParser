using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using MathStatementParser.Lexer;
using MathStatementParser;

namespace MathStatementParser.Tree
{
    /// <summary>
    /// 木方式Interpreterによる実装。
    /// 抽象構文木を走査しながら実行する。
    /// </summary>
    public class AstInterpreter
    {
        AbstractSyntaxTree ast = null;
        Parser.Parser parser = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="AstInterpreter"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        public AstInterpreter(Parser.Parser parser)
        {
            this.parser = parser;
        }
        /// <summary>
        /// Executes this instance.
        /// インタプリタ実行処理
        /// </summary>
        /// <returns>出力結果</returns>
        public double Exec()
        {
            ast = parser.ParseAst();

            return Exec(ast);
        }
        /// <summary>
        /// Executes the specified tree.
        /// インタプリタ内部実行処理
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException"></exception>
        private double Exec(AbstractSyntaxTree tree)
        {
            switch (tree.GetNodeType())
            {
                case Lexer.MathLexer.TYPE_NUM:
                    return int.Parse(tree.ToString());
                case Lexer.MathLexer.TYPE_REAL:
                    return double.Parse(tree.ToString());
                case Lexer.MathLexer.TYPE_OPE_ADD:
                    return Add(tree);
                case Lexer.MathLexer.TYPE_OPE_SUB:
                    return Sub(tree);
                case Lexer.MathLexer.TYPE_OPE_DIV:
                    return Div(tree);
                case Lexer.MathLexer.TYPE_OPE_MUL:
                    return Mul(tree);
                default:
                    throw new NotSupportedException(string.Format("AstInterpreter Not Support.type={0}",tree.GetNodeType()));
            }
        }
        /// <summary>
        /// Adds the specified tree.
        /// 加算処理を行う。
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns></returns>
        public double Add(AbstractSyntaxTree tree)
        {
            var lhs = tree.GetChild(0);
            var rhs = tree.GetChild(1);
            //　左辺・右辺を評価した結果を取得する。
            var lValue = this.Exec(lhs);
            var rValue = this.Exec(rhs);

            return lValue + rValue;
        }
        /// <summary>
        /// Subs the specified tree.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns></returns>
        public double Sub(AbstractSyntaxTree tree)
        {
            var lhs = tree.GetChild(0);
            var rhs = tree.GetChild(1);
            //　左辺・右辺を評価した結果を取得する。
            var lValue = this.Exec(lhs);
            var rValue = this.Exec(rhs);

            return lValue - rValue;
        }
        /// <summary>
        /// Muls the specified tree.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns></returns>
        public double Mul(AbstractSyntaxTree tree)
        {
            var lhs = tree.GetChild(0);
            var rhs = tree.GetChild(1);
            //　左辺・右辺を評価した結果を取得する。
            var lValue = this.Exec(lhs);
            var rValue = this.Exec(rhs);

            return lValue * rValue;
        }
        /// <summary>
        /// Divs the specified tree.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns></returns>
        public double Div(AbstractSyntaxTree tree)
        {
            var lhs = tree.GetChild(0);
            var rhs = tree.GetChild(1);
            //　左辺・右辺を評価した結果を取得する。
            var lValue = this.Exec(lhs);
            var rValue = this.Exec(rhs);

            return lValue / rValue;
        }

    }
}
