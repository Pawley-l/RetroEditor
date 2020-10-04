using System;
using System.Collections.Generic;
using System.IO;
using LearnersLanguage.Exceptions;
using LearnersLanguage.Nodes.Data;

namespace LearnersLanguage
{
    /**
     * <summary> The LearnersLanguage interpreter. </summary>
     * TODO(project): Add loop support
     * TODO(project): Add condition support
     * TODO(project): Add string literal and string variables
     * TODO(project): Improve method mapping
     * TODO(project): Check for better design patterns
     * TODO(project): Add line numbers to exceptions & better descriptions
     */
    public class Interpreter
    {
        private readonly Lexer _lexer = new Lexer();
        private readonly Eval _eval = new Eval();

        /**
         * <summary> Reads and executes code from a file. </summary>
         * <code>
         * var _interpreter = new Interpreter();
         * _interpreter.ExecuteFromFile(arg);
         * </code>
         * <exception cref="UndeclaredSymbolException"> throws if a variable or function isnt defined before being used. </exception>
         */
        public void ExecuteFromFile(string file)
        {
            using (var sr = File.OpenText(file))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                    _lexer.AddStatement(s);
            }

            var parser = new Parser();
            _eval.Reset();
            try
            {
                _eval.Execute(parser.LoadTokens(_lexer));
            }
            catch (UndeclaredSymbolException)
            {
                throw;
            }
        }
        
        /**
         * <summary> preloads a line of code, but does not execute the line. execute using Execute() method. </summary>
         */
        public void ParseLine(string line)
        {
            _lexer.AddStatement(line);
        }
        
        /**
         * <summary> Main execution method. Parse lines using ParseLine() method and run execute with this method </summary>
         * <exception cref="UndeclaredSymbolException"> throws if a variable or function isnt defined before being used. </exception>
         */
        public void Execute()
        {
            try
            {
                var parser = new Parser();
                _eval.Reset();
                _eval.Execute(parser.LoadTokens(_lexer));
            }
            catch (UndeclaredSymbolException)
            {
                throw;
            }
        }
        
        public void MapMethod(string identity, Func<List<IntNode>, IntNode> func)
        {
            _eval.MapMethod(identity, func);
        }
    }
}