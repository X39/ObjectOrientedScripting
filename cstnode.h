#pragma once

#include "tokenizer.h"

#include <vector>


namespace sqf::sqo
{
    struct cstnode
    {
        enum kind
        {
            empty,
            TYPE_IDENT,
            TYPE,
            TYPELIST,
            TEMPLATE_USE,
            ENCPSLTN,
            ENCPSLTN_N_CLS,
            TEMPLATE_DEF,
            TEMPLATE_DEFS,
            TEMPLATE,
            CVAL,
            THIS,
            NEW,
            DECLARATION,
            EXP12,
            CALL,
            ASSIGNMENT,
            DOTNAV,
            ARRGET,
            TERNARY_OPERATOR,
            BINARY_OPERATOR,
            UNARY_OPERATOR,
            VAL,
            EXPLIST,
            START,
            USING,
            USING_LOW,
            FILESTMNT,
            FILESTMNTS,
            CLASSSTMNT,
            CLASSSTMNTS,
            CODESTMNTS,
            NAMESPACE,
            ENUM,
            ENUM_BODY,
            ENUM_VALUES,
            ENUM_VALUE,
            CLASS,
            CLASSHEAD,
            CLASSBODY,
            MTHD,
            MTHD_HEAD,
            MTHD_HEAD_UNBOUND,
            MTHD_ARGS,
            MTHD_BODY,
            MTHD_ARGLIST,
            MTHD_ARG,
            MTHDOP,
            MTHDOP_HEAD,
            MTHDOP_ARGS,
            MTHDOP_OPS1P,
            MTHDOP_OPS1S,
            MTHDOP_OPS2,
            MTHDCNST,
            MTHDCNST_HEAD,
            MTHDDST_HEAD,
            PROP,
            PROP_HEAD,
            PROP_HEAD_UNBOUND,
            PROP_BODY,
            PROP_GET,
            PROP_SET,
            IFELSE,
            FOR,
            FOR_STEP,
            FOR_STEP_ARG,
            FOR_EACH,
            WHILE,
            DO_WHILE,
            SWITCH,
            SWITCH_CASES,
            SWITCH_CASE,
            CASE,
            TRY,
            CATCH,
            CATCHLIST,
            FINALLY,
            TRYCATCH,
            STATEMENT,
            SCOPE
        };
        kind type;
        ::sqf::sqo::tokenizer::token token;
        std::vector<cstnode> nodes;
    };
}