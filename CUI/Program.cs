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
            if (args.Length > 0)
            {
                string input = args[0];
                // TOKEN出力
                PrintStringTokens(input);
                // 抽象構文木を文字列出力
                PrintStringAbstractSyntaxTree(input);
            }
            else
            {
                // インタラクティブモード
                do
                {
                    var line = Console.ReadLine();
                    // 終了条件
                    if (line == null || line.ToLower().Equals("exit"))
                        break;
                    // 空文字
                    if(!line.Any() )
                    {
                        Console.Write(">");
                        continue;
                    }

                    PrintStringTokens(line);
                    PrintStringAbstractSyntaxTree(line);

                    Console.WriteLine("");
                } while (true);
            }
        }
        /// <summary>
        /// Prints the string tokens.
        /// </summary>
        /// <param name="input">The input.</param>
        static void PrintStringTokens(string input)
        {
            Console.WriteLine("Print tokens list.");
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
        /// <summary>
        /// Prints the string abstract syntax tree.
        /// </summary>
        /// <param name="input">The input.</param>
        static void PrintStringAbstractSyntaxTree(string input)
        {
            Console.WriteLine("Print string abstract syntax tree.");
            Console.WriteLine(">" + input);

            Lexer lexer = new MathLexer(input);
            Parser parser = new MathParser(lexer);

            var result = parser.ParseAst();

            Console.WriteLine(result.ToStringTree());
        }
    }
}
