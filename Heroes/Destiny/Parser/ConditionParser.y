// ==========================================================================
//  GPPG definition file
// ==========================================================================
//  Version:  1.0.0
//  Machine:  EHLION-MSI
//  DateTime: 01/10/2018 16:37:27
//  UserName: Ehlion
// ==========================================================================
%{
	public ICondition result;
	public ModuleReference modRef = new ModuleReference(null);
%}

%start MAIN
%partial
%namespace Weaver.Heroes.Destiny.Parser
%using Weaver.Heroes.Body.Value
%using Weaver.Heroes.Luck;
%visibility internal

%union {
	public int iVal;
	public char cVal;
	public string sVal;
	public IRoll iRoll;
	public ComparableReference<int> iTerm;
	public ICondition iCond;
}

%token <iVal> DIGIT
%token <sVal> CHAR
%type <iVal> INTEGER
%type <sVal> PATH
%type <sVal> MACRO
%type <iTerm> NUMERAL
%type <iTerm> INTEGER_ADD
%type <iTerm> INTEGER_MUL
%type <iCond> INTEGER_COMPARISON
%type <iTerm> INTEGER_TERMINAL
%type <iCond> BOOLEAN_TERMINAL
%type <iCond> BOOLEAN_AND
%type <iCond> BOOLEAN_OR

%%
//=====================MAIN=====================
MAIN
	:   /*empty */
	|  	BOOLEAN_OR { result = $1; }
	|   MAIN error '\n' { yyerrok(); }
	;	
//=====================TOKENS=====================
INTEGER
	: DIGIT { $$ = $1; }
	| INTEGER DIGIT { $$ = $1 * 10 + $2; }
	;
PATH
	: CHAR { $$ = $1; }
	| PATH CHAR { $$ = $1 + $2; }
	| PATH DIGIT { $$ = $1 + $2; }
	| PATH '.' CHAR { $$ = $1 + "." + $3; }
	;
MACRO
	: DIGIT { $$ = $1.ToString(); }
	| MACRO CHAR { $$ = $1 + $2; }
	| MACRO DIGIT { $$ = $1 + $2; }
	| MACRO '>' { $$ = $1 + $2; }
	| MACRO '<' { $$ = $1 + $2; }
	| MACRO '=' { $$ = $1 + $2; }
	| MACRO '!' { $$ = $1 + $2; }
	;
NUMERAL
	: INTEGER { $$ = new PrimitiveReader<int>($1); }
	| PATH { $$ = new ValueModuleReader<int>(modRef, $1); }
	| '[' MACRO ']' { $$ = new RollReader(Roll.Parse($2)); }
	;
INTEGER_TERMINAL
	: NUMERAL { $$ = $1; }
	| '-' NUMERAL %prec UMINUS { $$ = new NegativeOperator($2); }
	| '(' INTEGER_ADD ')' { $$ = new ReferenceOperator($2); }
	| '-' '(' INTEGER_ADD ')' %prec UMINUS { $$ = new NegativeOperator($3); }
	;
INTEGER_MUL
	: INTEGER_TERMINAL { $$ = $1; }
	| INTEGER_MUL '*' INTEGER_TERMINAL { $$ = new MulOperator($1, $3); }
	| INTEGER_MUL '/' INTEGER_TERMINAL { $$ = new DivOperator($1, $3); }
	;
INTEGER_ADD
	: INTEGER_MUL { $$ = $1; }
	| INTEGER_ADD '+' INTEGER_MUL { $$ = new AddOperator($1, $3); }
	| INTEGER_ADD '-' INTEGER_MUL { $$ = new SubOperator($1, $3); }
	;
INTEGER_COMPARISON
	: INTEGER_ADD '=' '=' INTEGER_ADD { $$ = new Equality<int>($1, $4); }
	| INTEGER_ADD '!' '=' INTEGER_ADD { $$ = new Inequality<int>($1, $4); }
	| INTEGER_ADD '>' '=' INTEGER_ADD { $$ = new GreaterOrEqual<int>($1, $4); }
	| INTEGER_ADD '<' '=' INTEGER_ADD { $$ = new LowerOrEqual<int>($1, $4); }
	| INTEGER_ADD '>' INTEGER_ADD { $$ = new Greater<int>($1, $3); }
	| INTEGER_ADD '<' INTEGER_ADD { $$ = new Lower<int>($1, $3); }
	;
BOOLEAN_TERMINAL
	: INTEGER_COMPARISON { $$ = $1; }
	| '(' BOOLEAN_OR ')' { $$ = new RefCondition($2); }
	| '!' '(' BOOLEAN_OR ')'     { $$ = new Not($3); }
	;
BOOLEAN_AND
	: BOOLEAN_TERMINAL { $$ = $1; }
	| BOOLEAN_AND '&' '&' BOOLEAN_TERMINAL { $$ = new And($1, $4); }
	;
BOOLEAN_OR
	: BOOLEAN_AND        { $$ = $1; }
	| BOOLEAN_OR '|' '|' BOOLEAN_AND { $$ = new Or($1, $4); }
	;