using System;
using System.Threading;

internal class Program
{
    private static int Main(string[] args)
    {
        if (!ValidateArgs(args.Length))
        {
            return 1;
        }

        Console.WriteLine("\tStarting 'Stock Quote Alert'");

        return 0;
    }

    private static bool ValidateArgs(int length)
    {
        if (length != 3)
        {
            Console.WriteLine("\t\tInvalid args\n");
            Console.WriteLine("Use:\n" +
                "\tstock-quote-alert.exe Arg1 Arg2 Arg3\n" +
                "When:\n" +
                "\tArg1: Stock name\n" +
                "\tArg2: Price to sell\n" +
                "\tArg3: Price to buy\n" +
                "\nExample:\n" +
                "\tstock-quote-alert.exe PETR4 22.67 22.59\n"
                );

            return false;
        }

        return true;
    }
}
