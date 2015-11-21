using System;
using System.Text;

namespace MathStatementParser.Lexer
{
    /// <summary>
    /// 数式文字列の字句解析。
    /// </summary>
    public partial class MathLexer : Lexer
    {
        #region STATIC_FIELDS
        /// <summary>
        /// 字句型：無効
        /// </summary>
        public static readonly int TYPE_INVALID = 0;
        // 上位で定義
        // public static readonly int TYPE_EOF = 1;

        /// <summary>
        /// 字句型：整数
        /// </summary>
        public static readonly int TYPE_NUM = 2;
        /// <summary>
        /// 字句型：演算子
        /// </summary>
        public static readonly int TYPE_OPE = 3;
        /// <summary>
        /// The typ e_ lparen
        /// </summary>
        public static readonly int TYPE_LPAREN = 4;
        /// <summary>
        /// The typ e_ rparen
        /// </summary>
        public static readonly int TYPE_RPAREN = 5;
        /// <summary>
        /// The typ e_ real
        /// </summary>
        public static readonly int TYPE_REAL = 6;
        #endregion

        #region FIELDS
        /// <summary>
        /// The token names
        /// </summary>
        public static readonly string[] tokenNames = new string[] 
        {
            "N/A",
            "<EOF>",
            "NUM",
            "OPE",
            "LPAREN",
            "RPAREN",
            "REAL",
        };
        #endregion


        #region METHODS

        /// <summary>
        /// Initializes a new instance of the <see cref="MathLexer"/> class.
        /// </summary>
        public MathLexer(string input)
            :base(input)
        {
        }

        /// <summary>
        /// Nexts the token.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MathStatementParser.Lexer.LexerException">invalid character:  + next_input.ToString()</exception>
        public override Token NextToken()
        {
            while (LOOK_AHEAD != EOF)
            {
                // 先読み文字
                char next_input = LOOK_AHEAD;
                switch (next_input)
                {
                    case ' ':
                    case '\t':
                    case '\n':
                    case '\r':
                        WhiteSpace();
                        continue;
                    case '(':
                        Consume();
                        return new Token(TYPE_LPAREN, next_input.ToString());
                    case ')':
                        Consume();
                        return new Token(TYPE_RPAREN, next_input.ToString());
                    case '*':
                    case '/':
                    case '+':
                    case '-':
                        Consume();
                        return new Token(TYPE_OPE, next_input.ToString());
                    default:
                        if (IsNumber())
                            return Number();
                        else
                            throw new LexerException("invalid character: " + next_input + " , expected Number.");
                }
            }
            return new Token(TYPE_EOF, "<EOF>");
        }
        /// <summary>
        /// Gets the name of the token.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <returns>Token name is contained in string.</returns>
        public override string GetTokenName(int tokenType)
        {
            return tokenNames[tokenType];
        }
        /// <summary>
        /// 先読み文字が空白であればすべて無視します。
        /// </summary>
        private void WhiteSpace()
        {
            while( ( LOOK_AHEAD == ' ')
                || ( LOOK_AHEAD == '\t')
                || ( LOOK_AHEAD == '\r')
                || ( LOOK_AHEAD == '\n')
                )
            {
                Consume();
            }
        }
        /// <summary>
        /// Determines whether this instance is number.
        /// </summary>
        /// <returns></returns>
        private bool IsNumber()
        {
            if ('0' <= LOOK_AHEAD && LOOK_AHEAD <= '9')
                return true;
            else
                return false;
        }
        /// <summary>
        /// 数字：(\d+)から構成される文字列を字句としたTokenインスタンスを生成する。
        /// 実数：(\d+\.\d+)から構成される文字列を字句としたTokenインスタンを生成する。
        /// </summary>
        /// <returns>字句型：数字または実数のTokenを生成する</returns>
        private Token Number()
        {
            StringBuilder buffer = new StringBuilder();
            // 整数型・実数型を問わず数値を抽出する
            do
            {
                buffer.Append(LOOK_AHEAD);
                Consume();
            } while (IsNumber());

            
            if( LOOK_AHEAD != '.')
            {
                return new Token(TYPE_NUM, buffer.ToString());
            }
            else
            {
                // 実数型
                buffer.Append(LOOK_AHEAD);
                Consume();
                if(!IsNumber())
                    throw new LexerException("invalid character: " + LOOK_AHEAD + " expected number.");

                do
                {
                    buffer.Append(LOOK_AHEAD);
                    Consume();
                } while (IsNumber());

                return new Token(TYPE_REAL, buffer.ToString());
            }
        }


        #endregion


    }
}
