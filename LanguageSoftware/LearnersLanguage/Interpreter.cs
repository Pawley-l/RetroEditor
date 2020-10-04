using System;
using System.Collections.Generic;
using System.IO;
using LearnersLanguage.Nodes.Data;

namespace LearnersLanguage
{
    /**
     * <summary> The LearnersLanguage interpreter. </summary>
     */
    public class Interpreter
    {
        private static Lexer _lexer = new Lexer();
        private static readonly Eval _eval = new Eval();
        private static int _line = 0;
        
        /**
         * <summary> Reads and executes code from a file. </summary>
         * <code>
         * var _interpreter = new Interpreter();
         * _interpreter.ExecuteFromFile(arg);
         * </code>
         */
        public void ExecuteFromFile(string file)
        {
            using (var sr = File.OpenText(file))
            {
                var s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    _line++;
                    _lexer.AddStatement(s);
                }
            }

            var parser = new Parser();
            _eval.Reset();
            _eval.Execute(parser.LoadTokens(_lexer));
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
         */
        public void Execute()
        {
            var parser = new Parser();
            _eval.Reset();
            _eval.Execute(parser.LoadTokens(_lexer));
        }
        
        public int GetLine()
        {
            return _line;
        }
        
        public void MapMethod(string identity, Func<List<IntNode>, IntNode> func)
        {
            _eval.MapMethod(identity, func);
        }
    }
}