#pragma once

#include "tokenizer.hpp"

#include <vector>


namespace yaoosl::compiler
{
    struct cstnode
    {
        enum class kind
        {
            empty,
            token,

            s_code_statements,
            s_file_statements,
            s_class,
            s_class_head,
            s_class_member_head,
            s_property,
            s_property_body,
            s_property_get,
            s_property_set,
            s_namespace,
            s_using,
            s_encapsulation,
            s_enum,
            s_enum_head,
            s_enum_body,
            s_enum_value,
            s_enum_statements,
            s_method,
            s_method_arg_list,
            s_statement_return,
            s_statement_throw,
            s_statement_delete,
            s_statement_break,
            s_statement_continue,
            s_label,
            
            ARRGET,
            ASSIGNMENT,
            BINARY_OPERATOR,
            CALL,
            CASE,
            CATCH,
            CATCHLIST,
            CLASS,
            CLASSBODY,
            CLASSHEAD,
            CLASSSTMNT,
            CLASSSTMNTS,
            CODESTMNT,
            CODESTMNTS,
            CONVERSION,
            CVAL,
            DECLARATION,
            DOTNAV,
            DO_WHILE,
            ENCPSLTN,
            ENCPSLTN_N_CLS,
            ENUM,
            ENUM_BODY,
            ENUM_VALUE,
            ENUM_VALUES,
            EXP12,
            EXPLIST,
            FILESTMNT,
            FILESTMNTS,
            FINALLY,
            FOR,
            FOR_EACH,
            FOR_STEP,
            FOR_STEP_ARG,
            IFELSE,
            MTHD,
            MTHDCNST,
            MTHDCNST_HEAD,
            MTHDDST_HEAD,
            MTHDOP,
            MTHDOP_ARGS,
            MTHDOP_HEAD,
            MTHDOP_OPS1P,
            MTHDOP_OPS1S,
            MTHDOP_OPS2,
            MTHD_ARG,
            MTHD_ARGLIST,
            MTHD_ARGS,
            MTHD_BODY,
            MTHD_HEAD,
            MTHD_HEAD_UNBOUND,
            NAMESPACE,
            NEW,
            PROP,
            PROP_BODY,
            PROP_GET,
            PROP_HEAD,
            PROP_HEAD_UNBOUND,
            PROP_SET,
            SCOPE,
            START,
            STATEMENT,
            SWITCH,
            SWITCH_CASE,
            SWITCH_CASE_CONTINUE,
            SWITCH_CASE_BREAK,
            SWITCH_CASES,
            TEMPLATE,
            TEMPLATE_DEF,
            TEMPLATE_DEFS,
            TEMPLATE_USE,
            TERNARY_OPERATOR,
            THIS,
            TRY,
            TRYCATCH,
            TYPE,
            ARRTYPE,
            TYPELIST,
            TYPE_IDENT,
            UNARY_OPERATOR,
            USING,
            USING_LOW,
            VAL,
            WHILE
        };
        kind type;
        yaoosl::compiler::tokenizer::token token;
        std::vector<cstnode> nodes;

        cstnode() : type(kind::empty), token(), nodes() {}
        cstnode(kind type) : type(type), token(), nodes() {}
        cstnode(kind type, yaoosl::compiler::tokenizer::token token) : type(type), token(token), nodes() {}
        cstnode(yaoosl::compiler::tokenizer::token token) : type(kind::token), token(token), nodes() {}
    };
}