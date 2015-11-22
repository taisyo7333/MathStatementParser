using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathStatementParser.Lexer;

namespace MathStatementParser.Parser
{
    /// <summary>
    /// Syntax Parser.
    /// 入力：字句解析結果。
    /// 処理：構文解析。
    /// 出力：抽象構文木。
    /// </summary>
    public abstract class Parser
    {
        #region FIELDS        
        /// <summary>
        /// 字句解析器
        /// </summary>
        Lexer.Lexer lexer;
        /// <summary>
        /// 現在の先読み字句
        /// </summary>
        Lexer.Token lookahead; 
        #endregion

        #region PROPERTY
        public Lexer.Token LOOK_AHEAD
        {
            get { return lookahead; }
        }
        #endregion

        #region METHODS        
        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class.
        /// </summary>
        /// <param name="lex">The lexer class's instance</param>
        public Parser(Lexer.Lexer lex )
        {
            this.lexer = lex;

            // 一文字先読みする
            Consume();
        }
        /// <summary>
        /// Matches the specified token_type.
        /// </summary>
        /// <param name="token_type">The token_type.</param>
        /// <exception cref="MathStatementParser.Parser.ParserException"></exception>
        public void Match(int token_type)
        {
            if (token_type.Equals(lookahead.Type))
            {
                Consume();
            }
            else
            {
                throw new ParserException(string.Format("Invalid Syntax :type {0}",token_type));
            }
        }
        /// <summary>
        /// Consumes token.
        /// 字句解析器からの出力を先に進める。
        /// 得られた字句解析結果を先読み字句として保持する。
        /// </summary>
        private void Consume()
        {
            lookahead = lexer.NextToken();
        }
        /// <summary>
        /// Starts syntax parsing.
        /// </summary>
        /// <returns>null : success , not null : error </returns>
        public abstract string Start();

        #endregion
    }
}
