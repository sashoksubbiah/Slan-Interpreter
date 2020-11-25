using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slan
{
    class Program
    {

        static void Main(string[] args)
        {

            if (args.Length > 1)
            {
                Console.WriteLine("Usage : Slan [script]");
                System.Environment.Exit(0);
            }
            else if (args.Length == 1)
            {
                Slan.runFile(args[0]);
            }
            else
            {
                Slan.runPrompt();
            }
        }

    }
}
