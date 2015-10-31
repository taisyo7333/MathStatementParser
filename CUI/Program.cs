using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathStatementParser.Lexer;

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
                input = "123 +456*(19- 20)    /7890123456";

            Console.WriteLine(">" + input);


            Lexer lexer = new ListLexer(input);
            Token token = lexer.NextToken();

            while (token.Type != Lexer.TYPE_EOF)
            {
                Console.WriteLine(token.ToString());
                token = lexer.NextToken();
            }
            Console.WriteLine(token.ToString());

        }
    }
}
