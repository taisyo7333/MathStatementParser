using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathStatementParser;
using MathStatementParser.Lexer;

namespace MathStatementParser.Parser
{
    /// <summary>
    /// 数式の構文解析器
    /// 構文解析結果として抽象構文木を出力する。
    /// </summary>
    public class MathParser : Parser
    {
        #region SYNTAX
        /* ******************************************
         * <expr>    ::= <term> ('+'|'-') <term>
         * <term>    ::= <factor> ('*'|'/') <factor>
         * <factor>  ::= '(' <expr> ')' | <element>
         * <elements>::= Integer | real-number
         * ***************************************** */
        #endregion

        #region FIELDS
        #endregion

        #region PROPERTY
        #endregion

        #region METHODS        
        /// <summary>
        /// Initializes a new instance of the <see cref="MathParser"/> class.
        /// </summary>
        /// <param name="lex">The lexer class's instance</param>
        public MathParser(Lexer.Lexer lex)
            :base(lex)
        {
        }

        /// <summary>
        /// Starts syntax parsing.
        /// </summary>
        /// <returns>
        /// null : success , not null : error
        /// </returns>
        public override string Start()
        {
            try
            {
                Expr();
                // Accepted 
                if( LOOK_AHEAD.Type == MathLexer.TYPE_EOF )
                {
                    return null;
                } else {
                    return string.Format("Syntax Error : TYPE:{0},TOKEN:{1}",LOOK_AHEAD.Type,LOOK_AHEAD.Text); 
                }
                
            }catch(ParserException ex)
            {
                return "Syntax Error" + ex.Message;
            }
        }
        #endregion

        #region SYNTAX_PARSE_IMPLEMENTS
        /// <summary>
        /// 文法 : expr を表現
        /// <![CDATA[ <expr>    ::= <term> ('+'|'-') <term> ]]>
        /// </summary>
        /// <remarks></remarks>
        void Expr()
        {
            Term();

            while ( LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_OPE_ADD
                ||  LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_OPE_SUB)
            {
                if (LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_OPE_ADD)
                    Match(Lexer.MathLexer.TYPE_OPE_ADD);
                else if (LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_OPE_SUB)
                    Match(Lexer.MathLexer.TYPE_OPE_SUB);

                Term();
            }
        }
        /// <summary>
        /// 文法：termを表現
        /// <![CDATA[<term>    ::= <factor> ('*'|'/') <factor>]]>
        /// </summary>
        void Term()
        {
            Factor();
            while( LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_OPE_MUL
                || LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_OPE_DIV)
            {
                if (LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_OPE_MUL)
                    Match(Lexer.MathLexer.TYPE_OPE_MUL);
                else if (LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_OPE_DIV)
                    Match(Lexer.MathLexer.TYPE_OPE_DIV);

                Factor();
            }
        }
        /// <summary>
        ///文法：factorを表現
        ///<![CDATA[<factor>  ::= '(' <expr> ')' | <element>]]>
        /// </summary>
        void Factor()
        {
            if( LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_LPAREN)
            {
                Match(Lexer.MathLexer.TYPE_LPAREN);
                Expr();
                Match(Lexer.MathLexer.TYPE_RPAREN);
            }
            else
            {
                Elements();
            }
        }
        /// <summary>
        /// 文法：elementesを表現
        /// <![CDATA[<elements> ::= Integer | real-number]]>
        /// </summary>
        /// <exception cref="MathStatementParser.Parser.ParserException"></exception>
        void Elements()
        {
            if(LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_NUM)
            {
                Match(Lexer.MathLexer.TYPE_NUM);
            }
            else if( LOOK_AHEAD.Type == Lexer.MathLexer.TYPE_REAL)
            {
                Match(Lexer.MathLexer.TYPE_REAL);
            }
            else
            {
                throw new ParserException(string.Format("Syntax Error : Invalid Token comes [{0}]",Lexer.MathLexer.tokenNames[LOOK_AHEAD.Type]));
            }
        }
        #endregion
    }
}
