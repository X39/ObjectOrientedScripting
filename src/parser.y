%language "c++"
%require "3.0"

%define api.value.type variant
%define api.token.constructor
%define api.namespace { yaoosl::compiler }

%code top {
    #include <string>
    #include <vector>
}


%code requires
{
    #include "tokenizer.hpp"
    #include "cstnode.hpp"
     namespace yaoosl::compiler
     {
          class parser;
     }
}
%code
{
     namespace yaoosl::compiler
     {
          // Return the next token.
          parser::symbol_type yylex (yaoosl::compiler::tokenizer&);
     }
}

%lex-param   { yaoosl::compiler::tokenizer &tokenizer }
%parse-param { yaoosl::compiler::tokenizer &tokenizer }
%parse-param { yaoosl::compiler::cstnode& result }
%parse-param { std::string fpath }

%locations
%define parse.error verbose
%define api.token.prefix {}

%token<yaoosl::compiler::tokenizer::token> NA

// Encapsulation - Item attached is available from everywhere
%token<yaoosl::compiler::tokenizer::token> PUBLIC "public"
// Encapsulation - Item attached is available from current namespace and lower
%token<yaoosl::compiler::tokenizer::token> LOCAL "local"
// Encapsulation - Item attached is only available to derived classes
%token<yaoosl::compiler::tokenizer::token> DERIVED "derived"
// Encapsulation - Item attached is only available to current class
%token<yaoosl::compiler::tokenizer::token> PRIVATE "private"
// Modifier - mthd this is attached to is not bound to any class
%token<yaoosl::compiler::tokenizer::token> UNBOUND "unbound"

%token<yaoosl::compiler::tokenizer::token> EQUAL                     "="
%token<yaoosl::compiler::tokenizer::token> ANDAND                    "&&"
%token<yaoosl::compiler::tokenizer::token> AND                       "&"
%token<yaoosl::compiler::tokenizer::token> SLASH                     "/"
%token<yaoosl::compiler::tokenizer::token> STAR                      "*"
%token<yaoosl::compiler::tokenizer::token> PERCENT                   "%"
%token<yaoosl::compiler::tokenizer::token> VLINEVLINE                "||"
%token<yaoosl::compiler::tokenizer::token> VLINE                     "|"
%token<yaoosl::compiler::tokenizer::token> CIRCUMFLEX                "^"
%token<yaoosl::compiler::tokenizer::token> TILDE                     "~"
%token<yaoosl::compiler::tokenizer::token> COLON                     ":"
%token<yaoosl::compiler::tokenizer::token> COLONCOLON                "::"
%token<yaoosl::compiler::tokenizer::token> PLUS                      "+"
%token<yaoosl::compiler::tokenizer::token> PLUSPLUS                  "++"
%token<yaoosl::compiler::tokenizer::token> MINUS                     "-"
%token<yaoosl::compiler::tokenizer::token> MINUSMINUS                "--"
%token<yaoosl::compiler::tokenizer::token> LTEQUAL                   "<="
%token<yaoosl::compiler::tokenizer::token> LT                        "<"
%token<yaoosl::compiler::tokenizer::token> LTLT                      "<<"
%token<yaoosl::compiler::tokenizer::token> LTLTLT                    "<<<"
%token<yaoosl::compiler::tokenizer::token> GTEQUAL                   ">="
%token<yaoosl::compiler::tokenizer::token> GT                        ">"
%token<yaoosl::compiler::tokenizer::token> GTGT                      ">>"
%token<yaoosl::compiler::tokenizer::token> GTGTGT                    ">>>"
%token<yaoosl::compiler::tokenizer::token> EQUALEQUAL                "=="
%token<yaoosl::compiler::tokenizer::token> TILDEEQUAL                "~="
%token<yaoosl::compiler::tokenizer::token> EXCLAMATIONMARKEQUAL      "!="
%token<yaoosl::compiler::tokenizer::token> EXCLAMATIONMARK           "!"
%token<yaoosl::compiler::tokenizer::token> CURLYO                    "{"
%token<yaoosl::compiler::tokenizer::token> CURLYC                    "}"
%token<yaoosl::compiler::tokenizer::token> ROUNDO                    "("
%token<yaoosl::compiler::tokenizer::token> ROUNDC                    ")"
%token<yaoosl::compiler::tokenizer::token> SQUAREO                   "["
%token<yaoosl::compiler::tokenizer::token> SQUAREC                   "]"
%token<yaoosl::compiler::tokenizer::token> SEMICOLON                 ";"
%token<yaoosl::compiler::tokenizer::token> COMMA                     ","
%token<yaoosl::compiler::tokenizer::token> DOT                       "."
%token<yaoosl::compiler::tokenizer::token> QUESTIONMARK              "?"
%token<yaoosl::compiler::tokenizer::token> ARROWHEAD                 "=>"
%token<yaoosl::compiler::tokenizer::token> DOTDOT                    ".."
%token<yaoosl::compiler::tokenizer::token> BREAK                     "break"
%token<yaoosl::compiler::tokenizer::token> CASE                      "case"
%token<yaoosl::compiler::tokenizer::token> CONTINUE                  "continue"
%token<yaoosl::compiler::tokenizer::token> CLASS                     "class"
%token<yaoosl::compiler::tokenizer::token> CATCH                     "catch"
%token<yaoosl::compiler::tokenizer::token> CONVERSION                "conversion"
%token<yaoosl::compiler::tokenizer::token> DEFAULT                   "default"
%token<yaoosl::compiler::tokenizer::token> DO                        "do"
%token<yaoosl::compiler::tokenizer::token> DELETE                    "delete"
%token<yaoosl::compiler::tokenizer::token> ELSE                      "else"
%token<yaoosl::compiler::tokenizer::token> ENUM                      "enum"
%token<yaoosl::compiler::tokenizer::token> FOR                       "for"
%token<yaoosl::compiler::tokenizer::token> FINALLY                   "finally"
%token<yaoosl::compiler::tokenizer::token> FALSE                     "false"
%token<yaoosl::compiler::tokenizer::token> GET                       "get"
%token<yaoosl::compiler::tokenizer::token> GOTO                      "goto"
%token<yaoosl::compiler::tokenizer::token> IF                        "if"
%token<yaoosl::compiler::tokenizer::token> NAMESPACE                 "namespace"
%token<yaoosl::compiler::tokenizer::token> NEW                       "new"
%token<yaoosl::compiler::tokenizer::token> OPERATOR                  "operator"
%token<yaoosl::compiler::tokenizer::token> RETURN                    "return"
%token<yaoosl::compiler::tokenizer::token> SET                       "set"
%token<yaoosl::compiler::tokenizer::token> SWITCH                    "switch"
%token<yaoosl::compiler::tokenizer::token> THROW                     "throw"
%token<yaoosl::compiler::tokenizer::token> TRY                       "try"
%token<yaoosl::compiler::tokenizer::token> TRUE                      "true"
%token<yaoosl::compiler::tokenizer::token> THIS                      "this"
%token<yaoosl::compiler::tokenizer::token> USING                     "using"
%token<yaoosl::compiler::tokenizer::token> WHILE                     "while"

%token<yaoosl::compiler::tokenizer::token> L_IDENT
%token<yaoosl::compiler::tokenizer::token> L_STRING
%token<yaoosl::compiler::tokenizer::token> L_NUMBER
%token<yaoosl::compiler::tokenizer::token> L_CHAR

%type <yaoosl::compiler::cstnode> type_ident type typelist template_use encpsltn encpsltn_n_cls template_def
%type <yaoosl::compiler::cstnode> template_defs template cval expp declaration exp12 call dotnav statement
%type <yaoosl::compiler::cstnode> arrget exp01 exp02 exp03 exp04 exp05 exp06 exp07 exp08 exp09 exp10 exp11 val
%type <yaoosl::compiler::cstnode> explist using using_low filestmnt filestmnts classstmnt classstmnts cnvrsn
%type <yaoosl::compiler::cstnode> codestmnt codestmnts namespace enum enum_body enum_values enum_value class
%type <yaoosl::compiler::cstnode> classhead classbody mthd mthd_head mthd_args mthd_body mthd_arglist mthd_arg
%type <yaoosl::compiler::cstnode> mthdop mthdop_head mthdop_args mthdop_ops1p mthdop_ops1s mthdop_ops2 
%type <yaoosl::compiler::cstnode> mthdcnst mthdcnst_head prop prop_head prop_body prop_get prop_set ifelse
%type <yaoosl::compiler::cstnode> for for_step for_step_arg for_each while switch switch_cases switch_case
%type <yaoosl::compiler::cstnode> case try catch catchlist finally trycatch scope assignment uecnvrsn ucnvrsn

%start start

%%
start           : %empty                                                { result = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::START, tokenizer.create_token(), {} }; }
                | filestmnts                                            { result = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::START, tokenizer.create_token(), { $1 } }; }
                ;
using           : using_low ";"                                         { $$ = $1; }
                | using_low "=" L_IDENT ";"                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING, $2, {} }; }
                ;
using_low       : "using" "namespace" type                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, $1, { $3 } }; }
                | "using" "enum" type                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, $1, { $3 } }; }
                | "using" "class" type                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, $1, { $3 } }; }
                ;
filestmnt       : class                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | namespace                                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | mthd                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | uecnvrsn                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | using                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                | enum                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { $1 } }; }
                ;
filestmnts      : filestmnt                                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNTS, tokenizer.create_token(), { $1 } }; }
                | filestmnts filestmnt                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNTS, tokenizer.create_token(), { $1 } }; }
                ;
classstmnt      : mthd                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | mthdop                                                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | mthdcnst                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | cnvrsn                                                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | class                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | prop                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | using                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                | enum                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { $1 } }; }
                ;
classstmnts     : classstmnt                                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNTS, tokenizer.create_token(), { $1 } }; }
                | classstmnts classstmnt                                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNTS, tokenizer.create_token(), { $1 } }; }
                ;
codestmnt       : ifelse                                                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | ucnvrsn                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | using                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | for                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | trycatch                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | while                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | switch                                                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | statement ";"                                         { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | scope                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | val ";"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { $1 } }; }
                | ";"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
                | error                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
                ;
codestmnts      : codestmnt                                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNTS, tokenizer.create_token(), { $1 } }; }
                | codestmnts codestmnt                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNTS, tokenizer.create_token(), { $1 } }; }
                ;
// --------------------- TYPE ---------------------- \\
type_ident      : L_IDENT                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, tokenizer.create_token(), { yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, $1, {} } } }; }
                | type_ident "::" L_IDENT                               { $$ = $1; $$.nodes.push_back(yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, $3, {} }); }
                ;
type            : type_ident template_use                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { $1, $2 } }; }
                | "::" type_ident template_use                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { $2, $3 } }; }
                | type_ident template_use "[" "]"                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ARRTYPE, tokenizer.create_token(), { $1, $2 } }; }
                | "::" type_ident template_use "[" "]"                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ARRTYPE, tokenizer.create_token(), { $2, $3 } }; }
                ;
typelist        : type                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { $1 } }; }
                | typelist "," type                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { $1, $3 } }; }
                | typelist ","                                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { $1 } }; }
                ;
// ----------------- ENCAPSULATION ----------------- \\
encpsltn        : encpsltn_n_cls                                        { $$ = $1; }
                | "derived"                                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN, $1, {} }; }
                | "private"                                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN, $1, {} }; }
                ;
encpsltn_n_cls  : "public"                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN_N_CLS, $1, {} }; }
                | "local"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN_N_CLS, $1, {} }; }
                ;
// ------------------- TEMPLATE -------------------- \\
template_def    : type L_IDENT                                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEF, $2, { $1 } }; }
                | type L_IDENT "=" cval                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEF, $2, { $1, $4 } }; }
                ;
template_defs   : template_def                                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { $1 } }; }
                | template_defs "," template_def                        { $$ = $1; $$.nodes.push_back($3); }
                | template_defs ","                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { $1 } }; }
                ;
template_use    : %empty                                                { $$ = yaoosl::compiler::cstnode{ }; }
                | "<" typelist ">"                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_USE, $1, { $2 } }; }
                ;
template        : "<" template_defs ">"                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE, $1, { $2 } }; }
                ;
// ------------------- NAMESPACE ------------------- \\
namespace       : "namespace" type_ident "{" filestmnt "}"              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::NAMESPACE, $1, { $2, $4 } }; }
                ;
// ---------------------- ENUM --------------------- \\
enum            : encpsltn_n_cls "enum" enum_body                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM, $2, { $1, $3 } }; }
                | encpsltn_n_cls "enum" ":" type enum_body              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM, $2, { $1, $4, $5 } }; }
                ;
enum_body       : "{" "}"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_BODY, $1, {} }; }
                | "{" enum_values "}"                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_BODY, $1, { $2 } }; }
                ;
enum_values     : enum_value                                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { $1 } }; }
                | enum_value ","                                        { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { $1 } }; }
                | enum_value ";"                                        { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { $1 } }; }
                | enum_values "," enum_value                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { $1 } }; }
                | enum_values ";" enum_value                            { $$ = $1; $$.nodes.push_back($3); }
                ;
enum_value      : L_IDENT                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUE, $1, {} }; }
                | L_IDENT "=" cval                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUE, $1, { $3 } }; }
                ;
// --------------------- CLASS --------------------- \\
class           : classhead classbody                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { $1, {}, {}, $2 } }; }
                | classhead ":" typelist classbody                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { $1, {}, $3, $4 } }; }
                | classhead template classbody                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { $1, $2, {}, $3 } }; }
                | classhead template ":" typelist classbody             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { $1, $2, $4, $5 } }; }
                ;
classhead       : encpsltn "class" L_IDENT                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSHEAD, $3, { $1 } }; }
                ;
classbody       :  "{" classstmnts "}"                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSBODY, $1, { $2 } }; }
                ;
// -------------------- mthd --------------------- \\
mthd            : mthd_head mthd_args mthd_body                         { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD, tokenizer.create_token(), { $1, {}, $2, $3 } }; }
                | mthd_head template mthd_args mthd_body                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD, tokenizer.create_token(), { $1, $2, $3, $4 } }; }
                ;
mthd_head       : encpsltn type L_IDENT                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_HEAD, $3, { $1, $2 } }; }
                | "unbound" encpsltn type L_IDENT                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_HEAD_UNBOUND, $4, { $2, $3 } }; }
                ;
mthd_args       : "(" ")"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGS, $1, {} }; }
                | "(" mthd_arglist ")"                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGS, $1, { $2 } }; }
                ;
mthd_body       : "{" "}"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_BODY, $1, {} }; }
                | "{" codestmnts "}"                                    { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_BODY, $1, { $2 } }; }
                ;
mthd_arglist    : mthd_arg                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { $1 } }; }
                | mthd_arglist "," mthd_arg                             { $$ = $1; $$.nodes.push_back($3); }
                | mthd_arglist ","                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { $1 } }; }
                ;
mthd_arg        : type L_IDENT                                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, $2, { $1 } }; }
                | type L_IDENT "=" cval                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, $2, { $1, $4 } }; }
                ;
// ----------------- conversion ------------------ \\
cnvrsn          : type "conversion" mthd_body                           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CONVERSION, $2, { $1, $3 } }; }
                ;
ucnvrsn         : "unbound" type "conversion"
                  "(" mthd_arg ")" mthd_body                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CONVERSION, $3, { $2, $5, $7 } }; }
                ;
uecnvrsn        : "unbound" encpsltn type "conversion"
                  "(" mthd_arg ")" mthd_body                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CONVERSION, $4, { $2, $3, $6, $8 } }; }
                ;
// --------------- mthd-OPERATORS ---------------- \\
mthdop          : mthdop_head mthdop_args mthd_body                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP, tokenizer.create_token(), { $1, {}, $2, $3 } }; }
                | mthdop_head template mthdop_args mthd_body            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP, tokenizer.create_token(), { $1, $2, $3, $4 } }; }
                ;
mthdop_head     : "unbound" encpsltn type                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_HEAD, $1, { $2, $3 } }; }
                ;
mthdop_args     : "operator" mthdop_ops1s "(" mthd_arg ")"              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, $1, { $2, $4 } }; }
                | mthdop_ops1p "operator" "(" mthd_arg ")"              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, $2, { $1, $4 } }; }
                | mthdop_ops1s "operator" "(" mthd_arg ")"              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, $2, { $1, $4 } }; }
                | "operator" mthdop_ops2 "(" mthd_arg "," mthd_arg ")"  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, $1, { $2, $4 } }; }
                ;
mthdop_ops1p    : "!"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, $1, {} }; }
                | "~"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, $1, {} }; }
                | "-"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, $1, {} }; }
                | "+"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, $1, {} }; }
                ;
mthdop_ops1s    : "++"                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1S, $1, {} }; }
                | "--"                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1S, $1, {} }; }
                ;
mthdop_ops2     : "+"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "-"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "*"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "/"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ">"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ">="                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ">>"                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ">>>"                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "<"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "<="                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "<<"                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "<<<"                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "!="                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "=="                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | ".."                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "&"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "&&"                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "|"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "||"                                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "^"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                | "%"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, $1, {} }; }
                ;
// -------------- mthd-CONSTRUCTOR --------------- \\
mthdcnst        : mthdcnst_head mthd_args mthd_body                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDCNST, tokenizer.create_token(), { $1, $2, $3 } }; }
                ;
mthdcnst_head   : encpsltn L_IDENT                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDCNST_HEAD, $2, { $1 } }; }
                | "~" L_IDENT                                           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDDST_HEAD, $2, {} }; }
                ;
// ------------------- PROPERTY -------------------- \\
prop            : prop_head prop_body                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP, tokenizer.create_token(), { $1, $2 } }; }
                ;
prop_head       : encpsltn type L_IDENT                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_HEAD, $3, { $1, $2 } }; }
                | "unbound" encpsltn type L_IDENT                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_HEAD_UNBOUND, $4, { $2, $3 } }; }
                ;
prop_body       : "{" prop_get prop_set "}"                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, $1, { $2, $3 } }; }
                | "{" prop_get "}"                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, $1, { $2, {} } }; }
                | "{" prop_set prop_get "}"                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, $1, { $3, $2 } }; }
                | "{" prop_set "}"                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, $1, { {}, $2 } }; }
                | ";"                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, $1, { {}, {} } }; }
                ;
prop_set        : "set" "(" L_IDENT ")" mthd_body                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, $1, { {}, { yaoosl::compiler::cstnode::PROP_SET, $3, {} }, $5 } }; }
                | encpsltn "set" "(" L_IDENT ")" mthd_body              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, $2, { $1, { yaoosl::compiler::cstnode::PROP_SET, $4, {} }, $6 } }; }
                | "set" mthd_body                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, $1, { {}, {}, $2 } }; }
                | encpsltn "set" mthd_body                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, $2, { $1, {}, $3 } }; }
                ;
prop_get        : "get" mthd_body                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_GET, $1, { {}, $2 } }; }
                | encpsltn "get" mthd_body                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_GET, $2, { $1, $3 } }; }
                ;
// ---------------------- IF ----------------------- \\
ifelse          : "if" "(" val ")" codestmnt                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::IFELSE, $1, { $3, $5 } }; }
                | "if" "(" val ")" codestmnt "else" codestmnt           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::IFELSE, $1, { $3, $5, $7 } }; }
                ;
// ---------------------- FOR ----------------------- \\
for             : "for" "(" for_step ")" codestmnt                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR, $1, { $3, $5 } }; }
                | "for" "(" for_each ")" codestmnt                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR, $1, { $3, $5 } }; }
                ;
for_step        : for_step_arg ";" for_step_arg ";" for_step_arg        { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { $1, $3, $5 } }; }
                | for_step_arg ";" for_step_arg ";"                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { $1, $3, {} } }; }
                | for_step_arg ";"              ";" for_step_arg        { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { $1, {}, $4 } }; }
                | for_step_arg ";"              ";"                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { $1, {}, {} } }; }
                |              ";" for_step_arg ";" for_step_arg        { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, $2, $4 } }; }
                |              ";" for_step_arg ";"                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, $2, {} } }; }
                |              ";"              ";" for_step_arg        { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, $3 } }; }
                |              ";"              ";"                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, {} } }; }
                ;
for_step_arg    : val                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { $1 } }; }
                | for_step_arg ","                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { $1 } }; }
                | for_step_arg "," val                                  { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { $1, $3 } }; }
                ;
for_each        : declaration ":" val                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_EACH, $2, { $1, $3 } }; }
                | declaration ":" val ".." val                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_EACH, $2, { $1, $3, $5 } }; }
                ;
// --------------------- WHILE ---------------------- \\
while           : "while" "(" val ")" codestmnt                         { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::WHILE, $1, { $3, $5 } }; }
                | "do" codestmnt "while" "(" val ")"                    { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DO_WHILE, $3, { $5, $2 } }; }
                ;
// --------------------- SWITCH --------------------- \\
switch          : "switch" "(" val ")" "{" switch_cases "}"             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH, $1, { $3, $6 } }; }
                ;
switch_cases    : switch_case                                           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASES, tokenizer.create_token(), { $1 } }; }
                | switch_cases switch_case                              { $$ = $1; $$.nodes.push_back($2); }
                ;
switch_case     : case ":"                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE,          $2, { $1 } }; }
                | case ":" codestmnt "break" ";"                        { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE_BREAK,    $2, { $1, $3 } }; }
                | case ":" codestmnt "continue" ";"                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE_CONTINUE, $2, { $1, $3 } }; }
                ;
case            : "case" cval                                           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CASE, $1, { $2 } }; }
                | "default"                                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CASE, $1, {    } }; }
                ;
// ------------------- TRYCATCH --------------------- \\
try             : "try" codestmnt                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRY, $1, { $2 } }; }
                ;
catch           : "catch" codestmnt                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCH, $1, { $2 } }; }
                | "catch" "(" declaration ")" codestmnt                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCH, $1, { $5, $3 } }; }
                ;
catchlist       : catch                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCHLIST, tokenizer.create_token(), { $1 } }; }
                | catch catchlist                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCHLIST, tokenizer.create_token(), { $1, $2 } }; }
                ;
finally         : "finally" codestmnt                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FINALLY, $1, { $2 } }; }
                ;
trycatch        : try catchlist finally                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { $1, $2, $3 } }; }
                | try catchlist                                         { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { $1, $2, {} } }; }
                | try finally                                           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { $1, {}, $2 } }; }
                ;
// ------------------- TRYCATCH --------------------- \\
statement       : "return"                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, {    } }; }
                | "return" val                                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, { $2 } }; }
                | "throw"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, {    } }; }
                | "throw" val                                           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, { $2 } }; }
                | "delete" val                                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, { $2 } }; }
                | "goto" L_IDENT                                        { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, { yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $2, {} } } }; }
                | "goto" case                                           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, { $2 } }; }
                | "break"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, {    } }; }
                | "continue"                                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, $1, {    } }; }
                ;
declaration     : type L_IDENT                                          { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DECLARATION, $2, { $1 } }; }
                ;
scope           : "{" "}"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SCOPE, $1, {    } }; }
                | "{" codestmnt "}"                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SCOPE, $1, { $2 } }; }
                ;
val             : exp01                                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::VAL, tokenizer.create_token(), { $1 } }; }
                | assignment                                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::VAL, tokenizer.create_token(), { $1 } }; }
                ;
assignment      : exp12 "=" val                                         { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ASSIGNMENT, $2, { $1, $3 } }; }
                ;
explist         : val                                                   { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::EXPLIST, tokenizer.create_token(), { $1 } }; }
                | explist "," val                                       { $$ = $1; $$.nodes.push_back($3); }
                | explist ","                                           { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::EXPLIST, tokenizer.create_token(), { $1 } }; }
                ;
exp01           : exp02                                                 { $$ = $1; }
                | exp02 "?" exp01 ":" exp01                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TERNARY_OPERATOR, $2, { $1, $3, $5 } }; }
                ;
exp02           : exp03                                                 { $$ = $1; }
                | exp02 "||" exp03                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp03           : exp04                                                 { $$ = $1; }
                | exp03 "&&" exp04                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp04           : exp05                                                 { $$ = $1; }
                | exp04 "==" exp05                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp04 "~=" exp05                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp04 "!=" exp05                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp05           : exp06                                                 { $$ = $1; }
                | exp05 "<"  exp06                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp05 "<=" exp06                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp05 ">"  exp06                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp05 ">=" exp06                                      { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp06           : exp07                                                 { $$ = $1; }
                | exp06 "+" exp07                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp06 "-" exp07                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp07           : exp08                                                 { $$ = $1; }
                | exp07 "*" exp08                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp07 "/" exp08                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp07 "%" exp08                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp08           : exp09                                                 { $$ = $1; }
                | exp08 "<<"  exp09                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp08 "<<<" exp09                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp08 ">>"  exp09                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp08 ">>>" exp09                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp09           : exp10                                                 { $$ = $1; }
                | exp09 "^" exp10                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                | exp09 "|" exp10                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp10           : exp11                                                 { $$ = $1; }
                | exp10 "&" exp11                                       { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, $2, { $1, $3 } }; }
                ;
exp11           : exp12                                                 { $$ = $1; }
                | "!" exp11                                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::UNARY_OPERATOR, $1, { $2 } }; }
                | "~" exp11                                             { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::UNARY_OPERATOR, $1, { $2 } }; }
                | "(" val ")"                                           { $$ = $2; }
                ;
exp12           : expp                                                  { $$ = $1; }
                | call                                                  { $$ = $1; }
                | dotnav                                                { $$ = $1; }
                | arrget                                                { $$ = $1; }
                ;
arrget          : exp12 "[" val "]"                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ARRGET, $2, { $1, $3 } }; }
                ;
dotnav          : exp12 "." L_IDENT                                     { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DOTNAV, $3, { $1 } }; }
                ;
call            : exp12 "(" explist ")"                                 { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CALL, $2, { $1, $3 } }; }
                | exp12 "(" ")"                                         { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CALL, $2, { $1 } }; }
                ;
expp            : cval                                                  { $$ = $1; }
                | "this"                                                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::THIS, $1, {    } }; }
                | "new" type                                            { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::NEW,  $1, { $2 } }; }
                | declaration                                           { $$ = $1; }
                ;
cval            : L_NUMBER                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, $1, {} }; }
                | L_STRING                                              { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, $1, {} }; }
                | L_CHAR                                                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, $1, {} }; }
                | "true"                                                { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, $1, {} }; }
                | "false"                                               { $$ = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, $1, {} }; }
                | type                                                  { $$ = $1; }
                ;
%%



namespace yaoosl::compiler
{
     void parser::error(const location_type& loc, const std::string& msg)
     {
         std::cout <<
             tokenizer.create_token().path << std::endl <<
             "[L" << loc.begin.line << "|C" << loc.begin.column << "]  " << msg << std::endl;
     }
     inline parser::symbol_type yylex(yaoosl::compiler::tokenizer& tokenizer)
     {
         auto token = tokenizer.next();
#if _DEBUG
        std::cout << "\u001b[33m" << "TOKEN: " << token.to_string() << "\u001b[0m" << std::endl;
#endif
         parser::location_type loc;
         loc.begin.line = token.line;
         loc.begin.column = token.column;
         loc.end.line = token.line;
         loc.end.column = token.column + token.contents.length();

         switch (token.type)
         {
         case tokenizer::etoken::eof:
         case tokenizer::etoken::invalid:
         default:
#if _DEBUG
         {
             tokenizer.undo_prev();
             auto token = tokenizer.next();
         }
#endif
             return parser::make_NA(token, loc);


         case tokenizer::etoken::m_line: return yylex(tokenizer);

         case tokenizer::etoken::i_comment_line: return yylex(tokenizer);
         case tokenizer::etoken::i_comment_block: return yylex(tokenizer);
         case tokenizer::etoken::i_whitespace: return yylex(tokenizer);



         case tokenizer::etoken::l_ident: return parser::make_L_IDENT(token, loc);
         case tokenizer::etoken::l_string: return parser::make_L_STRING(token, loc);
         case tokenizer::etoken::l_number: return parser::make_L_NUMBER(token, loc);
         case tokenizer::etoken::l_char: return parser::make_L_CHAR(token, loc);

         case tokenizer::etoken::t_case: return parser::make_CASE(token, loc);
         case tokenizer::etoken::t_catch: return parser::make_CATCH(token, loc);
         case tokenizer::etoken::t_class: return parser::make_CLASS(token, loc);
         case tokenizer::etoken::t_conversion: return parser::make_CONVERSION(token, loc);
         case tokenizer::etoken::t_default: return parser::make_DEFAULT(token, loc);
         case tokenizer::etoken::t_delete: return parser::make_DELETE(token, loc);
         case tokenizer::etoken::t_derived: return parser::make_DERIVED(token, loc);
         case tokenizer::etoken::t_do: return parser::make_DO(token, loc);
         case tokenizer::etoken::t_else: return parser::make_ELSE(token, loc);
         case tokenizer::etoken::t_enum: return parser::make_ENUM(token, loc);
         case tokenizer::etoken::t_false: return parser::make_FALSE(token, loc);
         case tokenizer::etoken::t_finally: return parser::make_FINALLY(token, loc);
         case tokenizer::etoken::t_for: return parser::make_FOR(token, loc);
         case tokenizer::etoken::t_get: return parser::make_GET(token, loc);
         case tokenizer::etoken::t_goto: return parser::make_GOTO(token, loc);
         case tokenizer::etoken::t_if: return parser::make_IF(token, loc);
         case tokenizer::etoken::t_local: return parser::make_LOCAL(token, loc);
         case tokenizer::etoken::t_namespace: return parser::make_NAMESPACE(token, loc);
         case tokenizer::etoken::t_new: return parser::make_NEW(token, loc);
         case tokenizer::etoken::t_operator: return parser::make_OPERATOR(token, loc);
         case tokenizer::etoken::t_private: return parser::make_PRIVATE(token, loc);
         case tokenizer::etoken::t_public: return parser::make_PUBLIC(token, loc);
         case tokenizer::etoken::t_return: return parser::make_RETURN(token, loc);
         case tokenizer::etoken::t_set: return parser::make_SET(token, loc);
         case tokenizer::etoken::t_switch: return parser::make_SWITCH(token, loc);
         case tokenizer::etoken::t_this: return parser::make_THIS(token, loc);
         case tokenizer::etoken::t_throw: return parser::make_THROW(token, loc);
         case tokenizer::etoken::t_true: return parser::make_TRUE(token, loc);
         case tokenizer::etoken::t_try: return parser::make_TRY(token, loc);
         case tokenizer::etoken::t_unbound: return parser::make_UNBOUND(token, loc);
         case tokenizer::etoken::t_using: return parser::make_USING(token, loc);
         case tokenizer::etoken::t_while: return parser::make_WHILE(token, loc);

         case tokenizer::etoken::s_and: return parser::make_AND(token, loc);
         case tokenizer::etoken::s_andand: return parser::make_ANDAND(token, loc);
         case tokenizer::etoken::s_arrowhead: return parser::make_ARROWHEAD(token, loc);
         case tokenizer::etoken::s_circumflex: return parser::make_CIRCUMFLEX(token, loc);
         case tokenizer::etoken::s_colon: return parser::make_COLON(token, loc);
         case tokenizer::etoken::s_coloncolon: return parser::make_COLONCOLON(token, loc);
         case tokenizer::etoken::s_comma: return parser::make_COMMA(token, loc);
         case tokenizer::etoken::s_curlyc: return parser::make_CURLYC(token, loc);
         case tokenizer::etoken::s_curlyo: return parser::make_CURLYO(token, loc);
         case tokenizer::etoken::s_dot: return parser::make_DOT(token, loc);
         case tokenizer::etoken::s_equal: return parser::make_EQUAL(token, loc);
         case tokenizer::etoken::s_equalequal: return parser::make_EQUALEQUAL(token, loc);
         case tokenizer::etoken::s_exclamationmarkequal: return parser::make_EXCLAMATIONMARKEQUAL(token, loc);
         case tokenizer::etoken::s_exclamationmark: return parser::make_EXCLAMATIONMARK(token, loc);
         case tokenizer::etoken::s_gt: return parser::make_GT(token, loc);
         case tokenizer::etoken::s_gtequal: return parser::make_GTEQUAL(token, loc);
         case tokenizer::etoken::s_gtgt: return parser::make_GTGT(token, loc);
         case tokenizer::etoken::s_gtgtgt: return parser::make_GTGTGT(token, loc);
         case tokenizer::etoken::s_lt: return parser::make_LT(token, loc);
         case tokenizer::etoken::s_ltequal: return parser::make_LTEQUAL(token, loc);
         case tokenizer::etoken::s_ltlt: return parser::make_LTLT(token, loc);
         case tokenizer::etoken::s_ltltlt: return parser::make_LTLTLT(token, loc);
         case tokenizer::etoken::s_minus: return parser::make_MINUS(token, loc);
         case tokenizer::etoken::s_minusminus: return parser::make_MINUSMINUS(token, loc);
         case tokenizer::etoken::s_percent: return parser::make_PERCENT(token, loc);
         case tokenizer::etoken::s_plus: return parser::make_PLUS(token, loc);
         case tokenizer::etoken::s_plusplus: return parser::make_PLUSPLUS(token, loc);
         case tokenizer::etoken::s_questionmark: return parser::make_QUESTIONMARK(token, loc);
         case tokenizer::etoken::s_roundc: return parser::make_ROUNDC(token, loc);
         case tokenizer::etoken::s_roundo: return parser::make_ROUNDO(token, loc);
         case tokenizer::etoken::s_semicolon: return parser::make_SEMICOLON(token, loc);
         case tokenizer::etoken::s_slash: return parser::make_SLASH(token, loc);
         case tokenizer::etoken::s_squarec: return parser::make_SQUAREC(token, loc);
         case tokenizer::etoken::s_squareo: return parser::make_SQUAREO(token, loc);
         case tokenizer::etoken::s_star: return parser::make_STAR(token, loc);
         case tokenizer::etoken::s_tilde: return parser::make_TILDE(token, loc);
         case tokenizer::etoken::s_tildeequal: return parser::make_TILDEEQUAL(token, loc);
         case tokenizer::etoken::s_vline: return parser::make_VLINE(token, loc);
         case tokenizer::etoken::s_vlinevline: return parser::make_VLINEVLINE(token, loc);
         }
     }
}
