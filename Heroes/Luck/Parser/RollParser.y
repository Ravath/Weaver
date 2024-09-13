// ==========================================================================
//  GPPG definition file
// ==========================================================================
//  Version:  1.0.0
//  Machine:  EHLION-MSI
//  DateTime: 01/10/2018 16:37:27
//  UserName: Ehlion
// ==========================================================================
%{
	public IRoll result;
%}

%start MAIN
%partial
%namespace Weaver.Heroes.Dice.Parser
%using Weaver.Heroes.Module.Value
%visibility internal

%union {
	public int iVal;
	public IRoll iRoll;
}

%token <iVal> DIGIT
%type <iVal> NUMBER
%type <iRoll> OPERATION

%%
//=====================MAIN=====================
MAIN
	:   /*empty */
	|  	OPERATION { result = $1; $1.Roll(); }
	|   MAIN error '\n' { yyerrok(); }
	;	
//=====================TOKENS=====================
NUMBER
	: DIGIT { $$ = $1; }
	| NUMBER DIGIT { $$ = $1 * 10 + $2; }
	;
OPERATION
	: NUMBER { $$ = new Intervalle($1, $1); }
	| NUMBER ':' NUMBER { $$ = new Intervalle(new ValueModule2<int>("", $1), new ValueModule2<int>("", $3)); }
	| NUMBER 'd' NUMBER { $$ = new Pool(new ValueModule2<int>("", $1), $3); }
	| OPERATION '>' '=' NUMBER { $$ = new KeepHigherThan($1, $4); }
	| OPERATION '<' '=' NUMBER { $$ = new KeepLowerThan($1, $4); }
	| OPERATION '>' NUMBER { $$ = new KeepHigherThan($1, $3+1); }
	| OPERATION '<' NUMBER { $$ = new KeepLowerThan($1, $3-1); }
	| OPERATION '+' NUMBER { $$ = new AddOp($1, $3); }
	| OPERATION '-' NUMBER { $$ = new AddOp($1, -$3); }
	| OPERATION 'h' NUMBER { $$ = new KeepHighestDice($1, $3); }
	| OPERATION 'l' NUMBER { $$ = new KeepLowestDice($1, $3); }
	| OPERATION 'a' 'h' NUMBER { $$ = new RerollHigherThan($1, $4){RerollOnce = true, DiscardPrevious = false}; }
	| OPERATION 'a' 'l' NUMBER { $$ = new RerollLowerThan($1, $4){RerollOnce = true, DiscardPrevious = false}; }
	| OPERATION 'A' 'h' NUMBER { $$ = new RerollHigherThan($1, $4){DiscardPrevious = false}; }
	| OPERATION 'A' 'l' NUMBER { $$ = new RerollLowerThan($1, $4){DiscardPrevious = false}; }
	| OPERATION 'r' 'h' NUMBER { $$ = new RerollHigherThan($1, $4){RerollOnce = true}; }
	| OPERATION 'r' 'l' NUMBER { $$ = new RerollLowerThan($1, $4){RerollOnce = true}; }
	| OPERATION 'R' 'h' NUMBER { $$ = new RerollHigherThan($1, $4); }
	| OPERATION 'R' 'l' NUMBER { $$ = new RerollLowerThan($1, $4); }
	| OPERATION 'e' 'h' NUMBER { $$ = new ExplodeHigherThan($1, $4){RerollOnce = true}; }
	| OPERATION 'e' 'l' NUMBER { $$ = new ExplodeLowerThan($1, $4){RerollOnce = true}; }
	| OPERATION 'E' 'h' NUMBER { $$ = new ExplodeHigherThan($1, $4); }
	| OPERATION 'E' 'l' NUMBER { $$ = new ExplodeLowerThan($1, $4); }
	| OPERATION 'c' { $$ = new CountOp($1); }
	;