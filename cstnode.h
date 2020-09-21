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
            EXPP,
            DECLARATION,
            EXP12,
            CALL,
            ASSIGNMENT,
            DOTNAV,
            ARRGET,
            EXP01,
            EXP02,
            EXP03,
            EXP04,
            EXP05,
            EXP06,
            EXP07,
            EXP08,
            EXP09,
            EXP10,
            EXP11,
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
            PROP,
            PROP_HEAD,
            PROP_BODY,
            PROP_GET,
            PROP_SET,
            IFELSE,
            FOR,
            FOR_STEP,
            FOR_STEP_ARG,
            FOR_EACH,
            WHILE,
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
        std::vector<cstnode> node;
    };
}