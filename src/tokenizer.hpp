#pragma once
#include <array>
#include <string>
#include <string_view>
#include <cctype>
#include <optional>


namespace yaoosl::compiler
{
    class tokenizer
    {
    public:
        enum class etoken
        {
            eof,
            invalid,

            m_line,

            i_comment_line,
            i_comment_block,
            i_whitespace,

            l_ident,
            l_string,
            l_number,
            l_char,

            t_break,
            t_case,
            t_catch,
            t_class,
            t_continue,
            t_conversion,
            t_default,
            t_delete,
            t_derived,
            t_do,
            t_else,
            t_enum,
            t_false,
            t_finally,
            t_for,
            t_get,
            t_goto,
            t_if,
            t_local,
            t_namespace,
            t_new,
            t_operator,
            t_private,
            t_public,
            t_return,
            t_set,
            t_switch,
            t_this,
            t_throw,
            t_true,
            t_try,
            t_typename,
            t_unbound,
            t_using,
            t_while,

            s_and,
            s_andand,
            s_arrowhead,
            s_circumflex,
            s_colon,
            s_coloncolon,
            s_comma,
            s_curlyc,
            s_curlyo,
            s_dot,
            s_dotdot,
            s_equal,
            s_equalequal,
            s_exclamationmarkequal,
            s_exclamationmark,
            s_gt,
            s_gtequal,
            s_gtgt,
            s_gtgtgt,
            s_lt,
            s_ltequal,
            s_ltlt,
            s_ltltlt,
            s_minus,
            s_minusminus,
            s_percent,
            s_plus,
            s_plusplus,
            s_questionmark,
            s_roundc,
            s_roundo,
            s_semicolon,
            s_slash,
            s_squarec,
            s_squareo,
            s_star,
            s_tilde,
            s_tildeequal,
            s_vline,
            s_vlinevline
        };
        struct token
        {
            etoken type;
            size_t line;
            size_t column;
            size_t offset;
            std::string_view contents;
            std::string path;

            std::string to_string() const
            {
                std::string s;
                s.append("L");
                s.append(std::to_string(line));
                s.append("; C");
                s.append(std::to_string(line));
                s.append("; O");
                s.append(std::to_string(line));
                s.append("; ");
                s.append(tokenizer::to_string(type));
                s.append("; P'");
                s.append(path);
                s.append("'; '");
                s.append(contents);
                s.append("'");
                return s;
            }
        };
        using iterator = std::string::iterator;
    private:
        iterator m_start;
        iterator m_current;
        iterator m_end;
        std::string m_path;

        size_t m_line;
        size_t m_column;

        template<typename = void>
        inline bool is_match(char value) { return false; }
        template<char TArg, char ... TArgs>
        inline bool is_match(char value)
        {
            switch (value)
            {
            case TArg: return true;
            default: return is_match<TArgs...>(value);
            }
        }
        template<char ... TArgs>
        bool is_match(iterator value) { return value < m_end && is_match<TArgs...>(*value); }
        template<size_t len, char ... TArgs>
        bool is_match_repeated(iterator value)
        {
            size_t i = 0;
            while (value < m_end && is_match<TArgs...>(*value++)) { ++i; }
            return len == i;
        }

        template<typename = void>
        std::optional<size_t> len_match_exact_(iterator value) { return 0; }
        template<char TArg, char ... TArgs>
        std::optional<size_t> len_match_exact_(iterator value)
        {
            auto res = len_match_exact_<TArgs...>(value + 1);
            return ((value < m_end) && (res.has_value()) && (*value == TArg)) ? std::optional<size_t>{ *res + 1 } : std::optional<size_t>{};
        }
        template<char ... TArgs>
        size_t len_match_exact(iterator value)
        {
            auto res = len_match_exact_<TArgs...>(value);
            return res.has_value() ? *res : 0;
        }

        template<char ... TArgs>
        size_t len_match(iterator str)
        {
            iterator it = str;
            while (it < m_end && is_match<TArgs...>(*it++)) {}
            return it - str - 1;
        }
        inline size_t len_match(iterator start, const char* against)
        {
            auto it = start;
            auto len = std::strlen(against);
            for (size_t i = 0; i < len && it < m_end; i++, ++it)
            {
                if ((char)std::tolower(*it) != against[i]) { return 0; }
            }
            return it - start;
        }
        size_t len_ident_match(iterator start, const char* against)
        {
            auto it = start;
            auto len = std::strlen(against);
            for (size_t i = 0; i < len && it < m_end; i++, ++it)
            {
                if ((char)std::tolower(*it) != against[i]) { return 0; }
            }
            if (it < m_end && ((char)std::tolower(*it) >= 'a' && (char)std::tolower(*it) <= 'z'))
            {
                return 0;
            }
            return it - start;
        }

        token try_match(std::initializer_list<etoken> tokens)
        {
            token t = create_token();
            for (auto token_type : tokens)
            {
                auto iter = m_current;
                size_t len = 0;
                switch (token_type)
                {
                default: return { etoken::invalid, m_line, m_column, (size_t)(m_current - m_start), {} };
                case etoken::m_line: /* ToDo: Properly handle #line instruction */ {
                    // Check if line comment start
                    if (len_ident_match(iter, "#line"))
                    {
                        iter += 6;

                        // Read in line num
                        auto start = iter;
                        for (; iter != m_end && *iter != '\n' && *iter != ' '; iter++);
                        std::string str_tmp(start, iter);
                        m_line = static_cast<size_t>(std::stoul(str_tmp));

                        // Try skip to file
                        iter += len_match<' ', '\t'>(iter);
                        start = iter;
                        for (; iter != m_end && *iter != '\n'; iter++);
                        if (iter != m_end && iter - start >= 2)
                        {
                            // Read-in file
                            m_path = { start + 1, iter - 1 };
                        }

                        // update column
                        m_column = null_col;

                        // set length
                        len = iter - m_current;
                    }
                } break;
                case etoken::i_comment_line: {
                    // Check if line comment start
                    if (is_match_repeated<2, '/'>(iter))
                    {
                        // find line comment end
                        while (!is_match<'\n'>(++iter) && iter != m_end);

                        // update position info
                        m_line++;
                        m_column = null_col;

                        // set length
                        len = iter - m_current + (iter == m_end ? 0 : 1);
                    }
                } break;
                case etoken::i_comment_block: {
                    // Check if block comment start
                    if (is_match<'/'>(iter) && is_match<'*'>(iter + 1))
                    {
                        ++iter;
                        ++iter;
                        // find block comment end
                        while (!(is_match<'*'>(iter) && is_match<'/'>(iter + 1)))
                        {
                            // update position info
                            if (!is_match<'\n'>(iter))
                            {
                                m_column++;
                            }
                            else
                            {
                                m_line++;
                                m_column = null_col;
                            }
                            ++iter;
                        }

                        // EOF check
                        if (is_match<'/'>(iter) && is_match<'/'>(iter + 1))
                        {
                            ++iter;
                            ++iter;
                        }
                        // set length
                        len = iter - m_current;
                    }
                } break;
                case etoken::i_whitespace: {
                    while (is_match<' ', '\n', '\r', '\t'>(iter))
                    {
                        // update position info
                        if (!is_match<'\n'>(iter))
                        {
                            m_column++;
                        }
                        else
                        {
                            m_line++;
                            m_column = null_col;
                        }
                        ++iter;
                    }
                    // set length
                    len = iter - m_current;
                } break;
                case etoken::l_string: {
                    ++iter;
                    // find string end
                    while (true)
                    {
                        if (is_match<'"'>(iter) && is_match<'"'>(iter + 1))
                        {
                            ++iter;
                        }
                        else if (is_match<'"'>(iter))
                        {
                            ++iter;
                            break;
                        }
                        // update position info
                        if (!is_match<'\n'>(iter))
                        {
                            m_column++;
                        }
                        else
                        {
                            m_line++;
                            m_column = null_col;
                        }
                        ++iter;
                    }
                    // set length
                    len = iter - m_current;
                } break;
                case etoken::l_char: {
                    ++iter;
                    // find string end
                    while (true)
                    {
                        if (is_match<'\''>(iter) && is_match<'\''>(iter + 1))
                        {
                            ++iter;
                        }
                        else if (is_match<'\''>(iter))
                        {
                            ++iter;
                            break;
                        }
                        // update position info
                        if (!is_match<'\n'>(iter))
                        {
                            m_column++;
                        }
                        else
                        {
                            m_line++;
                            m_column = null_col;
                        }
                        ++iter;
                    }
                    // set length
                    len = iter - m_current;
                } break;
                case etoken::l_ident: {
                    len = len_match<
                        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '_'>(iter);
                } m_column += len; break;
                case etoken::l_number: {
                    size_t res = 0;
                    // match (optional) prefix
                    res = is_match<'-', '+'>(iter);
                    len += res; iter += res;

                    // match first part of number
                    res = len_match<'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.'>(iter);
                    if (res == 0) { len = 0; break; }
                    len += res; iter += res;

                    // match optional dot
                    if (is_match<'.'>(iter))
                    {
                        len++; iter++;

                        // match second part of number
                        res = len_match<'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.'>(iter);
                        if (res == 0) { len--; iter--; }
                        else { len += res; iter += res; }
                    }
                } m_column += len; break;

                case etoken::t_break:                   len = len_ident_match(iter, "break");       m_column += len; break;
                case etoken::t_case:                    len = len_ident_match(iter, "case");        m_column += len; break;
                case etoken::t_catch:                   len = len_ident_match(iter, "catch");       m_column += len; break;
                case etoken::t_class:                   len = len_ident_match(iter, "class");       m_column += len; break;
                case etoken::t_continue:                len = len_ident_match(iter, "continue");    m_column += len; break;
                case etoken::t_conversion:              len = len_ident_match(iter, "conversion");  m_column += len; break;
                case etoken::t_default:                 len = len_ident_match(iter, "default");     m_column += len; break;
                case etoken::t_delete:                  len = len_ident_match(iter, "delete");      m_column += len; break;
                case etoken::t_derived:                 len = len_ident_match(iter, "derived");     m_column += len; break;
                case etoken::t_do:                      len = len_ident_match(iter, "do");          m_column += len; break;
                case etoken::t_else:                    len = len_ident_match(iter, "else");        m_column += len; break;
                case etoken::t_enum:                    len = len_ident_match(iter, "enum");        m_column += len; break;
                case etoken::t_false:                   len = len_ident_match(iter, "false");       m_column += len; break;
                case etoken::t_finally:                 len = len_ident_match(iter, "finally");     m_column += len; break;
                case etoken::t_for:                     len = len_ident_match(iter, "for");         m_column += len; break;
                case etoken::t_get:                     len = len_ident_match(iter, "get");         m_column += len; break;
                case etoken::t_goto:                    len = len_ident_match(iter, "goto");        m_column += len; break;
                case etoken::t_if:                      len = len_ident_match(iter, "if");          m_column += len; break;
                case etoken::t_local:                   len = len_ident_match(iter, "local");       m_column += len; break;
                case etoken::t_namespace:               len = len_ident_match(iter, "namespace");   m_column += len; break;
                case etoken::t_new:                     len = len_ident_match(iter, "new");         m_column += len; break;
                case etoken::t_operator:                len = len_ident_match(iter, "operator");    m_column += len; break;
                case etoken::t_private:                 len = len_ident_match(iter, "private");     m_column += len; break;
                case etoken::t_public:                  len = len_ident_match(iter, "public");      m_column += len; break;
                case etoken::t_return:                  len = len_ident_match(iter, "return");      m_column += len; break;
                case etoken::t_set:                     len = len_ident_match(iter, "set");         m_column += len; break;
                case etoken::t_switch:                  len = len_ident_match(iter, "switch");      m_column += len; break;
                case etoken::t_this:                    len = len_ident_match(iter, "this");        m_column += len; break;
                case etoken::t_throw:                   len = len_ident_match(iter, "throw");       m_column += len; break;
                case etoken::t_true:                    len = len_ident_match(iter, "true");        m_column += len; break;
                case etoken::t_try:                     len = len_ident_match(iter, "try");         m_column += len; break;
                case etoken::t_typename:                len = len_ident_match(iter, "typename");    m_column += len; break;
                case etoken::t_unbound:                 len = len_ident_match(iter, "unbound");     m_column += len; break;
                case etoken::t_using:                   len = len_ident_match(iter, "using");       m_column += len; break;
                case etoken::t_while:                   len = len_ident_match(iter, "while");       m_column += len; break;

                case etoken::s_and:                     len = len_match_exact<'&'>(iter);           m_column += len; break;
                case etoken::s_andand:                  len = len_match_exact<'&', '&'>(iter);      m_column += len; break;
                case etoken::s_arrowhead:               len = len_match_exact<'=', '>'>(iter);      m_column += len; break;
                case etoken::s_circumflex:              len = len_match_exact<'^'>(iter);           m_column += len; break;
                case etoken::s_colon:                   len = len_match_exact<':'>(iter);           m_column += len; break;
                case etoken::s_coloncolon:              len = len_match_exact<':', ':'>(iter);      m_column += len; break;
                case etoken::s_comma:                   len = len_match_exact<','>(iter);           m_column += len; break;
                case etoken::s_curlyc:                  len = len_match_exact<'}'>(iter);           m_column += len; break;
                case etoken::s_curlyo:                  len = len_match_exact<'{'>(iter);           m_column += len; break;
                case etoken::s_dot:                     len = len_match_exact<'.'>(iter);           m_column += len; break;
                case etoken::s_dotdot:                  len = len_match_exact<'.', '.'>(iter);      m_column += len; break;
                case etoken::s_equal:                   len = len_match_exact<'='>(iter);           m_column += len; break;
                case etoken::s_equalequal:              len = len_match_exact<'=', '='>(iter);      m_column += len; break;
                case etoken::s_exclamationmark:         len = len_match_exact<'!'>(iter);           m_column += len; break;
                case etoken::s_exclamationmarkequal:    len = len_match_exact<'!', '='>(iter);      m_column += len; break;
                case etoken::s_gt:                      len = len_match_exact<'>'>(iter);           m_column += len; break;
                case etoken::s_gtequal:                 len = len_match_exact<'>', '='>(iter);      m_column += len; break;
                case etoken::s_gtgt:                    len = len_match_exact<'>', '>'>(iter);      m_column += len; break;
                case etoken::s_gtgtgt:                  len = len_match_exact<'>', '>', '>'>(iter); m_column += len; break;
                case etoken::s_lt:                      len = len_match_exact<'<'>(iter);           m_column += len; break;
                case etoken::s_ltequal:                 len = len_match_exact<'<', '='>(iter);      m_column += len; break;
                case etoken::s_ltlt:                    len = len_match_exact<'<', '<'>(iter);      m_column += len; break;
                case etoken::s_ltltlt:                  len = len_match_exact<'<', '<', '<'>(iter); m_column += len; break;
                case etoken::s_minus:                   len = len_match_exact<'-'>(iter);           m_column += len; break;
                case etoken::s_minusminus:              len = len_match_exact<'-', '-'>(iter);      m_column += len; break;
                case etoken::s_percent:                 len = len_match_exact<'%'>(iter);           m_column += len; break;
                case etoken::s_plus:                    len = len_match_exact<'+'>(iter);           m_column += len; break;
                case etoken::s_plusplus:                len = len_match_exact<'+', '+'>(iter);      m_column += len; break;
                case etoken::s_questionmark:            len = len_match_exact<'?'>(iter);           m_column += len; break;
                case etoken::s_roundc:                  len = len_match_exact<')'>(iter);           m_column += len; break;
                case etoken::s_roundo:                  len = len_match_exact<'('>(iter);           m_column += len; break;
                case etoken::s_semicolon:               len = len_match_exact<';'>(iter);           m_column += len; break;
                case etoken::s_slash:                   len = len_match_exact<'/'>(iter);           m_column += len; break;
                case etoken::s_squarec:                 len = len_match_exact<']'>(iter);           m_column += len; break;
                case etoken::s_squareo:                 len = len_match_exact<'['>(iter);           m_column += len; break;
                case etoken::s_star:                    len = len_match_exact<'*'>(iter);           m_column += len; break;
                case etoken::s_tilde:                   len = len_match_exact<'~'>(iter);           m_column += len; break;
                case etoken::s_tildeequal:              len = len_match_exact<'~', '='>(iter);      m_column += len; break;
                case etoken::s_vline:                   len = len_match_exact<'|'>(iter);           m_column += len; break;
                case etoken::s_vlinevline:              len = len_match_exact<'|', '|'>(iter);      m_column += len; break;
                }

                if (len > 0)
                {
                    t.type = token_type;
                    t.contents = { &*m_current,  len }; // Dirty hack thanks to std::string_view not accepting iterators ...
                    m_current += len;
                    return t;
                }
            }
            return t;
        }

#ifdef _DEBUG
        iterator    m_prev_current;
        std::string m_prev_path;
        size_t      m_prev_line;
        size_t      m_prev_column;
#endif // _DEBUG

        static constexpr size_t null_col = 1;
        static constexpr size_t null_line = 1;

    public:
        tokenizer(iterator start, iterator end, std::string path) : m_start(start), m_current(start), m_end(end), m_line(null_line), m_column(null_col) {}
        token next()
        {
#ifdef _DEBUG
            m_prev_current = m_current;
            m_prev_path    = m_path;
            m_prev_line    = m_line;
            m_prev_column  = m_column;
#endif // _DEBUG
            
            if (m_current == m_end) { return { etoken::eof, m_line, m_column, (size_t)(m_current - m_start), {} }; };
            switch (*m_current)
            {
            case 'a':   return try_match({ etoken::l_ident });
            case 'b':   return try_match({ etoken::t_break, etoken::l_ident });
            case 'c':   return try_match({ etoken::t_case, etoken::t_catch, etoken::t_class, etoken::t_continue, etoken::t_conversion, etoken::l_ident });
            case 'd':   return try_match({ etoken::t_default, etoken::t_delete, etoken::t_derived, etoken::t_do, etoken::l_ident });
            case 'e':   return try_match({ etoken::t_else, etoken::t_enum, etoken::l_ident });
            case 'f':   return try_match({ etoken::t_false, etoken::t_finally, etoken::t_for, etoken::l_ident });
            case 'g':   return try_match({ etoken::t_get, etoken::t_goto, etoken::l_ident });
            case 'h':   return try_match({ etoken::l_ident });
            case 'i':   return try_match({ etoken::t_if, etoken::l_ident });
            case 'j':   return try_match({ etoken::l_ident });
            case 'k':   return try_match({ etoken::l_ident });
            case 'l':   return try_match({ etoken::t_local, etoken::l_ident });
            case 'm':   return try_match({ etoken::l_ident });
            case 'n':   return try_match({ etoken::t_namespace, etoken::t_new, etoken::l_ident });
            case 'o':   return try_match({ etoken::t_operator, etoken::l_ident });
            case 'p':   return try_match({ etoken::t_private, etoken::t_public, etoken::l_ident });
            case 'q':   return try_match({ etoken::l_ident });
            case 'r':   return try_match({ etoken::t_return, etoken::l_ident });
            case 's':   return try_match({ etoken::t_set, etoken::t_switch, etoken::l_ident });
            case 't':   return try_match({ etoken::t_this, etoken::t_throw, etoken::t_true, etoken::t_try, etoken::t_typename, etoken::l_ident });
            case 'u':   return try_match({ etoken::t_unbound, etoken::t_using, etoken::l_ident });
            case 'v':   return try_match({ etoken::l_ident });
            case 'w':   return try_match({ etoken::t_while, etoken::l_ident });
            case 'x':   return try_match({ etoken::l_ident });
            case 'y':   return try_match({ etoken::l_ident });
            case 'z':   return try_match({ etoken::l_ident });
            case 'A':   return try_match({ etoken::l_ident });
            case 'B':   return try_match({ etoken::l_ident });
            case 'C':   return try_match({ etoken::l_ident });
            case 'D':   return try_match({ etoken::l_ident });
            case 'E':   return try_match({ etoken::l_ident });
            case 'F':   return try_match({ etoken::l_ident });
            case 'G':   return try_match({ etoken::l_ident });
            case 'H':   return try_match({ etoken::l_ident });
            case 'I':   return try_match({ etoken::l_ident });
            case 'J':   return try_match({ etoken::l_ident });
            case 'K':   return try_match({ etoken::l_ident });
            case 'L':   return try_match({ etoken::l_ident });
            case 'M':   return try_match({ etoken::l_ident });
            case 'N':   return try_match({ etoken::l_ident });
            case 'O':   return try_match({ etoken::l_ident });
            case 'P':   return try_match({ etoken::l_ident });
            case 'Q':   return try_match({ etoken::l_ident });
            case 'R':   return try_match({ etoken::l_ident });
            case 'S':   return try_match({ etoken::l_ident });
            case 'T':   return try_match({ etoken::l_ident });
            case 'U':   return try_match({ etoken::l_ident });
            case 'V':   return try_match({ etoken::l_ident });
            case 'W':   return try_match({ etoken::l_ident });
            case 'X':   return try_match({ etoken::l_ident });
            case 'Y':   return try_match({ etoken::l_ident });
            case 'Z':   return try_match({ etoken::l_ident });
            case '_':   return try_match({ etoken::l_ident });
            case '0':   return try_match({ etoken::l_number });
            case '1':   return try_match({ etoken::l_number });
            case '2':   return try_match({ etoken::l_number });
            case '3':   return try_match({ etoken::l_number });
            case '4':   return try_match({ etoken::l_number });
            case '5':   return try_match({ etoken::l_number });
            case '6':   return try_match({ etoken::l_number });
            case '7':   return try_match({ etoken::l_number });
            case '8':   return try_match({ etoken::l_number });
            case '9':   return try_match({ etoken::l_number });
            case '^':   return try_match({ etoken::s_circumflex });
            case '~':   return try_match({ etoken::s_tilde, etoken::s_tildeequal });
            case '+':   return try_match({ etoken::s_plusplus, etoken::s_plus });
            case '-':   return try_match({ etoken::s_minusminus, etoken::s_minus });
            case '%':   return try_match({ etoken::s_percent });
            case '/':   return try_match({ etoken::i_comment_line, etoken::i_comment_block, etoken::s_slash });
            case '*':   return try_match({ etoken::s_star });
            case '(':   return try_match({ etoken::s_roundo });
            case ')':   return try_match({ etoken::s_roundc });
            case '[':   return try_match({ etoken::s_squareo });
            case ']':   return try_match({ etoken::s_squarec });
            case '{':   return try_match({ etoken::s_curlyo });
            case '}':   return try_match({ etoken::s_curlyc });
            case '&':   return try_match({ etoken::s_andand, etoken::s_and });
            case '!':   return try_match({ etoken::s_exclamationmarkequal, etoken::s_exclamationmark });
            case '|':   return try_match({ etoken::s_vlinevline, etoken::s_vline });
            case '>':   return try_match({ etoken::s_gtequal, etoken::s_gtgtgt, etoken::s_gtgt, etoken::s_gt });
            case '<':   return try_match({ etoken::s_ltequal, etoken::s_ltltlt, etoken::s_ltlt, etoken::s_lt });
            case '"':   return try_match({ etoken::l_string });
            case '\'':  return try_match({ etoken::l_char });
            case '=':   return try_match({ etoken::s_equalequal, etoken::s_equal });
            case '?':   return try_match({ etoken::s_questionmark });
            case ':':   return try_match({ etoken::s_coloncolon, etoken::s_colon });
            case ';':   return try_match({ etoken::s_semicolon });
            case ',':   return try_match({ etoken::s_comma });
            case '.':   return try_match({ etoken::s_dotdot, etoken::s_dot });
            case ' ':   return try_match({ etoken::i_whitespace });
            case '\r':  return try_match({ etoken::i_whitespace });
            case '\t':  return try_match({ etoken::i_whitespace });
            case '\n':  return try_match({ etoken::i_whitespace });
            case '#':   return try_match({ etoken::m_line });



            default:    return { etoken::invalid, m_line, m_column, (size_t)(m_current - m_start), {} };
            }
        }
#ifdef _DEBUG
        void undo_prev()
        {
            m_current = m_prev_current;
            m_path    = m_prev_path;
            m_line    = m_prev_line;
            m_column  = m_prev_column;
        }
#endif // _DEBUG
        token create_token() const
        {
            return { etoken::invalid, m_line, m_column, (size_t)(m_current - m_start), m_path };
        }
        static std::string_view to_string(etoken t)
        {
            using namespace std::string_view_literals;
            switch (t)
            {
            default:                                return "UNKNOWN"sv;
            case etoken::eof:                       return "eof"sv;
            case etoken::invalid:                   return "invalid"sv;
            case etoken::i_comment_line:            return "comment_line"sv;
            case etoken::i_comment_block:           return "comment_block"sv;
            case etoken::i_whitespace:              return "whitespace"sv;

            case etoken::t_break:                   return "break"sv;
            case etoken::t_case:                    return "case"sv;
            case etoken::t_catch:                   return "catch"sv;
            case etoken::t_class:                   return "class"sv;
            case etoken::t_continue:                return "continue"sv;
            case etoken::t_conversion:              return "conversion"sv;
            case etoken::t_default:                 return "default"sv;
            case etoken::t_delete:                  return "delete"sv;
            case etoken::t_derived:                 return "derived"sv;
            case etoken::t_do:                      return "do"sv;
            case etoken::t_else:                    return "else"sv;
            case etoken::t_enum:                    return "enum"sv;
            case etoken::t_false:                   return "false"sv;
            case etoken::t_finally:                 return "finally"sv;
            case etoken::t_for:                     return "for"sv;
            case etoken::t_get:                     return "get"sv;
            case etoken::t_goto:                    return "goto"sv;
            case etoken::t_if:                      return "if"sv;
            case etoken::t_local:                   return "local"sv;
            case etoken::t_namespace:               return "namespace"sv;
            case etoken::t_new:                     return "new"sv;
            case etoken::t_operator:                return "operator"sv;
            case etoken::t_private:                 return "private"sv;
            case etoken::t_public:                  return "public"sv;
            case etoken::t_return:                  return "return"sv;
            case etoken::t_set:                     return "set"sv;
            case etoken::t_switch:                  return "switch"sv;
            case etoken::t_this:                    return "this"sv;
            case etoken::t_throw:                   return "throw"sv;
            case etoken::t_true:                    return "true"sv;
            case etoken::t_try:                     return "try"sv;
            case etoken::t_typename:                return "typename"sv;
            case etoken::t_unbound:                 return "unbound"sv;
            case etoken::t_using:                   return "using"sv;
            case etoken::t_while:                   return "while"sv;

            case etoken::s_curlyo:                  return "{"sv;
            case etoken::s_curlyc:                  return "}"sv;
            case etoken::s_roundo:                  return "("sv;
            case etoken::s_roundc:                  return ")"sv;
            case etoken::s_squareo:                 return "["sv;
            case etoken::s_squarec:                 return "]"sv;
            case etoken::s_equalequal:              return "=="sv;
            case etoken::s_equal:                   return "="sv;
            case etoken::s_gtequal:                 return ">="sv;
            case etoken::s_gt:                      return ">"sv;
            case etoken::s_gtgt:                    return ">>"sv;
            case etoken::s_gtgtgt:                  return ">>>"sv;
            case etoken::s_ltequal:                 return "<="sv;
            case etoken::s_lt:                      return "<"sv;
            case etoken::s_ltlt:                    return "<<"sv;
            case etoken::s_ltltlt:                  return "<<<"sv;
            case etoken::s_plus:                    return "+"sv;
            case etoken::s_minus:                   return "-"sv;
            case etoken::s_exclamationmarkequal:    return "!="sv;
            case etoken::s_exclamationmark:         return "!"sv;
            case etoken::s_percent:                 return "%"sv;
            case etoken::s_star:                    return "*"sv;
            case etoken::s_slash:                   return "/"sv;
            case etoken::s_and:                     return "&"sv;
            case etoken::s_andand:                  return "&&"sv;
            case etoken::s_vline:                   return "|"sv;
            case etoken::s_vlinevline:              return "||"sv;
            case etoken::s_questionmark:            return "?"sv;
            case etoken::s_tilde:                   return "~"sv;
            case etoken::s_tildeequal:              return "~="sv;
            case etoken::s_circumflex:              return "^"sv;
            case etoken::s_colon:                   return ":"sv;
            case etoken::s_coloncolon:              return "::"sv;
            case etoken::s_semicolon:               return ";"sv;
            case etoken::s_comma:                   return ","sv;
            case etoken::s_dot:                     return "."sv;
            case etoken::s_dotdot:                  return ".."sv;

            case etoken::l_char:                    return "char"sv;
            case etoken::l_string:                  return "string"sv;
            case etoken::l_ident:                   return "ident"sv;
            case etoken::l_number:                  return "number"sv;
            }
        }
    };
}