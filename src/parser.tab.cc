// A Bison parser, made by GNU Bison 3.0.4.

// Skeleton implementation for Bison LALR(1) parsers in C++

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
// //                    "%code top" blocks.
#line 8 "parser.y" // lalr1.cc:397

    #include "tokenizer.hpp"
    #include "cstnode.hpp"
    #include <string>
    #include <vector>

#line 41 "parser.tab.cc" // lalr1.cc:397


// First part of user declarations.

#line 46 "parser.tab.cc" // lalr1.cc:404

# ifndef YY_NULLPTR
#  if defined __cplusplus && 201103L <= __cplusplus
#   define YY_NULLPTR nullptr
#  else
#   define YY_NULLPTR 0
#  endif
# endif

#include "parser.tab.hh"

// User implementation prologue.

#line 60 "parser.tab.cc" // lalr1.cc:412
// Unqualified %code blocks.
#line 24 "parser.y" // lalr1.cc:413

     namespace yaoosl::compiler
     {
          // Return the next token.
          parser::symbol_type yylex (yaoosl::compiler::tokenizer&);
     }

#line 70 "parser.tab.cc" // lalr1.cc:413


#ifndef YY_
# if defined YYENABLE_NLS && YYENABLE_NLS
#  if ENABLE_NLS
#   include <libintl.h> // FIXME: INFRINGES ON USER NAME SPACE.
#   define YY_(msgid) dgettext ("bison-runtime", msgid)
#  endif
# endif
# ifndef YY_
#  define YY_(msgid) msgid
# endif
#endif

#define YYRHSLOC(Rhs, K) ((Rhs)[K].location)
/* YYLLOC_DEFAULT -- Set CURRENT to span from RHS[1] to RHS[N].
   If N is 0, then set CURRENT to the empty location which ends
   the previous symbol: RHS[0] (always defined).  */

# ifndef YYLLOC_DEFAULT
#  define YYLLOC_DEFAULT(Current, Rhs, N)                               \
    do                                                                  \
      if (N)                                                            \
        {                                                               \
          (Current).begin  = YYRHSLOC (Rhs, 1).begin;                   \
          (Current).end    = YYRHSLOC (Rhs, N).end;                     \
        }                                                               \
      else                                                              \
        {                                                               \
          (Current).begin = (Current).end = YYRHSLOC (Rhs, 0).end;      \
        }                                                               \
    while (/*CONSTCOND*/ false)
# endif


// Suppress unused-variable warnings by "using" E.
#define YYUSE(E) ((void) (E))

// Enable debugging if requested.
#if YYDEBUG

// A pseudo ostream that takes yydebug_ into account.
# define YYCDEBUG if (yydebug_) (*yycdebug_)

# define YY_SYMBOL_PRINT(Title, Symbol)         \
  do {                                          \
    if (yydebug_)                               \
    {                                           \
      *yycdebug_ << Title << ' ';               \
      yy_print_ (*yycdebug_, Symbol);           \
      *yycdebug_ << std::endl;                  \
    }                                           \
  } while (false)

# define YY_REDUCE_PRINT(Rule)          \
  do {                                  \
    if (yydebug_)                       \
      yy_reduce_print_ (Rule);          \
  } while (false)

# define YY_STACK_PRINT()               \
  do {                                  \
    if (yydebug_)                       \
      yystack_print_ ();                \
  } while (false)

#else // !YYDEBUG

# define YYCDEBUG if (false) std::cerr
# define YY_SYMBOL_PRINT(Title, Symbol)  YYUSE(Symbol)
# define YY_REDUCE_PRINT(Rule)           static_cast<void>(0)
# define YY_STACK_PRINT()                static_cast<void>(0)

#endif // !YYDEBUG

#define yyerrok         (yyerrstatus_ = 0)
#define yyclearin       (yyla.clear ())

#define YYACCEPT        goto yyacceptlab
#define YYABORT         goto yyabortlab
#define YYERROR         goto yyerrorlab
#define YYRECOVERING()  (!!yyerrstatus_)

#line 6 "parser.y" // lalr1.cc:479
namespace  yaoosl { namespace compiler  {
#line 156 "parser.tab.cc" // lalr1.cc:479

  /* Return YYSTR after stripping away unnecessary quotes and
     backslashes, so that it's suitable for yyerror.  The heuristic is
     that double-quoting is unnecessary unless the string contains an
     apostrophe, a comma, or backslash (other than backslash-backslash).
     YYSTR is taken from yytname.  */
  std::string
  parser::yytnamerr_ (const char *yystr)
  {
    if (*yystr == '"')
      {
        std::string yyr = "";
        char const *yyp = yystr;

        for (;;)
          switch (*++yyp)
            {
            case '\'':
            case ',':
              goto do_not_strip_quotes;

            case '\\':
              if (*++yyp != '\\')
                goto do_not_strip_quotes;
              // Fall through.
            default:
              yyr += *yyp;
              break;

            case '"':
              return yyr;
            }
      do_not_strip_quotes: ;
      }

    return yystr;
  }


  /// Build a parser object.
  parser::parser (yaoosl::compiler::tokenizer &tokenizer_yyarg, yaoosl::compiler::cstnode& result_yyarg, yaoosl::compiler::parser& actual_yyarg, std::string fpath_yyarg)
    :
#if YYDEBUG
      yydebug_ (false),
      yycdebug_ (&std::cerr),
#endif
      tokenizer (tokenizer_yyarg),
      result (result_yyarg),
      actual (actual_yyarg),
      fpath (fpath_yyarg)
  {}

  parser::~parser ()
  {}


  /*---------------.
  | Symbol types.  |
  `---------------*/



  // by_state.
  inline
  parser::by_state::by_state ()
    : state (empty_state)
  {}

  inline
  parser::by_state::by_state (const by_state& other)
    : state (other.state)
  {}

  inline
  void
  parser::by_state::clear ()
  {
    state = empty_state;
  }

  inline
  void
  parser::by_state::move (by_state& that)
  {
    state = that.state;
    that.clear ();
  }

  inline
  parser::by_state::by_state (state_type s)
    : state (s)
  {}

  inline
  parser::symbol_number_type
  parser::by_state::type_get () const
  {
    if (state == empty_state)
      return empty_symbol;
    else
      return yystos_[state];
  }

  inline
  parser::stack_symbol_type::stack_symbol_type ()
  {}


  inline
  parser::stack_symbol_type::stack_symbol_type (state_type s, symbol_type& that)
    : super_type (s, that.location)
  {
      switch (that.type_get ())
    {
      case 80: // using
      case 81: // using_low
      case 82: // filestmnt
      case 83: // filestmnts
      case 84: // classstmnt
      case 85: // classstmnts
      case 86: // codestmnt
      case 87: // codestmnts
      case 88: // type_ident
      case 89: // type
      case 90: // typelist
      case 91: // encpsltn
      case 92: // encpsltn_n_cls
      case 93: // template_def
      case 94: // template_defs
      case 95: // template_use
      case 96: // template
      case 97: // namespace
      case 98: // enum
      case 99: // enum_body
      case 100: // enum_values
      case 101: // enum_value
      case 102: // class
      case 103: // classhead
      case 104: // classbody
      case 105: // mthd
      case 106: // mthd_head
      case 107: // mthd_args
      case 108: // mthd_body
      case 109: // mthd_arglist
      case 110: // mthd_arg
      case 111: // mthdop
      case 112: // mthdop_head
      case 113: // mthdop_args
      case 114: // mthdop_ops1p
      case 115: // mthdop_ops1s
      case 116: // mthdop_ops2
      case 117: // mthdcnst
      case 118: // mthdcnst_head
      case 119: // prop
      case 120: // prop_head
      case 121: // prop_body
      case 122: // prop_get
      case 123: // prop_set
      case 124: // ifelse
      case 125: // for
      case 126: // for_step
      case 127: // for_step_arg
      case 128: // for_each
      case 129: // while
      case 130: // switch
      case 131: // switch_cases
      case 132: // switch_case
      case 133: // case
      case 134: // try
      case 135: // catch
      case 136: // catchlist
      case 137: // finally
      case 138: // trycatch
      case 139: // statement
      case 140: // declaration
      case 141: // scope
      case 142: // val
      case 143: // assignment
      case 144: // explist
      case 145: // exp01
      case 146: // exp02
      case 147: // exp03
      case 148: // exp04
      case 149: // exp05
      case 150: // exp06
      case 151: // exp07
      case 152: // exp08
      case 153: // exp09
      case 154: // exp10
      case 155: // exp11
      case 156: // exp12
      case 157: // arrget
      case 158: // dotnav
      case 159: // call
      case 160: // expp
      case 161: // cval
        value.move< yaoosl::compiler::cstnode > (that.value);
        break;

      case 3: // "public"
      case 4: // "local"
      case 5: // "derived"
      case 6: // "private"
      case 7: // "unbound"
      case 8: // "="
      case 9: // "&&"
      case 10: // "&"
      case 11: // "/"
      case 12: // "*"
      case 13: // "%"
      case 14: // "||"
      case 15: // "|"
      case 16: // "^"
      case 17: // "~"
      case 18: // ":"
      case 19: // "::"
      case 20: // "+"
      case 21: // "++"
      case 22: // "-"
      case 23: // "--"
      case 24: // "<="
      case 25: // "<"
      case 26: // "<<"
      case 27: // "<<<"
      case 28: // ">="
      case 29: // ">"
      case 30: // ">>"
      case 31: // ">>>"
      case 32: // "=="
      case 33: // "~="
      case 34: // "!="
      case 35: // "!"
      case 36: // "{"
      case 37: // "}"
      case 38: // "("
      case 39: // ")"
      case 40: // "["
      case 41: // "]"
      case 42: // ";"
      case 43: // ","
      case 44: // "."
      case 45: // "?"
      case 46: // "=>"
      case 47: // "class"
      case 48: // "get"
      case 49: // "set"
      case 50: // "namespace"
      case 51: // "if"
      case 52: // "for"
      case 53: // "else"
      case 54: // "while"
      case 55: // "do"
      case 56: // "switch"
      case 57: // "case"
      case 58: // "default"
      case 59: // "return"
      case 60: // "throw"
      case 61: // "goto"
      case 62: // "try"
      case 63: // "catch"
      case 64: // "finally"
      case 65: // "operator"
      case 66: // "using"
      case 67: // "enum"
      case 68: // ".."
      case 69: // "true"
      case 70: // "false"
      case 71: // "this"
      case 72: // "new"
      case 73: // "delete"
      case 74: // IDENT
      case 75: // L_STRING
      case 76: // L_NUMBER
      case 77: // L_CHAR
        value.move< yaoosl::compiler::tokenizer::token > (that.value);
        break;

      default:
        break;
    }

    // that is emptied.
    that.type = empty_symbol;
  }

  inline
  parser::stack_symbol_type&
  parser::stack_symbol_type::operator= (const stack_symbol_type& that)
  {
    state = that.state;
      switch (that.type_get ())
    {
      case 80: // using
      case 81: // using_low
      case 82: // filestmnt
      case 83: // filestmnts
      case 84: // classstmnt
      case 85: // classstmnts
      case 86: // codestmnt
      case 87: // codestmnts
      case 88: // type_ident
      case 89: // type
      case 90: // typelist
      case 91: // encpsltn
      case 92: // encpsltn_n_cls
      case 93: // template_def
      case 94: // template_defs
      case 95: // template_use
      case 96: // template
      case 97: // namespace
      case 98: // enum
      case 99: // enum_body
      case 100: // enum_values
      case 101: // enum_value
      case 102: // class
      case 103: // classhead
      case 104: // classbody
      case 105: // mthd
      case 106: // mthd_head
      case 107: // mthd_args
      case 108: // mthd_body
      case 109: // mthd_arglist
      case 110: // mthd_arg
      case 111: // mthdop
      case 112: // mthdop_head
      case 113: // mthdop_args
      case 114: // mthdop_ops1p
      case 115: // mthdop_ops1s
      case 116: // mthdop_ops2
      case 117: // mthdcnst
      case 118: // mthdcnst_head
      case 119: // prop
      case 120: // prop_head
      case 121: // prop_body
      case 122: // prop_get
      case 123: // prop_set
      case 124: // ifelse
      case 125: // for
      case 126: // for_step
      case 127: // for_step_arg
      case 128: // for_each
      case 129: // while
      case 130: // switch
      case 131: // switch_cases
      case 132: // switch_case
      case 133: // case
      case 134: // try
      case 135: // catch
      case 136: // catchlist
      case 137: // finally
      case 138: // trycatch
      case 139: // statement
      case 140: // declaration
      case 141: // scope
      case 142: // val
      case 143: // assignment
      case 144: // explist
      case 145: // exp01
      case 146: // exp02
      case 147: // exp03
      case 148: // exp04
      case 149: // exp05
      case 150: // exp06
      case 151: // exp07
      case 152: // exp08
      case 153: // exp09
      case 154: // exp10
      case 155: // exp11
      case 156: // exp12
      case 157: // arrget
      case 158: // dotnav
      case 159: // call
      case 160: // expp
      case 161: // cval
        value.copy< yaoosl::compiler::cstnode > (that.value);
        break;

      case 3: // "public"
      case 4: // "local"
      case 5: // "derived"
      case 6: // "private"
      case 7: // "unbound"
      case 8: // "="
      case 9: // "&&"
      case 10: // "&"
      case 11: // "/"
      case 12: // "*"
      case 13: // "%"
      case 14: // "||"
      case 15: // "|"
      case 16: // "^"
      case 17: // "~"
      case 18: // ":"
      case 19: // "::"
      case 20: // "+"
      case 21: // "++"
      case 22: // "-"
      case 23: // "--"
      case 24: // "<="
      case 25: // "<"
      case 26: // "<<"
      case 27: // "<<<"
      case 28: // ">="
      case 29: // ">"
      case 30: // ">>"
      case 31: // ">>>"
      case 32: // "=="
      case 33: // "~="
      case 34: // "!="
      case 35: // "!"
      case 36: // "{"
      case 37: // "}"
      case 38: // "("
      case 39: // ")"
      case 40: // "["
      case 41: // "]"
      case 42: // ";"
      case 43: // ","
      case 44: // "."
      case 45: // "?"
      case 46: // "=>"
      case 47: // "class"
      case 48: // "get"
      case 49: // "set"
      case 50: // "namespace"
      case 51: // "if"
      case 52: // "for"
      case 53: // "else"
      case 54: // "while"
      case 55: // "do"
      case 56: // "switch"
      case 57: // "case"
      case 58: // "default"
      case 59: // "return"
      case 60: // "throw"
      case 61: // "goto"
      case 62: // "try"
      case 63: // "catch"
      case 64: // "finally"
      case 65: // "operator"
      case 66: // "using"
      case 67: // "enum"
      case 68: // ".."
      case 69: // "true"
      case 70: // "false"
      case 71: // "this"
      case 72: // "new"
      case 73: // "delete"
      case 74: // IDENT
      case 75: // L_STRING
      case 76: // L_NUMBER
      case 77: // L_CHAR
        value.copy< yaoosl::compiler::tokenizer::token > (that.value);
        break;

      default:
        break;
    }

    location = that.location;
    return *this;
  }


  template <typename Base>
  inline
  void
  parser::yy_destroy_ (const char* yymsg, basic_symbol<Base>& yysym) const
  {
    if (yymsg)
      YY_SYMBOL_PRINT (yymsg, yysym);
  }

#if YYDEBUG
  template <typename Base>
  void
  parser::yy_print_ (std::ostream& yyo,
                                     const basic_symbol<Base>& yysym) const
  {
    std::ostream& yyoutput = yyo;
    YYUSE (yyoutput);
    symbol_number_type yytype = yysym.type_get ();
    // Avoid a (spurious) G++ 4.8 warning about "array subscript is
    // below array bounds".
    if (yysym.empty ())
      std::abort ();
    yyo << (yytype < yyntokens_ ? "token" : "nterm")
        << ' ' << yytname_[yytype] << " ("
        << yysym.location << ": ";
    YYUSE (yytype);
    yyo << ')';
  }
#endif

  inline
  void
  parser::yypush_ (const char* m, state_type s, symbol_type& sym)
  {
    stack_symbol_type t (s, sym);
    yypush_ (m, t);
  }

  inline
  void
  parser::yypush_ (const char* m, stack_symbol_type& s)
  {
    if (m)
      YY_SYMBOL_PRINT (m, s);
    yystack_.push (s);
  }

  inline
  void
  parser::yypop_ (unsigned int n)
  {
    yystack_.pop (n);
  }

#if YYDEBUG
  std::ostream&
  parser::debug_stream () const
  {
    return *yycdebug_;
  }

  void
  parser::set_debug_stream (std::ostream& o)
  {
    yycdebug_ = &o;
  }


  parser::debug_level_type
  parser::debug_level () const
  {
    return yydebug_;
  }

  void
  parser::set_debug_level (debug_level_type l)
  {
    yydebug_ = l;
  }
#endif // YYDEBUG

  inline parser::state_type
  parser::yy_lr_goto_state_ (state_type yystate, int yysym)
  {
    int yyr = yypgoto_[yysym - yyntokens_] + yystate;
    if (0 <= yyr && yyr <= yylast_ && yycheck_[yyr] == yystate)
      return yytable_[yyr];
    else
      return yydefgoto_[yysym - yyntokens_];
  }

  inline bool
  parser::yy_pact_value_is_default_ (int yyvalue)
  {
    return yyvalue == yypact_ninf_;
  }

  inline bool
  parser::yy_table_value_is_error_ (int yyvalue)
  {
    return yyvalue == yytable_ninf_;
  }

  int
  parser::parse ()
  {
    // State.
    int yyn;
    /// Length of the RHS of the rule being reduced.
    int yylen = 0;

    // Error handling.
    int yynerrs_ = 0;
    int yyerrstatus_ = 0;

    /// The lookahead symbol.
    symbol_type yyla;

    /// The locations where the error started and ended.
    stack_symbol_type yyerror_range[3];

    /// The return value of parse ().
    int yyresult;

    // FIXME: This shoud be completely indented.  It is not yet to
    // avoid gratuitous conflicts when merging into the master branch.
    try
      {
    YYCDEBUG << "Starting parse" << std::endl;


    /* Initialize the stack.  The initial state will be set in
       yynewstate, since the latter expects the semantical and the
       location values to have been already stored, initialize these
       stacks with a primary value.  */
    yystack_.clear ();
    yypush_ (YY_NULLPTR, 0, yyla);

    // A new symbol was pushed on the stack.
  yynewstate:
    YYCDEBUG << "Entering state " << yystack_[0].state << std::endl;

    // Accept?
    if (yystack_[0].state == yyfinal_)
      goto yyacceptlab;

    goto yybackup;

    // Backup.
  yybackup:

    // Try to take a decision without lookahead.
    yyn = yypact_[yystack_[0].state];
    if (yy_pact_value_is_default_ (yyn))
      goto yydefault;

    // Read a lookahead token.
    if (yyla.empty ())
      {
        YYCDEBUG << "Reading a token: ";
        try
          {
            symbol_type yylookahead (yylex (tokenizer));
            yyla.move (yylookahead);
          }
        catch (const syntax_error& yyexc)
          {
            error (yyexc);
            goto yyerrlab1;
          }
      }
    YY_SYMBOL_PRINT ("Next token is", yyla);

    /* If the proper action on seeing token YYLA.TYPE is to reduce or
       to detect an error, take that action.  */
    yyn += yyla.type_get ();
    if (yyn < 0 || yylast_ < yyn || yycheck_[yyn] != yyla.type_get ())
      goto yydefault;

    // Reduce or error.
    yyn = yytable_[yyn];
    if (yyn <= 0)
      {
        if (yy_table_value_is_error_ (yyn))
          goto yyerrlab;
        yyn = -yyn;
        goto yyreduce;
      }

    // Count tokens shifted since error; after three, turn off error status.
    if (yyerrstatus_)
      --yyerrstatus_;

    // Shift the lookahead token.
    yypush_ ("Shifting", yyn, yyla);
    goto yynewstate;

  /*-----------------------------------------------------------.
  | yydefault -- do the default action for the current state.  |
  `-----------------------------------------------------------*/
  yydefault:
    yyn = yydefact_[yystack_[0].state];
    if (yyn == 0)
      goto yyerrlab;
    goto yyreduce;

  /*-----------------------------.
  | yyreduce -- Do a reduction.  |
  `-----------------------------*/
  yyreduce:
    yylen = yyr2_[yyn];
    {
      stack_symbol_type yylhs;
      yylhs.state = yy_lr_goto_state_(yystack_[yylen].state, yyr1_[yyn]);
      /* Variants are always initialized to an empty instance of the
         correct type. The default '$$ = $1' action is NOT applied
         when using variants.  */
        switch (yyr1_[yyn])
    {
      case 80: // using
      case 81: // using_low
      case 82: // filestmnt
      case 83: // filestmnts
      case 84: // classstmnt
      case 85: // classstmnts
      case 86: // codestmnt
      case 87: // codestmnts
      case 88: // type_ident
      case 89: // type
      case 90: // typelist
      case 91: // encpsltn
      case 92: // encpsltn_n_cls
      case 93: // template_def
      case 94: // template_defs
      case 95: // template_use
      case 96: // template
      case 97: // namespace
      case 98: // enum
      case 99: // enum_body
      case 100: // enum_values
      case 101: // enum_value
      case 102: // class
      case 103: // classhead
      case 104: // classbody
      case 105: // mthd
      case 106: // mthd_head
      case 107: // mthd_args
      case 108: // mthd_body
      case 109: // mthd_arglist
      case 110: // mthd_arg
      case 111: // mthdop
      case 112: // mthdop_head
      case 113: // mthdop_args
      case 114: // mthdop_ops1p
      case 115: // mthdop_ops1s
      case 116: // mthdop_ops2
      case 117: // mthdcnst
      case 118: // mthdcnst_head
      case 119: // prop
      case 120: // prop_head
      case 121: // prop_body
      case 122: // prop_get
      case 123: // prop_set
      case 124: // ifelse
      case 125: // for
      case 126: // for_step
      case 127: // for_step_arg
      case 128: // for_each
      case 129: // while
      case 130: // switch
      case 131: // switch_cases
      case 132: // switch_case
      case 133: // case
      case 134: // try
      case 135: // catch
      case 136: // catchlist
      case 137: // finally
      case 138: // trycatch
      case 139: // statement
      case 140: // declaration
      case 141: // scope
      case 142: // val
      case 143: // assignment
      case 144: // explist
      case 145: // exp01
      case 146: // exp02
      case 147: // exp03
      case 148: // exp04
      case 149: // exp05
      case 150: // exp06
      case 151: // exp07
      case 152: // exp08
      case 153: // exp09
      case 154: // exp10
      case 155: // exp11
      case 156: // exp12
      case 157: // arrget
      case 158: // dotnav
      case 159: // call
      case 160: // expp
      case 161: // cval
        yylhs.value.build< yaoosl::compiler::cstnode > ();
        break;

      case 3: // "public"
      case 4: // "local"
      case 5: // "derived"
      case 6: // "private"
      case 7: // "unbound"
      case 8: // "="
      case 9: // "&&"
      case 10: // "&"
      case 11: // "/"
      case 12: // "*"
      case 13: // "%"
      case 14: // "||"
      case 15: // "|"
      case 16: // "^"
      case 17: // "~"
      case 18: // ":"
      case 19: // "::"
      case 20: // "+"
      case 21: // "++"
      case 22: // "-"
      case 23: // "--"
      case 24: // "<="
      case 25: // "<"
      case 26: // "<<"
      case 27: // "<<<"
      case 28: // ">="
      case 29: // ">"
      case 30: // ">>"
      case 31: // ">>>"
      case 32: // "=="
      case 33: // "~="
      case 34: // "!="
      case 35: // "!"
      case 36: // "{"
      case 37: // "}"
      case 38: // "("
      case 39: // ")"
      case 40: // "["
      case 41: // "]"
      case 42: // ";"
      case 43: // ","
      case 44: // "."
      case 45: // "?"
      case 46: // "=>"
      case 47: // "class"
      case 48: // "get"
      case 49: // "set"
      case 50: // "namespace"
      case 51: // "if"
      case 52: // "for"
      case 53: // "else"
      case 54: // "while"
      case 55: // "do"
      case 56: // "switch"
      case 57: // "case"
      case 58: // "default"
      case 59: // "return"
      case 60: // "throw"
      case 61: // "goto"
      case 62: // "try"
      case 63: // "catch"
      case 64: // "finally"
      case 65: // "operator"
      case 66: // "using"
      case 67: // "enum"
      case 68: // ".."
      case 69: // "true"
      case 70: // "false"
      case 71: // "this"
      case 72: // "new"
      case 73: // "delete"
      case 74: // IDENT
      case 75: // L_STRING
      case 76: // L_NUMBER
      case 77: // L_CHAR
        yylhs.value.build< yaoosl::compiler::tokenizer::token > ();
        break;

      default:
        break;
    }


      // Compute the default @$.
      {
        slice<stack_symbol_type, stack_type> slice (yystack_, yylen);
        YYLLOC_DEFAULT (yylhs.location, slice, yylen);
      }

      // Perform the reduction.
      YY_REDUCE_PRINT (yyn);
      try
        {
          switch (yyn)
            {
  case 2:
#line 140 "parser.y" // lalr1.cc:859
    { result = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::START, tokenizer.create_token(), {} }; }
#line 1023 "parser.tab.cc" // lalr1.cc:859
    break;

  case 3:
#line 141 "parser.y" // lalr1.cc:859
    { result = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::START, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1029 "parser.tab.cc" // lalr1.cc:859
    break;

  case 4:
#line 143 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 1035 "parser.tab.cc" // lalr1.cc:859
    break;

  case 5:
#line 144 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1041 "parser.tab.cc" // lalr1.cc:859
    break;

  case 6:
#line 146 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1047 "parser.tab.cc" // lalr1.cc:859
    break;

  case 7:
#line 147 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1053 "parser.tab.cc" // lalr1.cc:859
    break;

  case 8:
#line 148 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1059 "parser.tab.cc" // lalr1.cc:859
    break;

  case 9:
#line 150 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1065 "parser.tab.cc" // lalr1.cc:859
    break;

  case 10:
#line 151 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1071 "parser.tab.cc" // lalr1.cc:859
    break;

  case 11:
#line 152 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1077 "parser.tab.cc" // lalr1.cc:859
    break;

  case 12:
#line 153 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1083 "parser.tab.cc" // lalr1.cc:859
    break;

  case 13:
#line 154 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1089 "parser.tab.cc" // lalr1.cc:859
    break;

  case 14:
#line 156 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1095 "parser.tab.cc" // lalr1.cc:859
    break;

  case 15:
#line 157 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1101 "parser.tab.cc" // lalr1.cc:859
    break;

  case 16:
#line 159 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1107 "parser.tab.cc" // lalr1.cc:859
    break;

  case 17:
#line 160 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1113 "parser.tab.cc" // lalr1.cc:859
    break;

  case 18:
#line 161 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1119 "parser.tab.cc" // lalr1.cc:859
    break;

  case 19:
#line 162 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1125 "parser.tab.cc" // lalr1.cc:859
    break;

  case 20:
#line 163 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1131 "parser.tab.cc" // lalr1.cc:859
    break;

  case 21:
#line 164 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1137 "parser.tab.cc" // lalr1.cc:859
    break;

  case 22:
#line 165 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1143 "parser.tab.cc" // lalr1.cc:859
    break;

  case 23:
#line 167 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1149 "parser.tab.cc" // lalr1.cc:859
    break;

  case 24:
#line 168 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1155 "parser.tab.cc" // lalr1.cc:859
    break;

  case 25:
#line 170 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1161 "parser.tab.cc" // lalr1.cc:859
    break;

  case 26:
#line 171 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1167 "parser.tab.cc" // lalr1.cc:859
    break;

  case 27:
#line 172 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1173 "parser.tab.cc" // lalr1.cc:859
    break;

  case 28:
#line 173 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1179 "parser.tab.cc" // lalr1.cc:859
    break;

  case 29:
#line 174 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1185 "parser.tab.cc" // lalr1.cc:859
    break;

  case 30:
#line 175 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1191 "parser.tab.cc" // lalr1.cc:859
    break;

  case 31:
#line 176 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1197 "parser.tab.cc" // lalr1.cc:859
    break;

  case 32:
#line 177 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1203 "parser.tab.cc" // lalr1.cc:859
    break;

  case 33:
#line 178 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1209 "parser.tab.cc" // lalr1.cc:859
    break;

  case 34:
#line 179 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
#line 1215 "parser.tab.cc" // lalr1.cc:859
    break;

  case 35:
#line 180 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
#line 1221 "parser.tab.cc" // lalr1.cc:859
    break;

  case 36:
#line 182 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1227 "parser.tab.cc" // lalr1.cc:859
    break;

  case 37:
#line 183 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1233 "parser.tab.cc" // lalr1.cc:859
    break;

  case 38:
#line 186 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, tokenizer.create_token(), { yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} } } }; }
#line 1239 "parser.tab.cc" // lalr1.cc:859
    break;

  case 39:
#line 187 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }); }
#line 1245 "parser.tab.cc" // lalr1.cc:859
    break;

  case 40:
#line 189 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1251 "parser.tab.cc" // lalr1.cc:859
    break;

  case 41:
#line 190 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1257 "parser.tab.cc" // lalr1.cc:859
    break;

  case 42:
#line 191 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1263 "parser.tab.cc" // lalr1.cc:859
    break;

  case 43:
#line 192 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1269 "parser.tab.cc" // lalr1.cc:859
    break;

  case 44:
#line 194 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1275 "parser.tab.cc" // lalr1.cc:859
    break;

  case 45:
#line 195 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1281 "parser.tab.cc" // lalr1.cc:859
    break;

  case 46:
#line 196 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1287 "parser.tab.cc" // lalr1.cc:859
    break;

  case 47:
#line 199 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 1293 "parser.tab.cc" // lalr1.cc:859
    break;

  case 48:
#line 200 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1299 "parser.tab.cc" // lalr1.cc:859
    break;

  case 49:
#line 201 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1305 "parser.tab.cc" // lalr1.cc:859
    break;

  case 50:
#line 203 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN_N_CLS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1311 "parser.tab.cc" // lalr1.cc:859
    break;

  case 51:
#line 204 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN_N_CLS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1317 "parser.tab.cc" // lalr1.cc:859
    break;

  case 52:
#line 207 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEF, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1323 "parser.tab.cc" // lalr1.cc:859
    break;

  case 53:
#line 208 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEF, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1329 "parser.tab.cc" // lalr1.cc:859
    break;

  case 54:
#line 210 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1335 "parser.tab.cc" // lalr1.cc:859
    break;

  case 55:
#line 211 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1341 "parser.tab.cc" // lalr1.cc:859
    break;

  case 56:
#line 212 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1347 "parser.tab.cc" // lalr1.cc:859
    break;

  case 57:
#line 214 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_USE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1353 "parser.tab.cc" // lalr1.cc:859
    break;

  case 58:
#line 216 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1359 "parser.tab.cc" // lalr1.cc:859
    break;

  case 59:
#line 219 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::NAMESPACE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1365 "parser.tab.cc" // lalr1.cc:859
    break;

  case 60:
#line 222 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1371 "parser.tab.cc" // lalr1.cc:859
    break;

  case 61:
#line 223 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1377 "parser.tab.cc" // lalr1.cc:859
    break;

  case 62:
#line 225 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_BODY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1383 "parser.tab.cc" // lalr1.cc:859
    break;

  case 63:
#line 226 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1389 "parser.tab.cc" // lalr1.cc:859
    break;

  case 64:
#line 228 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1395 "parser.tab.cc" // lalr1.cc:859
    break;

  case 65:
#line 229 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1401 "parser.tab.cc" // lalr1.cc:859
    break;

  case 66:
#line 230 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1407 "parser.tab.cc" // lalr1.cc:859
    break;

  case 67:
#line 231 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1413 "parser.tab.cc" // lalr1.cc:859
    break;

  case 68:
#line 232 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1419 "parser.tab.cc" // lalr1.cc:859
    break;

  case 69:
#line 234 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUE, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1425 "parser.tab.cc" // lalr1.cc:859
    break;

  case 70:
#line 235 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1431 "parser.tab.cc" // lalr1.cc:859
    break;

  case 71:
#line 238 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1437 "parser.tab.cc" // lalr1.cc:859
    break;

  case 72:
#line 239 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1443 "parser.tab.cc" // lalr1.cc:859
    break;

  case 73:
#line 240 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1449 "parser.tab.cc" // lalr1.cc:859
    break;

  case 74:
#line 241 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1455 "parser.tab.cc" // lalr1.cc:859
    break;

  case 75:
#line 243 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSHEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1461 "parser.tab.cc" // lalr1.cc:859
    break;

  case 76:
#line 245 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSBODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1467 "parser.tab.cc" // lalr1.cc:859
    break;

  case 77:
#line 248 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1473 "parser.tab.cc" // lalr1.cc:859
    break;

  case 78:
#line 249 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1479 "parser.tab.cc" // lalr1.cc:859
    break;

  case 79:
#line 251 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1485 "parser.tab.cc" // lalr1.cc:859
    break;

  case 80:
#line 252 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_HEAD_UNBOUND, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1491 "parser.tab.cc" // lalr1.cc:859
    break;

  case 81:
#line 254 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGS, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1497 "parser.tab.cc" // lalr1.cc:859
    break;

  case 82:
#line 255 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGS, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1503 "parser.tab.cc" // lalr1.cc:859
    break;

  case 83:
#line 257 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_BODY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1509 "parser.tab.cc" // lalr1.cc:859
    break;

  case 84:
#line 258 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1515 "parser.tab.cc" // lalr1.cc:859
    break;

  case 85:
#line 260 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1521 "parser.tab.cc" // lalr1.cc:859
    break;

  case 86:
#line 261 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1527 "parser.tab.cc" // lalr1.cc:859
    break;

  case 87:
#line 262 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1533 "parser.tab.cc" // lalr1.cc:859
    break;

  case 88:
#line 264 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1539 "parser.tab.cc" // lalr1.cc:859
    break;

  case 89:
#line 265 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1545 "parser.tab.cc" // lalr1.cc:859
    break;

  case 90:
#line 268 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1551 "parser.tab.cc" // lalr1.cc:859
    break;

  case 91:
#line 269 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1557 "parser.tab.cc" // lalr1.cc:859
    break;

  case 92:
#line 271 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_HEAD, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1563 "parser.tab.cc" // lalr1.cc:859
    break;

  case 93:
#line 273 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1569 "parser.tab.cc" // lalr1.cc:859
    break;

  case 94:
#line 274 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1575 "parser.tab.cc" // lalr1.cc:859
    break;

  case 95:
#line 275 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1581 "parser.tab.cc" // lalr1.cc:859
    break;

  case 96:
#line 276 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[5].value.as< yaoosl::compiler::cstnode > (), yystack_[3].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1587 "parser.tab.cc" // lalr1.cc:859
    break;

  case 97:
#line 278 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1593 "parser.tab.cc" // lalr1.cc:859
    break;

  case 98:
#line 279 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1599 "parser.tab.cc" // lalr1.cc:859
    break;

  case 99:
#line 280 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1605 "parser.tab.cc" // lalr1.cc:859
    break;

  case 100:
#line 281 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1611 "parser.tab.cc" // lalr1.cc:859
    break;

  case 101:
#line 283 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1S, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1617 "parser.tab.cc" // lalr1.cc:859
    break;

  case 102:
#line 284 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1S, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1623 "parser.tab.cc" // lalr1.cc:859
    break;

  case 103:
#line 286 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1629 "parser.tab.cc" // lalr1.cc:859
    break;

  case 104:
#line 287 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1635 "parser.tab.cc" // lalr1.cc:859
    break;

  case 105:
#line 288 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1641 "parser.tab.cc" // lalr1.cc:859
    break;

  case 106:
#line 289 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1647 "parser.tab.cc" // lalr1.cc:859
    break;

  case 107:
#line 290 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1653 "parser.tab.cc" // lalr1.cc:859
    break;

  case 108:
#line 291 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1659 "parser.tab.cc" // lalr1.cc:859
    break;

  case 109:
#line 292 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1665 "parser.tab.cc" // lalr1.cc:859
    break;

  case 110:
#line 293 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1671 "parser.tab.cc" // lalr1.cc:859
    break;

  case 111:
#line 294 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1677 "parser.tab.cc" // lalr1.cc:859
    break;

  case 112:
#line 295 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1683 "parser.tab.cc" // lalr1.cc:859
    break;

  case 113:
#line 296 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1689 "parser.tab.cc" // lalr1.cc:859
    break;

  case 114:
#line 297 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1695 "parser.tab.cc" // lalr1.cc:859
    break;

  case 115:
#line 298 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1701 "parser.tab.cc" // lalr1.cc:859
    break;

  case 116:
#line 299 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1707 "parser.tab.cc" // lalr1.cc:859
    break;

  case 117:
#line 300 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1713 "parser.tab.cc" // lalr1.cc:859
    break;

  case 118:
#line 301 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1719 "parser.tab.cc" // lalr1.cc:859
    break;

  case 119:
#line 302 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1725 "parser.tab.cc" // lalr1.cc:859
    break;

  case 120:
#line 303 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1731 "parser.tab.cc" // lalr1.cc:859
    break;

  case 121:
#line 304 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1737 "parser.tab.cc" // lalr1.cc:859
    break;

  case 122:
#line 305 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1743 "parser.tab.cc" // lalr1.cc:859
    break;

  case 123:
#line 306 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1749 "parser.tab.cc" // lalr1.cc:859
    break;

  case 124:
#line 309 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDCNST, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1755 "parser.tab.cc" // lalr1.cc:859
    break;

  case 125:
#line 311 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDCNST_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1761 "parser.tab.cc" // lalr1.cc:859
    break;

  case 126:
#line 312 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDDST_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1767 "parser.tab.cc" // lalr1.cc:859
    break;

  case 127:
#line 315 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1773 "parser.tab.cc" // lalr1.cc:859
    break;

  case 128:
#line 317 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1779 "parser.tab.cc" // lalr1.cc:859
    break;

  case 129:
#line 318 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_HEAD_UNBOUND, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1785 "parser.tab.cc" // lalr1.cc:859
    break;

  case 130:
#line 320 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1791 "parser.tab.cc" // lalr1.cc:859
    break;

  case 131:
#line 321 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1797 "parser.tab.cc" // lalr1.cc:859
    break;

  case 132:
#line 322 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1803 "parser.tab.cc" // lalr1.cc:859
    break;

  case 133:
#line 323 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { {}, yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1809 "parser.tab.cc" // lalr1.cc:859
    break;

  case 134:
#line 324 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { {}, {} } }; }
#line 1815 "parser.tab.cc" // lalr1.cc:859
    break;

  case 135:
#line 326 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_GET, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { { yaoosl::compiler::cstnode::PROP_GET, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), {} }, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1821 "parser.tab.cc" // lalr1.cc:859
    break;

  case 136:
#line 327 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_GET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1827 "parser.tab.cc" // lalr1.cc:859
    break;

  case 137:
#line 329 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1833 "parser.tab.cc" // lalr1.cc:859
    break;

  case 138:
#line 332 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::IFELSE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1839 "parser.tab.cc" // lalr1.cc:859
    break;

  case 139:
#line 333 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::IFELSE, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1845 "parser.tab.cc" // lalr1.cc:859
    break;

  case 140:
#line 336 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1851 "parser.tab.cc" // lalr1.cc:859
    break;

  case 141:
#line 337 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1857 "parser.tab.cc" // lalr1.cc:859
    break;

  case 142:
#line 339 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1863 "parser.tab.cc" // lalr1.cc:859
    break;

  case 143:
#line 340 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1869 "parser.tab.cc" // lalr1.cc:859
    break;

  case 144:
#line 341 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1875 "parser.tab.cc" // lalr1.cc:859
    break;

  case 145:
#line 342 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, {} } }; }
#line 1881 "parser.tab.cc" // lalr1.cc:859
    break;

  case 146:
#line 343 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1887 "parser.tab.cc" // lalr1.cc:859
    break;

  case 147:
#line 344 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1893 "parser.tab.cc" // lalr1.cc:859
    break;

  case 148:
#line 345 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1899 "parser.tab.cc" // lalr1.cc:859
    break;

  case 149:
#line 346 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, {} } }; }
#line 1905 "parser.tab.cc" // lalr1.cc:859
    break;

  case 150:
#line 348 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1911 "parser.tab.cc" // lalr1.cc:859
    break;

  case 151:
#line 349 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1917 "parser.tab.cc" // lalr1.cc:859
    break;

  case 152:
#line 350 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1923 "parser.tab.cc" // lalr1.cc:859
    break;

  case 153:
#line 352 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_EACH, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1929 "parser.tab.cc" // lalr1.cc:859
    break;

  case 154:
#line 353 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_EACH, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1935 "parser.tab.cc" // lalr1.cc:859
    break;

  case 155:
#line 356 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::WHILE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1941 "parser.tab.cc" // lalr1.cc:859
    break;

  case 156:
#line 357 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DO_WHILE, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[4].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1947 "parser.tab.cc" // lalr1.cc:859
    break;

  case 157:
#line 360 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1953 "parser.tab.cc" // lalr1.cc:859
    break;

  case 158:
#line 362 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASES, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1959 "parser.tab.cc" // lalr1.cc:859
    break;

  case 159:
#line 363 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[1].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1965 "parser.tab.cc" // lalr1.cc:859
    break;

  case 160:
#line 365 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1971 "parser.tab.cc" // lalr1.cc:859
    break;

  case 161:
#line 366 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1977 "parser.tab.cc" // lalr1.cc:859
    break;

  case 162:
#line 368 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CASE, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1983 "parser.tab.cc" // lalr1.cc:859
    break;

  case 163:
#line 369 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CASE, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 1989 "parser.tab.cc" // lalr1.cc:859
    break;

  case 164:
#line 372 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1995 "parser.tab.cc" // lalr1.cc:859
    break;

  case 165:
#line 374 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCH, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2001 "parser.tab.cc" // lalr1.cc:859
    break;

  case 166:
#line 375 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCH, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2007 "parser.tab.cc" // lalr1.cc:859
    break;

  case 167:
#line 377 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCHLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2013 "parser.tab.cc" // lalr1.cc:859
    break;

  case 168:
#line 378 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCHLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2019 "parser.tab.cc" // lalr1.cc:859
    break;

  case 169:
#line 380 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FINALLY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2025 "parser.tab.cc" // lalr1.cc:859
    break;

  case 170:
#line 382 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2031 "parser.tab.cc" // lalr1.cc:859
    break;

  case 171:
#line 383 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 2037 "parser.tab.cc" // lalr1.cc:859
    break;

  case 172:
#line 384 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2043 "parser.tab.cc" // lalr1.cc:859
    break;

  case 173:
#line 387 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2049 "parser.tab.cc" // lalr1.cc:859
    break;

  case 174:
#line 388 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2055 "parser.tab.cc" // lalr1.cc:859
    break;

  case 175:
#line 389 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2061 "parser.tab.cc" // lalr1.cc:859
    break;

  case 176:
#line 390 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2067 "parser.tab.cc" // lalr1.cc:859
    break;

  case 177:
#line 391 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2073 "parser.tab.cc" // lalr1.cc:859
    break;

  case 178:
#line 392 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} } } }; }
#line 2079 "parser.tab.cc" // lalr1.cc:859
    break;

  case 179:
#line 393 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2085 "parser.tab.cc" // lalr1.cc:859
    break;

  case 180:
#line 395 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DECLARATION, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2091 "parser.tab.cc" // lalr1.cc:859
    break;

  case 181:
#line 397 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SCOPE, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2097 "parser.tab.cc" // lalr1.cc:859
    break;

  case 182:
#line 398 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SCOPE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2103 "parser.tab.cc" // lalr1.cc:859
    break;

  case 183:
#line 400 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::VAL, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2109 "parser.tab.cc" // lalr1.cc:859
    break;

  case 184:
#line 401 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::VAL, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2115 "parser.tab.cc" // lalr1.cc:859
    break;

  case 185:
#line 403 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ASSIGNMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2121 "parser.tab.cc" // lalr1.cc:859
    break;

  case 186:
#line 405 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::EXPLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2127 "parser.tab.cc" // lalr1.cc:859
    break;

  case 187:
#line 406 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::EXPLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2133 "parser.tab.cc" // lalr1.cc:859
    break;

  case 188:
#line 407 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 2139 "parser.tab.cc" // lalr1.cc:859
    break;

  case 189:
#line 409 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2145 "parser.tab.cc" // lalr1.cc:859
    break;

  case 190:
#line 410 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TERNARY_OPERATOR, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2151 "parser.tab.cc" // lalr1.cc:859
    break;

  case 191:
#line 412 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2157 "parser.tab.cc" // lalr1.cc:859
    break;

  case 192:
#line 413 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2163 "parser.tab.cc" // lalr1.cc:859
    break;

  case 193:
#line 415 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2169 "parser.tab.cc" // lalr1.cc:859
    break;

  case 194:
#line 416 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2175 "parser.tab.cc" // lalr1.cc:859
    break;

  case 195:
#line 418 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2181 "parser.tab.cc" // lalr1.cc:859
    break;

  case 196:
#line 419 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2187 "parser.tab.cc" // lalr1.cc:859
    break;

  case 197:
#line 420 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2193 "parser.tab.cc" // lalr1.cc:859
    break;

  case 198:
#line 421 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2199 "parser.tab.cc" // lalr1.cc:859
    break;

  case 199:
#line 423 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2205 "parser.tab.cc" // lalr1.cc:859
    break;

  case 200:
#line 424 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2211 "parser.tab.cc" // lalr1.cc:859
    break;

  case 201:
#line 425 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2217 "parser.tab.cc" // lalr1.cc:859
    break;

  case 202:
#line 426 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2223 "parser.tab.cc" // lalr1.cc:859
    break;

  case 203:
#line 427 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2229 "parser.tab.cc" // lalr1.cc:859
    break;

  case 204:
#line 429 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2235 "parser.tab.cc" // lalr1.cc:859
    break;

  case 205:
#line 430 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2241 "parser.tab.cc" // lalr1.cc:859
    break;

  case 206:
#line 431 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2247 "parser.tab.cc" // lalr1.cc:859
    break;

  case 207:
#line 433 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2253 "parser.tab.cc" // lalr1.cc:859
    break;

  case 208:
#line 434 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2259 "parser.tab.cc" // lalr1.cc:859
    break;

  case 209:
#line 435 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2265 "parser.tab.cc" // lalr1.cc:859
    break;

  case 210:
#line 436 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2271 "parser.tab.cc" // lalr1.cc:859
    break;

  case 211:
#line 438 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2277 "parser.tab.cc" // lalr1.cc:859
    break;

  case 212:
#line 439 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2283 "parser.tab.cc" // lalr1.cc:859
    break;

  case 213:
#line 440 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2289 "parser.tab.cc" // lalr1.cc:859
    break;

  case 214:
#line 441 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2295 "parser.tab.cc" // lalr1.cc:859
    break;

  case 215:
#line 442 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2301 "parser.tab.cc" // lalr1.cc:859
    break;

  case 216:
#line 444 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2307 "parser.tab.cc" // lalr1.cc:859
    break;

  case 217:
#line 445 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2313 "parser.tab.cc" // lalr1.cc:859
    break;

  case 218:
#line 446 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2319 "parser.tab.cc" // lalr1.cc:859
    break;

  case 219:
#line 448 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2325 "parser.tab.cc" // lalr1.cc:859
    break;

  case 220:
#line 449 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2331 "parser.tab.cc" // lalr1.cc:859
    break;

  case 221:
#line 451 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2337 "parser.tab.cc" // lalr1.cc:859
    break;

  case 222:
#line 452 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::UNARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2343 "parser.tab.cc" // lalr1.cc:859
    break;

  case 223:
#line 453 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::UNARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2349 "parser.tab.cc" // lalr1.cc:859
    break;

  case 224:
#line 454 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[1].value.as< yaoosl::compiler::cstnode > (); }
#line 2355 "parser.tab.cc" // lalr1.cc:859
    break;

  case 225:
#line 456 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2361 "parser.tab.cc" // lalr1.cc:859
    break;

  case 226:
#line 457 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2367 "parser.tab.cc" // lalr1.cc:859
    break;

  case 227:
#line 458 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2373 "parser.tab.cc" // lalr1.cc:859
    break;

  case 228:
#line 459 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2379 "parser.tab.cc" // lalr1.cc:859
    break;

  case 229:
#line 461 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ARRGET, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2385 "parser.tab.cc" // lalr1.cc:859
    break;

  case 230:
#line 463 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DOTNAV, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2391 "parser.tab.cc" // lalr1.cc:859
    break;

  case 231:
#line 465 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CALL, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2397 "parser.tab.cc" // lalr1.cc:859
    break;

  case 232:
#line 466 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CALL, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2403 "parser.tab.cc" // lalr1.cc:859
    break;

  case 233:
#line 468 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2409 "parser.tab.cc" // lalr1.cc:859
    break;

  case 234:
#line 469 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::THIS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2415 "parser.tab.cc" // lalr1.cc:859
    break;

  case 235:
#line 470 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::NEW,  yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2421 "parser.tab.cc" // lalr1.cc:859
    break;

  case 236:
#line 471 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2427 "parser.tab.cc" // lalr1.cc:859
    break;

  case 237:
#line 473 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2433 "parser.tab.cc" // lalr1.cc:859
    break;

  case 238:
#line 474 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2439 "parser.tab.cc" // lalr1.cc:859
    break;

  case 239:
#line 475 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2445 "parser.tab.cc" // lalr1.cc:859
    break;

  case 240:
#line 476 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2451 "parser.tab.cc" // lalr1.cc:859
    break;

  case 241:
#line 477 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2457 "parser.tab.cc" // lalr1.cc:859
    break;

  case 242:
#line 478 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2463 "parser.tab.cc" // lalr1.cc:859
    break;


#line 2467 "parser.tab.cc" // lalr1.cc:859
            default:
              break;
            }
        }
      catch (const syntax_error& yyexc)
        {
          error (yyexc);
          YYERROR;
        }
      YY_SYMBOL_PRINT ("-> $$ =", yylhs);
      yypop_ (yylen);
      yylen = 0;
      YY_STACK_PRINT ();

      // Shift the result of the reduction.
      yypush_ (YY_NULLPTR, yylhs);
    }
    goto yynewstate;

  /*--------------------------------------.
  | yyerrlab -- here on detecting error.  |
  `--------------------------------------*/
  yyerrlab:
    // If not already recovering from an error, report this error.
    if (!yyerrstatus_)
      {
        ++yynerrs_;
        error (yyla.location, yysyntax_error_ (yystack_[0].state, yyla));
      }


    yyerror_range[1].location = yyla.location;
    if (yyerrstatus_ == 3)
      {
        /* If just tried and failed to reuse lookahead token after an
           error, discard it.  */

        // Return failure if at end of input.
        if (yyla.type_get () == yyeof_)
          YYABORT;
        else if (!yyla.empty ())
          {
            yy_destroy_ ("Error: discarding", yyla);
            yyla.clear ();
          }
      }

    // Else will try to reuse lookahead token after shifting the error token.
    goto yyerrlab1;


  /*---------------------------------------------------.
  | yyerrorlab -- error raised explicitly by YYERROR.  |
  `---------------------------------------------------*/
  yyerrorlab:

    /* Pacify compilers like GCC when the user code never invokes
       YYERROR and the label yyerrorlab therefore never appears in user
       code.  */
    if (false)
      goto yyerrorlab;
    yyerror_range[1].location = yystack_[yylen - 1].location;
    /* Do not reclaim the symbols of the rule whose action triggered
       this YYERROR.  */
    yypop_ (yylen);
    yylen = 0;
    goto yyerrlab1;

  /*-------------------------------------------------------------.
  | yyerrlab1 -- common code for both syntax error and YYERROR.  |
  `-------------------------------------------------------------*/
  yyerrlab1:
    yyerrstatus_ = 3;   // Each real token shifted decrements this.
    {
      stack_symbol_type error_token;
      for (;;)
        {
          yyn = yypact_[yystack_[0].state];
          if (!yy_pact_value_is_default_ (yyn))
            {
              yyn += yyterror_;
              if (0 <= yyn && yyn <= yylast_ && yycheck_[yyn] == yyterror_)
                {
                  yyn = yytable_[yyn];
                  if (0 < yyn)
                    break;
                }
            }

          // Pop the current state because it cannot handle the error token.
          if (yystack_.size () == 1)
            YYABORT;

          yyerror_range[1].location = yystack_[0].location;
          yy_destroy_ ("Error: popping", yystack_[0]);
          yypop_ ();
          YY_STACK_PRINT ();
        }

      yyerror_range[2].location = yyla.location;
      YYLLOC_DEFAULT (error_token.location, yyerror_range, 2);

      // Shift the error token.
      error_token.state = yyn;
      yypush_ ("Shifting", error_token);
    }
    goto yynewstate;

    // Accept.
  yyacceptlab:
    yyresult = 0;
    goto yyreturn;

    // Abort.
  yyabortlab:
    yyresult = 1;
    goto yyreturn;

  yyreturn:
    if (!yyla.empty ())
      yy_destroy_ ("Cleanup: discarding lookahead", yyla);

    /* Do not reclaim the symbols of the rule whose action triggered
       this YYABORT or YYACCEPT.  */
    yypop_ (yylen);
    while (1 < yystack_.size ())
      {
        yy_destroy_ ("Cleanup: popping", yystack_[0]);
        yypop_ ();
      }

    return yyresult;
  }
    catch (...)
      {
        YYCDEBUG << "Exception caught: cleaning lookahead and stack"
                 << std::endl;
        // Do not try to display the values of the reclaimed symbols,
        // as their printer might throw an exception.
        if (!yyla.empty ())
          yy_destroy_ (YY_NULLPTR, yyla);

        while (1 < yystack_.size ())
          {
            yy_destroy_ (YY_NULLPTR, yystack_[0]);
            yypop_ ();
          }
        throw;
      }
  }

  void
  parser::error (const syntax_error& yyexc)
  {
    error (yyexc.location, yyexc.what());
  }

  // Generate an error message.
  std::string
  parser::yysyntax_error_ (state_type yystate, const symbol_type& yyla) const
  {
    // Number of reported tokens (one for the "unexpected", one per
    // "expected").
    size_t yycount = 0;
    // Its maximum.
    enum { YYERROR_VERBOSE_ARGS_MAXIMUM = 5 };
    // Arguments of yyformat.
    char const *yyarg[YYERROR_VERBOSE_ARGS_MAXIMUM];

    /* There are many possibilities here to consider:
       - If this state is a consistent state with a default action, then
         the only way this function was invoked is if the default action
         is an error action.  In that case, don't check for expected
         tokens because there are none.
       - The only way there can be no lookahead present (in yyla) is
         if this state is a consistent state with a default action.
         Thus, detecting the absence of a lookahead is sufficient to
         determine that there is no unexpected or expected token to
         report.  In that case, just report a simple "syntax error".
       - Don't assume there isn't a lookahead just because this state is
         a consistent state with a default action.  There might have
         been a previous inconsistent state, consistent state with a
         non-default action, or user semantic action that manipulated
         yyla.  (However, yyla is currently not documented for users.)
       - Of course, the expected token list depends on states to have
         correct lookahead information, and it depends on the parser not
         to perform extra reductions after fetching a lookahead from the
         scanner and before detecting a syntax error.  Thus, state
         merging (from LALR or IELR) and default reductions corrupt the
         expected token list.  However, the list is correct for
         canonical LR with one exception: it will still contain any
         token that will not be accepted due to an error action in a
         later state.
    */
    if (!yyla.empty ())
      {
        int yytoken = yyla.type_get ();
        yyarg[yycount++] = yytname_[yytoken];
        int yyn = yypact_[yystate];
        if (!yy_pact_value_is_default_ (yyn))
          {
            /* Start YYX at -YYN if negative to avoid negative indexes in
               YYCHECK.  In other words, skip the first -YYN actions for
               this state because they are default actions.  */
            int yyxbegin = yyn < 0 ? -yyn : 0;
            // Stay within bounds of both yycheck and yytname.
            int yychecklim = yylast_ - yyn + 1;
            int yyxend = yychecklim < yyntokens_ ? yychecklim : yyntokens_;
            for (int yyx = yyxbegin; yyx < yyxend; ++yyx)
              if (yycheck_[yyx + yyn] == yyx && yyx != yyterror_
                  && !yy_table_value_is_error_ (yytable_[yyx + yyn]))
                {
                  if (yycount == YYERROR_VERBOSE_ARGS_MAXIMUM)
                    {
                      yycount = 1;
                      break;
                    }
                  else
                    yyarg[yycount++] = yytname_[yyx];
                }
          }
      }

    char const* yyformat = YY_NULLPTR;
    switch (yycount)
      {
#define YYCASE_(N, S)                         \
        case N:                               \
          yyformat = S;                       \
        break
        YYCASE_(0, YY_("syntax error"));
        YYCASE_(1, YY_("syntax error, unexpected %s"));
        YYCASE_(2, YY_("syntax error, unexpected %s, expecting %s"));
        YYCASE_(3, YY_("syntax error, unexpected %s, expecting %s or %s"));
        YYCASE_(4, YY_("syntax error, unexpected %s, expecting %s or %s or %s"));
        YYCASE_(5, YY_("syntax error, unexpected %s, expecting %s or %s or %s or %s"));
#undef YYCASE_
      }

    std::string yyres;
    // Argument number.
    size_t yyi = 0;
    for (char const* yyp = yyformat; *yyp; ++yyp)
      if (yyp[0] == '%' && yyp[1] == 's' && yyi < yycount)
        {
          yyres += yytnamerr_ (yyarg[yyi++]);
          ++yyp;
        }
      else
        yyres += *yyp;
    return yyres;
  }


  const short int parser::yypact_ninf_ = -302;

  const short int parser::yytable_ninf_ = -161;

  const short int
  parser::yypact_[] =
  {
      47,  -302,  -302,  -302,  -302,   265,   -54,   107,    42,  -302,
      53,  -302,    47,     8,    20,  -302,  -302,  -302,   133,  -302,
     188,    10,  -302,  -302,   134,    10,    10,    10,  -302,    66,
    -302,   -54,    82,   195,   141,    74,    10,    10,    98,    94,
    -302,    44,   194,   201,   150,   173,    47,   217,   235,   253,
    -302,   195,  -302,    10,  -302,  -302,    10,   -26,  -302,   260,
     186,   244,   280,   123,   265,   294,  -302,  -302,    30,    12,
    -302,  -302,  -302,  -302,   100,  -302,   194,  -302,   209,    10,
    -302,  -302,   296,   146,   329,   201,   352,  -302,  -302,  -302,
     341,  -302,  -302,  -302,  -302,   139,   347,  -302,   378,   207,
     144,  -302,    10,  -302,   383,  -302,  -302,    10,    10,  -302,
    -302,  -302,   354,   319,  -302,  -302,  -302,  -302,  -302,  -302,
     646,   125,   201,   335,   336,   201,   215,  -302,  -302,   186,
     394,  -302,    10,  -302,  -302,  -302,   305,   305,   398,  -302,
     305,  -302,   367,   371,   372,   490,   381,   305,   305,   104,
     490,  -302,  -302,  -302,    10,   305,  -302,  -302,  -302,  -302,
    -302,   444,   342,  -302,  -302,  -302,  -302,   221,  -302,   388,
    -302,  -302,   389,  -302,  -302,    71,   411,   223,   237,   276,
     303,   256,   318,   422,  -302,    99,  -302,  -302,  -302,  -302,
    -302,  -302,  -302,  -302,    57,  -302,   363,   363,  -302,  -302,
    -302,    57,  -302,   364,   212,  -302,  -302,  -302,  -302,  -302,
    -302,  -302,  -302,  -302,  -302,  -302,  -302,  -302,  -302,  -302,
    -302,  -302,  -302,  -302,  -302,  -302,   401,   403,   201,  -302,
     404,   405,  -302,   268,   201,   118,   179,  -302,    57,  -302,
    -302,   190,  -302,  -302,   407,   408,   305,   204,   305,   392,
     305,  -302,  -302,    57,  -302,  -302,  -302,  -302,  -302,  -302,
    -302,  -302,  -302,   536,   490,   385,   387,  -302,  -302,  -302,
     305,   305,   305,   305,   305,   305,   305,   305,   305,   305,
     305,   305,   305,   305,   305,   305,   305,   305,   305,   305,
     305,   305,   305,   565,   305,   382,  -302,  -302,  -302,  -302,
    -302,   216,    10,    10,  -302,    10,    10,   391,  -302,  -302,
    -302,   418,  -302,   425,  -302,  -302,  -302,   427,   551,   437,
     435,   439,   465,   441,   446,   449,   450,  -302,   305,  -302,
    -302,  -302,  -302,   411,   470,   223,   237,   237,   237,   276,
     276,   276,   276,   303,   303,   256,   256,   256,   318,   318,
     318,   318,   422,   422,  -302,  -302,  -302,   447,   192,   451,
    -302,   454,   458,   455,   463,   469,  -302,  -302,   490,   305,
     480,   490,   612,   490,   305,   305,   490,   305,   461,   472,
     305,  -302,  -302,   305,  -302,  -302,    10,  -302,  -302,   201,
     459,  -302,   305,  -302,   305,   481,  -302,   456,  -302,  -302,
     488,   263,   490,  -302,  -302,   491,  -302,   490,  -302,  -302,
     305,   305,  -302,   126,  -302,   511,  -302,  -302,  -302,  -302,
    -302,  -302,  -302,   290,  -302
  };

  const unsigned char
  parser::yydefact_[] =
  {
       2,    50,    51,    48,    49,     0,     0,     0,     0,    12,
       4,    14,     3,     0,    47,    10,    13,     9,     0,    11,
       0,     0,    47,    38,     0,     0,     0,     0,     1,     0,
      15,     0,     0,    43,     0,     0,     0,     0,     0,     0,
      71,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       5,    41,    75,     0,    40,    79,     0,     0,    60,    44,
       0,     0,    54,     0,     0,     0,    21,    23,     0,     0,
      22,    19,    16,    17,     0,    18,     0,    20,     0,     0,
      73,    81,     0,     0,    85,     0,     0,    77,    80,    39,
       0,     8,     6,     7,    42,     0,     0,    62,    69,     0,
      64,    45,     0,    72,    52,    55,    58,     0,     0,   126,
      76,    24,    38,     0,    98,   100,   101,    99,   102,    97,
       0,     0,     0,     0,     0,     0,     0,   134,   127,     0,
      88,    82,     0,    86,    78,    35,     0,     0,     0,    83,
       0,    34,     0,     0,     0,     0,     0,   173,   175,     0,
       0,   240,   241,   234,     0,     0,   238,   237,   239,    26,
      36,     0,   242,    25,    27,    29,    30,     0,    28,     0,
     236,    32,     0,   184,   183,   189,   191,   193,   195,   199,
     204,   207,   211,   216,   219,   221,   228,   227,   226,   225,
     233,    59,    57,    61,     0,    63,     0,     0,    66,    65,
      46,     0,    56,    92,    79,   119,   118,   106,   105,   123,
     121,   120,   122,   103,   104,   112,   111,   113,   114,   108,
     107,   109,   110,   116,   115,   117,     0,     0,     0,    90,
       0,     0,   124,     0,     0,     0,     0,    74,     0,    87,
     223,   221,   222,   181,     0,     0,     0,     0,     0,     0,
       0,   174,   176,     0,   163,   178,   179,   164,   235,   177,
      84,    37,   180,     0,     0,   167,   171,   172,    31,    33,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,   242,    70,    68,    67,
      53,    80,     0,     0,    91,     0,     0,     0,   136,   137,
     131,     0,   133,     0,    89,   182,   224,     0,     0,     0,
       0,     0,   236,   150,     0,     0,     0,   162,     0,   165,
     169,   168,   170,   192,     0,   194,   196,   197,   198,   201,
     200,   203,   202,   205,   206,   209,   208,   210,   212,   213,
     214,   215,   218,   217,   220,   185,   232,   186,     0,     0,
     230,     0,     0,     0,     0,     0,   130,   132,     0,   149,
       0,     0,     0,     0,     0,   151,     0,     0,     0,   236,
       0,   187,   231,     0,   229,    93,     0,    94,    95,     0,
     138,   148,   147,   140,   145,     0,   141,   153,   152,   155,
       0,     0,     0,   190,   188,     0,   135,     0,   146,   144,
     143,     0,   156,     0,   158,     0,   166,    96,   139,   142,
     154,   157,   159,     0,   161
  };

  const short int
  parser::yypgoto_[] =
  {
    -302,  -302,    32,  -302,    52,  -302,   466,  -302,  -135,  -302,
      15,   -13,   -34,    11,    17,   424,  -302,   482,    40,  -302,
       1,   440,  -302,   158,    68,  -302,   -30,    70,  -302,    48,
     -84,  -302,  -126,  -302,  -302,   414,  -302,   419,  -302,  -302,
    -302,  -302,  -302,  -302,   302,   308,  -302,  -302,  -302,  -301,
    -302,  -302,  -302,  -302,   127,   399,  -302,  -302,   282,   288,
    -302,  -302,  -240,  -302,   -75,  -302,  -302,  -269,  -302,   287,
     286,    56,    34,    77,    55,   110,    95,  -132,   -79,  -302,
    -302,  -302,  -302,  -176
  };

  const short int
  parser::yydefgoto_[] =
  {
      -1,     8,   159,    10,    11,    12,    67,    68,   160,   161,
      33,   162,    60,    13,    14,    62,    63,    54,    39,    15,
      16,    58,    99,   100,    17,    18,    40,    19,    20,    43,
      87,    83,    84,    73,    74,   122,   123,   124,   227,    75,
      76,    77,    78,   128,   235,   236,   163,   164,   319,   320,
     321,   165,   166,   413,   414,   415,   167,   265,   266,   267,
     168,   169,   170,   171,   172,   173,   358,   174,   175,   176,
     177,   178,   179,   180,   181,   182,   183,   184,   185,   186,
     187,   188,   189,   190
  };

  const short int
  parser::yytable_[] =
  {
      34,   134,   334,   244,   240,   242,   239,   322,    44,    80,
     249,    97,    47,    48,    49,   257,    21,   370,   297,    95,
      23,    24,    22,    59,    61,   300,   261,    31,    82,    31,
     103,    31,     9,     1,     2,     3,     4,    64,   229,    70,
      59,   232,    28,    96,     9,   129,    51,    65,    98,    69,
       1,     2,     3,     4,     5,    32,   113,   241,   241,    32,
      42,    29,   314,    31,    30,   245,    59,   110,   391,    70,
      66,   395,   251,   252,   398,   108,    31,   327,     9,    69,
     259,    22,    23,    81,    23,   270,   112,    35,   379,   200,
      85,   408,    56,   409,    61,   203,     7,     6,    90,   237,
      66,     1,     2,     3,     4,    64,    71,   292,    72,   419,
      57,   403,    79,     7,   121,    65,   271,   114,    23,    82,
     115,   116,   117,   118,   125,    37,   151,   152,   329,   330,
      38,    23,   156,   157,   158,   119,    71,   293,    72,   294,
      50,   258,   114,   295,   304,   115,   116,   117,   118,   308,
     309,    36,   106,    45,    25,   310,    52,    26,    37,   354,
     119,   253,   254,   421,     7,   120,   107,   234,   192,    38,
      46,   317,   323,   324,    27,   326,   361,   362,   255,   363,
     364,   296,   102,   253,   254,   131,   198,   199,   296,   132,
     120,   241,   241,   241,   241,   241,   241,   241,   241,   241,
     241,   241,   241,   241,   241,   241,   241,   241,   241,   241,
     241,   241,   241,    37,    45,    55,   312,   355,   357,   359,
      53,   136,    38,    31,    88,   296,    41,   233,   293,   102,
     294,   382,    41,   390,   295,   383,   393,    86,   396,   137,
     296,   399,   140,   323,   195,   126,   318,    89,  -128,   196,
     197,   127,  -129,   245,  -128,   273,   274,   275,  -129,    91,
     405,   276,   277,   233,   234,   278,   279,   416,     1,     2,
       3,     4,   418,   151,   152,   153,   154,    92,    23,   156,
     157,   158,   285,   286,   263,   264,   287,   288,   424,    82,
      82,   135,    82,    82,   323,    93,   280,   323,   281,   397,
     323,   241,   400,   101,    86,   406,   307,   136,   404,    31,
     339,   340,   341,   342,   282,   283,   284,   323,   104,   323,
     253,   254,   136,   105,    31,   137,   138,  -160,   140,   336,
     337,   338,   141,   289,   290,   323,   420,   345,   346,   347,
     137,   142,   143,   140,   144,   145,   146,  -160,  -160,   147,
     148,   149,   150,   135,   298,   299,     7,   343,   344,   151,
     152,   153,   154,   155,    23,   156,   157,   158,   109,   136,
     130,    31,   133,    82,   151,   152,   153,   154,   191,    23,
     156,   157,   158,    57,   352,   353,   194,   137,   138,   139,
     140,   201,  -125,   204,   141,   348,   349,   350,   351,   135,
     230,   231,   238,   142,   143,   246,   144,   145,   146,   247,
     248,   147,   148,   149,   150,   136,   262,    31,     7,   250,
     272,   151,   152,   153,   154,   155,    23,   156,   157,   158,
     268,   269,   291,   137,   138,   243,   140,    98,   301,   302,
     141,   303,   305,   306,   315,   135,   325,   316,   263,   142,
     143,   264,   144,   145,   146,   366,   360,   147,   148,   149,
     150,   136,   367,    31,     7,   365,   368,   151,   152,   153,
     154,   155,    23,   156,   157,   158,   371,   372,   373,   137,
     138,   260,   140,   374,   375,   376,   141,   377,   380,   378,
     381,   135,   384,   385,   387,   142,   143,   401,   144,   145,
     146,   386,   388,   147,   148,   149,   150,   136,   389,    31,
       7,   402,   407,   151,   152,   153,   154,   155,    23,   156,
     157,   158,   392,   410,   411,   137,   138,   412,   140,   423,
     417,   202,   141,    94,   111,   228,   193,   135,   313,   226,
     422,   142,   143,   311,   144,   145,   146,   331,   256,   147,
     148,   149,   150,   136,   332,    31,     7,   333,   335,   151,
     152,   153,   154,   155,    23,   156,   157,   158,   136,     0,
      31,   137,   138,     0,   328,     0,     0,     0,   141,     0,
       0,     0,   136,     0,    31,     0,   137,   142,   143,   140,
     144,   145,   146,   369,     0,   147,   148,   149,   150,     0,
     137,     0,     7,   140,   356,   151,   152,   153,   154,   155,
      23,   156,   157,   158,     0,     0,     0,     0,     0,     0,
     151,   152,   153,   154,     0,    23,   156,   157,   158,   136,
       0,    31,     0,     0,   151,   152,   153,   154,     0,    23,
     156,   157,   158,     0,     0,     0,     0,   137,     0,     0,
     140,     0,     0,     0,   394,   205,   206,   207,   208,   209,
     210,   211,   212,     0,     0,     0,   213,   116,   214,   118,
     215,   216,   217,   218,   219,   220,   221,   222,   223,     0,
     224,   151,   152,   153,   154,     0,    23,   156,   157,   158,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,   225
  };

  const short int
  parser::yycheck_[] =
  {
      13,    85,   271,   138,   136,   137,   132,   247,    21,    39,
     145,    37,    25,    26,    27,   150,     5,   318,   194,    53,
      74,     6,     5,    36,    37,   201,   161,    19,    41,    19,
      60,    19,     0,     3,     4,     5,     6,     7,   122,    38,
      53,   125,     0,    56,    12,    79,    31,    17,    74,    38,
       3,     4,     5,     6,     7,    47,    69,   136,   137,    47,
      20,     8,   238,    19,    12,   140,    79,    37,   369,    68,
      38,   372,   147,   148,   375,    64,    19,   253,    46,    68,
     155,    64,    74,    39,    74,    14,    74,    67,   328,   102,
      42,   392,    18,   394,   107,   108,    66,    50,    46,   129,
      68,     3,     4,     5,     6,     7,    38,     8,    38,   410,
      36,   380,    18,    66,    74,    17,    45,    17,    74,   132,
      20,    21,    22,    23,    76,    25,    69,    70,   263,   264,
      36,    74,    75,    76,    77,    35,    68,    38,    68,    40,
      74,   154,    17,    44,   228,    20,    21,    22,    23,   233,
     234,    18,    29,    19,    47,    37,    74,    50,    25,   291,
      35,    57,    58,    37,    66,    65,    43,    49,    29,    36,
      36,   246,   247,   248,    67,   250,   302,   303,    74,   305,
     306,   194,    43,    57,    58,    39,    42,    43,   201,    43,
      65,   270,   271,   272,   273,   274,   275,   276,   277,   278,
     279,   280,   281,   282,   283,   284,   285,   286,   287,   288,
     289,   290,   291,    25,    19,    74,    37,   292,   293,   294,
      25,    17,    36,    19,    74,   238,    38,    48,    38,    43,
      40,    39,    38,   368,    44,    43,   371,    36,   373,    35,
     253,   376,    38,   318,    37,    36,    42,    74,    36,    42,
      43,    42,    36,   328,    42,    32,    33,    34,    42,    42,
     386,    24,    25,    48,    49,    28,    29,   402,     3,     4,
       5,     6,   407,    69,    70,    71,    72,    42,    74,    75,
      76,    77,    26,    27,    63,    64,    30,    31,   423,   302,
     303,     1,   305,   306,   369,    42,    20,   372,    22,   374,
     375,   380,   377,    43,    36,   389,    38,    17,   383,    19,
     276,   277,   278,   279,    11,    12,    13,   392,    74,   394,
      57,    58,    17,    43,    19,    35,    36,    37,    38,   273,
     274,   275,    42,    15,    16,   410,   411,   282,   283,   284,
      35,    51,    52,    38,    54,    55,    56,    57,    58,    59,
      60,    61,    62,     1,   196,   197,    66,   280,   281,    69,
      70,    71,    72,    73,    74,    75,    76,    77,    74,    17,
      74,    19,    43,   386,    69,    70,    71,    72,    37,    74,
      75,    76,    77,    36,   289,   290,     8,    35,    36,    37,
      38,     8,    38,    74,    42,   285,   286,   287,   288,     1,
      65,    65,     8,    51,    52,    38,    54,    55,    56,    38,
      38,    59,    60,    61,    62,    17,    74,    19,    66,    38,
       9,    69,    70,    71,    72,    73,    74,    75,    76,    77,
      42,    42,    10,    35,    36,    37,    38,    74,    74,    38,
      42,    38,    38,    38,    37,     1,    54,    39,    63,    51,
      52,    64,    54,    55,    56,    37,    74,    59,    60,    61,
      62,    17,    37,    19,    66,    74,    39,    69,    70,    71,
      72,    73,    74,    75,    76,    77,    39,    42,    39,    35,
      36,    37,    38,    18,    43,    39,    42,    38,    18,    39,
      43,     1,    41,    39,    39,    51,    52,    36,    54,    55,
      56,    43,    39,    59,    60,    61,    62,    17,    39,    19,
      66,    39,    53,    69,    70,    71,    72,    73,    74,    75,
      76,    77,    42,    42,    68,    35,    36,    39,    38,    18,
      39,   107,    42,    51,    68,   121,    96,     1,   236,   120,
     413,    51,    52,   235,    54,    55,    56,   265,   149,    59,
      60,    61,    62,    17,   266,    19,    66,   270,   272,    69,
      70,    71,    72,    73,    74,    75,    76,    77,    17,    -1,
      19,    35,    36,    -1,    38,    -1,    -1,    -1,    42,    -1,
      -1,    -1,    17,    -1,    19,    -1,    35,    51,    52,    38,
      54,    55,    56,    42,    -1,    59,    60,    61,    62,    -1,
      35,    -1,    66,    38,    39,    69,    70,    71,    72,    73,
      74,    75,    76,    77,    -1,    -1,    -1,    -1,    -1,    -1,
      69,    70,    71,    72,    -1,    74,    75,    76,    77,    17,
      -1,    19,    -1,    -1,    69,    70,    71,    72,    -1,    74,
      75,    76,    77,    -1,    -1,    -1,    -1,    35,    -1,    -1,
      38,    -1,    -1,    -1,    42,     9,    10,    11,    12,    13,
      14,    15,    16,    -1,    -1,    -1,    20,    21,    22,    23,
      24,    25,    26,    27,    28,    29,    30,    31,    32,    -1,
      34,    69,    70,    71,    72,    -1,    74,    75,    76,    77,
      -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    -1,    68
  };

  const unsigned char
  parser::yystos_[] =
  {
       0,     3,     4,     5,     6,     7,    50,    66,    79,    80,
      81,    82,    83,    91,    92,    97,    98,   102,   103,   105,
     106,    91,    92,    74,    88,    47,    50,    67,     0,     8,
      82,    19,    47,    88,    89,    67,    18,    25,    36,    96,
     104,    38,    96,   107,    89,    19,    36,    89,    89,    89,
      74,    88,    74,    25,    95,    74,    18,    36,    99,    89,
      90,    89,    93,    94,     7,    17,    80,    84,    85,    91,
      98,   102,   105,   111,   112,   117,   118,   119,   120,    18,
     104,    39,    89,   109,   110,   107,    36,   108,    74,    74,
      82,    42,    42,    42,    95,    90,    89,    37,    74,   100,
     101,    43,    43,   104,    74,    43,    29,    43,    91,    74,
      37,    84,    74,    89,    17,    20,    21,    22,    23,    35,
      65,    96,   113,   114,   115,   107,    36,    42,   121,    90,
      74,    39,    43,    43,   108,     1,    17,    35,    36,    37,
      38,    42,    51,    52,    54,    55,    56,    59,    60,    61,
      62,    69,    70,    71,    72,    73,    75,    76,    77,    80,
      86,    87,    89,   124,   125,   129,   130,   134,   138,   139,
     140,   141,   142,   143,   145,   146,   147,   148,   149,   150,
     151,   152,   153,   154,   155,   156,   157,   158,   159,   160,
     161,    37,    29,    99,     8,    37,    42,    43,    42,    43,
      89,     8,    93,    89,    74,     9,    10,    11,    12,    13,
      14,    15,    16,    20,    22,    24,    25,    26,    27,    28,
      29,    30,    31,    32,    34,    68,   115,   116,   113,   108,
      65,    65,   108,    48,    49,   122,   123,   104,     8,   110,
     155,   156,   155,    37,    86,   142,    38,    38,    38,    86,
      38,   142,   142,    57,    58,    74,   133,    86,    89,   142,
      37,    86,    74,    63,    64,   135,   136,   137,    42,    42,
      14,    45,     9,    32,    33,    34,    24,    25,    28,    29,
      20,    22,    11,    12,    13,    26,    27,    30,    31,    15,
      16,    10,     8,    38,    40,    44,    89,   161,   101,   101,
     161,    74,    38,    38,   108,    38,    38,    38,   108,   108,
      37,   123,    37,   122,   161,    37,    39,   142,    42,   126,
     127,   128,   140,   142,   142,    54,   142,   161,    38,    86,
      86,   136,   137,   147,   145,   148,   149,   149,   149,   150,
     150,   150,   150,   151,   151,   152,   152,   152,   153,   153,
     153,   153,   154,   154,   155,   142,    39,   142,   144,   142,
      74,   110,   110,   110,   110,    74,    37,    37,    39,    42,
     127,    39,    42,    39,    18,    43,    39,    38,    39,   140,
      18,    43,    39,    43,    41,    39,    43,    39,    39,    39,
      86,   127,    42,    86,    42,   127,    86,   142,   127,    86,
     142,    36,    39,   145,   142,   110,   108,    53,   127,   127,
      42,    68,    39,   131,   132,   133,    86,    39,    86,   127,
     142,    37,   132,    18,    86
  };

  const unsigned char
  parser::yyr1_[] =
  {
       0,    78,    79,    79,    80,    80,    81,    81,    81,    82,
      82,    82,    82,    82,    83,    83,    84,    84,    84,    84,
      84,    84,    84,    85,    85,    86,    86,    86,    86,    86,
      86,    86,    86,    86,    86,    86,    87,    87,    88,    88,
      89,    89,    89,    89,    90,    90,    90,    91,    91,    91,
      92,    92,    93,    93,    94,    94,    94,    95,    96,    97,
      98,    98,    99,    99,   100,   100,   100,   100,   100,   101,
     101,   102,   102,   102,   102,   103,   104,   105,   105,   106,
     106,   107,   107,   108,   108,   109,   109,   109,   110,   110,
     111,   111,   112,   113,   113,   113,   113,   114,   114,   114,
     114,   115,   115,   116,   116,   116,   116,   116,   116,   116,
     116,   116,   116,   116,   116,   116,   116,   116,   116,   116,
     116,   116,   116,   116,   117,   118,   118,   119,   120,   120,
     121,   121,   121,   121,   121,   122,   122,   123,   124,   124,
     125,   125,   126,   126,   126,   126,   126,   126,   126,   126,
     127,   127,   127,   128,   128,   129,   129,   130,   131,   131,
     132,   132,   133,   133,   134,   135,   135,   136,   136,   137,
     138,   138,   138,   139,   139,   139,   139,   139,   139,   139,
     140,   141,   141,   142,   142,   143,   144,   144,   144,   145,
     145,   146,   146,   147,   147,   148,   148,   148,   148,   149,
     149,   149,   149,   149,   150,   150,   150,   151,   151,   151,
     151,   152,   152,   152,   152,   152,   153,   153,   153,   154,
     154,   155,   155,   155,   155,   156,   156,   156,   156,   157,
     158,   159,   159,   160,   160,   160,   160,   161,   161,   161,
     161,   161,   161
  };

  const unsigned char
  parser::yyr2_[] =
  {
       0,     2,     0,     1,     1,     3,     4,     4,     4,     1,
       1,     1,     1,     1,     1,     2,     1,     1,     1,     1,
       1,     1,     1,     1,     2,     1,     1,     1,     1,     1,
       1,     2,     1,     2,     1,     1,     1,     2,     1,     3,
       2,     2,     3,     1,     1,     2,     3,     1,     1,     1,
       1,     1,     2,     4,     1,     2,     3,     3,     3,     5,
       3,     5,     2,     3,     1,     2,     2,     3,     3,     1,
       3,     2,     4,     3,     5,     3,     3,     3,     4,     3,
       4,     2,     3,     2,     3,     1,     2,     3,     2,     4,
       3,     4,     3,     5,     5,     5,     7,     1,     1,     1,
       1,     1,     1,     1,     1,     1,     1,     1,     1,     1,
       1,     1,     1,     1,     1,     1,     1,     1,     1,     1,
       1,     1,     1,     1,     3,     2,     2,     2,     3,     4,
       4,     3,     4,     3,     1,     5,     2,     2,     5,     7,
       5,     5,     5,     4,     4,     3,     4,     3,     3,     2,
       1,     2,     3,     3,     5,     5,     6,     7,     1,     2,
       2,     3,     2,     1,     2,     2,     5,     1,     2,     2,
       3,     2,     2,     1,     2,     1,     2,     2,     2,     2,
       2,     2,     3,     1,     1,     3,     1,     2,     3,     1,
       5,     1,     3,     1,     3,     1,     3,     3,     3,     1,
       3,     3,     3,     3,     1,     3,     3,     1,     3,     3,
       3,     1,     3,     3,     3,     3,     1,     3,     3,     1,
       3,     1,     2,     2,     3,     1,     1,     1,     1,     4,
       3,     4,     3,     1,     1,     2,     1,     1,     1,     1,
       1,     1,     1
  };



  // YYTNAME[SYMBOL-NUM] -- String name of the symbol SYMBOL-NUM.
  // First, the terminals, then, starting at \a yyntokens_, nonterminals.
  const char*
  const parser::yytname_[] =
  {
  "$end", "error", "$undefined", "\"public\"", "\"local\"", "\"derived\"",
  "\"private\"", "\"unbound\"", "\"=\"", "\"&&\"", "\"&\"", "\"/\"",
  "\"*\"", "\"%\"", "\"||\"", "\"|\"", "\"^\"", "\"~\"", "\":\"", "\"::\"",
  "\"+\"", "\"++\"", "\"-\"", "\"--\"", "\"<=\"", "\"<\"", "\"<<\"",
  "\"<<<\"", "\">=\"", "\">\"", "\">>\"", "\">>>\"", "\"==\"", "\"~=\"",
  "\"!=\"", "\"!\"", "\"{\"", "\"}\"", "\"(\"", "\")\"", "\"[\"", "\"]\"",
  "\";\"", "\",\"", "\".\"", "\"?\"", "\"=>\"", "\"class\"", "\"get\"",
  "\"set\"", "\"namespace\"", "\"if\"", "\"for\"", "\"else\"", "\"while\"",
  "\"do\"", "\"switch\"", "\"case\"", "\"default\"", "\"return\"",
  "\"throw\"", "\"goto\"", "\"try\"", "\"catch\"", "\"finally\"",
  "\"operator\"", "\"using\"", "\"enum\"", "\"..\"", "\"true\"",
  "\"false\"", "\"this\"", "\"new\"", "\"delete\"", "IDENT", "L_STRING",
  "L_NUMBER", "L_CHAR", "$accept", "start", "using", "using_low",
  "filestmnt", "filestmnts", "classstmnt", "classstmnts", "codestmnt",
  "codestmnts", "type_ident", "type", "typelist", "encpsltn",
  "encpsltn_n_cls", "template_def", "template_defs", "template_use",
  "template", "namespace", "enum", "enum_body", "enum_values",
  "enum_value", "class", "classhead", "classbody", "mthd", "mthd_head",
  "mthd_args", "mthd_body", "mthd_arglist", "mthd_arg", "mthdop",
  "mthdop_head", "mthdop_args", "mthdop_ops1p", "mthdop_ops1s",
  "mthdop_ops2", "mthdcnst", "mthdcnst_head", "prop", "prop_head",
  "prop_body", "prop_get", "prop_set", "ifelse", "for", "for_step",
  "for_step_arg", "for_each", "while", "switch", "switch_cases",
  "switch_case", "case", "try", "catch", "catchlist", "finally",
  "trycatch", "statement", "declaration", "scope", "val", "assignment",
  "explist", "exp01", "exp02", "exp03", "exp04", "exp05", "exp06", "exp07",
  "exp08", "exp09", "exp10", "exp11", "exp12", "arrget", "dotnav", "call",
  "expp", "cval", YY_NULLPTR
  };

#if YYDEBUG
  const unsigned short int
  parser::yyrline_[] =
  {
       0,   140,   140,   141,   143,   144,   146,   147,   148,   150,
     151,   152,   153,   154,   156,   157,   159,   160,   161,   162,
     163,   164,   165,   167,   168,   170,   171,   172,   173,   174,
     175,   176,   177,   178,   179,   180,   182,   183,   186,   187,
     189,   190,   191,   192,   194,   195,   196,   199,   200,   201,
     203,   204,   207,   208,   210,   211,   212,   214,   216,   219,
     222,   223,   225,   226,   228,   229,   230,   231,   232,   234,
     235,   238,   239,   240,   241,   243,   245,   248,   249,   251,
     252,   254,   255,   257,   258,   260,   261,   262,   264,   265,
     268,   269,   271,   273,   274,   275,   276,   278,   279,   280,
     281,   283,   284,   286,   287,   288,   289,   290,   291,   292,
     293,   294,   295,   296,   297,   298,   299,   300,   301,   302,
     303,   304,   305,   306,   309,   311,   312,   315,   317,   318,
     320,   321,   322,   323,   324,   326,   327,   329,   332,   333,
     336,   337,   339,   340,   341,   342,   343,   344,   345,   346,
     348,   349,   350,   352,   353,   356,   357,   360,   362,   363,
     365,   366,   368,   369,   372,   374,   375,   377,   378,   380,
     382,   383,   384,   387,   388,   389,   390,   391,   392,   393,
     395,   397,   398,   400,   401,   403,   405,   406,   407,   409,
     410,   412,   413,   415,   416,   418,   419,   420,   421,   423,
     424,   425,   426,   427,   429,   430,   431,   433,   434,   435,
     436,   438,   439,   440,   441,   442,   444,   445,   446,   448,
     449,   451,   452,   453,   454,   456,   457,   458,   459,   461,
     463,   465,   466,   468,   469,   470,   471,   473,   474,   475,
     476,   477,   478
  };

  // Print the state stack on the debug stream.
  void
  parser::yystack_print_ ()
  {
    *yycdebug_ << "Stack now";
    for (stack_type::const_iterator
           i = yystack_.begin (),
           i_end = yystack_.end ();
         i != i_end; ++i)
      *yycdebug_ << ' ' << i->state;
    *yycdebug_ << std::endl;
  }

  // Report on the debug stream that the rule \a yyrule is going to be reduced.
  void
  parser::yy_reduce_print_ (int yyrule)
  {
    unsigned int yylno = yyrline_[yyrule];
    int yynrhs = yyr2_[yyrule];
    // Print the symbols being reduced, and their result.
    *yycdebug_ << "Reducing stack by rule " << yyrule - 1
               << " (line " << yylno << "):" << std::endl;
    // The symbols being reduced.
    for (int yyi = 0; yyi < yynrhs; yyi++)
      YY_SYMBOL_PRINT ("   $" << yyi + 1 << " =",
                       yystack_[(yynrhs) - (yyi + 1)]);
  }
#endif // YYDEBUG


#line 6 "parser.y" // lalr1.cc:1167
} } //  yaoosl::compiler 
#line 3212 "parser.tab.cc" // lalr1.cc:1167
#line 480 "parser.y" // lalr1.cc:1168
