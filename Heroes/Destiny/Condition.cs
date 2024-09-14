using System;

namespace Weaver.Heroes.Destiny;

public static class Condition
{
    #region Static generators
        private static Random _generator = new Random();//the numbers generator.

        private static Parser.Parser? _parser;
        /// <summary>
        /// Singleton access to the parser.
        /// </summary>
        private static Parser.Parser Parser
        {
            get
            {
                if(_parser == null)
                {
                    _parser = new Parser.Parser();
                }
                return _parser;
            }
        }
    #endregion

    /// <summary>
    /// Parse the given condition macro and return the equivalent model object.
    /// </summary>
    /// <param name="macro"></param>
    /// <returns>Null if couldn't parse.</returns>
    public static ICondition? Parse(string macro, Body.Module? basemodule = null)
    {
        TextReader tr = new StringReader(macro);

        //Parse string
        Parser.Scanner = new Parser.Parser.Lexer(tr);
        if(basemodule != null)
            Parser.modRef.Module = basemodule;

        if (Parser.Parse())
        {
            return Parser.result;
        }
        else
        {
            return null;
        }
    }

}
