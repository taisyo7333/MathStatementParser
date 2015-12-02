using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathStatementParser.Lexer;
using MathStatementParser.Parser;
using MathStatementParser.Tree;

namespace CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            if( args.Length > 0)
                input = args[0];
            else
                input = "123 +456+789*(19- 20)    /7890123456";

            {
                Console.WriteLine(">" + input);


                Lexer lexer = new MathLexer(input);
                Token token = lexer.NextToken();

                while (token.Type != Lexer.TYPE_EOF)
                {
                    Console.WriteLine(token.ToString());
                    token = lexer.NextToken();
                }
                Console.WriteLine(token.ToString());
            }
            Console.WriteLine("+++++ PARSER PHASES ++++");
            {
                Console.WriteLine(">" + input);
                Lexer lexer = new MathLexer(input);
                MathParser parser = new MathParser(lexer);

                var result = parser.Test();
                Console.WriteLine(string.Format("result = {0} ",result == null ? "" : result ));
            }
            Console.WriteLine("+++++++ Abstruct Syntax Tree #1 ++++++++");
            {
                AbstractSyntaxTree root = new AbstractSyntaxTree(null);

                AbstractSyntaxTree tree = new AbstractSyntaxTree(MathLexer.TYPE_OPE_ADD, "+");
                tree.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_NUM, "123"));
                tree.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_REAL, "456.7"));

                Console.WriteLine(tree.ToStringTree());
            }
            {
                Console.WriteLine("+++++++ Abstruct Syntax Tree #2 ++++++++");

                AbstractSyntaxTree root = new AbstractSyntaxTree(null);

                AbstractSyntaxTree tree = new AbstractSyntaxTree(MathLexer.TYPE_OPE_ADD, "+");
                tree.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_NUM, "123"));
//                tree.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_REAL, "456.7"));

                root.AddChild(tree);

                AbstractSyntaxTree tree_child = new AbstractSyntaxTree(MathLexer.TYPE_OPE_DIV, "/");
                tree_child.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_NUM, "678.9"));
                tree_child.AddChild(new AbstractSyntaxTree(MathLexer.TYPE_REAL, "1"));

                tree.AddChild(tree_child);

                Console.WriteLine(tree.ToStringTree());
            }
            {
                Console.WriteLine("++++++++ Translate from string to Abstruct Syntax Tree ++++++++");
                Lexer lexer = new MathLexer(input);
                Parser parser = new MathParser(lexer);

                var ast = parser.ParseAst();
                Console.WriteLine(ast.ToStringTree());

            }
            {
                Console.WriteLine("++++++++ Translate from string to Abstruct Syntax Tree #2 ++++++++");
                var statement = "1+2+3";

                Lexer lexer = new MathLexer(statement);
                Parser parser = new MathParser(lexer);

                var ast = parser.ParseAst();
                Console.WriteLine(statement);
                Console.WriteLine(ast.ToStringTree());

            }

        }
    }
}
