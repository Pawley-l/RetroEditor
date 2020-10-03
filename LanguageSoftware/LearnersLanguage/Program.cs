using System;

namespace LearnersLanguage
{
    class Program
    {
        private static bool running = true;
        private static Lexer lexer = new Lexer();
        
        static void Main(string[] args)
        {
            string input;
            
            while (running)
            {
                Console.Write("Command> ");
                input = Console.ReadLine();

                if (input == "exit")
                    running = false;
                else
                    Console.WriteLine(RunCommand(input));
            }
        }

        private static string RunCommand(string command)
        {
            lexer.AddCommand(command);
            return "Command: " + command;
        }
    }
}