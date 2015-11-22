using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathStatementParser.Parser
{
    /// <summary>
    /// 構文解析実行時の例外クラス
    /// </summary>
    public class ParserException : Exception
    {
        #region FIELDS
        #endregion

        #region PROPERTY
        #endregion

        #region METHODS        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserException"/> class.
        /// </summary>
        public ParserException()
            : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserException"/> class.
        /// </summary>
        /// <param name="message">エラーを説明するメッセージ。</param>
        public ParserException(string message)
            :base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserException"/> class.
        /// </summary>
        /// <param name="message">例外の原因を説明するエラー メッセージ。</param>
        /// <param name="innerException">現在の例外の原因である例外。内部例外が指定されていない場合は null 参照 (Visual Basic では、Nothing)。</param>
        public ParserException(string message , Exception innerException)
            :base(message,innerException)
        {
        }
        #endregion
    }
}
