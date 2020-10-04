using System.Collections.Generic;
using LearnersLanguage;
using LearnersLanguage.Nodes.Data;
using NUnit.Framework;

namespace LearnersLanguageTest
{
    [TestFixture]
    public class InterpreterTest
    {
        private static Interpreter _interpreter;
        
        // variables to be manipulated in the script
        private static bool _methodTrue = false;
        private static bool _loopTrue = false;
        private static bool _conditionTrue = false;
        private static bool _mapMethod = false;

        private static IntNode TestMethod(List<IntNode> input)
        {
            _methodTrue = true;
            return null;
        }
        
        private static IntNode TestLoop(List<IntNode> input)
        {
            _loopTrue = true;
            return null;
        }
        
        private static IntNode TestCondition(List<IntNode> input)
        {
            _conditionTrue = true;
            return null;
        }
        
        private static IntNode TestMethodMap(List<IntNode> input)
        {
            _mapMethod = true;
            return null;
        }

        [Test]
        public void TestMethods()
        {
            _interpreter = new Interpreter();
            _interpreter.MapMethod("test" ,TestMethod);
            _interpreter.ExecuteFromFile("../../../../../Examples/Tests/method.ll");
            Assert.IsTrue(_methodTrue);
        }
        
        [Test]
        public void TestLoops()
        {
            _interpreter = new Interpreter();
            _interpreter.MapMethod("test" ,TestLoop);
            _interpreter.ExecuteFromFile("../../../../../Examples/Tests/loop.ll");      
            Assert.IsTrue(_loopTrue);
        }
        
        [Test]
        public void TestMethodMap()
        {
            _interpreter = new Interpreter();
            _interpreter.MapMethod("test" , TestMethodMap);
            _interpreter.ExecuteFromFile("../../../../../Examples/Tests/method.ll");
            Assert.IsTrue(_mapMethod);
        }
        
        [Test]
        public void TestConditions()
        {
            _interpreter = new Interpreter();
            _interpreter.MapMethod("test" ,TestCondition);
            _interpreter.ExecuteFromFile("../../../../../Examples/Tests/conditions.ll");
            Assert.IsTrue(_conditionTrue);
        }
    }
}