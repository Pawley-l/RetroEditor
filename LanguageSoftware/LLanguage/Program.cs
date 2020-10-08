using System;
using System.Collections.Generic;
using LLanguage.Exceptions;
using LLanguage.Nodes.Types;

namespace LLanguage
{
    /**
     * <summary>
     * When executing from the terminal, use -r for interactive terminal.
     * Otherwise, direct it to the file that you want to execute
     * </summary>
     */
    class Program
    {
        private static readonly string banner = " LLanguage Interpreter ";
        private static bool _running = true;
        private static Interpreter _interpreter = new Interpreter();
        
        private static void Main(string[] args)
        {
            _interpreter.MapMethod("print", Print);
            
            if (args.Length < 1) InteractiveMode();
            
            if (args.Length > 1)
                if (args[0] == "-r") InteractiveMode();
            
            // Executes every file that is inputted
            foreach (var arg in args)
            {
                _interpreter.ExecuteFromFile(arg);
            }
        }

        private static void InteractiveMode()
        {
            string input;
            Console.WriteLine(banner);
                
            _interpreter.MapMethod("exit", Exit);

            while (_running)
            {
                Console.Write(" >> ");
                input = Console.ReadLine();

                try
                {
                    if (input != "execute")
                        _interpreter.ParseLine(input);
                    
                    if (input == "execute")
                        _interpreter.Execute();
                }
                catch (UndeclaredSymbolException e)
                {
                    Console.WriteLine(e.Line + ": " + e.Statement);
                    
                    Exit(null);
                }
            }
        }
        
        /**
         * Method for binding print. Only useful when executing from terminal.
         */
        private static IntNode Print(List<IntNode> input)
        {
            foreach (var node in input)
            {
                Console.WriteLine(node);
            }
            return new IntNode(-1);
        }
        
        /**
         * Method for binding an exit command
         */
        private static IntNode Exit(List<IntNode> input)
        {
            _running = false;
            return new IntNode(0);
        }
    }
}