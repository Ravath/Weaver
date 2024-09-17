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
%type <iVal> NUMBER
%type <sVal> PATH
%type <sVal> MACRO
%type <iTerm> TERMINAL
%type <iCond> OPERATION

%%
//=====================MAIN=====================
MAIN
	:   /*empty */
	|  	OPERATION { result = $1; }
	|   MAIN error '\n' { yyerrok(); }
	;	
//=====================TOKENS=====================
PATH
	: CHAR { $$ = $1; }
	| PATH CHAR { $$ = $1 + $2; }
	| PATH DIGIT { $$ = $1 + $2; }
	| PATH '.' CHAR { $$ = $1 + "." + $3; }
	;
NUMBER
	: DIGIT { $$ = $1; }
	| NUMBER DIGIT { $$ = $1 * 10 + $2; }
	;
TERMINAL
	: NUMBER { $$ = new PrimitiveReader<int>($1); }
	| '-' NUMBER { $$ = new PrimitiveReader<int>(-1 * $2); }
	| PATH { $$ = new ValueModuleReader<int>(modRef, $1); }
	| '[' MACRO ']' { $$ = new RollReader(Roll.Parse($2)); }
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
OPERATION
	: TERMINAL '=' '=' TERMINAL { $$ = new Equality<int>($1, $4); }
	| TERMINAL '!' '=' TERMINAL { $$ = new Inequality<int>($1, $4); }
	| TERMINAL '>' '=' TERMINAL { $$ = new GreaterOrEqual<int>($1, $4); }
	| TERMINAL '<' '=' TERMINAL { $$ = new LowerOrEqual<int>($1, $4); }
	| TERMINAL '>' TERMINAL { $$ = new Greater<int>($1, $3); }
	| TERMINAL '<' TERMINAL { $$ = new Lower<int>($1, $3); }
	| '!' OPERATION     { $$ = new Not($2); }
	| '(' OPERATION ')' { $$ = new RefCondition($2); }
	| OPERATION '|' '|' OPERATION { $$ = new Or($1, $4); }
	| OPERATION '&' '&' OPERATION { $$ = new And($1, $4); }
	;