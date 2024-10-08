// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  FUKUROU
// DateTime: 2024/09/20 22:36:05
// UserName: rvi
// Input file <ConditionParser.y - 2024/09/20 22:36:01>

// options: lines

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using Weaver.Heroes.Body.Value;
using Weaver.Heroes.Luck;

namespace Weaver.Heroes.Destiny.Parser
{
internal enum Tokens {error=126,
    EOF=127,DIGIT=128,CHAR=129,UMINUS=130};

internal partial struct ValueType
#line 21 "ConditionParser.y"
       {
	public int iVal;
	public char cVal;
	public string sVal;
	public IRoll iRoll;
	public ComparableReference<int> iTerm;
	public ICondition iCond;
}
#line default
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal partial class Parser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from ConditionParser.y - 2024/09/20 22:36:01
#line 10 "ConditionParser.y"
	public ICondition result;
	public ModuleReference modRef = new ModuleReference(null);
#line default
  // End verbatim content from ConditionParser.y - 2024/09/20 22:36:01

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[44];
  private static State[] states = new State[76];
  private static string[] nonTerms = new string[] {
      "MAIN", "INTEGER", "PATH", "MACRO", "NUMERAL", "INTEGER_ADD", "INTEGER_MUL", 
      "INTEGER_COMPARISON", "INTEGER_TERMINAL", "BOOLEAN_TERMINAL", "BOOLEAN_AND", 
      "BOOLEAN_OR", "$accept", };

  static Parser() {
    states[0] = new State(new int[]{128,24,129,30,91,31,45,41,40,66,33,72,127,-2,126,-2},new int[]{-1,1,-12,5,-11,70,-10,71,-8,12,-6,13,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[1] = new State(new int[]{127,2,126,3});
    states[2] = new State(-1);
    states[3] = new State(new int[]{10,4});
    states[4] = new State(-4);
    states[5] = new State(new int[]{124,6,127,-3,126,-3});
    states[6] = new State(new int[]{124,7});
    states[7] = new State(new int[]{128,24,129,30,91,31,45,41,40,66,33,72},new int[]{-11,8,-10,71,-8,12,-6,13,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[8] = new State(new int[]{38,9,124,-43,127,-43,126,-43,41,-43});
    states[9] = new State(new int[]{38,10});
    states[10] = new State(new int[]{128,24,129,30,91,31,45,41,40,66,33,72},new int[]{-10,11,-8,12,-6,13,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[11] = new State(-41);
    states[12] = new State(-37);
    states[13] = new State(new int[]{61,14,43,17,45,46,33,55,62,58,60,62});
    states[14] = new State(new int[]{61,15});
    states[15] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-6,16,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[16] = new State(new int[]{43,17,45,46,38,-31,124,-31,127,-31,126,-31,41,-31});
    states[17] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-7,18,-9,54,-5,21,-2,22,-3,25});
    states[18] = new State(new int[]{42,19,47,48,61,-29,43,-29,45,-29,33,-29,62,-29,60,-29,38,-29,124,-29,127,-29,126,-29,41,-29});
    states[19] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-9,20,-5,21,-2,22,-3,25});
    states[20] = new State(-26);
    states[21] = new State(-21);
    states[22] = new State(new int[]{128,23,42,-18,47,-18,61,-18,43,-18,45,-18,33,-18,62,-18,60,-18,38,-18,124,-18,127,-18,126,-18,41,-18});
    states[23] = new State(-6);
    states[24] = new State(-5);
    states[25] = new State(new int[]{129,26,128,27,46,28,42,-19,47,-19,61,-19,43,-19,45,-19,33,-19,62,-19,60,-19,38,-19,124,-19,127,-19,126,-19,41,-19});
    states[26] = new State(-8);
    states[27] = new State(-9);
    states[28] = new State(new int[]{129,29});
    states[29] = new State(-10);
    states[30] = new State(-7);
    states[31] = new State(new int[]{128,40},new int[]{-4,32});
    states[32] = new State(new int[]{93,33,129,34,128,35,62,36,60,37,61,38,33,39});
    states[33] = new State(-20);
    states[34] = new State(-12);
    states[35] = new State(-13);
    states[36] = new State(-14);
    states[37] = new State(-15);
    states[38] = new State(-16);
    states[39] = new State(-17);
    states[40] = new State(-11);
    states[41] = new State(new int[]{40,43,128,24,129,30,91,31},new int[]{-5,42,-2,22,-3,25});
    states[42] = new State(-22);
    states[43] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-6,44,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[44] = new State(new int[]{41,45,43,17,45,46});
    states[45] = new State(-24);
    states[46] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-7,47,-9,54,-5,21,-2,22,-3,25});
    states[47] = new State(new int[]{42,19,47,48,61,-30,43,-30,45,-30,33,-30,62,-30,60,-30,38,-30,124,-30,127,-30,126,-30,41,-30});
    states[48] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-9,49,-5,21,-2,22,-3,25});
    states[49] = new State(-27);
    states[50] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-6,51,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[51] = new State(new int[]{41,52,43,17,45,46});
    states[52] = new State(-23);
    states[53] = new State(new int[]{42,19,47,48,61,-28,43,-28,45,-28,33,-28,62,-28,60,-28,38,-28,124,-28,127,-28,126,-28,41,-28});
    states[54] = new State(-25);
    states[55] = new State(new int[]{61,56});
    states[56] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-6,57,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[57] = new State(new int[]{43,17,45,46,38,-32,124,-32,127,-32,126,-32,41,-32});
    states[58] = new State(new int[]{61,59,128,24,129,30,91,31,45,41,40,50},new int[]{-6,61,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[59] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-6,60,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[60] = new State(new int[]{43,17,45,46,38,-33,124,-33,127,-33,126,-33,41,-33});
    states[61] = new State(new int[]{43,17,45,46,38,-35,124,-35,127,-35,126,-35,41,-35});
    states[62] = new State(new int[]{61,63,128,24,129,30,91,31,45,41,40,50},new int[]{-6,65,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[63] = new State(new int[]{128,24,129,30,91,31,45,41,40,50},new int[]{-6,64,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[64] = new State(new int[]{43,17,45,46,38,-34,124,-34,127,-34,126,-34,41,-34});
    states[65] = new State(new int[]{43,17,45,46,38,-36,124,-36,127,-36,126,-36,41,-36});
    states[66] = new State(new int[]{128,24,129,30,91,31,45,41,40,66,33,72},new int[]{-6,67,-12,68,-7,53,-9,54,-5,21,-2,22,-3,25,-11,70,-10,71,-8,12});
    states[67] = new State(new int[]{41,52,43,17,45,46,61,14,33,55,62,58,60,62});
    states[68] = new State(new int[]{41,69,124,6});
    states[69] = new State(-38);
    states[70] = new State(new int[]{38,9,124,-42,127,-42,126,-42,41,-42});
    states[71] = new State(-40);
    states[72] = new State(new int[]{40,73});
    states[73] = new State(new int[]{128,24,129,30,91,31,45,41,40,66,33,72},new int[]{-12,74,-11,70,-10,71,-8,12,-6,13,-7,53,-9,54,-5,21,-2,22,-3,25});
    states[74] = new State(new int[]{41,75,124,6});
    states[75] = new State(-39);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-13, new int[]{-1,127});
    rules[2] = new Rule(-1, new int[]{});
    rules[3] = new Rule(-1, new int[]{-12});
    rules[4] = new Rule(-1, new int[]{-1,126,10});
    rules[5] = new Rule(-2, new int[]{128});
    rules[6] = new Rule(-2, new int[]{-2,128});
    rules[7] = new Rule(-3, new int[]{129});
    rules[8] = new Rule(-3, new int[]{-3,129});
    rules[9] = new Rule(-3, new int[]{-3,128});
    rules[10] = new Rule(-3, new int[]{-3,46,129});
    rules[11] = new Rule(-4, new int[]{128});
    rules[12] = new Rule(-4, new int[]{-4,129});
    rules[13] = new Rule(-4, new int[]{-4,128});
    rules[14] = new Rule(-4, new int[]{-4,62});
    rules[15] = new Rule(-4, new int[]{-4,60});
    rules[16] = new Rule(-4, new int[]{-4,61});
    rules[17] = new Rule(-4, new int[]{-4,33});
    rules[18] = new Rule(-5, new int[]{-2});
    rules[19] = new Rule(-5, new int[]{-3});
    rules[20] = new Rule(-5, new int[]{91,-4,93});
    rules[21] = new Rule(-9, new int[]{-5});
    rules[22] = new Rule(-9, new int[]{45,-5});
    rules[23] = new Rule(-9, new int[]{40,-6,41});
    rules[24] = new Rule(-9, new int[]{45,40,-6,41});
    rules[25] = new Rule(-7, new int[]{-9});
    rules[26] = new Rule(-7, new int[]{-7,42,-9});
    rules[27] = new Rule(-7, new int[]{-7,47,-9});
    rules[28] = new Rule(-6, new int[]{-7});
    rules[29] = new Rule(-6, new int[]{-6,43,-7});
    rules[30] = new Rule(-6, new int[]{-6,45,-7});
    rules[31] = new Rule(-8, new int[]{-6,61,61,-6});
    rules[32] = new Rule(-8, new int[]{-6,33,61,-6});
    rules[33] = new Rule(-8, new int[]{-6,62,61,-6});
    rules[34] = new Rule(-8, new int[]{-6,60,61,-6});
    rules[35] = new Rule(-8, new int[]{-6,62,-6});
    rules[36] = new Rule(-8, new int[]{-6,60,-6});
    rules[37] = new Rule(-10, new int[]{-8});
    rules[38] = new Rule(-10, new int[]{40,-12,41});
    rules[39] = new Rule(-10, new int[]{33,40,-12,41});
    rules[40] = new Rule(-11, new int[]{-10});
    rules[41] = new Rule(-11, new int[]{-11,38,38,-10});
    rules[42] = new Rule(-12, new int[]{-11});
    rules[43] = new Rule(-12, new int[]{-12,124,124,-11});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 3: // MAIN -> BOOLEAN_OR
#line 48 "ConditionParser.y"
                { result = ValueStack[ValueStack.Depth-1].iCond; }
#line default
        break;
      case 4: // MAIN -> MAIN, error, '\n'
#line 49 "ConditionParser.y"
                     { yyerrok(); }
#line default
        break;
      case 5: // INTEGER -> DIGIT
#line 53 "ConditionParser.y"
         { CurrentSemanticValue.iVal = ValueStack[ValueStack.Depth-1].iVal; }
#line default
        break;
      case 6: // INTEGER -> INTEGER, DIGIT
#line 54 "ConditionParser.y"
                 { CurrentSemanticValue.iVal = ValueStack[ValueStack.Depth-2].iVal * 10 + ValueStack[ValueStack.Depth-1].iVal; }
#line default
        break;
      case 7: // PATH -> CHAR
#line 57 "ConditionParser.y"
        { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-1].sVal; }
#line default
        break;
      case 8: // PATH -> PATH, CHAR
#line 58 "ConditionParser.y"
             { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-2].sVal + ValueStack[ValueStack.Depth-1].sVal; }
#line default
        break;
      case 9: // PATH -> PATH, DIGIT
#line 59 "ConditionParser.y"
              { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-2].sVal + ValueStack[ValueStack.Depth-1].iVal; }
#line default
        break;
      case 10: // PATH -> PATH, '.', CHAR
#line 60 "ConditionParser.y"
                 { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-3].sVal + "." + ValueStack[ValueStack.Depth-1].sVal; }
#line default
        break;
      case 11: // MACRO -> DIGIT
#line 63 "ConditionParser.y"
         { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-1].iVal.ToString(); }
#line default
        break;
      case 12: // MACRO -> MACRO, CHAR
#line 64 "ConditionParser.y"
              { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-2].sVal + ValueStack[ValueStack.Depth-1].sVal; }
#line default
        break;
      case 13: // MACRO -> MACRO, DIGIT
#line 65 "ConditionParser.y"
               { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-2].sVal + ValueStack[ValueStack.Depth-1].iVal; }
#line default
        break;
      case 14: // MACRO -> MACRO, '>'
#line 66 "ConditionParser.y"
             { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-2].sVal + ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 15: // MACRO -> MACRO, '<'
#line 67 "ConditionParser.y"
             { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-2].sVal + ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 16: // MACRO -> MACRO, '='
#line 68 "ConditionParser.y"
             { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-2].sVal + ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 17: // MACRO -> MACRO, '!'
#line 69 "ConditionParser.y"
             { CurrentSemanticValue.sVal = ValueStack[ValueStack.Depth-2].sVal + ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 18: // NUMERAL -> INTEGER
#line 72 "ConditionParser.y"
           { CurrentSemanticValue.iTerm = new PrimitiveReader<int>(ValueStack[ValueStack.Depth-1].iVal); }
#line default
        break;
      case 19: // NUMERAL -> PATH
#line 73 "ConditionParser.y"
        { CurrentSemanticValue.iTerm = new ValueModuleReader<int>(modRef, ValueStack[ValueStack.Depth-1].sVal); }
#line default
        break;
      case 20: // NUMERAL -> '[', MACRO, ']'
#line 74 "ConditionParser.y"
                 { CurrentSemanticValue.iTerm = new RollReader(Roll.Parse(ValueStack[ValueStack.Depth-2].sVal)); }
#line default
        break;
      case 21: // INTEGER_TERMINAL -> NUMERAL
#line 77 "ConditionParser.y"
           { CurrentSemanticValue.iTerm = ValueStack[ValueStack.Depth-1].iTerm; }
#line default
        break;
      case 22: // INTEGER_TERMINAL -> '-', NUMERAL
#line 78 "ConditionParser.y"
                            { CurrentSemanticValue.iTerm = new NegativeOperator(ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 23: // INTEGER_TERMINAL -> '(', INTEGER_ADD, ')'
#line 79 "ConditionParser.y"
                       { CurrentSemanticValue.iTerm = new ReferenceOperator(ValueStack[ValueStack.Depth-2].iTerm); }
#line default
        break;
      case 24: // INTEGER_TERMINAL -> '-', '(', INTEGER_ADD, ')'
#line 80 "ConditionParser.y"
                                        { CurrentSemanticValue.iTerm = new NegativeOperator(ValueStack[ValueStack.Depth-2].iTerm); }
#line default
        break;
      case 25: // INTEGER_MUL -> INTEGER_TERMINAL
#line 83 "ConditionParser.y"
                    { CurrentSemanticValue.iTerm = ValueStack[ValueStack.Depth-1].iTerm; }
#line default
        break;
      case 26: // INTEGER_MUL -> INTEGER_MUL, '*', INTEGER_TERMINAL
#line 84 "ConditionParser.y"
                                    { CurrentSemanticValue.iTerm = new MulOperator(ValueStack[ValueStack.Depth-3].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 27: // INTEGER_MUL -> INTEGER_MUL, '/', INTEGER_TERMINAL
#line 85 "ConditionParser.y"
                                    { CurrentSemanticValue.iTerm = new DivOperator(ValueStack[ValueStack.Depth-3].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 28: // INTEGER_ADD -> INTEGER_MUL
#line 88 "ConditionParser.y"
               { CurrentSemanticValue.iTerm = ValueStack[ValueStack.Depth-1].iTerm; }
#line default
        break;
      case 29: // INTEGER_ADD -> INTEGER_ADD, '+', INTEGER_MUL
#line 89 "ConditionParser.y"
                               { CurrentSemanticValue.iTerm = new AddOperator(ValueStack[ValueStack.Depth-3].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 30: // INTEGER_ADD -> INTEGER_ADD, '-', INTEGER_MUL
#line 90 "ConditionParser.y"
                               { CurrentSemanticValue.iTerm = new SubOperator(ValueStack[ValueStack.Depth-3].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 31: // INTEGER_COMPARISON -> INTEGER_ADD, '=', '=', INTEGER_ADD
#line 93 "ConditionParser.y"
                                   { CurrentSemanticValue.iCond = new Equality<int>(ValueStack[ValueStack.Depth-4].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 32: // INTEGER_COMPARISON -> INTEGER_ADD, '!', '=', INTEGER_ADD
#line 94 "ConditionParser.y"
                                   { CurrentSemanticValue.iCond = new Inequality<int>(ValueStack[ValueStack.Depth-4].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 33: // INTEGER_COMPARISON -> INTEGER_ADD, '>', '=', INTEGER_ADD
#line 95 "ConditionParser.y"
                                   { CurrentSemanticValue.iCond = new GreaterOrEqual<int>(ValueStack[ValueStack.Depth-4].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 34: // INTEGER_COMPARISON -> INTEGER_ADD, '<', '=', INTEGER_ADD
#line 96 "ConditionParser.y"
                                   { CurrentSemanticValue.iCond = new LowerOrEqual<int>(ValueStack[ValueStack.Depth-4].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 35: // INTEGER_COMPARISON -> INTEGER_ADD, '>', INTEGER_ADD
#line 97 "ConditionParser.y"
                               { CurrentSemanticValue.iCond = new Greater<int>(ValueStack[ValueStack.Depth-3].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 36: // INTEGER_COMPARISON -> INTEGER_ADD, '<', INTEGER_ADD
#line 98 "ConditionParser.y"
                               { CurrentSemanticValue.iCond = new Lower<int>(ValueStack[ValueStack.Depth-3].iTerm, ValueStack[ValueStack.Depth-1].iTerm); }
#line default
        break;
      case 37: // BOOLEAN_TERMINAL -> INTEGER_COMPARISON
#line 101 "ConditionParser.y"
                      { CurrentSemanticValue.iCond = ValueStack[ValueStack.Depth-1].iCond; }
#line default
        break;
      case 38: // BOOLEAN_TERMINAL -> '(', BOOLEAN_OR, ')'
#line 102 "ConditionParser.y"
                      { CurrentSemanticValue.iCond = new RefCondition(ValueStack[ValueStack.Depth-2].iCond); }
#line default
        break;
      case 39: // BOOLEAN_TERMINAL -> '!', '(', BOOLEAN_OR, ')'
#line 103 "ConditionParser.y"
                              { CurrentSemanticValue.iCond = new Not(ValueStack[ValueStack.Depth-2].iCond); }
#line default
        break;
      case 40: // BOOLEAN_AND -> BOOLEAN_TERMINAL
#line 106 "ConditionParser.y"
                    { CurrentSemanticValue.iCond = ValueStack[ValueStack.Depth-1].iCond; }
#line default
        break;
      case 41: // BOOLEAN_AND -> BOOLEAN_AND, '&', '&', BOOLEAN_TERMINAL
#line 107 "ConditionParser.y"
                                        { CurrentSemanticValue.iCond = new And(ValueStack[ValueStack.Depth-4].iCond, ValueStack[ValueStack.Depth-1].iCond); }
#line default
        break;
      case 42: // BOOLEAN_OR -> BOOLEAN_AND
#line 110 "ConditionParser.y"
                      { CurrentSemanticValue.iCond = ValueStack[ValueStack.Depth-1].iCond; }
#line default
        break;
      case 43: // BOOLEAN_OR -> BOOLEAN_OR, '|', '|', BOOLEAN_AND
#line 111 "ConditionParser.y"
                                  { CurrentSemanticValue.iCond = new Or(ValueStack[ValueStack.Depth-4].iCond, ValueStack[ValueStack.Depth-1].iCond); }
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

}
}
