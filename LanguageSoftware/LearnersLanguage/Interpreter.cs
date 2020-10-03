using System;
using System.Collections.Generic;
using System.IO;
using LearnersLanguage.Nodes.Data;

namespace LearnersLanguage
{
    public class Interpreter
    {
        private static Lexer _lexer = new Lexer();
        private static Parser _parser = new Parser();
        private static readonly Eval _eval = new Eval();
        private static int _line = 0;
        
        
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

            _parser.LoadTokens(_lexer);
            _eval.Execute(_parser.GetAst());
        }
        
        public void ExecuteLine(string line)
        {
            _lexer.AddStatement(line);

            _parser.LoadTokens(_lexer);
                
            _eval.Execute(_parser.GetAst());
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