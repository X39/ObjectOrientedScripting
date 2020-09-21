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
%token<token> TILDEEQUAL                "~="
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
%type <sqf::sqo::data::constval> cval
%type <sqf::sqo::data::exp_primary> expp
%type <sqf::sqo::data::declaration> declaration
%type <std::vector<sqf::sqo::data::exp_primary>> exp12 call assignment dotnav arrget
%type <sqf::sqo::data::exp> exp01 exp02 exp03 exp04 exp05 exp06 exp07 exp08 exp09 exp10 exp11 val
%type <std::vector<sqf::sqo::data::exp>> explist

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

declaration     : type IDENT                    { $$ = ::sqf::sqo::data::declaration{ $1, $2 }; }
                ;

scope           : "{" "}"
                | "{" codestmnt "}"
                ;

val             : exp01                         { $$ = $1; }
                ;
explist         : val                           { $$ = std::vector<::sqf::sqo::data::exp>{ $1 }; }
                | val ","                       { $$ = std::vector<::sqf::sqo::data::exp>{ $1 }; }
                | explist "," val               { $$ = $1; $$.push_back($3); }
                ;
exp01           : exp02                         { $$ = $1; }
                | exp02 "?" exp01 ":" exp01     { $$ = sqf::sqo::data::exp{ $1, $3, $5 }; }
                ;
exp02           : exp03                         { $$ = $1; }
                | exp02 "||" exp03              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_LOGICAL_OR,         $1, $3 }; }
                ;
exp03           : exp04                         { $$ = $1; }
                | exp03 "&&" exp04              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_LOGICAL_AND,        $1, $3 }; }
                ;
exp04           : exp05                         { $$ = $1; }
                | exp04 "==" exp05              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_EQUAL,              $1, $3 }; }
                | exp04 "~=" exp05              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_EQUAL_INVARIANT,    $1, $3 }; }
                | exp04 "!=" exp05              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_NOT_EQUAL,          $1, $3 }; }
                ;
exp05           : exp06                         { $$ = $1; }
                | exp05 "<"  exp06              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_LESS_THEN,          $1, $3 }; }
                | exp05 "<=" exp06              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_LESS_THEN_EQUAL,    $1, $3 }; }
                | exp05 ">"  exp06              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_GREATER_THEN,       $1, $3 }; }
                | exp05 ">=" exp06              { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_GREATER_THEN_EQUAL, $1, $3 }; }
                ;
exp06           : exp07                         { $$ = $1; }
                | exp06 "+" exp07               { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_PLUS,               $1, $3 }; }
                | exp06 "-" exp07               { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_MINUS,              $1, $3 }; }
                ;

exp07           : exp08                         { $$ = $1; }
                | exp07 "*" exp08               { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_MULTIPLY,           $1, $3 }; }
                | exp07 "/" exp08               { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_DIVIDE,             $1, $3 }; }
                | exp07 "%" exp08               { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_REMAINDER,          $1, $3 }; }
                ;

exp08           : exp09                         { $$ = $1; }
                | exp08 "<<"  exp09             { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_SHIFT_LEFT2,        $1, $3 }; }
                | exp08 "<<<" exp09             { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_SHIFT_LEFT3,        $1, $3 }; }
                | exp08 ">>"  exp09             { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_SHIFT_RIGHT2,       $1, $3 }; }
                | exp08 ">>>" exp09             { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_SHIFT_RIGHT3,       $1, $3 }; }
                ;

exp09           : exp10                         { $$ = $1; }
                | exp09 "^" exp10               { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_BINARY_XOR,         $1, $3 }; }
                | exp09 "|" exp10               { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_BINARY_OR,          $1, $3 }; }
                ;
exp10           : exp11                         { $$ = $1; }
                | exp10 "&" exp11               { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_BINARY_AND,         $1, $3 }; }
                ;

exp11           : exp12                         { $$ = sqf::sqo::data::exp{ $1 };  }
                | "!" exp11                     { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_LOGICAL_INVERT,     $2 }; }
                | "~" exp11                     { $$ = sqf::sqo::data::exp{ sqf::sqo::data::exp::OP_BINARY_INVERT,      $2 }; }
                | "(" val ")"                   { $$ = $2; }
                ;

exp12           : expp                          { $$ = std::vector<sqf::sqo::data::exp>{ $1 } }; }
                | call                          { $$ = $1; }
                | dotnav                        { $$ = $1; }
                | arrget                        { $$ = $1; }
                ;

arrget          : exp12 "[" val "]"             { $$ = $1; $$.push_back(::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::ARRAY_ACCESS, {}, {}, std::vector<::sqf::sqo::data::value>{ $3 } }); }
                ;

dotnav          : exp12 "." IDENT               { $$ = $1; $$.push_back(::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::NAVIGATE,     {}, {}, {}, $3 }); }
                ;

call            : exp12 "(" explist ")"         { $$ = $1; $$.push_back(::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::CALL,         {}, {}, $3, {} }); }
                | exp12 "(" ")"                 { $$ = $1; $$.push_back(::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::CALL,         {}, {}, {}, {} }); }
                ;
                
assignment      : exp12 "=" val                 { $$ = $1; $$.push_back(::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::ASSIGNMENT,   {}, {}, std::vector<::sqf::sqo::data::value>{ $3 }, {} }); }
                ;

expp            : cval                          { $$ = ::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::CVAL,           $1, {}, {}, {} }; }
                | "this"                        { $$ = ::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::THIS,           {}, {}, {}, {} }; }
                | "new" type                    { $$ = ::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::NEW,            ::sqf::sqo::data::cval{ ::sqf::sqo::data::cval::TYPE,     {}, $2 }, {}, {}, {} }; }
                | declaration                   { $$ = ::sqf::sqo::data::exp_primary{ ::sqf::sqo::data::exp_primary::DECLARATION,    {}, $1, {}, {} }; }
                ;

cval            : L_NUMBER                      { $$ = ::sqf::sqo::data::cval{ ::sqf::sqo::data::cval::NUMBER,   $1, {} }; }
                | L_STRING                      { $$ = ::sqf::sqo::data::cval{ ::sqf::sqo::data::cval::STRING,   $1, {} }; }
                | L_CHAR                        { $$ = ::sqf::sqo::data::cval{ ::sqf::sqo::data::cval::CHAR,     $1, {} }; }
                | "true"                        { $$ = ::sqf::sqo::data::cval{ ::sqf::sqo::data::cval::TRUE,     {}, {} }; }
                | "false"                       { $$ = ::sqf::sqo::data::cval{ ::sqf::sqo::data::cval::FALSE,    {}, {} }; }
                | type                          { $$ = ::sqf::sqo::data::cval{ ::sqf::sqo::data::cval::TYPE,     {}, $1 }; }
                ;

%%