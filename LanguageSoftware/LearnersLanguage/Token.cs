using System.Linq;

namespace LearnersLanguage
{
    /**
     * <summary>
     * Token class which is used to hold a tokens type and its value.
     * </summary>
     */
    public class Token
    {
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
        public TokenType Type;
        public string Value;

        public enum TokenType
        {
            TOKEN_ADD, TOKEN_SUB, 
            TOKEN_DIV, TOKEN_MUL, 
            TOKEN_EQU, TOKEN_LPAR, 
            TOKEN_RPAR, TOKEN_COMMA,
            TOKEN_KEYWORD, TOKEN_INT, 
            TOKEN_SYMBOL, UNKNOWN,
            TOKEN_END
        }
        
        private static string[] keywords = new string[7]
        {
            "IF",
            "ENDIF",
            "METHOD",
            "ENDMETHOD",
            "LOOP",
            "FOR",
            "ENDLOOP"
        };
        
        /**
         * <summary>Converts a tokens type into a string </summary>
         */
        public static TokenType StrToToken(string token)
        {
            var constants = token switch
            {
                "+" => TokenType.TOKEN_ADD,
                "-" => TokenType.TOKEN_SUB,
                "/" => TokenType.TOKEN_DIV,
                "*" => TokenType.TOKEN_MUL,
                "=" => TokenType.TOKEN_EQU,
                "(" => TokenType.TOKEN_LPAR,
                ")" => TokenType.TOKEN_RPAR,
                "," => TokenType.TOKEN_COMMA,
                _ => TokenType.UNKNOWN
            };

            if (constants != TokenType.UNKNOWN) return constants;
            if (keywords.Any(keyword => keyword == token))
            {
                return TokenType.TOKEN_KEYWORD;
            }
                
            return int.TryParse(token, out _) ? TokenType.TOKEN_INT : TokenType.TOKEN_SYMBOL;

        }

        public override string ToString()
        {
            if (Value == "null")
                return " " + Type;
            
            return " " + Type +":"+ Value;
        }
    }
}