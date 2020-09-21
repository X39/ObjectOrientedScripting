#pragma once

#include "tokenizer.h"

#include <vector>
#include <variant>
#include <optional>
#include <string>
#include <string_view>


namespace sqf::sqo::data
{
    template<class T>
    class autoptr
    {
    private:
        T* data;
    public:
        autoptr() : data(nullptr) {}
        autoptr(const T& t) : data(new T(t)) {}
        autoptr(T&& t) : data(new T(t)) {}

        bool has_value() const { return data; }
        T* value() const { return data; }
        ~autoptr()
        {
            if (data)
            {
                delete data;
            }
        }
    }
    enum class encapsulation
    {
        PRIVATE,
        DERIVED,
        LOCAL,
        PUBLIC
    };
    struct type;
    struct value;
    struct type_ident 
    {
        std::vector<std::string_view> idents;
    };
    struct type
    {
        type_ident ident;
        std::optional<std::vector<type>> template;
    };
    struct declaration
    {
        std::type type;
        std::string variable;
    };
    struct assignment
    {
        std::optional<type> type;
        std::string variable;
        value value;
    };
    struct exp_primary;
    struct exp
    {
        enum eoperator
        {
            NA,
            OP_PLUS,
            OP_MINUS,
            OP_MULTIPLY,
            OP_DIVIDE,
            OP_REMAINDER,
            OP_BINARY_INVERT,
            OP_BINARY_XOR,
            OP_BINARY_OR,
            OP_BINARY_AND,
            OP_LOGICAL_INVERT,
            OP_LOGICAL_OR,
            OP_LOGICAL_AND,
            OP_SHIFT_LEFT2,
            OP_SHIFT_LEFT3,
            OP_SHIFT_RIGHT2,
            OP_SHIFT_RIGHT3,
            OP_LESS_THEN,
            OP_LESS_THEN_EQUAL,
            OP_GREATER_THEN,
            OP_GREATER_THEN_EQUAL,
            OP_EQUAL,
            OP_EQUAL_INVARIANT,
            OP_NOT_EQUAL
            OP_TERNARY
        };
        autoptr<exp> left;
        std::optional<eoperator> op;
        autoptr<exp> right;
        autoptr<exp> third;
        std::vector<exp_primary> actual;

        exp(std::vector<exp_primary> actual) : op(), right(), third(), actual(actual) {}
        exp(eoperator op, exp argr) : op(op), right(argr), third(), actual() {}
        exp(eoperator op, exp argl, exp argr) : left(argl), op(op), right(argr), third(), actual() {}
        exp(eoperator op, exp argl, exp argr) : left(argl), op(op), right(argr), third(), actual() {}
        exp(exp condition, exp on_true, exp on_false) : left(condition), op(OP_TERNARY), right(on_true), third(on_false), actual() {}
    };
    struct exp_primary
    {
        enum etype
        {
            CVAL,
            THIS,
            NEW,
            DECLARATION,
            ASSIGNMENT,
            CALL,
            NAVIGATE,
            ARRAY_ACCESS
        };
        std::optional<cval> constant;
        std::optional<declaration> decl;
        std::vector<value> values;
        std::string target;
    };
    struct cval 
    {
        enum etype
        {
            NUMBER,
            STRING,
            CHAR,
            TRUE,
            FALSE,
            TYPE
        };
        std::string value;
        etype type;
        type ident;
    };
    struct template_def
    {
        type t;
        std::string ident;
        std::optional<cval> value;
    };
}