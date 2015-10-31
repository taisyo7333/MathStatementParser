using System;
using System.Linq;

namespace MathStatementParser.Lexer
{
    /// <summary>
    /// Token Analyzer for Mathmatical statement.
    /// Abstract class.
    /// </summary>
    public abstract class Lexer
    {
        #region STATIC_FIELDS        
        /// <summary>
        /// The EOF : express the end of the file input.
        /// </summary>
        public static readonly char EOF = (char)0xFF;

        /// <summary>
        /// The TYPE express EOF ( End Of File ).
        /// </summary>
        public static readonly int TYPE_EOF = 1;
        #endregion

        #region FIELDS

        /// <summary>
        /// The input : 入力文字列.
        /// </summary>
        string input;

        /// <summary>
        /// The p : 入力文字列中の現在位置.
        /// </summary>
        int p = 0;
        /// <summary>
        /// The c : 現在位置にある文字.
        /// </summary>
        char c;
        #endregion

        #region PROPERTY

        /// <summary>
        /// Gets the 先読み文字
        /// </summary>
        /// <value>
        /// 先読み文字
        /// </value>
        public char LOOK_AHEAD
        {
            get { return c; }
        }
        #endregion

        #region METHODS


        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        public Lexer(string input)
        {
            this.input = input;
            // 先読み処理の準備
            c = this.input.ElementAt(p);
        }


        /// <summary>
        /// Cosumes this instance.
        /// </summary>
        public void Consume()
        {
            // 一文字分の移動またはファイル終端の検出をします。
            p++;
            if( p >= input.Length)
            {
                this.c = EOF;
            }
            else
            {
                this.c = input.ElementAt(p);
            }
        }
        /// <summary>
        /// Matches the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <exception cref="MathStatementParser.Lexer.LexerException">expecting  + x + ; found  + c</exception>
        public void match(char x)
        {
            // xと入力列中の次の文字が一致することを確認します。
            if (this.c == x)
            {
                Consume();
            }
            else
            {
                throw new LexerException("expecting " + x + "; found " + c);
            }
        }

        public abstract Token NextToken();
        public abstract string GetTokenName(int tokenType);
        #endregion

    }
}
