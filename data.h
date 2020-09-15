#pragma once

#include "tokenizer.h"

#include <vector>
#include <variant>
#include <optional>
#include <string>
#include <string_view>


namespace sqf::sqo::data
{
    enum class encapsulation
    {
        PRIVATE,
        DERIVED,
        LOCAL,
        PUBLIC
    };
    struct type;
    struct constval;
    struct type_ident 
    {
        std::vector<std::string_view> idents;
    };
    struct type 
    {
        type_ident ident;
        std::optional<std::vector<type>> template;
    };
    struct constval 
    {
        enum valtype
        {
            NUMBER,
            STRING,
            CHAR,
            TRUE,
            FALSE,
            TYPE
        };
        std::string value;
        valtype type;
        type ident;
    };
    struct template_def
    {
        type t;
        std::string ident;
        std::optional<constval> value;
    };
}