using System;
using System.Collections.Generic;
using LearnersLanguage.Exceptions;
using LearnersLanguage.Nodes.Data;

namespace LearnersLanguage
{
    /**
     * <summary>
     * When executing from the terminal, use -r for interactive terminal.
     * Otherwise, direct it to the file that you want to execute
     * </summary>
     * TODO: Fix all unhandled exceptions
     * TODO: Improve design
     */
    class Program
    {
        private static readonly string banner = " LearnersLanguage Interpreter ";
        private static bool _running = true;
        private static Interpreter _interpreter = new Interpreter();
        
        private static void Main(string[] args)
        {
            // Nothing to execute if no args
            if (args.Length < 1) return;
            
            _interpreter.MapMethod("print", Print);
            
            if (args[0] == "-r")
            {
                string input;
                Console.WriteLine(banner);
                
                _interpreter.MapMethod("exit", Exit);
                
                while (_running)
                {
                    Console.Write(" >> ");
                    input = Console.ReadLine();
            
                    if (input == "exit")
                        _running = false;
                    else
                        _interpreter.ExecuteLine(input);
                }
            }
            
            // Executes every file that is inputted
            foreach (var arg in args)
            {
                _interpreter.ExecuteFromFile(arg);
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