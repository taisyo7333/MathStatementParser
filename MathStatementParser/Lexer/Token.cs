
namespace MathStatementParser.Lexer
{
    /// <summary>
    /// 字句を表現するクラス
    /// </summary>
    public class Token
    {
        #region FIELDS
        /// <summary>
        /// 字句型：Lexerクラスのサブクラスで定義されている字句型が設定される。
        /// </summary>
        private int type;
        /// <summary>
        /// 字句に紐づく文字列
        /// </summary>
        private string text;
        #endregion

        #region PROPERTY

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public int Type
        {
            get { return type; }
        }
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get { return text; }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Token type.</param>
        /// <param name="text"></param>
        public Token(int type,string text)
        {
            this.type = type;
            this.text = text;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var name = MathLexer.tokenNames[type];
            return "<'" + text + "'," + name + ">";
        }
        #endregion
    }
}
