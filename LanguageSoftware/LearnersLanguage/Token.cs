namespace LearnersLanguage
{
    enum Token
    {
        // Values and Arith
        Add,
        Sub, 
        Div, 
        Mul,
        Equ,
        Int,
        Name,
        
        // Seperators
        LPar,
        RPar,
        Comma,
        
        // Methods and Loops
        MethodStart,
        MethodEnd,
        LoopStart,
        LoopEnd,
        ConditionStart,
        ConditionEnd,
        
        // Graphics
        Rect,
        Circle,
        Triangle,
        Colour,
    }
}