#include "parser.h"

std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_start()
{
    return p_file_statements();
}


// p_file_statements = { p_namespace | p_method | p_conversion | p_enum | p_using ";" }
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_file_statements()
{
    bool flag = false;
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_file_statements;
    // Parse nodes until we can no longer parse nodes
    do
    {
        auto __mark = mark();
        std::optional<cstnode> tmp;
        if ((tmp = p_class()).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_namespace()).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_method(false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_conversion(false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_enum()).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_using()).has_value() &&
            next_token().type == tokenizer::etoken::s_semicolon)
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else
        {
            __mark.rollback();
            break;
        }
    } while (true);
    return flag ? self_node : std::optional<cstnode>();
}

// p_class = p_class_head [ p_template_definition ] [ ":" p_type_list ] p_class_body
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_class()
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_class;
    
    // p_class_head ...
    auto node_class_head = p_class_head();
    if (!node_class_head.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_class_head.value()); }

    // ... [ p_template_definition ] ...
    auto node_template_definition = p_template_definition();
    if (node_template_definition.has_value()) { self_node.nodes.push_back(node_template_definition.value()); }

    // ... [ ":" p_type_list ] ...
    if (look_ahead_token().type == tokenizer::etoken::s_colon)
    {
        // ... ":" ...
        next_token();

        // ... p_type_list ...
        auto node_type_list = p_type_list();
        if (!node_type_list.has_value()) { __mark.rollback(); return {}; }
        else { self_node.nodes.push_back(node_type_list.value()); }
    }

    // ... p_class_body
    auto node_class_body = p_class_body();
    if (!node_class_body.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_class_body.value()); }

    return self_node;
}

// p_class_head = p_encapsulation(false) "class" L_IDENT
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_class_head()
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_class_head;

    // p_encapsulation(false) ...
    auto node_encapsulation = p_encapsulation(false);
    if (!node_encapsulation.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_encapsulation.value()); }

    // ... "class" ...
    auto token_class = next_token();
    if (token_class.type != tokenizer::etoken::t_class) { __mark.rollback(); return {}; }
    else { self_node.token = token_class; }

    // ... L_IDENT
    auto literal_ident = next_token();
    if (literal_ident.type != tokenizer::etoken::l_ident) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(literal_ident); }

    return self_node;
}

// p_class_body = "{" p_class_statements "}"
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_class_body()
{
    auto __mark = mark();

    // "{" ...
    auto token_curlyo = next_token();
    if (token_curlyo.type != tokenizer::etoken::s_curlyo) { __mark.rollback(); return {}; }

    // ... p_class_statements ...
    auto node_class_statements = p_class_statements();
    if (!node_class_statements.has_value()) { __mark.rollback(); return {}; }

    // ... "}"
    auto token_curlyc = next_token();
    if (token_curlyc.type != tokenizer::etoken::s_curlyc) { __mark.rollback(); return {}; }

    return node_class_statements.value();
}

// p_namespace = "namespace" p_type_ident "{" p_file_statements "}"
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_namespace()
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_namespace;

    // "namespace" ...
    auto token_namespace = next_token();
    if (token_namespace.type != tokenizer::etoken::t_namespace) { __mark.rollback(); return {}; }
    else { self_node.token = token_namespace; }

    // ... p_type_ident ...
    auto node_type_ident = p_type_ident();
    if (!node_type_ident.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_type_ident.value()); }

    // ... "{" ...
    auto token_curlyo = next_token();
    if (token_curlyo.type != tokenizer::etoken::s_curlyo) { __mark.rollback(); return {}; }

    // ... p_file_statements ...
    auto node_file_statements = p_file_statements();
    if (!node_file_statements.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_file_statements.value()); }

    // ... "}"
    auto token_curlyc = next_token();
    if (token_curlyc.type != tokenizer::etoken::s_curlyc) { __mark.rollback(); return {}; }

    return self_node;
}

// p_using = "using" type [ "=" L_IDENT ]
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_using()
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_using;

    // "using" ...
    auto token_using = next_token();
    if (token_using.type != tokenizer::etoken::t_using) { __mark.rollback(); return {}; }
    else { self_node.token = token_using; }
    
    // ... p_type ...
    auto node_type = p_type();
    if (!node_type.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_type.value()); }

    // ... [ "=" L_IDENT ]
    if (look_ahead_token().type == tokenizer::etoken::s_equal)
    {
        // ... "=" ...
        next_token();

        // ... L_IDENT
        auto literal_ident = next_token();
        if (literal_ident.type != tokenizer::etoken::l_ident) { __mark.rollback(); return {}; }
        else { self_node.nodes.push_back(literal_ident); }
    }

    return self_node;
}

// p_enum = p_enum_head p_enum_body
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum()
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_enum;

    // p_enum_head ...
    auto node_enum_head = p_enum_head();
    if (!node_enum_head.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_enum_head.value()); }

    // ... p_enum_body
    auto node_enum_body = p_enum_body();
    if (!node_enum_body.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_enum_body.value()); }

    return self_node;
}

// p_enum_head = p_encapsulation(false) "enum" L_IDENT
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum_head()
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_enum_head;

    // p_encapsulation(false) ...
    auto node_encapsulation = p_encapsulation(false);
    if (!node_encapsulation.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_encapsulation.value()); }

    // ... "enum" ...
    auto token_class = next_token();
    if (token_class.type != tokenizer::etoken::t_enum) { __mark.rollback(); return {}; }
    else { self_node.token = token_class; }

    // ... L_IDENT
    auto literal_ident = next_token();
    if (literal_ident.type != tokenizer::etoken::l_ident) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(literal_ident); }

    return self_node;
}

// p_enum_body = "{" p_enum_statements "}"
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum_body()
{
    auto __mark = mark();

    // "{" ...
    auto token_curlyo = next_token();
    if (token_curlyo.type != tokenizer::etoken::s_curlyo) { __mark.rollback(); return {}; }

    // ... p_enum_statements ...
    auto node_enum_statements = p_enum_statements();
    if (!node_enum_statements.has_value()) { __mark.rollback(); return {}; }

    // ... "}"
    auto token_curlyc = next_token();
    if (token_curlyc.type != tokenizer::etoken::s_curlyc) { __mark.rollback(); return {}; }

    return node_enum_statements.value();
}

// p_enum_value = L_IDENT [ "=" p_value_constant ]
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum_value()
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_enum_value;

    // L_IDENT ...
    auto literal_ident = next_token();
    if (literal_ident.type != tokenizer::etoken::l_ident) { __mark.rollback(); return {}; }
    else { self_node.token = literal_ident; }

    // ... [ "=" p_value_constant ]
    if (look_ahead_token().type == tokenizer::etoken::s_equal)
    {
        // ... "=" ...
        next_token();

        // ... p_value_constant
        auto node_value_constant = p_value_constant();
        if (!node_value_constant.has_value()) { __mark.rollback(); return {}; }
        else { self_node.nodes.push_back(node_value_constant.value()); }
    }

    return self_node;
}

// @allow_instance = true:  p_encapsulation = "derived" | "private" | "public" | "local"
// @allow_instance = false: p_encapsulation = "public" | "local"
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_encapsulation(bool allow_instance)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_encapsulation;

    auto token_mid = next_token();
    if (token_mid.type == tokenizer::etoken::t_public) { self_node.token = token_mid; }
    else if (token_mid.type == tokenizer::etoken::t_local) { self_node.token = token_mid; }
    else if (allow_instance && token_mid.type == tokenizer::etoken::t_derived) { self_node.token = token_mid; }
    else if (allow_instance && token_mid.type == tokenizer::etoken::t_private) { self_node.token = token_mid; }
    else { __mark.rollback(); return {}; }

    return self_node;
}
