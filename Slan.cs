using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slan
{
    class Slan
    {
        static bool hadError = false;
        internal static void runPrompt()
        {
            for (; ; )
            {
                Console.WriteLine("> ");
                string line = Console.ReadLine();
                if (line == null) break;

                run(line);
                hadError = false;
            }
        }

        internal static void runFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(Path.GetFullPath(path));
            run(System.Text.Encoding.UTF8.GetString(bytes));

            if (hadError) System.Environment.Exit(65);
        }

        internal static void run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }

       internal static void error(int line, String message)
        {
            report(line, "", message);
        }

        internal static void report(int line, string where, string message)
        {
            Console.Error.WriteLine("[line " + line + "] Error" + where + ": " + message);
            hadError = true;

        }
    }
}
