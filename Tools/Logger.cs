using System;
using System.Diagnostics;

namespace Tools
{
    public static class Logger
    {
        public static void WriteLine(string @string = "")
        {
            Debug.WriteLine(@string);
            Console.WriteLine(@string);
        }

        public static void Write(string @string)
        {
            Debug.Write(@string);
            Console.Write(@string);
        }
    }
}
