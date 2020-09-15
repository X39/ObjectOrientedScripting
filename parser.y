%glr-parser

%union
{
    token token;
}
// Encapsulation - Item attached is available from everywhere
%token<token> PUBLIC "public"
// Encapsulation - Item attached is available from current namespace and lower
%token<token> LOCAL "local"
// Encapsulation - Item attached is only available to derived classes
%token<token> DERIVED "derived"
// Encapsulation - Item attached is only available to current class
%token<token> PRIVATE "private"
// Modifier - mthd this is attached to is not bound to any class
%token<token> UNBOUND "unbound"

%token<token> EQUAL                     "="
%token<token> ANDAND                    "&&"
%token<token> AND                       "&"
%token<token> SLASH                     "/"
%token<token> STAR                      "*"
%token<token> PERCENT                   "%"
%token<token> VLINEVLINE                "||"
%token<token> VLINE                     "|"
%token<token> CIRCUMFLEX                "^"
%token<token> TILDE                     "~"
%token<token> COLON                     ":"
%token<token> COLONCOLON                "::"
%token<token> PLUS                      "+"
%token<token> PLUSPLUS                  "++"
%token<token> MINUS                     "-"
%token<token> MINUSMINUS                "--"
%token<token> LTEQUAL                   "<="
%token<token> LT                        "<"
%token<token> LTLT                      "<<"
%token<token> LTLTLT                    "<<<"
%token<token> GTEQUAL                   ">="
%token<token> GT                        ">"
%token<token> GTGT                      ">>"
%token<token> GTGTGT                    ">>>"
%token<token> EQUALEQUAL                "=="
%token<token> EXCLAMATIONMARKEQUAL      "!="
%token<token> EXCLAMATIONMARK           "!"
%token<token> CURLYO                    "{"
%token<token> CURLYC                    "}"
%token<token> ROUNDO                    "("
%token<token> ROUNDC                    ")"
%token<token> SQUAREO                   "["
%token<token> SQUAREC                   "]"
%token<token> SEMICOLON                 ";"
%token<token> COMMA                     ","
%token<token> DOT                       "."
%token<token> QUESTIONMARK              "?"
%token<token> ARROWHEAD                 "=>"
%token<token> CLASS                     "class"
%token<token> GET                       "get"
%token<token> SET                       "set"
%token<token> NAMESPACE                 "namespace"
%token<token> IF                        "if"
%token<token> FOR                       "for"
%token<token> ELSE                      "else"
%token<token> WHILE                     "while"
%token<token> DO                        "do"
%token<token> SWITCH                    "switch"
%token<token> CASE                      "case"
%token<token> DEFAULT                   "default"
%token<token> return                    "return"
%token<token> THROW                     "throw"
%token<token> GOTO                      "goto"
%token<token> TRY                       "try"
%token<token> CATCH                     "catch"
%token<token> FINALLY                   "finally"
%token<token> OPERATOR                  "operator"
%token<token> USING                     "using"
%token<token> ENUM                      "enum"
%token<token> DOTDOT                    ".."
%token<token> TRUE                      "true"
%token<token> FLASE                     "false"
%token<token> THIS                      "this"
%token<token> NEW                       "new"
%token<token> DELETE                    "delete"

%token<token> IDENT
%token<token> L_STRING
%token<token> L_NUMBER
%token<token> L_CHAR

%type <sqf::sqo::data::type_ident> type_ident
%type <sqf::sqo::data::type> type
%type <std::vector<sqf::sqo::data::type>> typelist template_use
%type <sqf::sqo::data::encapsulation> encpsltn encpsltn_n_cls
%type <sqf::sqo:data::template_def> template_def
%type <std::vector<sqf::sqo::data::template_def>> template_defs template
%type <std::vector<sqf::sqo::data::constval>> cval

%%
start           : %empty
                | filestmnts
                ;

using           : using_low
                | using_low "=" IDENT
                ;
using_low       : "using" "namespace" type ";"
                | "using" "enum" type ";"
                | "using" "class" type ";"
                ;

filestmnt       : class
                | namespace
                | mthd
                | using
                | enum
                ;
filestmnts      : filestmnt
                | filestmnts filestmnt
                ;
                
classstmnt      : mthd
                | mthdop
                | mthdcnst
                | class
                | prop
                | using
                | enum
                ;
classstmnts     : classstmnt
                | classstmnts classstmnt
                ;
                
codestmnt       : ifelse
                | using
                | for
                | trycatch
                | while
                | switch
                | statement ";"
                | scope
                | declaration ";"
                | val ";"
                | ";"
                | error
                ;
codestmnts      : codestmnt
                | codestmnts codestmnt
                ;

// --------------------- TYPE ---------------------- \\
type_ident      : IDENT                                 { $$ = sqf::sqo::data::type_ident{ $1, { $1.contents } }; }
                | type_ident "::" IDENT                 { $1.idents.push_back($3.contents); $$ = $1; }
                ;
                
type            : type_ident                            { $$ = sqf::sqo::data::type{ $1, {} }; }
                | "::" type_ident                       { $$ = sqf::sqo::data::type{ $2, {} }; }
                | "::" type_ident template_use          { $$ = sqf::sqo::data::type{ $2, $3 }; }
                | type_ident template_use               { $$ = sqf::sqo::data::type{ $1, $2 }; }
                ;

typelist        : type                                  { $$ = std::vector<sqf::sqo::data::type>{ $1 }; }
                | type ","                              { $$ = std::vector<sqf::sqo::data::type>{ $1 }; }
                | typelist "," type                     { $1.push_back($3); $$ = $1; }
                ;

// ----------------- ENCAPSULATION ----------------- \\
encpsltn        : encpsltn_n_cls                        { $$ = $1; }
                | "derived"                             { $$ = sqf::sqo::data::encapsulation::DERIVED; }
                | "private"                             { $$ = sqf::sqo::data::encapsulation::PRIVATE; }
                ;
encpsltn_n_cls  : "public"                              { $$ = sqf::sqo::data::encapsulation::PUBLIC; }
                | "local"                               { $$ = sqf::sqo::data::encapsulation::LOCAL; }
                ;
// ------------------- TEMPLATE -------------------- \\
template_def    : type IDENT                            { $$ = sqf::sqo::data::template_def{ $1, $2.contents, {} }; }
                | type IDENT "=" cval                   { $$ = sqf::sqo::data::template_def{ $1, $2.contents, $4 }; }
                ;

template_defs   : template_def                          { $$ = std::vector<sqf::sqo::data::template_def>{ $1 }; }
                | template_def ","                      { $$ = std::vector<sqf::sqo::data::template_def>{ $1 }; }
                | template_defs "," template_def        { $1.push_back($3); $$ = $1; }
                ;
template_use    : "<" typelist ">"                      { $$ = $2; }
                ;
template        : "<" template_defs ">"                 { $$ = $2; }
                ;

// ------------------- NAMESPACE ------------------- \\
namespace       : "namespace" type_ident "{" filestmnt "}"
                ;

// ---------------------- ENUM --------------------- \\
enum            : encpsltn_n_cls "enum" enum_body
                | encpsltn_n_cls "enum" ":" type enum_body
                ;

enum_body       : "{" "}"
                | "{" enum_values "}"
                ;

enum_values     : enum_value
                | enum_value ","
                | enum_value ";"
                | enum_value "," enum_values
                | enum_value ";" enum_values
                ;

enum_value      : IDENT
                | IDENT "=" cval
                ;

// --------------------- CLASS --------------------- \\
class           : classhead classbody
                | classhead ":" typelist classbody
                | classhead template classbody
                | classhead template ":" typelist classbody
                ;
classhead       : encpsltn "class" IDENT
                ;
classbody       :  "{" classstmnts "}"
                ;

// -------------------- mthd --------------------- \\
mthd            : mthd_head mthd_args mthd_body
                | mthd_head template mthd_args mthd_body
                ;

mthd_head       : encpsltn type IDENT
                | "unbound" encpsltn type IDENT
                ;

mthd_args       : "(" ")"
                | "(" mthd_arglist ")"
                ;

mthd_body       : "{" "}"
                | "{" codestmnts "}"
                | "=>" codestmnt
                ;

mthd_arglist    : mthd_arg
                | mthd_arg ","
                | mthd_arg "," mthd_arglist
                ;

mthd_arg        : type IDENT
                | type IDENT "=" cval
                ;
// --------------- mthd-OPERATORS ---------------- \\
mthdop          : mthdop_head mthdop_args mthd_body
                | mthdop_head template mthdop_args mthd_body
                ;
mthdop_head     : "unbound" encpsltn type
                ;
mthdop_args     : "operator" mthdop_ops1s "(" mthd_arg ")"
                | mthdop_ops1p "operator" "(" mthd_arg ")"
                | mthdop_ops1s "operator" "(" mthd_arg ")"
                | "operator" mthdop_ops2 "(" mthd_arg "," mthd_arg ")"
                ;
mthdop_ops1p    : "!"
                | "~"
                | "-"
                | "+"
                ;
mthdop_ops1s    : "++"
                | "--"
                ;
mthdop_ops2     : "+"
                | "-"
                | "*"
                | "/"
                | ">"
                | ">="
                | ">>"
                | ">>>"
                | "<"
                | "<="
                | "<<"
                | "<<<"
                | "!="
                | "=="
                | ".."
                | "&"
                | "&&"
                | "|"
                | "||"
                | "^"
                | "%"
                ;
// -------------- mthd-CONSTRUCTOR --------------- \\
mthdcnst        : mthdcnst_head mthd_args mthd_body
                ;

mthdcnst_head   : encpsltn IDENT
                | "~" IDENT
                ;

// ------------------- PROPERTY -------------------- \\
prop            : prop_head prop_body
                ;

prop_head       : encpsltn type IDENT
                | "unbound" encpsltn type IDENT
                ;

prop_body       : "{" prop_get prop_set "}"
                | "{" prop_get "}"
                | "{" prop_set prop_get "}"
                | "{" prop_set "}"
                | ";"
                ;
prop_get        : "get" "(" IDENT ")" mthd_body
                ;

prop_set        : "set" mthd_body
                ;

// ---------------------- IF ----------------------- \\
ifelse          : "if" "(" val ")" codestmnt
                | "if" "(" val ")" codestmnt "else" codestmnt
                ;

// ---------------------- FOR ----------------------- \\
for             : "for" "(" for_step ")" codestmnt
                | "for" "(" for_each ")" codestmnt
                ;
for_step        : for_step_arg ";" for_step_arg ";" for_step_arg
                | for_step_arg ";" for_step_arg ";"
                | for_step_arg ";"              ";" for_step_arg
                | for_step_arg ";"              ";" 
                |              ";" for_step_arg ";" for_step_arg
                |              ";" for_step_arg ";" 
                |              ";"              ";" for_step_arg
                ;
for_step_arg    : val
                | val ","
                | val "," for_step_arg
                ;

for_each        : declaration ":" val
                | declaration ":" val ".." val
                ;

// --------------------- WHILE ---------------------- \\
while           : "while" "(" val ")" codestmnt
                | "do" codestmnt "while" "(" val ")"
                ;

// --------------------- SWITCH --------------------- \\
switch          : "switch" "(" val ")" "{" switch_cases "}"
                ;

switch_cases    : switch_case
                | switch_case switch_cases
                ;

switch_case     : case ":"
                | case ":" codestmnt
                ;
case            : "case" cval
                | "default"
                ;

// ------------------- TRYCATCH --------------------- \\
try             : "try" codestmnt
                ;
catch           : "catch" codestmnt
                | "catch" "(" declaration ")" codestmnt
                ;
catchlist       : catch
                | catch catchlist
                ;
finally         : "finally" codestmnt
                ;
trycatch        : try catchlist finally
                | try catchlist
                | try finally
                ;
// ------------------- TRYCATCH --------------------- \\

statement       : "return"
                | "return" val
                | "throw"
                | "throw" val
                | "delete" val
                | "goto" IDENT
                | "goto" case
                ;

declaration     : type IDENT
                ;

assignment      : IDENT "=" val
                | declaration "=" val
                ;

scope           : "{" "}"
                | "{" codestmnt "}"
                ;

val             : exp01
                ;
exp01           : exp02
                | exp02 "?" exp01 ":" exp01
                ;
exp02           : exp03
                | exp02 "||" exp03
                ;
exp03           : exp04
                | exp03 "&&" exp04
                ;
exp04           : exp05
                | exp04 "==" exp05
                | exp04 "!=" exp05
                ;
exp05           : exp06
                | exp05 "<"  exp06
                | exp05 "<=" exp06
                | exp05 ">"  exp06
                | exp05 ">=" exp06
                ;
exp06           : exp07
                | exp06 "+" exp07
                | exp06 "-" exp07
                ;

exp07           : exp08
                | exp07 "*" exp08
                | exp07 "/" exp08
                | exp07 "%" exp08
                ;

exp08           : exp09
                | exp08 "<<"  exp09
                | exp08 "<<<" exp09
                | exp08 ">>"  exp09
                | exp08 ">>>" exp09
                ;

exp09           : exp10
                | exp09 "^" exp10
                | exp09 "|" exp10
                | exp09 "&" exp10
                ;

exp10           : exp11
                | "!" exp10
                | "~" exp10
                ;

exp11           : expp
                | call
                | dotnav
                | arrget
                ;

arrget          : exp11 "[" exp01 "]"
                ;

dotnav          : exp11 "." IDENT
                ;

call            : exp11 "(" explist ")"
                | exp11 "(" ")"
                ;

explist         : exp01
                | exp01 ","
                | explist "," exp01
                ;

expp            : cval                          { }
                | "this"                        { }
                | "new" type                    { }
                | assignment                    { }
                ;

cval            : L_NUMBER                      { $$ = ::sqf::sqo::data::constval{ $1, ::sqf::sqo::data::cval::NUMBER, {} }; }
                | L_STRING                      { $$ = ::sqf::sqo::data::constval{ $1, ::sqf::sqo::data::cval::STRING, {} }; }
                | L_CHAR                        { $$ = ::sqf::sqo::data::constval{ $1, ::sqf::sqo::data::cval::CHAR, {} }; }
                | "true"                        { $$ = ::sqf::sqo::data::constval{ {}, ::sqf::sqo::data::cval::TRUE, {} }; }
                | "false"                       { $$ = ::sqf::sqo::data::constval{ {}, ::sqf::sqo::data::cval::FALSE, {} }; }
                | type                          { $$ = ::sqf::sqo::data::constval{ {}, ::sqf::sqo::data::cval::TYPE, $1 }; }
                ;

%%