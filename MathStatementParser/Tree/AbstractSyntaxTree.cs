using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathStatementParser.Lexer;

namespace MathStatementParser.Tree
{
    /// <summary>
    /// 抽象構文木(均質抽象構文木)
    /// 演算子と被演算子からなる木を構築する。
    /// </summary>
    /// <remarks>
    /// '(',')'は取り除かれる。
    /// 優先順位はnodeの位置(深さ)によって決まる。
    /// </remarks>
    public class AbstractSyntaxTree
    {
        #region FIELDS        
        /// <summary>
        /// The token. 自nodeが保持するToken
        /// </summary>
        Token token = null;
        /// <summary>
        /// The children.自nodeが保持する子nodes
        /// </summary>
        List<AbstractSyntaxTree> children = null;
        #endregion

        #region PROPERTY
        #endregion

        #region METHODS        
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSyntaxTree"/> class.
        /// </summary>
        public AbstractSyntaxTree()
        {
            ;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSyntaxTree"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public AbstractSyntaxTree(Token token)
        {
            this.token = token;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSyntaxTree"/> class.
        /// </summary>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="text">The text.</param>
        public AbstractSyntaxTree(int tokenType,string text)
        {
            this.token = new Token(tokenType,text);
        }
        /// <summary>
        /// Gets the type of the node.
        /// </summary>
        /// <returns></returns>
        public int GetNodeType()
        {
            return this.token.Type;
        }
        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="tree">The abstract sysntax tree.</param>
        public void AddChild(AbstractSyntaxTree tree)
        {
            if (children == null)
            {
                children = new List<AbstractSyntaxTree>();
            }
            children.Add(tree);
        }
        /// <summary>
        /// Determines whether this node is nil.
        /// </summary>
        /// <returns></returns>
        public bool IsNil()
        {
            return token == null;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return token == null ? "nil" : token.Text;
        }
        /// <summary>
        /// To the string tree.
        /// 抽象構文木を文字列出力する。
        /// </summary>
        /// <returns>抽象構文木を平坦(一次元)化した文字列を出力する</returns>
        public string ToStringTree()
        {
            if (children == null || !children.Any())
                return this.ToString();

            // root node は nil node 可能
            if(IsNil())
            {
                StringBuilder buf = new StringBuilder();
                foreach( var child in children)
                {
                    buf.Append(child.ToStringTree());
                }
                return buf.ToString();
            }
            else
            {
                StringBuilder buf = new StringBuilder();
                buf.Append("(");
                buf.Append(this.ToString());
                foreach (var child in children)
                {
                    buf.Append(" ");
                    buf.Append(child.ToStringTree());
                }
                buf.Append(")");
                return buf.ToString();
            }
        }
        #endregion
    }
}
