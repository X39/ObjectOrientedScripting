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

%type <sqf::sqo::cstnode> type_ident type typelist template_use encpsltn encpsltn_n_cls template_def
%type <sqf::sqo::cstnode> template_defs template cval expp declaration exp12 call dotnav statement
%type <sqf::sqo::cstnode> arrget exp01 exp02 exp03 exp04 exp05 exp06 exp07 exp08 exp09 exp10 exp11 val
%type <sqf::sqo::cstnode> explist start using using_low filestmnt filestmnts classstmnt classstmnts
%type <sqf::sqo::cstnode> codestmnt codestmnts namespace enum enum_body enum_values enum_value class
%type <sqf::sqo::cstnode> classhead classbody mthd mthd_head mthd_args mthd_body mthd_arglist mthd_arg
%type <sqf::sqo::cstnode> mthdop mthdop_head mthdop_args mthdop_ops1p mthdop_ops1s mthdop_ops2 
%type <sqf::sqo::cstnode> mthdcnst mthdcnst_head prop prop_head prop_body prop_get prop_set ifelse
%type <sqf::sqo::cstnode> for for_step for_step_arg for_each while switch switch_cases switch_case
%type <sqf::sqo::cstnode> case try catch catchlist finally trycatch scope assignment

%%
start           : %empty                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::START, {}, {} }; }
                | filestmnts                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::START, {}, { $1 } }; }
                ;
using           : using_low                                             { $$ = $1; }
                | using_low "=" IDENT                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::USING, $2, {} }; }
                ;
using_low       : "using" "namespace" type ";"                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::USING_LOW, $1, { $3 } }; }
                | "using" "enum" type ";"                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::USING_LOW, $1, { $3 } }; }
                | "using" "class" type ";"                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::USING_LOW, $1, { $3 } }; }
                ;
filestmnt       : class                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, {}, { $1 } }; }
                | namespace                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, {}, { $1 } }; }
                | mthd                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, {}, { $1 } }; }
                | using                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, {}, { $1 } }; }
                | enum                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, {}, { $1 } }; }
                ;
filestmnts      : filestmnt                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNTS, {}, { $1 } }; }
                | filestmnts filestmnt                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNTS, {}, { $1 } }; }
                ;
classstmnt      : mthd                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, {}, { $1 } }; }
                | mthdop                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, {}, { $1 } }; }
                | mthdcnst                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, {}, { $1 } }; }
                | class                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, {}, { $1 } }; }
                | prop                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, {}, { $1 } }; }
                | using                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, {}, { $1 } }; }
                | enum                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, {}, { $1 } }; }
                ;
classstmnts     : classstmnt                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNTS, {}, { $1 } }; }
                | classstmnts classstmnt                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNTS, {}, { $1 } }; }
                ;
codestmnt       : ifelse                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | using                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | for                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | trycatch                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | while                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | switch                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | statement ";"                                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | scope                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | val ";"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, { $1 } }; }
                | ";"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, {} }; }
                | error                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, {}, {} }; }
                ;
codestmnts      : codestmnt                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNTS, {}, { $1 } }; }
                | codestmnts codestmnt                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNTS, {}, { $1 } }; }
                ;
// --------------------- TYPE ---------------------- \\
type_ident      : IDENT                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE_IDENT, {}, {} }; }
                | type_ident "::" IDENT                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE_IDENT, {}, {} }; }
                ;
type            : type_ident template_use                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE, {}, {} }; }
                | "::" type_ident                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE, {}, {} }; }
                | "::" type_ident template_use                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE, {}, {} }; }
                | type_ident                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE, {}, {} }; }
                ;
typelist        : type                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPELIST, {}, {} }; }
                | type ","                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPELIST, {}, {} }; }
                | typelist "," type                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPELIST, {}, {} }; }
                ;
// ----------------- ENCAPSULATION ----------------- \\
encpsltn        : encpsltn_n_cls                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN, {}, {} }; }
                | "derived"                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN, {}, {} }; }
                | "private"                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN, {}, {} }; }
                ;
encpsltn_n_cls  : "public"                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN_N_CLS, {}, {} }; }
                | "local"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN_N_CLS, {}, {} }; }
                ;
// ------------------- TEMPLATE -------------------- \\
template_def    : type IDENT                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEF, {}, {} }; }
                | type IDENT "=" cval                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEF, {}, {} }; }
                ;
template_defs   : template_def                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEFS, {}, {} }; }
                | template_def ","                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEFS, {}, {} }; }
                | template_defs "," template_def                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEFS, {}, {} }; }
                ;
template_use    : "<" typelist ">"                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_USE, {}, {} }; }
                ;
template        : "<" template_defs ">"                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE, {}, {} }; }
                ;
// ------------------- NAMESPACE ------------------- \\
namespace       : "namespace" type_ident "{" filestmnt "}"              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::NAMESPACE, {}, {} }; }
                ;
// ---------------------- ENUM --------------------- \\
enum            : encpsltn_n_cls "enum" enum_body                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM, {}, {} }; }
                | encpsltn_n_cls "enum" ":" type enum_body              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM, {}, {} }; }
                ;
enum_body       : "{" "}"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_BODY, {}, {} }; }
                | "{" enum_values "}"                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_BODY, {}, {} }; }
                ;
enum_values     : enum_value                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, {}, {} }; }
                | enum_value ","                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, {}, {} }; }
                | enum_value ";"                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, {}, {} }; }
                | enum_value "," enum_values                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, {}, {} }; }
                | enum_value ";" enum_values                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, {}, {} }; }
                ;
enum_value      : IDENT                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUE, {}, {} }; }
                | IDENT "=" cval                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUE, {}, {} }; }
                ;
// --------------------- CLASS --------------------- \\
class           : classhead classbody                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASS, {}, {} }; }
                | classhead ":" typelist classbody                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASS, {}, {} }; }
                | classhead template classbody                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASS, {}, {} }; }
                | classhead template ":" typelist classbody             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASS, {}, {} }; }
                ;
classhead       : encpsltn "class" IDENT                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSHEAD, {}, {} }; }
                ;
classbody       :  "{" classstmnts "}"                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSBODY, {}, {} }; }
                ;
// -------------------- mthd --------------------- \\
mthd            : mthd_head mthd_args mthd_body                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD, {}, {} }; }
                | mthd_head template mthd_args mthd_body                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD, {}, {} }; }
                ;
mthd_head       : encpsltn type IDENT                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_HEAD, {}, {} }; }
                | "unbound" encpsltn type IDENT                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_HEAD, {}, {} }; }
                ;
mthd_args       : "(" ")"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGS, {}, {} }; }
                | "(" mthd_arglist ")"                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGS, {}, {} }; }
                ;
mthd_body       : "{" "}"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_BODY, {}, {} }; }
                | "{" codestmnts "}"                                    { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_BODY, {}, {} }; }
                | "=>" codestmnt                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_BODY, {}, {} }; }
                ;
mthd_arglist    : mthd_arg                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGLIST, {}, {} }; }
                | mthd_arg ","                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGLIST, {}, {} }; }
                | mthd_arg "," mthd_arglist                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGLIST, {}, {} }; }
                ;
mthd_arg        : type IDENT                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARG, {}, {} }; }
                | type IDENT "=" cval                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARG, {}, {} }; }
                ;
// --------------- mthd-OPERATORS ---------------- \\
mthdop          : mthdop_head mthdop_args mthd_body                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP, {}, {} }; }
                | mthdop_head template mthdop_args mthd_body            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP, {}, {} }; }
                ;
mthdop_head     : "unbound" encpsltn type                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_HEAD, {}, {} }; }
                ;
mthdop_args     : "operator" mthdop_ops1s "(" mthd_arg ")"              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_ARGS, {}, {} }; }
                | mthdop_ops1p "operator" "(" mthd_arg ")"              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_ARGS, {}, {} }; }
                | mthdop_ops1s "operator" "(" mthd_arg ")"              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_ARGS, {}, {} }; }
                | "operator" mthdop_ops2 "(" mthd_arg "," mthd_arg ")"  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_ARGS, {}, {} }; }
                ;
mthdop_ops1p    : "!"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS1P, $1, {} }; }
                | "~"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS1P, $1, {} }; }
                | "-"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS1P, $1, {} }; }
                | "+"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS1P, $1, {} }; }
                ;
mthdop_ops1s    : "++"                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS1S, $1, {} }; }
                | "--"                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS1S, $1, {} }; }
                ;
mthdop_ops2     : "+"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "-"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "*"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "/"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ">"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ">="                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ">>"                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ">>>"                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "<"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "<="                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "<<"                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "<<<"                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "!="                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "=="                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ".."                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "&"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "&&"                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "|"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "||"                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "^"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "%"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_OPS2, $1, {} }; }
                ;
// -------------- mthd-CONSTRUCTOR --------------- \\
mthdcnst        : mthdcnst_head mthd_args mthd_body                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDCNST, {}, {} }; }
                ;
mthdcnst_head   : encpsltn IDENT                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDCNST_HEAD, {}, {} }; }
                | "~" IDENT                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDCNST_HEAD, {}, {} }; }
                ;
// ------------------- PROPERTY -------------------- \\
prop            : prop_head prop_body                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP, {}, {} }; }
                ;
prop_head       : encpsltn type IDENT                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_HEAD, {}, {} }; }
                | "unbound" encpsltn type IDENT                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_HEAD, {}, {} }; }
                ;
prop_body       : "{" prop_get prop_set "}"                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, {}, {} }; }
                | "{" prop_get "}"                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, {}, {} }; }
                | "{" prop_set prop_get "}"                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, {}, {} }; }
                | "{" prop_set "}"                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, {}, {} }; }
                | ";"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, {}, {} }; }
                ;
prop_get        : "get" "(" IDENT ")" mthd_body                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_GET, {}, {} }; }
                ;
prop_set        : "set" mthd_body                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_SET, {}, {} }; }
                ;
// ---------------------- IF ----------------------- \\
ifelse          : "if" "(" val ")" codestmnt                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::IFELSE, {}, {} }; }
                | "if" "(" val ")" codestmnt "else" codestmnt           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::IFELSE, {}, {} }; }
                ;
// ---------------------- FOR ----------------------- \\
for             : "for" "(" for_step ")" codestmnt                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR, {}, {} }; }
                | "for" "(" for_each ")" codestmnt                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR, {}, {} }; }
                ;
for_step        : for_step_arg ";" for_step_arg ";" for_step_arg        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, {}, {} }; }
                | for_step_arg ";" for_step_arg ";"                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, {}, {} }; }
                | for_step_arg ";"              ";" for_step_arg        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, {}, {} }; }
                | for_step_arg ";"              ";"                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, {}, {} }; }
                |              ";" for_step_arg ";" for_step_arg        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, {}, {} }; }
                |              ";" for_step_arg ";"                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, {}, {} }; }
                |              ";"              ";" for_step_arg        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, {}, {} }; }
                ;
for_step_arg    : val                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP_ARG, {}, {} }; }
                | val ","                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP_ARG, {}, {} }; }
                | val "," for_step_arg                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP_ARG, {}, {} }; }
                ;
for_each        : declaration ":" val                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_EACH, {}, {} }; }
                | declaration ":" val ".." val                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_EACH, {}, {} }; }
                ;
// --------------------- WHILE ---------------------- \\
while           : "while" "(" val ")" codestmnt                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::WHILE, {}, {} }; }
                | "do" codestmnt "while" "(" val ")"                    { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::WHILE, {}, {} }; }
                ;
// --------------------- SWITCH --------------------- \\
switch          : "switch" "(" val ")" "{" switch_cases "}"             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SWITCH, {}, {} }; }
                ;
switch_cases    : switch_case                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SWITCH_CASES, {}, {} }; }
                | switch_case switch_cases                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SWITCH_CASES, {}, {} }; }
                ;
switch_case     : case ":"                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::switch_case, {}, {} }; }
                | case ":" codestmnt                                    { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::switch_case, {}, {} }; }
                ;
case            : "case" cval                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CASE, {}, {} }; }
                | "default"                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CASE, {}, {} }; }
                ;
// ------------------- TRYCATCH --------------------- \\
try             : "try" codestmnt                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TRY, {}, {} }; }
                ;
catch           : "catch" codestmnt                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CATCH, {}, {} }; }
                | "catch" "(" declaration ")" codestmnt                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CATCH, {}, {} }; }
                ;
catchlist       : catch                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CATCHLIST, {}, {} }; }
                | catch catchlist                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CATCHLIST, {}, {} }; }
                ;
finally         : "finally" codestmnt                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FINALLY, {}, {} }; }
                ;
trycatch        : try catchlist finally                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TRYCATCH, {}, {} }; }
                | try catchlist                                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TRYCATCH, {}, {} }; }
                | try finally                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TRYCATCH, {}, {} }; }
                ;
// ------------------- TRYCATCH --------------------- \\
statement       : "return"                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, {}, {} }; }
                | "return" val                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, {}, {} }; }
                | "throw"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, {}, {} }; }
                | "throw" val                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, {}, {} }; }
                | "delete" val                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, {}, {} }; }
                | "goto" IDENT                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, {}, {} }; }
                | "goto" case                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, {}, {} }; }
                ;
declaration     : type IDENT                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::DECLARATION, {}, {} }; }
                ;
scope           : "{" "}"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SCOPE, {}, {} }; }
                | "{" codestmnt "}"                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SCOPE, {}, {} }; }
                ;
val             : exp01                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::VAL, {}, {} }; }
                | assignment                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::VAL, {}, {} }; }
                ;
assignment      : exp12 "=" val                                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ASSIGNMENT, {}, {} }; }
                ;
explist         : val                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPLIST, {}, {} }; }
                | val ","                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPLIST, {}, {} }; }
                | explist "," val                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPLIST, {}, {} }; }
                ;
exp01           : exp02                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP01, {}, {} }; }
                | exp02 "?" exp01 ":" exp01                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP01, {}, {} }; }
                ;
exp02           : exp03                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP02, {}, {} }; }
                | exp02 "||" exp03                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP02, {}, {} }; }
                ;
exp03           : exp04                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP03, {}, {} }; }
                | exp03 "&&" exp04                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP03, {}, {} }; }
                ;
exp04           : exp05                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP04, {}, {} }; }
                | exp04 "==" exp05                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP04, {}, {} }; }
                | exp04 "~=" exp05                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP04, {}, {} }; }
                | exp04 "!=" exp05                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP04, {}, {} }; }
                ;
exp05           : exp06                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP05, {}, {} }; }
                | exp05 "<"  exp06                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP05, {}, {} }; }
                | exp05 "<=" exp06                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP05, {}, {} }; }
                | exp05 ">"  exp06                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP05, {}, {} }; }
                | exp05 ">=" exp06                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP05, {}, {} }; }
                ;
exp06           : exp07                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP06, {}, {} }; }
                | exp06 "+" exp07                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP06, {}, {} }; }
                | exp06 "-" exp07                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP06, {}, {} }; }
                ;
exp07           : exp08                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP07, {}, {} }; }
                | exp07 "*" exp08                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP07, {}, {} }; }
                | exp07 "/" exp08                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP07, {}, {} }; }
                | exp07 "%" exp08                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP07, {}, {} }; }
                ;
exp08           : exp09                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP08, {}, {} }; }
                | exp08 "<<"  exp09                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP08, {}, {} }; }
                | exp08 "<<<" exp09                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP08, {}, {} }; }
                | exp08 ">>"  exp09                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP08, {}, {} }; }
                | exp08 ">>>" exp09                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP08, {}, {} }; }
                ;
exp09           : exp10                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP09, {}, {} }; }
                | exp09 "^" exp10                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP09, {}, {} }; }
                | exp09 "|" exp10                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP09, {}, {} }; }
                ;
exp10           : exp11                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP10, {}, {} }; }
                | exp10 "&" exp11                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP10, {}, {} }; }
                ;
exp11           : exp12                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP11, {}, {} }; }
                | "!" exp11                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP11, {}, {} }; }
                | "~" exp11                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP11, {}, {} }; }
                | "(" val ")"                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP11, {}, {} }; }
                ;
exp12           : expp                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP12, {}, {} }; }
                | call                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP12, {}, {} }; }
                | dotnav                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP12, {}, {} }; }
                | arrget                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXP12, {}, {} }; }
                ;
arrget          : exp12 "[" val "]"                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ARRGET, {}, {} }; }
                ;
dotnav          : exp12 "." IDENT                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::DOTNAV, {}, {} }; }
                ;
call            : exp12 "(" explist ")"                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CALL, {}, {} }; }
                | exp12 "(" ")"                                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CALL, {}, {} }; }
                ;
expp            : cval                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPP, {}, {} }; }
                | "this"                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPP, {}, {} }; }
                | "new" type                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPP, {}, {} }; }
                | declaration                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPP, {}, {} }; }
                ;
cval            : L_NUMBER                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, {}, {} }; }
                | L_STRING                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, {}, {} }; }
                | L_CHAR                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, {}, {} }; }
                | "true"                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, {}, {} }; }
                | "false"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, {}, {} }; }
                | type                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, {}, {} }; }
                ;
%%