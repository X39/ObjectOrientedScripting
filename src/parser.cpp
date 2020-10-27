#include "parser.h"
#include "logging.h"

namespace msgs
{
    class syntax_error_generic : yaoosl::logging::message
    {
    public:
        syntax_error_generic(yaoosl::logging::position position) :
            yaoosl::logging::message(yaoosl::logging::severity::error, position) {}
        virtual std::string get_message() const
        {
            using namespace std::string_literals;
            return "Syntax Error"s;
        }
    };
}
yaoosl::logging::position to_position(yaoosl::compiler::tokenizer::token t)
{
    return { t.line, t.column, t.offset, t.contents.length(), t.path };
}

std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_start(bool require)
{
    return p_file_statements(require);
}

// p_file_statements = { p_namespace | p_method | p_conversion | p_enum | p_using ";" }
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_file_statements(bool require)
{
    bool flag = false;
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_file_statements;
    // Parse nodes until we can no longer parse nodes
    do
    {
        auto __mark = mark();
        std::optional<cstnode> tmp;
        if ((tmp = p_class(false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_namespace(false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_method(false, false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_conversion(false, false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_enum(false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_using(false)).has_value())
        {
            /* s_semicolon is required after using.                       *
             * However, this is a weak error and we can continue parsing. */
            if (next_token().type != tokenizer::etoken::s_semicolon)
            {
                log(msgs::syntax_error_generic(to_position(current_token())));
            }
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if (!flag)
        {
            if (require)
            {
                log(msgs::syntax_error_generic(to_position(current_token())));
            }
            __mark.rollback();
            break;
        }
    } while (true);
    return flag ? self_node : std::optional<cstnode>();
}

// p_class = p_class_head [ p_template_definition ] [ ":" p_type_list ] p_class_body
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_class(bool require)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_class;
    tokenizer::token class_name_literal;
    
    /* If this is a class can be determined in p_class_head so pass require down */
    // p_class_head ...
    auto node_class_head = p_class_head(require, &class_name_literal);
    if (!node_class_head.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_class_head.value()); }

    /* Optional feature does not need require */
    // ... [ p_template_definition ] ...
    auto node_template_definition = p_template_definition(false);
    if (node_template_definition.has_value()) { self_node.nodes.push_back(node_template_definition.value()); }

    // ... [ ":" p_type_list ] ...
    if (look_ahead_token().type == tokenizer::etoken::s_colon)
    {
        // ... ":" ...
        next_token();

        /* next is required after colon */
        // ... p_type_list ...
        auto node_type_list = p_type_list(true);
        if (!node_type_list.has_value()) { __mark.rollback(); return {}; }
        else { self_node.nodes.push_back(node_type_list.value()); }
    }

    /* next is required after p_class_head was successfull */
    // ... p_class_body
    auto node_class_body = p_class_body(true, class_name_literal);
    if (!node_class_body.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_class_body.value()); }

    return self_node;
}

// p_class_head = p_encapsulation(false) "class" L_IDENT
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_class_head(bool require, tokenizer::token* OUT_class_name_literal)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_class_head;

    /* it is possible that we are not a p_class_head. Pass require down. */
    // p_encapsulation(false) ...
    auto node_encapsulation = p_encapsulation(require, false);
    if (!node_encapsulation.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_encapsulation.value()); }

    /* if require, fail here if not "class" token. */
    // ... "class" ...
    auto token_class = next_token();
    if (token_class.type != tokenizer::etoken::t_class) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }
    else { self_node.token = token_class; }

    /* we are sure now that we are a p_class_head. Next has to be a L_IDENT token. */
    // ... L_IDENT
    auto literal_ident = next_token();
    if (literal_ident.type != tokenizer::etoken::l_ident) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(literal_ident); *OUT_class_name_literal = literal_ident; }

    return self_node;
}

// p_class_body = "{" p_class_statements "}"
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_class_body(bool require, tokenizer::token class_name_literal)
{
    auto __mark = mark();

    /* unless we are required, this determines a possible body-start. */
    // "{" ...
    auto token_curlyo = next_token();
    if (token_curlyo.type != tokenizer::etoken::s_curlyo) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }

    /* unless we are required, the following is just a possible set. Pass down require as that only can tell wether we are or not. */
    // ... p_class_statements ...
    auto node_class_statements = p_class_statements(require, class_name_literal);
    if (!node_class_statements.has_value()) { __mark.rollback(); return {}; }

    /* as we got to here, next token has to be s_curlyc. */
    // ... "}"
    auto token_curlyc = next_token();
    if (token_curlyc.type != tokenizer::etoken::s_curlyc) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }

    return node_class_statements.value();
}

// p_class_statements { p_constructor | p_destructor | p_method | p_operator | p_conversion | p_property | p_class | p_using | p_enum }
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_class_statements(bool require, tokenizer::token class_name_literal)
{
    bool flag = false;
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_file_statements;
    // Parse nodes until we can no longer parse nodes
    bool has_destructor = false;
    do
    {
        auto __mark = mark();
        std::optional<cstnode> tmp;
        if ((tmp = p_constructor(false, class_name_literal)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if (!has_destructor && (tmp = p_destructor(false, class_name_literal)).has_value())
        {
            has_destructor = true;
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_method(false, true)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_operator(false, true)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_conversion(false, true)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_property(false, true)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_class(false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_using(false)).has_value())
        {
            /* s_semicolon is required after using.                       *
             * However, this is a weak error and we can continue parsing. */
            if (next_token().type != tokenizer::etoken::s_semicolon)
            {
                log(msgs::syntax_error_generic(to_position(current_token())));
            }
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if ((tmp = p_enum(false)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if (!flag)
        {
            if (require)
            {
                log(msgs::syntax_error_generic(to_position(current_token())));
            }
            __mark.rollback();
            break;
        }
    } while (true);
    return flag ? self_node : std::optional<cstnode>();
}

// p_class_member_head = p_encapsulation p_type L_IDENT
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_class_member_head(bool require, bool allow_instance)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_class_member_head;

    /* it is possible that we are not a p_class_member_head. Pass require down. */
    // p_encapsulation(false) ...
    auto node_encapsulation = p_encapsulation(require, false);
    if (!node_encapsulation.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_encapsulation.value()); }

    /* we still cannot know duh... so rense and repeat. */
    // ... p_type ...
    auto node_type = p_type(require);
    if (!node_type.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_type.value()); }

    /* still unknown. This is highly ambigous. "Pass require down" a last time. */
    // ... L_IDENT
    auto literal_ident = next_token();
    if (literal_ident.type != tokenizer::etoken::l_ident) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(literal_ident); }

    return self_node;
}

// p_namespace = "namespace" p_type_ident "{" p_file_statements "}"
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_namespace(bool require)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_namespace;

    /* if require, fail here if next token is not t_namespace. */
    // "namespace" ...
    auto token_namespace = next_token();
    if (token_namespace.type != tokenizer::etoken::t_namespace) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }
    else { self_node.token = token_namespace; }

    /* we are sure now that we are a p_namespace. Next has to be p_type_ident. */
    // ... p_type_ident ...
    auto node_type_ident = p_type_ident(true);
    if (!node_type_ident.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_type_ident.value()); }

    /* we are sure now that we are a p_namespace. Next has to be s_curlyo. */
    // ... "{" ...
    auto token_curlyo = next_token();
    if (token_curlyo.type != tokenizer::etoken::s_curlyo) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }

    /* we are sure now that we are a p_namespace. Next has to be p_file_statements. */
    // ... p_file_statements ...
    auto node_file_statements = p_file_statements(true);
    if (!node_file_statements.has_value()) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_file_statements.value()); }

    /* we are sure now that we are a p_namespace. Next has to be s_curlyc. */
    // ... "}"
    auto token_curlyc = next_token();
    if (token_curlyc.type != tokenizer::etoken::s_curlyc) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }

    return self_node;
}

// p_property = p_class_member_head p_property_body
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_property(bool require, bool allow_instance)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_property;

    /* pass down require to p_class_member_head. */
    // p_class_member_head ...
    auto node_class_member_head = p_class_member_head(require, allow_instance);
    if (!node_class_member_head.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_class_member_head.value()); }

    /* p_property_head is ambigous. So pass down require to p_property_body. */
    // ... p_property_body
    auto node_property_body = p_property_body(require, allow_instance);
    if (!node_property_body.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_property_body.value()); }

    return self_node;
}

// p_property_body = ( ";" | "{" ( ( p_property_get p_property_set ) | ( p_property_get ) | ( p_property_set ) | ( p_property_set p_property_get ) ) "}" )
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_property_body(bool require, bool allow_instance)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_property_body;

    { // ";"

        /* if next is semicolon, we are done. */
        // ";"
        auto token_namespace = next_token();
        if (token_namespace.type != tokenizer::etoken::t_namespace) {}
        else { self_node.token = token_namespace; return self_node; }
    }
    { // "{" ( ( p_property_get p_property_set ) | ( p_property_get ) | ( p_property_set ) | ( p_property_set p_property_get ) ) "}"

        /* following approach works by:               *
         * 1. test p_property_get                     *
         * 2. test p_property_set                     *
         * 3. if 1) failed, test p_property_get again */

        /* attempt p_property_get. */
        // 'p_property_get'
        auto node_property_get = p_property_get(false, allow_instance);
        if (!node_property_get.has_value()) {}
        else { self_node.nodes.push_back(node_property_get.value()); }

        /* if require and p_property_get failed, require p_property_set. */
        // 'p_property_get'
        auto node_property_set = p_property_set(require && !node_property_get.has_value(), allow_instance);
        if (!node_property_set.has_value()) { if (require && !node_property_get.has_value()) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; } }
        else { self_node.nodes.push_back(node_property_set.value()); }

        /* repeat p_property_get if prior p_property_get has failed. */
        if (!node_property_get.has_value())
        {
            // 'p_property_get'
            node_property_get = p_property_get(false, allow_instance);
            if (!node_property_get.has_value()) {}
            else { self_node.nodes.push_back(node_property_get.value()); }
        }
    }

    return self_node;
}

// p_property_get = [ p_encapsulation ] "get" p_method_body
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_property_get(bool require, bool allow_instance)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_property_get;

    /* optional p_encapsulation. */
    // [ p_encapsulation ] ...
    auto node_encapsulation = p_encapsulation(false, allow_instance);
    if (!node_encapsulation.has_value()) { }
    else { self_node.nodes.push_back(node_encapsulation.value()); }

    /* following token denotes if we are indeed a p_property_get. */
    // ... "get" ...
    auto token_using = next_token();
    if (token_using.type != tokenizer::etoken::t_get) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }
    else { self_node.token = token_using; }

    /* we know we are a p_property_get here. Require p_method_body */
    // ... p_method_body
    auto node_method_body = p_method_body(true, allow_instance);
    if (!node_method_body.has_value()) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_method_body.value()); }

    return self_node;
}

// p_property_set = [ p_encapsulation ] "set" [ "(" L_IDENT ")" ] p_method_body
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_property_set(bool require, bool allow_instance)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_property_set;

    /* optional p_encapsulation. */
    // [ p_encapsulation ] ...
    auto node_encapsulation = p_encapsulation(false, allow_instance);
    if (!node_encapsulation.has_value()) {}
    else { self_node.nodes.push_back(node_encapsulation.value()); }

    /* following token denotes if we are indeed a p_property_set. */
    // ... "set" ...
    auto token_using = next_token();
    if (token_using.type != tokenizer::etoken::t_get) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }
    else { self_node.token = token_using; }

    // [ "(" L_IDENT ")" ]
    if (look_ahead_token().type == tokenizer::etoken::s_roundo)
    {
        // "(" ...
        next_token();

        /* next is required after s_roundo */
        // ... L_IDENT ...
        auto token_using = next_token();
        if (token_using.type != tokenizer::etoken::l_ident) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } else { __mark.rollback(); return {}; } }
        else { self_node.token = token_using; }

        /* following has to be s_roundc or we are no valid p_property_set. * 
         * If in require, emit syntax error and continue parsing.          *
         * If not, exit parsing.                                           */
        // ")"
        auto token_using = next_token();
        if (token_using.type != tokenizer::etoken::s_roundc) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } else { __mark.rollback(); return {}; } }
    }

    /* we know we are a p_property_set here. Require p_method_body */
    // ... p_method_body
    auto node_method_body = p_method_body(true, allow_instance);
    if (!node_method_body.has_value()) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_method_body.value()); }

    return self_node;
}

// p_using = "using" type [ "=" L_IDENT ]
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_using(bool require)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_using;

    /* following token denotes if we are indeed a p_using. */
    // "using" ...
    auto token_using = next_token();
    if (token_using.type != tokenizer::etoken::t_using) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }
    else { self_node.token = token_using; }
    
    /* Required as we already determined that we are a p_using. */
    // ... p_type ...
    auto node_type = p_type(true);
    if (!node_type.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_type.value()); }

    // ... [ "=" L_IDENT ]
    if (look_ahead_token().type == tokenizer::etoken::s_equal)
    {
        // ... "=" ...
        next_token();

        /* next is required after s_equal */
        // ... L_IDENT
        auto literal_ident = next_token();
        if (literal_ident.type != tokenizer::etoken::l_ident) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }
        else { self_node.nodes.push_back(literal_ident); }
    }

    return self_node;
}

// p_enum = p_enum_head p_enum_body
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum(bool require)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_enum;

    // p_enum_head ...
    auto node_enum_head = p_enum_head(require);
    if (!node_enum_head.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_enum_head.value()); }

    // ... p_enum_body
    auto node_enum_body = p_enum_body(true);
    if (!node_enum_body.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_enum_body.value()); }

    return self_node;
}

// p_enum_head = p_encapsulation(false) "enum" L_IDENT
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum_head(bool require)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_enum_head;

    /* it is possible that we are not a p_enum_head. Pass require down. */
    // p_encapsulation(false) ...
    auto node_encapsulation = p_encapsulation(require, false);
    if (!node_encapsulation.has_value()) { __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(node_encapsulation.value()); }

    /* if require, fail here if next token is not t_enum. */
    // ... "enum" ...
    auto token_class = next_token();
    if (token_class.type != tokenizer::etoken::t_enum) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }
    else { self_node.token = token_class; }

    /* we know for sure we are an enum here. Next is required to be L_IDENT. */
    // ... L_IDENT
    auto literal_ident = next_token();
    if (literal_ident.type != tokenizer::etoken::l_ident) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }
    else { self_node.nodes.push_back(literal_ident); }

    return self_node;
}

// p_enum_body = "{" p_enum_statements "}"
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum_body(bool require)
{
    auto __mark = mark();

    /* unless we are required, this determines a possible body-start. */
    // "{" ...
    auto token_curlyo = next_token();
    if (token_curlyo.type != tokenizer::etoken::s_curlyo) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }

    /* unless we are required, the following is just a possible set. Pass down require as that only can tell wether we are or not. */
    // ... p_enum_statements ...
    auto node_enum_statements = p_enum_statements(require);
    if (!node_enum_statements.has_value()) { __mark.rollback(); return {}; }

    /* as we got to here, next token has to be s_curlyc. */
    // ... "}"
    auto token_curlyc = next_token();
    if (token_curlyc.type != tokenizer::etoken::s_curlyc) { log(msgs::syntax_error_generic(to_position(current_token()))); __mark.rollback(); return {}; }

    return node_enum_statements.value();
}

// p_enum_statements = [ p_enum_value { "," p_enum_value } [ "," ] ]
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum_statements(bool require)
{
    bool flag = false;
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_enum_statements;
    // Parse nodes until we can no longer parse nodes
    bool had_comma = true; // set to true to prevent initial comma-check
    do
    {
        if (!had_comma)
        {
            log(msgs::syntax_error_generic(to_position(current_token())));
        }
        std::optional<cstnode> tmp;
        if ((tmp = p_enum_value(require)).has_value())
        {
            self_node.nodes.push_back(tmp.value());
            flag = true;
        }
        else if (!flag)
        {
            if (require)
            {
                log(msgs::syntax_error_generic(to_position(current_token())));
            }
            break;
        }

        // Check if next token is comma.
        if (!(had_comma = next_token().type == tokenizer::etoken::s_comma))
        {
            undo_token();
        }
    } while (true);
    return flag ? self_node : std::optional<cstnode>();
}

// p_enum_value = L_IDENT [ "=" p_value_constant ]
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_enum_value(bool require)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_enum_value;

    /* we cannot know for sure ever if we are indeed a p_enum_value. Use require to determine if mandatory. */
    // L_IDENT ...
    auto literal_ident = next_token();
    if (literal_ident.type != tokenizer::etoken::l_ident) { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }
    else { self_node.token = literal_ident; }

    // ... [ "=" p_value_constant ]
    if (look_ahead_token().type == tokenizer::etoken::s_equal)
    {
        // ... "=" ...
        next_token();

        /* pass down require to p_value_constant as this is ambigous with assignment */
        // ... p_value_constant
        auto node_value_constant = p_value_constant(require);
        if (!node_value_constant.has_value()) { __mark.rollback(); return {}; }
        else { self_node.nodes.push_back(node_value_constant.value()); }
    }

    return self_node;
}

// @allow_instance = true:  p_encapsulation = "derived" | "private" | "public" | "local"
// @allow_instance = false: p_encapsulation = "public" | "local"
std::optional<yaoosl::compiler::cstnode> yaoosl::compiler::parser::p_encapsulation(bool require, bool allow_instance)
{
    auto __mark = mark();
    cstnode self_node = {};
    self_node.type = cstnode::kind::s_encapsulation;

    auto token_mid = next_token();
    if (token_mid.type == tokenizer::etoken::t_public) { self_node.token = token_mid; }
    else if (token_mid.type == tokenizer::etoken::t_local) { self_node.token = token_mid; }
    else if (allow_instance && token_mid.type == tokenizer::etoken::t_derived) { self_node.token = token_mid; }
    else if (allow_instance && token_mid.type == tokenizer::etoken::t_private) { self_node.token = token_mid; }
    else { if (require) { log(msgs::syntax_error_generic(to_position(current_token()))); } __mark.rollback(); return {}; }

    return self_node;
}
