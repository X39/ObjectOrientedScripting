// A Bison parser, made by GNU Bison 3.0.4.

// Skeleton interface for Bison LALR(1) parsers in C++

// Copyright (C) 2002-2015 Free Software Foundation, Inc.

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

// As a special exception, you may create a larger work that contains
// part or all of the Bison parser skeleton and distribute that work
// under terms of your choice, so long as that work isn't itself a
// parser generator using the skeleton or a modified version thereof
// as a parser skeleton.  Alternatively, if you modify or redistribute
// the parser skeleton itself, you may (at your option) remove this
// special exception, which will cause the skeleton and the resulting
// Bison output files to be licensed under the GNU General Public
// License without this special exception.

// This special exception was added by the Free Software Foundation in
// version 2.2 of Bison.

/**
 ** \file parser.tab.hh
 ** Define the  yaoosl::compiler ::parser class.
 */

// C++ LALR(1) parser skeleton written by Akim Demaille.

#ifndef YY_YY_PARSER_TAB_HH_INCLUDED
# define YY_YY_PARSER_TAB_HH_INCLUDED
// //                    "%code requires" blocks.
#line 15 "parser.y" // lalr1.cc:377

    #include "tokenizer.hpp"
    #include "cstnode.hpp"
     namespace yaoosl::compiler
     {
          class parser;
     }

#line 53 "parser.tab.hh" // lalr1.cc:377


# include <cstdlib> // std::abort
# include <iostream>
# include <stdexcept>
# include <string>
# include <vector>
# include "stack.hh"
# include "location.hh"

#ifndef YYASSERT
# include <cassert>
# define YYASSERT assert
#endif


#ifndef YY_ATTRIBUTE
# if (defined __GNUC__                                               \
      && (2 < __GNUC__ || (__GNUC__ == 2 && 96 <= __GNUC_MINOR__)))  \
     || defined __SUNPRO_C && 0x5110 <= __SUNPRO_C
#  define YY_ATTRIBUTE(Spec) __attribute__(Spec)
# else
#  define YY_ATTRIBUTE(Spec) /* empty */
# endif
#endif

#ifndef YY_ATTRIBUTE_PURE
# define YY_ATTRIBUTE_PURE   YY_ATTRIBUTE ((__pure__))
#endif

#ifndef YY_ATTRIBUTE_UNUSED
# define YY_ATTRIBUTE_UNUSED YY_ATTRIBUTE ((__unused__))
#endif

#if !defined _Noreturn \
     && (!defined __STDC_VERSION__ || __STDC_VERSION__ < 201112)
# if defined _MSC_VER && 1200 <= _MSC_VER
#  define _Noreturn __declspec (noreturn)
# else
#  define _Noreturn YY_ATTRIBUTE ((__noreturn__))
# endif
#endif

/* Suppress unused-variable warnings by "using" E.  */
#if ! defined lint || defined __GNUC__
# define YYUSE(E) ((void) (E))
#else
# define YYUSE(E) /* empty */
#endif

#if defined __GNUC__ && 407 <= __GNUC__ * 100 + __GNUC_MINOR__
/* Suppress an incorrect diagnostic about yylval being uninitialized.  */
# define YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN \
    _Pragma ("GCC diagnostic push") \
    _Pragma ("GCC diagnostic ignored \"-Wuninitialized\"")\
    _Pragma ("GCC diagnostic ignored \"-Wmaybe-uninitialized\"")
# define YY_IGNORE_MAYBE_UNINITIALIZED_END \
    _Pragma ("GCC diagnostic pop")
#else
# define YY_INITIAL_VALUE(Value) Value
#endif
#ifndef YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN
# define YY_IGNORE_MAYBE_UNINITIALIZED_BEGIN
# define YY_IGNORE_MAYBE_UNINITIALIZED_END
#endif
#ifndef YY_INITIAL_VALUE
# define YY_INITIAL_VALUE(Value) /* Nothing. */
#endif

/* Debug traces.  */
#ifndef YYDEBUG
# define YYDEBUG 1
#endif

#line 6 "parser.y" // lalr1.cc:377
namespace  yaoosl { namespace compiler  {
#line 130 "parser.tab.hh" // lalr1.cc:377



  /// A char[S] buffer to store and retrieve objects.
  ///
  /// Sort of a variant, but does not keep track of the nature
  /// of the stored data, since that knowledge is available
  /// via the current state.
  template <size_t S>
  struct variant
  {
    /// Type of *this.
    typedef variant<S> self_type;

    /// Empty construction.
    variant ()
    {}

    /// Construct and fill.
    template <typename T>
    variant (const T& t)
    {
      YYASSERT (sizeof (T) <= S);
      new (yyas_<T> ()) T (t);
    }

    /// Destruction, allowed only if empty.
    ~variant ()
    {}

    /// Instantiate an empty \a T in here.
    template <typename T>
    T&
    build ()
    {
      return *new (yyas_<T> ()) T;
    }

    /// Instantiate a \a T in here from \a t.
    template <typename T>
    T&
    build (const T& t)
    {
      return *new (yyas_<T> ()) T (t);
    }

    /// Accessor to a built \a T.
    template <typename T>
    T&
    as ()
    {
      return *yyas_<T> ();
    }

    /// Const accessor to a built \a T (for %printer).
    template <typename T>
    const T&
    as () const
    {
      return *yyas_<T> ();
    }

    /// Swap the content with \a other, of same type.
    ///
    /// Both variants must be built beforehand, because swapping the actual
    /// data requires reading it (with as()), and this is not possible on
    /// unconstructed variants: it would require some dynamic testing, which
    /// should not be the variant's responsability.
    /// Swapping between built and (possibly) non-built is done with
    /// variant::move ().
    template <typename T>
    void
    swap (self_type& other)
    {
      std::swap (as<T> (), other.as<T> ());
    }

    /// Move the content of \a other to this.
    ///
    /// Destroys \a other.
    template <typename T>
    void
    move (self_type& other)
    {
      build<T> ();
      swap<T> (other);
      other.destroy<T> ();
    }

    /// Copy the content of \a other to this.
    template <typename T>
    void
    copy (const self_type& other)
    {
      build<T> (other.as<T> ());
    }

    /// Destroy the stored \a T.
    template <typename T>
    void
    destroy ()
    {
      as<T> ().~T ();
    }

  private:
    /// Prohibit blind copies.
    self_type& operator=(const self_type&);
    variant (const self_type&);

    /// Accessor to raw memory as \a T.
    template <typename T>
    T*
    yyas_ ()
    {
      void *yyp = yybuffer_.yyraw;
      return static_cast<T*> (yyp);
     }

    /// Const accessor to raw memory as \a T.
    template <typename T>
    const T*
    yyas_ () const
    {
      const void *yyp = yybuffer_.yyraw;
      return static_cast<const T*> (yyp);
     }

    union
    {
      /// Strongest alignment constraints.
      long double yyalign_me;
      /// A buffer large enough to store any of the semantic values.
      char yyraw[S];
    } yybuffer_;
  };


  /// A Bison parser.
  class parser
  {
  public:
#ifndef YYSTYPE
    /// An auxiliary type to compute the largest semantic type.
    union union_type
    {
      // using
      // using_low
      // filestmnt
      // filestmnts
      // classstmnt
      // classstmnts
      // codestmnt
      // codestmnts
      // type_ident
      // type
      // typelist
      // encpsltn
      // encpsltn_n_cls
      // template_def
      // template_defs
      // template_use
      // template
      // namespace
      // enum
      // enum_body
      // enum_values
      // enum_value
      // class
      // classhead
      // classbody
      // mthd
      // mthd_head
      // mthd_args
      // mthd_body
      // mthd_arglist
      // mthd_arg
      // cnvrsn
      // ucnvrsn
      // uecnvrsn
      // mthdop
      // mthdop_head
      // mthdop_args
      // mthdop_ops1p
      // mthdop_ops1s
      // mthdop_ops2
      // mthdcnst
      // mthdcnst_head
      // prop
      // prop_head
      // prop_body
      // prop_set
      // prop_get
      // ifelse
      // for
      // for_step
      // for_step_arg
      // for_each
      // while
      // switch
      // switch_cases
      // switch_case
      // case
      // try
      // catch
      // catchlist
      // finally
      // trycatch
      // statement
      // declaration
      // scope
      // val
      // assignment
      // explist
      // exp01
      // exp02
      // exp03
      // exp04
      // exp05
      // exp06
      // exp07
      // exp08
      // exp09
      // exp10
      // exp11
      // exp12
      // arrget
      // dotnav
      // call
      // expp
      // cval
      char dummy1[sizeof(yaoosl::compiler::cstnode)];

      // NA
      // "public"
      // "local"
      // "derived"
      // "private"
      // "unbound"
      // "="
      // "&&"
      // "&"
      // "/"
      // "*"
      // "%"
      // "||"
      // "|"
      // "^"
      // "~"
      // ":"
      // "::"
      // "+"
      // "++"
      // "-"
      // "--"
      // "<="
      // "<"
      // "<<"
      // "<<<"
      // ">="
      // ">"
      // ">>"
      // ">>>"
      // "=="
      // "~="
      // "!="
      // "!"
      // "{"
      // "}"
      // "("
      // ")"
      // "["
      // "]"
      // ";"
      // ","
      // "."
      // "?"
      // "=>"
      // ".."
      // "break"
      // "case"
      // "continue"
      // "class"
      // "catch"
      // "conversion"
      // "default"
      // "do"
      // "delete"
      // "else"
      // "enum"
      // "for"
      // "finally"
      // "false"
      // "get"
      // "goto"
      // "if"
      // "namespace"
      // "new"
      // "operator"
      // "return"
      // "set"
      // "switch"
      // "throw"
      // "try"
      // "true"
      // "this"
      // "using"
      // "while"
      // L_IDENT
      // L_STRING
      // L_NUMBER
      // L_CHAR
      char dummy2[sizeof(yaoosl::compiler::tokenizer::token)];
};

    /// Symbol semantic values.
    typedef variant<sizeof(union_type)> semantic_type;
#else
    typedef YYSTYPE semantic_type;
#endif
    /// Symbol locations.
    typedef location location_type;

    /// Syntax errors thrown from user actions.
    struct syntax_error : std::runtime_error
    {
      syntax_error (const location_type& l, const std::string& m);
      location_type location;
    };

    /// Tokens.
    struct token
    {
      enum yytokentype
      {
        NA = 258,
        PUBLIC = 259,
        LOCAL = 260,
        DERIVED = 261,
        PRIVATE = 262,
        UNBOUND = 263,
        EQUAL = 264,
        ANDAND = 265,
        AND = 266,
        SLASH = 267,
        STAR = 268,
        PERCENT = 269,
        VLINEVLINE = 270,
        VLINE = 271,
        CIRCUMFLEX = 272,
        TILDE = 273,
        COLON = 274,
        COLONCOLON = 275,
        PLUS = 276,
        PLUSPLUS = 277,
        MINUS = 278,
        MINUSMINUS = 279,
        LTEQUAL = 280,
        LT = 281,
        LTLT = 282,
        LTLTLT = 283,
        GTEQUAL = 284,
        GT = 285,
        GTGT = 286,
        GTGTGT = 287,
        EQUALEQUAL = 288,
        TILDEEQUAL = 289,
        EXCLAMATIONMARKEQUAL = 290,
        EXCLAMATIONMARK = 291,
        CURLYO = 292,
        CURLYC = 293,
        ROUNDO = 294,
        ROUNDC = 295,
        SQUAREO = 296,
        SQUAREC = 297,
        SEMICOLON = 298,
        COMMA = 299,
        DOT = 300,
        QUESTIONMARK = 301,
        ARROWHEAD = 302,
        DOTDOT = 303,
        BREAK = 304,
        CASE = 305,
        CONTINUE = 306,
        CLASS = 307,
        CATCH = 308,
        CONVERSION = 309,
        DEFAULT = 310,
        DO = 311,
        DELETE = 312,
        ELSE = 313,
        ENUM = 314,
        FOR = 315,
        FINALLY = 316,
        FALSE = 317,
        GET = 318,
        GOTO = 319,
        IF = 320,
        NAMESPACE = 321,
        NEW = 322,
        OPERATOR = 323,
        RETURN = 324,
        SET = 325,
        SWITCH = 326,
        THROW = 327,
        TRY = 328,
        TRUE = 329,
        THIS = 330,
        USING = 331,
        WHILE = 332,
        L_IDENT = 333,
        L_STRING = 334,
        L_NUMBER = 335,
        L_CHAR = 336
      };
    };

    /// (External) token type, as returned by yylex.
    typedef token::yytokentype token_type;

    /// Symbol type: an internal symbol number.
    typedef int symbol_number_type;

    /// The symbol type number to denote an empty symbol.
    enum { empty_symbol = -2 };

    /// Internal symbol number for tokens (subsumed by symbol_number_type).
    typedef unsigned char token_number_type;

    /// A complete symbol.
    ///
    /// Expects its Base type to provide access to the symbol type
    /// via type_get().
    ///
    /// Provide access to semantic value and location.
    template <typename Base>
    struct basic_symbol : Base
    {
      /// Alias to Base.
      typedef Base super_type;

      /// Default constructor.
      basic_symbol ();

      /// Copy constructor.
      basic_symbol (const basic_symbol& other);

      /// Constructor for valueless symbols, and symbols from each type.

  basic_symbol (typename Base::kind_type t, const location_type& l);

  basic_symbol (typename Base::kind_type t, const yaoosl::compiler::cstnode v, const location_type& l);

  basic_symbol (typename Base::kind_type t, const yaoosl::compiler::tokenizer::token v, const location_type& l);


      /// Constructor for symbols with semantic value.
      basic_symbol (typename Base::kind_type t,
                    const semantic_type& v,
                    const location_type& l);

      /// Destroy the symbol.
      ~basic_symbol ();

      /// Destroy contents, and record that is empty.
      void clear ();

      /// Whether empty.
      bool empty () const;

      /// Destructive move, \a s is emptied into this.
      void move (basic_symbol& s);

      /// The semantic value.
      semantic_type value;

      /// The location.
      location_type location;

    private:
      /// Assignment operator.
      basic_symbol& operator= (const basic_symbol& other);
    };

    /// Type access provider for token (enum) based symbols.
    struct by_type
    {
      /// Default constructor.
      by_type ();

      /// Copy constructor.
      by_type (const by_type& other);

      /// The symbol type as needed by the constructor.
      typedef token_type kind_type;

      /// Constructor from (external) token numbers.
      by_type (kind_type t);

      /// Record that this symbol is empty.
      void clear ();

      /// Steal the symbol type from \a that.
      void move (by_type& that);

      /// The (internal) type number (corresponding to \a type).
      /// \a empty when empty.
      symbol_number_type type_get () const;

      /// The token.
      token_type token () const;

      /// The symbol type.
      /// \a empty_symbol when empty.
      /// An int, not token_number_type, to be able to store empty_symbol.
      int type;
    };

    /// "External" symbols: returned by the scanner.
    typedef basic_symbol<by_type> symbol_type;

    // Symbol constructors declarations.
    static inline
    symbol_type
    make_NA (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_PUBLIC (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_LOCAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_DERIVED (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_PRIVATE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_UNBOUND (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_EQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_ANDAND (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_AND (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_SLASH (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_STAR (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_PERCENT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_VLINEVLINE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_VLINE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_CIRCUMFLEX (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_TILDE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_COLON (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_COLONCOLON (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_PLUS (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_PLUSPLUS (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_MINUS (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_MINUSMINUS (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_LTEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_LT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_LTLT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_LTLTLT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_GTEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_GT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_GTGT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_GTGTGT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_EQUALEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_TILDEEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_EXCLAMATIONMARKEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_EXCLAMATIONMARK (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_CURLYO (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_CURLYC (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_ROUNDO (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_ROUNDC (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_SQUAREO (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_SQUAREC (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_SEMICOLON (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_COMMA (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_DOT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_QUESTIONMARK (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_ARROWHEAD (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_DOTDOT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_BREAK (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_CASE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_CONTINUE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_CLASS (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_CATCH (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_CONVERSION (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_DEFAULT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_DO (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_DELETE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_ELSE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_ENUM (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_FOR (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_FINALLY (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_FALSE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_GET (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_GOTO (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_IF (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_NAMESPACE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_NEW (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_OPERATOR (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_RETURN (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_SET (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_SWITCH (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_THROW (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_TRY (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_TRUE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_THIS (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_USING (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_WHILE (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_L_IDENT (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_L_STRING (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_L_NUMBER (const yaoosl::compiler::tokenizer::token& v, const location_type& l);

    static inline
    symbol_type
    make_L_CHAR (const yaoosl::compiler::tokenizer::token& v, const location_type& l);


    /// Build a parser object.
    parser (yaoosl::compiler::tokenizer &tokenizer_yyarg, yaoosl::compiler::cstnode& result_yyarg, std::string fpath_yyarg);
    virtual ~parser ();

    /// Parse.
    /// \returns  0 iff parsing succeeded.
    virtual int parse ();

#if YYDEBUG
    /// The current debugging stream.
    std::ostream& debug_stream () const YY_ATTRIBUTE_PURE;
    /// Set the current debugging stream.
    void set_debug_stream (std::ostream &);

    /// Type for debugging levels.
    typedef int debug_level_type;
    /// The current debugging level.
    debug_level_type debug_level () const YY_ATTRIBUTE_PURE;
    /// Set the current debugging level.
    void set_debug_level (debug_level_type l);
#endif

    /// Report a syntax error.
    /// \param loc    where the syntax error is found.
    /// \param msg    a description of the syntax error.
    virtual void error (const location_type& loc, const std::string& msg);

    /// Report a syntax error.
    void error (const syntax_error& err);

  private:
    /// This class is not copyable.
    parser (const parser&);
    parser& operator= (const parser&);

    /// State numbers.
    typedef int state_type;

    /// Generate an error message.
    /// \param yystate   the state where the error occurred.
    /// \param yyla      the lookahead token.
    virtual std::string yysyntax_error_ (state_type yystate,
                                         const symbol_type& yyla) const;

    /// Compute post-reduction state.
    /// \param yystate   the current state
    /// \param yysym     the nonterminal to push on the stack
    state_type yy_lr_goto_state_ (state_type yystate, int yysym);

    /// Whether the given \c yypact_ value indicates a defaulted state.
    /// \param yyvalue   the value to check
    static bool yy_pact_value_is_default_ (int yyvalue);

    /// Whether the given \c yytable_ value indicates a syntax error.
    /// \param yyvalue   the value to check
    static bool yy_table_value_is_error_ (int yyvalue);

    static const short int yypact_ninf_;
    static const short int yytable_ninf_;

    /// Convert a scanner token number \a t to a symbol number.
    static token_number_type yytranslate_ (token_type t);

    // Tables.
  // YYPACT[STATE-NUM] -- Index in YYTABLE of the portion describing
  // STATE-NUM.
  static const short int yypact_[];

  // YYDEFACT[STATE-NUM] -- Default reduction number in state STATE-NUM.
  // Performed when YYTABLE does not specify something else to do.  Zero
  // means the default is an error.
  static const unsigned char yydefact_[];

  // YYPGOTO[NTERM-NUM].
  static const short int yypgoto_[];

  // YYDEFGOTO[NTERM-NUM].
  static const short int yydefgoto_[];

  // YYTABLE[YYPACT[STATE-NUM]] -- What to do in state STATE-NUM.  If
  // positive, shift that token.  If negative, reduce the rule whose
  // number is the opposite.  If YYTABLE_NINF, syntax error.
  static const short int yytable_[];

  static const short int yycheck_[];

  // YYSTOS[STATE-NUM] -- The (internal number of the) accessing
  // symbol of state STATE-NUM.
  static const unsigned char yystos_[];

  // YYR1[YYN] -- Symbol number of symbol that rule YYN derives.
  static const unsigned char yyr1_[];

  // YYR2[YYN] -- Number of symbols on the right hand side of rule YYN.
  static const unsigned char yyr2_[];


    /// Convert the symbol name \a n to a form suitable for a diagnostic.
    static std::string yytnamerr_ (const char *n);


    /// For a symbol, its name in clear.
    static const char* const yytname_[];
#if YYDEBUG
  // YYRLINE[YYN] -- Source line where rule number YYN was defined.
  static const unsigned short int yyrline_[];
    /// Report on the debug stream that the rule \a r is going to be reduced.
    virtual void yy_reduce_print_ (int r);
    /// Print the state stack on the debug stream.
    virtual void yystack_print_ ();

    // Debugging.
    int yydebug_;
    std::ostream* yycdebug_;

    /// \brief Display a symbol type, value and location.
    /// \param yyo    The output stream.
    /// \param yysym  The symbol.
    template <typename Base>
    void yy_print_ (std::ostream& yyo, const basic_symbol<Base>& yysym) const;
#endif

    /// \brief Reclaim the memory associated to a symbol.
    /// \param yymsg     Why this token is reclaimed.
    ///                  If null, print nothing.
    /// \param yysym     The symbol.
    template <typename Base>
    void yy_destroy_ (const char* yymsg, basic_symbol<Base>& yysym) const;

  private:
    /// Type access provider for state based symbols.
    struct by_state
    {
      /// Default constructor.
      by_state ();

      /// The symbol type as needed by the constructor.
      typedef state_type kind_type;

      /// Constructor.
      by_state (kind_type s);

      /// Copy constructor.
      by_state (const by_state& other);

      /// Record that this symbol is empty.
      void clear ();

      /// Steal the symbol type from \a that.
      void move (by_state& that);

      /// The (internal) type number (corresponding to \a state).
      /// \a empty_symbol when empty.
      symbol_number_type type_get () const;

      /// The state number used to denote an empty symbol.
      enum { empty_state = -1 };

      /// The state.
      /// \a empty when empty.
      state_type state;
    };

    /// "Internal" symbol: element of the stack.
    struct stack_symbol_type : basic_symbol<by_state>
    {
      /// Superclass.
      typedef basic_symbol<by_state> super_type;
      /// Construct an empty symbol.
      stack_symbol_type ();
      /// Steal the contents from \a sym to build this.
      stack_symbol_type (state_type s, symbol_type& sym);
      /// Assignment, needed by push_back.
      stack_symbol_type& operator= (const stack_symbol_type& that);
    };

    /// Stack type.
    typedef stack<stack_symbol_type> stack_type;

    /// The stack.
    stack_type yystack_;

    /// Push a new state on the stack.
    /// \param m    a debug message to display
    ///             if null, no trace is output.
    /// \param s    the symbol
    /// \warning the contents of \a s.value is stolen.
    void yypush_ (const char* m, stack_symbol_type& s);

    /// Push a new look ahead token on the state on the stack.
    /// \param m    a debug message to display
    ///             if null, no trace is output.
    /// \param s    the state
    /// \param sym  the symbol (for its value and location).
    /// \warning the contents of \a s.value is stolen.
    void yypush_ (const char* m, state_type s, symbol_type& sym);

    /// Pop \a n symbols the three stacks.
    void yypop_ (unsigned int n = 1);

    /// Constants.
    enum
    {
      yyeof_ = 0,
      yylast_ = 945,     ///< Last index in yytable_.
      yynnts_ = 87,  ///< Number of nonterminal symbols.
      yyfinal_ = 29, ///< Termination state number.
      yyterror_ = 1,
      yyerrcode_ = 256,
      yyntokens_ = 82  ///< Number of tokens.
    };


    // User arguments.
    yaoosl::compiler::tokenizer &tokenizer;
    yaoosl::compiler::cstnode& result;
    std::string fpath;
  };

  // Symbol number corresponding to token number t.
  inline
  parser::token_number_type
  parser::yytranslate_ (token_type t)
  {
    static
    const token_number_type
    translate_table[] =
    {
     0,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     2,     2,     2,     2,
       2,     2,     2,     2,     2,     2,     1,     2,     3,     4,
       5,     6,     7,     8,     9,    10,    11,    12,    13,    14,
      15,    16,    17,    18,    19,    20,    21,    22,    23,    24,
      25,    26,    27,    28,    29,    30,    31,    32,    33,    34,
      35,    36,    37,    38,    39,    40,    41,    42,    43,    44,
      45,    46,    47,    48,    49,    50,    51,    52,    53,    54,
      55,    56,    57,    58,    59,    60,    61,    62,    63,    64,
      65,    66,    67,    68,    69,    70,    71,    72,    73,    74,
      75,    76,    77,    78,    79,    80,    81
    };
    const unsigned int user_token_number_max_ = 336;
    const token_number_type undef_token_ = 2;

    if (static_cast<int>(t) <= yyeof_)
      return yyeof_;
    else if (static_cast<unsigned int> (t) <= user_token_number_max_)
      return translate_table[t];
    else
      return undef_token_;
  }

  inline
  parser::syntax_error::syntax_error (const location_type& l, const std::string& m)
    : std::runtime_error (m)
    , location (l)
  {}

  // basic_symbol.
  template <typename Base>
  inline
  parser::basic_symbol<Base>::basic_symbol ()
    : value ()
  {}

  template <typename Base>
  inline
  parser::basic_symbol<Base>::basic_symbol (const basic_symbol& other)
    : Base (other)
    , value ()
    , location (other.location)
  {
      switch (other.type_get ())
    {
      case 84: // using
      case 85: // using_low
      case 86: // filestmnt
      case 87: // filestmnts
      case 88: // classstmnt
      case 89: // classstmnts
      case 90: // codestmnt
      case 91: // codestmnts
      case 92: // type_ident
      case 93: // type
      case 94: // typelist
      case 95: // encpsltn
      case 96: // encpsltn_n_cls
      case 97: // template_def
      case 98: // template_defs
      case 99: // template_use
      case 100: // template
      case 101: // namespace
      case 102: // enum
      case 103: // enum_body
      case 104: // enum_values
      case 105: // enum_value
      case 106: // class
      case 107: // classhead
      case 108: // classbody
      case 109: // mthd
      case 110: // mthd_head
      case 111: // mthd_args
      case 112: // mthd_body
      case 113: // mthd_arglist
      case 114: // mthd_arg
      case 115: // cnvrsn
      case 116: // ucnvrsn
      case 117: // uecnvrsn
      case 118: // mthdop
      case 119: // mthdop_head
      case 120: // mthdop_args
      case 121: // mthdop_ops1p
      case 122: // mthdop_ops1s
      case 123: // mthdop_ops2
      case 124: // mthdcnst
      case 125: // mthdcnst_head
      case 126: // prop
      case 127: // prop_head
      case 128: // prop_body
      case 129: // prop_set
      case 130: // prop_get
      case 131: // ifelse
      case 132: // for
      case 133: // for_step
      case 134: // for_step_arg
      case 135: // for_each
      case 136: // while
      case 137: // switch
      case 138: // switch_cases
      case 139: // switch_case
      case 140: // case
      case 141: // try
      case 142: // catch
      case 143: // catchlist
      case 144: // finally
      case 145: // trycatch
      case 146: // statement
      case 147: // declaration
      case 148: // scope
      case 149: // val
      case 150: // assignment
      case 151: // explist
      case 152: // exp01
      case 153: // exp02
      case 154: // exp03
      case 155: // exp04
      case 156: // exp05
      case 157: // exp06
      case 158: // exp07
      case 159: // exp08
      case 160: // exp09
      case 161: // exp10
      case 162: // exp11
      case 163: // exp12
      case 164: // arrget
      case 165: // dotnav
      case 166: // call
      case 167: // expp
      case 168: // cval
        value.copy< yaoosl::compiler::cstnode > (other.value);
        break;

      case 3: // NA
      case 4: // "public"
      case 5: // "local"
      case 6: // "derived"
      case 7: // "private"
      case 8: // "unbound"
      case 9: // "="
      case 10: // "&&"
      case 11: // "&"
      case 12: // "/"
      case 13: // "*"
      case 14: // "%"
      case 15: // "||"
      case 16: // "|"
      case 17: // "^"
      case 18: // "~"
      case 19: // ":"
      case 20: // "::"
      case 21: // "+"
      case 22: // "++"
      case 23: // "-"
      case 24: // "--"
      case 25: // "<="
      case 26: // "<"
      case 27: // "<<"
      case 28: // "<<<"
      case 29: // ">="
      case 30: // ">"
      case 31: // ">>"
      case 32: // ">>>"
      case 33: // "=="
      case 34: // "~="
      case 35: // "!="
      case 36: // "!"
      case 37: // "{"
      case 38: // "}"
      case 39: // "("
      case 40: // ")"
      case 41: // "["
      case 42: // "]"
      case 43: // ";"
      case 44: // ","
      case 45: // "."
      case 46: // "?"
      case 47: // "=>"
      case 48: // ".."
      case 49: // "break"
      case 50: // "case"
      case 51: // "continue"
      case 52: // "class"
      case 53: // "catch"
      case 54: // "conversion"
      case 55: // "default"
      case 56: // "do"
      case 57: // "delete"
      case 58: // "else"
      case 59: // "enum"
      case 60: // "for"
      case 61: // "finally"
      case 62: // "false"
      case 63: // "get"
      case 64: // "goto"
      case 65: // "if"
      case 66: // "namespace"
      case 67: // "new"
      case 68: // "operator"
      case 69: // "return"
      case 70: // "set"
      case 71: // "switch"
      case 72: // "throw"
      case 73: // "try"
      case 74: // "true"
      case 75: // "this"
      case 76: // "using"
      case 77: // "while"
      case 78: // L_IDENT
      case 79: // L_STRING
      case 80: // L_NUMBER
      case 81: // L_CHAR
        value.copy< yaoosl::compiler::tokenizer::token > (other.value);
        break;

      default:
        break;
    }

  }


  template <typename Base>
  inline
  parser::basic_symbol<Base>::basic_symbol (typename Base::kind_type t, const semantic_type& v, const location_type& l)
    : Base (t)
    , value ()
    , location (l)
  {
    (void) v;
      switch (this->type_get ())
    {
      case 84: // using
      case 85: // using_low
      case 86: // filestmnt
      case 87: // filestmnts
      case 88: // classstmnt
      case 89: // classstmnts
      case 90: // codestmnt
      case 91: // codestmnts
      case 92: // type_ident
      case 93: // type
      case 94: // typelist
      case 95: // encpsltn
      case 96: // encpsltn_n_cls
      case 97: // template_def
      case 98: // template_defs
      case 99: // template_use
      case 100: // template
      case 101: // namespace
      case 102: // enum
      case 103: // enum_body
      case 104: // enum_values
      case 105: // enum_value
      case 106: // class
      case 107: // classhead
      case 108: // classbody
      case 109: // mthd
      case 110: // mthd_head
      case 111: // mthd_args
      case 112: // mthd_body
      case 113: // mthd_arglist
      case 114: // mthd_arg
      case 115: // cnvrsn
      case 116: // ucnvrsn
      case 117: // uecnvrsn
      case 118: // mthdop
      case 119: // mthdop_head
      case 120: // mthdop_args
      case 121: // mthdop_ops1p
      case 122: // mthdop_ops1s
      case 123: // mthdop_ops2
      case 124: // mthdcnst
      case 125: // mthdcnst_head
      case 126: // prop
      case 127: // prop_head
      case 128: // prop_body
      case 129: // prop_set
      case 130: // prop_get
      case 131: // ifelse
      case 132: // for
      case 133: // for_step
      case 134: // for_step_arg
      case 135: // for_each
      case 136: // while
      case 137: // switch
      case 138: // switch_cases
      case 139: // switch_case
      case 140: // case
      case 141: // try
      case 142: // catch
      case 143: // catchlist
      case 144: // finally
      case 145: // trycatch
      case 146: // statement
      case 147: // declaration
      case 148: // scope
      case 149: // val
      case 150: // assignment
      case 151: // explist
      case 152: // exp01
      case 153: // exp02
      case 154: // exp03
      case 155: // exp04
      case 156: // exp05
      case 157: // exp06
      case 158: // exp07
      case 159: // exp08
      case 160: // exp09
      case 161: // exp10
      case 162: // exp11
      case 163: // exp12
      case 164: // arrget
      case 165: // dotnav
      case 166: // call
      case 167: // expp
      case 168: // cval
        value.copy< yaoosl::compiler::cstnode > (v);
        break;

      case 3: // NA
      case 4: // "public"
      case 5: // "local"
      case 6: // "derived"
      case 7: // "private"
      case 8: // "unbound"
      case 9: // "="
      case 10: // "&&"
      case 11: // "&"
      case 12: // "/"
      case 13: // "*"
      case 14: // "%"
      case 15: // "||"
      case 16: // "|"
      case 17: // "^"
      case 18: // "~"
      case 19: // ":"
      case 20: // "::"
      case 21: // "+"
      case 22: // "++"
      case 23: // "-"
      case 24: // "--"
      case 25: // "<="
      case 26: // "<"
      case 27: // "<<"
      case 28: // "<<<"
      case 29: // ">="
      case 30: // ">"
      case 31: // ">>"
      case 32: // ">>>"
      case 33: // "=="
      case 34: // "~="
      case 35: // "!="
      case 36: // "!"
      case 37: // "{"
      case 38: // "}"
      case 39: // "("
      case 40: // ")"
      case 41: // "["
      case 42: // "]"
      case 43: // ";"
      case 44: // ","
      case 45: // "."
      case 46: // "?"
      case 47: // "=>"
      case 48: // ".."
      case 49: // "break"
      case 50: // "case"
      case 51: // "continue"
      case 52: // "class"
      case 53: // "catch"
      case 54: // "conversion"
      case 55: // "default"
      case 56: // "do"
      case 57: // "delete"
      case 58: // "else"
      case 59: // "enum"
      case 60: // "for"
      case 61: // "finally"
      case 62: // "false"
      case 63: // "get"
      case 64: // "goto"
      case 65: // "if"
      case 66: // "namespace"
      case 67: // "new"
      case 68: // "operator"
      case 69: // "return"
      case 70: // "set"
      case 71: // "switch"
      case 72: // "throw"
      case 73: // "try"
      case 74: // "true"
      case 75: // "this"
      case 76: // "using"
      case 77: // "while"
      case 78: // L_IDENT
      case 79: // L_STRING
      case 80: // L_NUMBER
      case 81: // L_CHAR
        value.copy< yaoosl::compiler::tokenizer::token > (v);
        break;

      default:
        break;
    }
}


  // Implementation of basic_symbol constructor for each type.

  template <typename Base>
  parser::basic_symbol<Base>::basic_symbol (typename Base::kind_type t, const location_type& l)
    : Base (t)
    , value ()
    , location (l)
  {}

  template <typename Base>
  parser::basic_symbol<Base>::basic_symbol (typename Base::kind_type t, const yaoosl::compiler::cstnode v, const location_type& l)
    : Base (t)
    , value (v)
    , location (l)
  {}

  template <typename Base>
  parser::basic_symbol<Base>::basic_symbol (typename Base::kind_type t, const yaoosl::compiler::tokenizer::token v, const location_type& l)
    : Base (t)
    , value (v)
    , location (l)
  {}


  template <typename Base>
  inline
  parser::basic_symbol<Base>::~basic_symbol ()
  {
    clear ();
  }

  template <typename Base>
  inline
  void
  parser::basic_symbol<Base>::clear ()
  {
    // User destructor.
    symbol_number_type yytype = this->type_get ();
    basic_symbol<Base>& yysym = *this;
    (void) yysym;
    switch (yytype)
    {
   default:
      break;
    }

    // Type destructor.
    switch (yytype)
    {
      case 84: // using
      case 85: // using_low
      case 86: // filestmnt
      case 87: // filestmnts
      case 88: // classstmnt
      case 89: // classstmnts
      case 90: // codestmnt
      case 91: // codestmnts
      case 92: // type_ident
      case 93: // type
      case 94: // typelist
      case 95: // encpsltn
      case 96: // encpsltn_n_cls
      case 97: // template_def
      case 98: // template_defs
      case 99: // template_use
      case 100: // template
      case 101: // namespace
      case 102: // enum
      case 103: // enum_body
      case 104: // enum_values
      case 105: // enum_value
      case 106: // class
      case 107: // classhead
      case 108: // classbody
      case 109: // mthd
      case 110: // mthd_head
      case 111: // mthd_args
      case 112: // mthd_body
      case 113: // mthd_arglist
      case 114: // mthd_arg
      case 115: // cnvrsn
      case 116: // ucnvrsn
      case 117: // uecnvrsn
      case 118: // mthdop
      case 119: // mthdop_head
      case 120: // mthdop_args
      case 121: // mthdop_ops1p
      case 122: // mthdop_ops1s
      case 123: // mthdop_ops2
      case 124: // mthdcnst
      case 125: // mthdcnst_head
      case 126: // prop
      case 127: // prop_head
      case 128: // prop_body
      case 129: // prop_set
      case 130: // prop_get
      case 131: // ifelse
      case 132: // for
      case 133: // for_step
      case 134: // for_step_arg
      case 135: // for_each
      case 136: // while
      case 137: // switch
      case 138: // switch_cases
      case 139: // switch_case
      case 140: // case
      case 141: // try
      case 142: // catch
      case 143: // catchlist
      case 144: // finally
      case 145: // trycatch
      case 146: // statement
      case 147: // declaration
      case 148: // scope
      case 149: // val
      case 150: // assignment
      case 151: // explist
      case 152: // exp01
      case 153: // exp02
      case 154: // exp03
      case 155: // exp04
      case 156: // exp05
      case 157: // exp06
      case 158: // exp07
      case 159: // exp08
      case 160: // exp09
      case 161: // exp10
      case 162: // exp11
      case 163: // exp12
      case 164: // arrget
      case 165: // dotnav
      case 166: // call
      case 167: // expp
      case 168: // cval
        value.template destroy< yaoosl::compiler::cstnode > ();
        break;

      case 3: // NA
      case 4: // "public"
      case 5: // "local"
      case 6: // "derived"
      case 7: // "private"
      case 8: // "unbound"
      case 9: // "="
      case 10: // "&&"
      case 11: // "&"
      case 12: // "/"
      case 13: // "*"
      case 14: // "%"
      case 15: // "||"
      case 16: // "|"
      case 17: // "^"
      case 18: // "~"
      case 19: // ":"
      case 20: // "::"
      case 21: // "+"
      case 22: // "++"
      case 23: // "-"
      case 24: // "--"
      case 25: // "<="
      case 26: // "<"
      case 27: // "<<"
      case 28: // "<<<"
      case 29: // ">="
      case 30: // ">"
      case 31: // ">>"
      case 32: // ">>>"
      case 33: // "=="
      case 34: // "~="
      case 35: // "!="
      case 36: // "!"
      case 37: // "{"
      case 38: // "}"
      case 39: // "("
      case 40: // ")"
      case 41: // "["
      case 42: // "]"
      case 43: // ";"
      case 44: // ","
      case 45: // "."
      case 46: // "?"
      case 47: // "=>"
      case 48: // ".."
      case 49: // "break"
      case 50: // "case"
      case 51: // "continue"
      case 52: // "class"
      case 53: // "catch"
      case 54: // "conversion"
      case 55: // "default"
      case 56: // "do"
      case 57: // "delete"
      case 58: // "else"
      case 59: // "enum"
      case 60: // "for"
      case 61: // "finally"
      case 62: // "false"
      case 63: // "get"
      case 64: // "goto"
      case 65: // "if"
      case 66: // "namespace"
      case 67: // "new"
      case 68: // "operator"
      case 69: // "return"
      case 70: // "set"
      case 71: // "switch"
      case 72: // "throw"
      case 73: // "try"
      case 74: // "true"
      case 75: // "this"
      case 76: // "using"
      case 77: // "while"
      case 78: // L_IDENT
      case 79: // L_STRING
      case 80: // L_NUMBER
      case 81: // L_CHAR
        value.template destroy< yaoosl::compiler::tokenizer::token > ();
        break;

      default:
        break;
    }

    Base::clear ();
  }

  template <typename Base>
  inline
  bool
  parser::basic_symbol<Base>::empty () const
  {
    return Base::type_get () == empty_symbol;
  }

  template <typename Base>
  inline
  void
  parser::basic_symbol<Base>::move (basic_symbol& s)
  {
    super_type::move(s);
      switch (this->type_get ())
    {
      case 84: // using
      case 85: // using_low
      case 86: // filestmnt
      case 87: // filestmnts
      case 88: // classstmnt
      case 89: // classstmnts
      case 90: // codestmnt
      case 91: // codestmnts
      case 92: // type_ident
      case 93: // type
      case 94: // typelist
      case 95: // encpsltn
      case 96: // encpsltn_n_cls
      case 97: // template_def
      case 98: // template_defs
      case 99: // template_use
      case 100: // template
      case 101: // namespace
      case 102: // enum
      case 103: // enum_body
      case 104: // enum_values
      case 105: // enum_value
      case 106: // class
      case 107: // classhead
      case 108: // classbody
      case 109: // mthd
      case 110: // mthd_head
      case 111: // mthd_args
      case 112: // mthd_body
      case 113: // mthd_arglist
      case 114: // mthd_arg
      case 115: // cnvrsn
      case 116: // ucnvrsn
      case 117: // uecnvrsn
      case 118: // mthdop
      case 119: // mthdop_head
      case 120: // mthdop_args
      case 121: // mthdop_ops1p
      case 122: // mthdop_ops1s
      case 123: // mthdop_ops2
      case 124: // mthdcnst
      case 125: // mthdcnst_head
      case 126: // prop
      case 127: // prop_head
      case 128: // prop_body
      case 129: // prop_set
      case 130: // prop_get
      case 131: // ifelse
      case 132: // for
      case 133: // for_step
      case 134: // for_step_arg
      case 135: // for_each
      case 136: // while
      case 137: // switch
      case 138: // switch_cases
      case 139: // switch_case
      case 140: // case
      case 141: // try
      case 142: // catch
      case 143: // catchlist
      case 144: // finally
      case 145: // trycatch
      case 146: // statement
      case 147: // declaration
      case 148: // scope
      case 149: // val
      case 150: // assignment
      case 151: // explist
      case 152: // exp01
      case 153: // exp02
      case 154: // exp03
      case 155: // exp04
      case 156: // exp05
      case 157: // exp06
      case 158: // exp07
      case 159: // exp08
      case 160: // exp09
      case 161: // exp10
      case 162: // exp11
      case 163: // exp12
      case 164: // arrget
      case 165: // dotnav
      case 166: // call
      case 167: // expp
      case 168: // cval
        value.move< yaoosl::compiler::cstnode > (s.value);
        break;

      case 3: // NA
      case 4: // "public"
      case 5: // "local"
      case 6: // "derived"
      case 7: // "private"
      case 8: // "unbound"
      case 9: // "="
      case 10: // "&&"
      case 11: // "&"
      case 12: // "/"
      case 13: // "*"
      case 14: // "%"
      case 15: // "||"
      case 16: // "|"
      case 17: // "^"
      case 18: // "~"
      case 19: // ":"
      case 20: // "::"
      case 21: // "+"
      case 22: // "++"
      case 23: // "-"
      case 24: // "--"
      case 25: // "<="
      case 26: // "<"
      case 27: // "<<"
      case 28: // "<<<"
      case 29: // ">="
      case 30: // ">"
      case 31: // ">>"
      case 32: // ">>>"
      case 33: // "=="
      case 34: // "~="
      case 35: // "!="
      case 36: // "!"
      case 37: // "{"
      case 38: // "}"
      case 39: // "("
      case 40: // ")"
      case 41: // "["
      case 42: // "]"
      case 43: // ";"
      case 44: // ","
      case 45: // "."
      case 46: // "?"
      case 47: // "=>"
      case 48: // ".."
      case 49: // "break"
      case 50: // "case"
      case 51: // "continue"
      case 52: // "class"
      case 53: // "catch"
      case 54: // "conversion"
      case 55: // "default"
      case 56: // "do"
      case 57: // "delete"
      case 58: // "else"
      case 59: // "enum"
      case 60: // "for"
      case 61: // "finally"
      case 62: // "false"
      case 63: // "get"
      case 64: // "goto"
      case 65: // "if"
      case 66: // "namespace"
      case 67: // "new"
      case 68: // "operator"
      case 69: // "return"
      case 70: // "set"
      case 71: // "switch"
      case 72: // "throw"
      case 73: // "try"
      case 74: // "true"
      case 75: // "this"
      case 76: // "using"
      case 77: // "while"
      case 78: // L_IDENT
      case 79: // L_STRING
      case 80: // L_NUMBER
      case 81: // L_CHAR
        value.move< yaoosl::compiler::tokenizer::token > (s.value);
        break;

      default:
        break;
    }

    location = s.location;
  }

  // by_type.
  inline
  parser::by_type::by_type ()
    : type (empty_symbol)
  {}

  inline
  parser::by_type::by_type (const by_type& other)
    : type (other.type)
  {}

  inline
  parser::by_type::by_type (token_type t)
    : type (yytranslate_ (t))
  {}

  inline
  void
  parser::by_type::clear ()
  {
    type = empty_symbol;
  }

  inline
  void
  parser::by_type::move (by_type& that)
  {
    type = that.type;
    that.clear ();
  }

  inline
  int
  parser::by_type::type_get () const
  {
    return type;
  }

  inline
  parser::token_type
  parser::by_type::token () const
  {
    // YYTOKNUM[NUM] -- (External) token number corresponding to the
    // (internal) symbol number NUM (which must be that of a token).  */
    static
    const unsigned short int
    yytoken_number_[] =
    {
       0,   256,   257,   258,   259,   260,   261,   262,   263,   264,
     265,   266,   267,   268,   269,   270,   271,   272,   273,   274,
     275,   276,   277,   278,   279,   280,   281,   282,   283,   284,
     285,   286,   287,   288,   289,   290,   291,   292,   293,   294,
     295,   296,   297,   298,   299,   300,   301,   302,   303,   304,
     305,   306,   307,   308,   309,   310,   311,   312,   313,   314,
     315,   316,   317,   318,   319,   320,   321,   322,   323,   324,
     325,   326,   327,   328,   329,   330,   331,   332,   333,   334,
     335,   336
    };
    return static_cast<token_type> (yytoken_number_[type]);
  }
  // Implementation of make_symbol for each symbol type.
  parser::symbol_type
  parser::make_NA (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::NA, v, l);
  }

  parser::symbol_type
  parser::make_PUBLIC (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::PUBLIC, v, l);
  }

  parser::symbol_type
  parser::make_LOCAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::LOCAL, v, l);
  }

  parser::symbol_type
  parser::make_DERIVED (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::DERIVED, v, l);
  }

  parser::symbol_type
  parser::make_PRIVATE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::PRIVATE, v, l);
  }

  parser::symbol_type
  parser::make_UNBOUND (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::UNBOUND, v, l);
  }

  parser::symbol_type
  parser::make_EQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::EQUAL, v, l);
  }

  parser::symbol_type
  parser::make_ANDAND (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::ANDAND, v, l);
  }

  parser::symbol_type
  parser::make_AND (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::AND, v, l);
  }

  parser::symbol_type
  parser::make_SLASH (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::SLASH, v, l);
  }

  parser::symbol_type
  parser::make_STAR (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::STAR, v, l);
  }

  parser::symbol_type
  parser::make_PERCENT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::PERCENT, v, l);
  }

  parser::symbol_type
  parser::make_VLINEVLINE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::VLINEVLINE, v, l);
  }

  parser::symbol_type
  parser::make_VLINE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::VLINE, v, l);
  }

  parser::symbol_type
  parser::make_CIRCUMFLEX (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::CIRCUMFLEX, v, l);
  }

  parser::symbol_type
  parser::make_TILDE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::TILDE, v, l);
  }

  parser::symbol_type
  parser::make_COLON (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::COLON, v, l);
  }

  parser::symbol_type
  parser::make_COLONCOLON (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::COLONCOLON, v, l);
  }

  parser::symbol_type
  parser::make_PLUS (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::PLUS, v, l);
  }

  parser::symbol_type
  parser::make_PLUSPLUS (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::PLUSPLUS, v, l);
  }

  parser::symbol_type
  parser::make_MINUS (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::MINUS, v, l);
  }

  parser::symbol_type
  parser::make_MINUSMINUS (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::MINUSMINUS, v, l);
  }

  parser::symbol_type
  parser::make_LTEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::LTEQUAL, v, l);
  }

  parser::symbol_type
  parser::make_LT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::LT, v, l);
  }

  parser::symbol_type
  parser::make_LTLT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::LTLT, v, l);
  }

  parser::symbol_type
  parser::make_LTLTLT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::LTLTLT, v, l);
  }

  parser::symbol_type
  parser::make_GTEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::GTEQUAL, v, l);
  }

  parser::symbol_type
  parser::make_GT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::GT, v, l);
  }

  parser::symbol_type
  parser::make_GTGT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::GTGT, v, l);
  }

  parser::symbol_type
  parser::make_GTGTGT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::GTGTGT, v, l);
  }

  parser::symbol_type
  parser::make_EQUALEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::EQUALEQUAL, v, l);
  }

  parser::symbol_type
  parser::make_TILDEEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::TILDEEQUAL, v, l);
  }

  parser::symbol_type
  parser::make_EXCLAMATIONMARKEQUAL (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::EXCLAMATIONMARKEQUAL, v, l);
  }

  parser::symbol_type
  parser::make_EXCLAMATIONMARK (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::EXCLAMATIONMARK, v, l);
  }

  parser::symbol_type
  parser::make_CURLYO (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::CURLYO, v, l);
  }

  parser::symbol_type
  parser::make_CURLYC (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::CURLYC, v, l);
  }

  parser::symbol_type
  parser::make_ROUNDO (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::ROUNDO, v, l);
  }

  parser::symbol_type
  parser::make_ROUNDC (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::ROUNDC, v, l);
  }

  parser::symbol_type
  parser::make_SQUAREO (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::SQUAREO, v, l);
  }

  parser::symbol_type
  parser::make_SQUAREC (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::SQUAREC, v, l);
  }

  parser::symbol_type
  parser::make_SEMICOLON (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::SEMICOLON, v, l);
  }

  parser::symbol_type
  parser::make_COMMA (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::COMMA, v, l);
  }

  parser::symbol_type
  parser::make_DOT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::DOT, v, l);
  }

  parser::symbol_type
  parser::make_QUESTIONMARK (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::QUESTIONMARK, v, l);
  }

  parser::symbol_type
  parser::make_ARROWHEAD (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::ARROWHEAD, v, l);
  }

  parser::symbol_type
  parser::make_DOTDOT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::DOTDOT, v, l);
  }

  parser::symbol_type
  parser::make_BREAK (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::BREAK, v, l);
  }

  parser::symbol_type
  parser::make_CASE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::CASE, v, l);
  }

  parser::symbol_type
  parser::make_CONTINUE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::CONTINUE, v, l);
  }

  parser::symbol_type
  parser::make_CLASS (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::CLASS, v, l);
  }

  parser::symbol_type
  parser::make_CATCH (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::CATCH, v, l);
  }

  parser::symbol_type
  parser::make_CONVERSION (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::CONVERSION, v, l);
  }

  parser::symbol_type
  parser::make_DEFAULT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::DEFAULT, v, l);
  }

  parser::symbol_type
  parser::make_DO (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::DO, v, l);
  }

  parser::symbol_type
  parser::make_DELETE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::DELETE, v, l);
  }

  parser::symbol_type
  parser::make_ELSE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::ELSE, v, l);
  }

  parser::symbol_type
  parser::make_ENUM (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::ENUM, v, l);
  }

  parser::symbol_type
  parser::make_FOR (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::FOR, v, l);
  }

  parser::symbol_type
  parser::make_FINALLY (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::FINALLY, v, l);
  }

  parser::symbol_type
  parser::make_FALSE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::FALSE, v, l);
  }

  parser::symbol_type
  parser::make_GET (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::GET, v, l);
  }

  parser::symbol_type
  parser::make_GOTO (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::GOTO, v, l);
  }

  parser::symbol_type
  parser::make_IF (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::IF, v, l);
  }

  parser::symbol_type
  parser::make_NAMESPACE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::NAMESPACE, v, l);
  }

  parser::symbol_type
  parser::make_NEW (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::NEW, v, l);
  }

  parser::symbol_type
  parser::make_OPERATOR (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::OPERATOR, v, l);
  }

  parser::symbol_type
  parser::make_RETURN (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::RETURN, v, l);
  }

  parser::symbol_type
  parser::make_SET (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::SET, v, l);
  }

  parser::symbol_type
  parser::make_SWITCH (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::SWITCH, v, l);
  }

  parser::symbol_type
  parser::make_THROW (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::THROW, v, l);
  }

  parser::symbol_type
  parser::make_TRY (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::TRY, v, l);
  }

  parser::symbol_type
  parser::make_TRUE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::TRUE, v, l);
  }

  parser::symbol_type
  parser::make_THIS (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::THIS, v, l);
  }

  parser::symbol_type
  parser::make_USING (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::USING, v, l);
  }

  parser::symbol_type
  parser::make_WHILE (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::WHILE, v, l);
  }

  parser::symbol_type
  parser::make_L_IDENT (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::L_IDENT, v, l);
  }

  parser::symbol_type
  parser::make_L_STRING (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::L_STRING, v, l);
  }

  parser::symbol_type
  parser::make_L_NUMBER (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::L_NUMBER, v, l);
  }

  parser::symbol_type
  parser::make_L_CHAR (const yaoosl::compiler::tokenizer::token& v, const location_type& l)
  {
    return symbol_type (token::L_CHAR, v, l);
  }


#line 6 "parser.y" // lalr1.cc:377
} } //  yaoosl::compiler 
#line 2586 "parser.tab.hh" // lalr1.cc:377




#endif // !YY_YY_PARSER_TAB_HH_INCLUDED
