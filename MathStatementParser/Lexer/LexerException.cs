using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathStatementParser.Lexer
{

    /// <summary>
    /// Lexer 実行時の例外
    /// </summary>
    class LexerException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LexerException"/> class.
        /// </summary>
        public LexerException()
            :base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LexerException"/> class.
        /// </summary>
        /// <param name="message">エラーを説明するメッセージ。</param>
        public LexerException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LexerException"/> class.
        /// </summary>
        /// <param name="message">例外の原因を説明するエラー メッセージ。</param>
        /// <param name="innerException">現在の例外の原因である例外。内部例外が指定されていない場合は null 参照 (Visual Basic では、Nothing)。</param>
        public LexerException(string message , Exception innerException)
            :base(message , innerException)
        {

        }
    }
}
