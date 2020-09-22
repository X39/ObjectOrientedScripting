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
start           : %empty                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::START, tokenizer.create_token(), {} }; }
                | filestmnts                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::START, tokenizer.create_token(), { $1 } }; }
                ;
using           : using_low                                             { $$ = $1; }
                | using_low "=" IDENT                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::USING, $2, {} }; }
                ;
using_low       : "using" "namespace" type ";"                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::USING_LOW, $1, { $3 } }; }
                | "using" "enum" type ";"                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::USING_LOW, $1, { $3 } }; }
                | "using" "class" type ";"                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::USING_LOW, $1, { $3 } }; }
                ;
filestmnt       : class                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | namespace                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | mthd                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | using                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | enum                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                ;
filestmnts      : filestmnt                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNTS, tokenizer.create_token(), { $1 } }; }
                | filestmnts filestmnt                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FILESTMNTS, tokenizer.create_token(), { $1 } }; }
                ;
classstmnt      : mthd                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | mthdop                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | mthdcnst                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | class                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | prop                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | using                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | enum                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                ;
classstmnts     : classstmnt                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNTS, tokenizer.create_token(), { $1 } }; }
                | classstmnts classstmnt                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSSTMNTS, tokenizer.create_token(), { $1 } }; }
                ;
codestmnt       : ifelse                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | using                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | for                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | trycatch                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | while                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | switch                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | statement ";"                                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | scope                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | val ";"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | ";"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
                | error                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
                ;
codestmnts      : codestmnt                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNTS, tokenizer.create_token(), { $1 } }; }
                | codestmnts codestmnt                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CODESTMNTS, tokenizer.create_token(), { $1 } }; }
                ;
// --------------------- TYPE ---------------------- \\
type_ident      : IDENT                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE_IDENT, tokenizer.create_token(), { sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE_IDENT, $1, {} } } }; }
                | type_ident "::" IDENT                                 { $$ = $1; $$.nodes.push_back(sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE_IDENT, $3, {} }); }
                ;
type            : type_ident template_use                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE, tokenizer.create_token(), { $1, $2 } }; }
                | "::" type_ident                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE, tokenizer.create_token(), { $2 } }; }
                | "::" type_ident template_use                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE, tokenizer.create_token(), { $2, $3 } }; }
                | type_ident                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPE, tokenizer.create_token(), { $1 } }; }
                ;
typelist        : type                                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPELIST, tokenizer.create_token(), { $1 } }; }
                | type ","                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPELIST, tokenizer.create_token(), { $1 } }; }
                | typelist "," type                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TYPELIST, tokenizer.create_token(), { $1, $3 } }; }
                ;
// ----------------- ENCAPSULATION ----------------- \\
encpsltn        : encpsltn_n_cls                                        { $$ = $1; }
                | "derived"                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN, $1, {} }; }
                | "private"                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN, $1, {} }; }
                ;
encpsltn_n_cls  : "public"                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN_N_CLS, $1, {} }; }
                | "local"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENCPSLTN_N_CLS, $1, {} }; }
                ;
// ------------------- TEMPLATE -------------------- \\
template_def    : type IDENT                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEF, $2, { $1 } }; }
                | type IDENT "=" cval                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEF, $2, { $1, $4 } }; }
                ;
template_defs   : template_def                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { $1 } }; }
                | template_def ","                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { $1 } }; }
                | template_defs "," template_def                        { $$ = $1; $$.nodes.push_back($3); }
                ;
template_use    : "<" typelist ">"                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE_USE, $1, { $2 } }; }
                ;
template        : "<" template_defs ">"                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TEMPLATE, $1, { $2 } }; }
                ;
// ------------------- NAMESPACE ------------------- \\
namespace       : "namespace" type_ident "{" filestmnt "}"              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::NAMESPACE, $1, { $2, $4 } }; }
                ;
// ---------------------- ENUM --------------------- \\
enum            : encpsltn_n_cls "enum" enum_body                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM, $2, { $1, $3 } }; }
                | encpsltn_n_cls "enum" ":" type enum_body              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM, $2, { $1, $4, $5 } }; }
                ;
enum_body       : "{" "}"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_BODY, $1, {} }; }
                | "{" enum_values "}"                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_BODY, $1, { $2 } }; }
                ;
enum_values     : enum_value                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, tokenizer.create_token(), { $1 } }; }
                | enum_value ","                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, tokenizer.create_token(), { $1 } }; }
                | enum_value ";"                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, tokenizer.create_token(), { $1 } }; }
                | enum_values "," enum_value                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUES, tokenizer.create_token(), { $1 } }; }
                | enum_values ";" enum_value                            { $$ = $1; $$.nodes.push_back($3); }
                ;
enum_value      : IDENT                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUE, $1, {} }; }
                | IDENT "=" cval                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ENUM_VALUE, $1, { $3 } }; }
                ;
// --------------------- CLASS --------------------- \\
class           : classhead classbody                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASS, tokenizer.create_token(), { $1, tokenizer.create_token(), {}, $2 } }; }
                | classhead ":" typelist classbody                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASS, tokenizer.create_token(), { $1, {}, $3, $4 } }; }
                | classhead template classbody                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASS, tokenizer.create_token(), { $1, $2, {}, $3 } }; }
                | classhead template ":" typelist classbody             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASS, tokenizer.create_token(), { $1, $2, $4, $5 } }; }
                ;
classhead       : encpsltn "class" IDENT                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSHEAD, $3, { $1 } }; }
                ;
classbody       :  "{" classstmnts "}"                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CLASSBODY, $1, { $2 } }; }
                ;
// -------------------- mthd --------------------- \\
mthd            : mthd_head mthd_args mthd_body                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD, tokenizer.create_token(), { $1, {}, $2, $3 } }; }
                | mthd_head template mthd_args mthd_body                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD, tokenizer.create_token(), { $1, $2, $3, $4 } }; }
                ;
mthd_head       : encpsltn type IDENT                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_HEAD, $3, { $1, $2 } }; }
                | "unbound" encpsltn type IDENT                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_HEAD_UNBOUND, $4, { $2, $3 } }; }
                ;
mthd_args       : "(" ")"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGS, $1, {} }; }
                | "(" mthd_arglist ")"                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGS, $1, { $2 } }; }
                ;
mthd_body       : "{" "}"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_BODY, $1, {} }; }
                | "{" codestmnts "}"                                    { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_BODY, $1, { $2 } }; }
                ;
mthd_arglist    : mthd_arg                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { $1 } }; }
                | mthd_arg ","                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { $1 } }; }
                | mthd_arglist "," mthd_arg                             { $$ = $1; $$.nodes.push_back($3); }
                ;
mthd_arg        : type IDENT                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARG, $2, { $1 } }; }
                | type IDENT "=" cval                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHD_ARG, $2, { $1, $4 } }; }
                ;
// --------------- mthd-OPERATORS ---------------- \\
mthdop          : mthdop_head mthdop_args mthd_body                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP, tokenizer.create_token(), { $1, {}, $2, $3 } }; }
                | mthdop_head template mthdop_args mthd_body            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP, tokenizer.create_token(), { $1, $2, $3, $4 } }; }
                ;
mthdop_head     : "unbound" encpsltn type                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_HEAD, $1, { $2, $3 } }; }
                ;
mthdop_args     : "operator" mthdop_ops1s "(" mthd_arg ")"              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_ARGS, $1, { $2, $4 } }; }
                | mthdop_ops1p "operator" "(" mthd_arg ")"              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_ARGS, $2, { $1, $4 } }; }
                | mthdop_ops1s "operator" "(" mthd_arg ")"              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_ARGS, $2, { $1, $4 } }; }
                | "operator" mthdop_ops2 "(" mthd_arg "," mthd_arg ")"  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDOP_ARGS, $1, { $2, $4 } }; }
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
mthdcnst        : mthdcnst_head mthd_args mthd_body                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDCNST, tokenizer.create_token(), { $1, $2, $3 } }; }
                ;
mthdcnst_head   : encpsltn IDENT                                        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDCNST_HEAD, $2, { $1 } }; }
                | "~" IDENT                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::MTHDDST_HEAD, $2, {} }; }
                ;
// ------------------- PROPERTY -------------------- \\
prop            : prop_head prop_body                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP, tokenizer.create_token(), { $1, $2 } }; }
                ;
prop_head       : encpsltn type IDENT                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_HEAD, $3, { $1, $2 } }; }
                | "unbound" encpsltn type IDENT                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_HEAD_UNBOUND, $4, { $2, $3 } }; }
                ;
prop_body       : "{" prop_get prop_set "}"                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, $1, { $1, $2 } }; }
                | "{" prop_get "}"                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, $1, { $1, {} } }; }
                | "{" prop_set prop_get "}"                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, $1, { $2, $1 } }; }
                | "{" prop_set "}"                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, $1, { {}, $1 } }; }
                | ";"                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_BODY, $1, { {}, {} } }; }
                ;
prop_get        : "get" "(" IDENT ")" mthd_body                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_GET, $1, { { sqf::sqo::cstnode::PROP_GET, $3, {} }, $5 } }; }
                | "get" mthd_body                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_GET, $1, { {}, $2 } }; }
                ;
prop_set        : "set" mthd_body                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::PROP_SET, $2, { $1 } }; }
                ;
// ---------------------- IF ----------------------- \\
ifelse          : "if" "(" val ")" codestmnt                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::IFELSE, $1, { $3, $5 } }; }
                | "if" "(" val ")" codestmnt "else" codestmnt           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::IFELSE, $1, { $3, $5, $7 } }; }
                ;
// ---------------------- FOR ----------------------- \\
for             : "for" "(" for_step ")" codestmnt                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR, $1, { $3, $5 } }; }
                | "for" "(" for_each ")" codestmnt                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR, $1, { $3, $5 } }; }
                ;
for_step        : for_step_arg ";" for_step_arg ";" for_step_arg        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, tokenizer.create_token(), { $1, $2, $3 } }; }
                | for_step_arg ";" for_step_arg ";"                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, tokenizer.create_token(), { $1, $2, {} } }; }
                | for_step_arg ";"              ";" for_step_arg        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, tokenizer.create_token(), { $1, {}, $2 } }; }
                | for_step_arg ";"              ";"                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, tokenizer.create_token(), { $1, tokenizer.create_token(), {} } }; }
                |              ";" for_step_arg ";" for_step_arg        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, tokenizer.create_token(), { {}, $1, $2 } }; }
                |              ";" for_step_arg ";"                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, tokenizer.create_token(), { {}, $1, {} } }; }
                |              ";"              ";" for_step_arg        { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, $1 } }; }
                |              ";"              ";"                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP, tokenizer.create_token(), { {}, tokenizer.create_token(), {} } }; }
                ;
for_step_arg    : val                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { $1 } }; }
                | val ","                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { $1 } }; }
                | val "," for_step_arg                                  { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { $1, $3 } }; }
                ;
for_each        : declaration ":" val                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_EACH, $2, { $1, $3 } }; }
                | declaration ":" val ".." val                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FOR_EACH, $2, { $1, $3, $4 } }; }
                ;
// --------------------- WHILE ---------------------- \\
while           : "while" "(" val ")" codestmnt                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::WHILE, $1, { $3, $5 } }; }
                | "do" codestmnt "while" "(" val ")"                    { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::DO_WHILE, $3, { $5, $2 } }; }
                ;
// --------------------- SWITCH --------------------- \\
switch          : "switch" "(" val ")" "{" switch_cases "}"             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SWITCH, $1, { $3, $6 } }; }
                ;
switch_cases    : switch_case                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SWITCH_CASES, tokenizer.create_token(), { $1 } }; }
                | switch_cases switch_case                              { $$ = $1; $$.nodes.push_back($2); }
                ;
switch_case     : case ":"                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::switch_case, $2, { $1 } }; }
                | case ":" codestmnt                                    { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::switch_case, $2, { $1, $3 } }; }
                ;
case            : "case" cval                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CASE, $1, { $1 } }; }
                | "default"                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CASE, $1, {    } }; }
                ;
// ------------------- TRYCATCH --------------------- \\
try             : "try" codestmnt                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TRY, $1, { $2 } }; }
                ;
catch           : "catch" codestmnt                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CATCH, $1, { $2 } }; }
                | "catch" "(" declaration ")" codestmnt                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CATCH, $1, { $5, $3 } }; }
                ;
catchlist       : catch                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CATCHLIST, tokenizer.create_token(), { $1 } }; }
                | catch catchlist                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CATCHLIST, tokenizer.create_token(), { $1, $2 } }; }
                ;
finally         : "finally" codestmnt                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::FINALLY, $1, { $2 } }; }
                ;
trycatch        : try catchlist finally                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TRYCATCH, tokenizer.create_token(), { $1, $2, $3 } }; }
                | try catchlist                                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TRYCATCH, tokenizer.create_token(), { $1, $2, {} } }; }
                | try finally                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TRYCATCH, tokenizer.create_token(), { $1, {}, $2 } }; }
                ;
// ------------------- TRYCATCH --------------------- \\
statement       : "return"                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, $1, {    } }; }
                | "return" val                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, $1, { $2 } }; }
                | "throw"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, $1, {    } }; }
                | "throw" val                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, $1, { $2 } }; }
                | "delete" val                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, $1, { $2 } }; }
                | "goto" IDENT                                          { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, $1, { sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, $2, {} } } }; }
                | "goto" case                                           { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::STATEMENT, $1, { $2 } }; }
                ;
declaration     : type IDENT                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::DECLARATION, $2, { $1 } }; }
                ;
scope           : "{" "}"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SCOPE, $1, {    } }; }
                | "{" codestmnt "}"                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::SCOPE, $1, { $2 } }; }
                ;
val             : exp01                                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::VAL, tokenizer.create_token(), { $1 } }; }
                | assignment                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::VAL, tokenizer.create_token(), { $1 } }; }
                ;
assignment      : exp12 "=" val                                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ASSIGNMENT, $2, { $1, $3 } }; }
                ;
explist         : val                                                   { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPLIST, tokenizer.create_token(), { $1 } }; }
                | val ","                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::EXPLIST, tokenizer.create_token(), { $1 } }; }
                | explist "," val                                       { $$ = $1; $$.nodes.push_back($3); }
                ;
exp01           : exp02                                                 { $$ = $1; }
                | exp02 "?" exp01 ":" exp01                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::TERNARY_OPERATOR, $2, { $1, $3, $5 } }; }
                ;
exp02           : exp03                                                 { $$ = $1; }
                | exp02 "||" exp03                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp03           : exp04                                                 { $$ = $1; }
                | exp03 "&&" exp04                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp04           : exp05                                                 { $$ = $1; }
                | exp04 "==" exp05                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp04 "~=" exp05                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp04 "!=" exp05                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp05           : exp06                                                 { $$ = $1; }
                | exp05 "<"  exp06                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp05 "<=" exp06                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp05 ">"  exp06                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp05 ">=" exp06                                      { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp06           : exp07                                                 { $$ = $1; }
                | exp06 "+" exp07                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp06 "-" exp07                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp07           : exp08                                                 { $$ = $1; }
                | exp07 "*" exp08                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp07 "/" exp08                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp07 "%" exp08                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp08           : exp09                                                 { $$ = $1; }
                | exp08 "<<"  exp09                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp08 "<<<" exp09                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp08 ">>"  exp09                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp08 ">>>" exp09                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp09           : exp10                                                 { $$ = $1; }
                | exp09 "^" exp10                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp09 "|" exp10                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp10           : exp11                                                 { $$ = $1; }
                | exp10 "&" exp11                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp11           : exp12                                                 { $$ = $1; }
                | "!" exp11                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::UNARY_OPERATOR, $1, { $2 } }; }
                | "~" exp11                                             { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::UNARY_OPERATOR, $1, { $2 } }; }
                | "(" val ")"                                           { $$ = $2; }
                ;
exp12           : expp                                                  { $$ = $1; }
                | call                                                  { $$ = $1; }
                | dotnav                                                { $$ = $1; }
                | arrget                                                { $$ = $1; }
                ;
arrget          : exp12 "[" val "]"                                     { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::ARRGET, $2, { $1, $3 } }; }
                ;
dotnav          : exp12 "." IDENT                                       { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::DOTNAV, $3, { $1 } }; }
                ;
call            : exp12 "(" explist ")"                                 { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CALL, $2, { $1, $3 } }; }
                | exp12 "(" ")"                                         { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CALL, $2, { $1 } }; }
                ;
expp            : cval                                                  { $$ = $1; }
                | "this"                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::THIS, $1, {    } }; }
                | "new" type                                            { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::NEW,  $1, { $2 } }; }
                | declaration                                           { $$ = $1; }
                ;
cval            : L_NUMBER                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, $1, {} }; }
                | L_STRING                                              { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, $1, {} }; }
                | L_CHAR                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, $1, {} }; }
                | "true"                                                { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, $1, {} }; }
                | "false"                                               { $$ = sqf::sqo::cstnode{ sqf::sqo::cstnode::CVAL, $1, {} }; }
                | type                                                  { $$ = $1; }
                ;
%%