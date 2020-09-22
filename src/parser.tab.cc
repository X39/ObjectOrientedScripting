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

    #include <string>
    #include <vector>

#line 39 "parser.tab.cc" // lalr1.cc:397


// First part of user declarations.

#line 44 "parser.tab.cc" // lalr1.cc:404

# ifndef YY_NULLPTR
#  if defined __cplusplus && 201103L <= __cplusplus
#   define YY_NULLPTR nullptr
#  else
#   define YY_NULLPTR 0
#  endif
# endif

#include "parser.tab.hh"

// User implementation prologue.

#line 58 "parser.tab.cc" // lalr1.cc:412
// Unqualified %code blocks.
#line 24 "parser.y" // lalr1.cc:413

     namespace yaoosl::compiler
     {
          // Return the next token.
          parser::symbol_type yylex (yaoosl::compiler::tokenizer&);
     }

#line 68 "parser.tab.cc" // lalr1.cc:413


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
#line 154 "parser.tab.cc" // lalr1.cc:479

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
  parser::parser (yaoosl::compiler::tokenizer &tokenizer_yyarg, yaoosl::compiler::cstnode& result_yyarg, std::string fpath_yyarg)
    :
#if YYDEBUG
      yydebug_ (false),
      yycdebug_ (&std::cerr),
#endif
      tokenizer (tokenizer_yyarg),
      result (result_yyarg),
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
      case 82: // using
      case 83: // using_low
      case 84: // filestmnt
      case 85: // filestmnts
      case 86: // classstmnt
      case 87: // classstmnts
      case 88: // codestmnt
      case 89: // codestmnts
      case 90: // type_ident
      case 91: // type
      case 92: // typelist
      case 93: // encpsltn
      case 94: // encpsltn_n_cls
      case 95: // template_def
      case 96: // template_defs
      case 97: // template_use
      case 98: // template
      case 99: // namespace
      case 100: // enum
      case 101: // enum_body
      case 102: // enum_values
      case 103: // enum_value
      case 104: // class
      case 105: // classhead
      case 106: // classbody
      case 107: // mthd
      case 108: // mthd_head
      case 109: // mthd_args
      case 110: // mthd_body
      case 111: // mthd_arglist
      case 112: // mthd_arg
      case 113: // cnvrsn
      case 114: // mthdop
      case 115: // mthdop_head
      case 116: // mthdop_args
      case 117: // mthdop_ops1p
      case 118: // mthdop_ops1s
      case 119: // mthdop_ops2
      case 120: // mthdcnst
      case 121: // mthdcnst_head
      case 122: // prop
      case 123: // prop_head
      case 124: // prop_body
      case 125: // prop_set
      case 126: // prop_get
      case 127: // ifelse
      case 128: // for
      case 129: // for_step
      case 130: // for_step_arg
      case 131: // for_each
      case 132: // while
      case 133: // switch
      case 134: // switch_cases
      case 135: // switch_case
      case 136: // case
      case 137: // try
      case 138: // catch
      case 139: // catchlist
      case 140: // finally
      case 141: // trycatch
      case 142: // statement
      case 143: // declaration
      case 144: // scope
      case 145: // val
      case 146: // assignment
      case 147: // explist
      case 148: // exp01
      case 149: // exp02
      case 150: // exp03
      case 151: // exp04
      case 152: // exp05
      case 153: // exp06
      case 154: // exp07
      case 155: // exp08
      case 156: // exp09
      case 157: // exp10
      case 158: // exp11
      case 159: // exp12
      case 160: // arrget
      case 161: // dotnav
      case 162: // call
      case 163: // expp
      case 164: // cval
        value.move< yaoosl::compiler::cstnode > (that.value);
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
      case 48: // "class"
      case 49: // "conversion"
      case 50: // "get"
      case 51: // "set"
      case 52: // "namespace"
      case 53: // "if"
      case 54: // "for"
      case 55: // "else"
      case 56: // "while"
      case 57: // "do"
      case 58: // "switch"
      case 59: // "case"
      case 60: // "default"
      case 61: // "return"
      case 62: // "throw"
      case 63: // "goto"
      case 64: // "try"
      case 65: // "catch"
      case 66: // "finally"
      case 67: // "operator"
      case 68: // "using"
      case 69: // "enum"
      case 70: // ".."
      case 71: // "true"
      case 72: // "false"
      case 73: // "this"
      case 74: // "new"
      case 75: // "delete"
      case 76: // L_IDENT
      case 77: // L_STRING
      case 78: // L_NUMBER
      case 79: // L_CHAR
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
      case 82: // using
      case 83: // using_low
      case 84: // filestmnt
      case 85: // filestmnts
      case 86: // classstmnt
      case 87: // classstmnts
      case 88: // codestmnt
      case 89: // codestmnts
      case 90: // type_ident
      case 91: // type
      case 92: // typelist
      case 93: // encpsltn
      case 94: // encpsltn_n_cls
      case 95: // template_def
      case 96: // template_defs
      case 97: // template_use
      case 98: // template
      case 99: // namespace
      case 100: // enum
      case 101: // enum_body
      case 102: // enum_values
      case 103: // enum_value
      case 104: // class
      case 105: // classhead
      case 106: // classbody
      case 107: // mthd
      case 108: // mthd_head
      case 109: // mthd_args
      case 110: // mthd_body
      case 111: // mthd_arglist
      case 112: // mthd_arg
      case 113: // cnvrsn
      case 114: // mthdop
      case 115: // mthdop_head
      case 116: // mthdop_args
      case 117: // mthdop_ops1p
      case 118: // mthdop_ops1s
      case 119: // mthdop_ops2
      case 120: // mthdcnst
      case 121: // mthdcnst_head
      case 122: // prop
      case 123: // prop_head
      case 124: // prop_body
      case 125: // prop_set
      case 126: // prop_get
      case 127: // ifelse
      case 128: // for
      case 129: // for_step
      case 130: // for_step_arg
      case 131: // for_each
      case 132: // while
      case 133: // switch
      case 134: // switch_cases
      case 135: // switch_case
      case 136: // case
      case 137: // try
      case 138: // catch
      case 139: // catchlist
      case 140: // finally
      case 141: // trycatch
      case 142: // statement
      case 143: // declaration
      case 144: // scope
      case 145: // val
      case 146: // assignment
      case 147: // explist
      case 148: // exp01
      case 149: // exp02
      case 150: // exp03
      case 151: // exp04
      case 152: // exp05
      case 153: // exp06
      case 154: // exp07
      case 155: // exp08
      case 156: // exp09
      case 157: // exp10
      case 158: // exp11
      case 159: // exp12
      case 160: // arrget
      case 161: // dotnav
      case 162: // call
      case 163: // expp
      case 164: // cval
        value.copy< yaoosl::compiler::cstnode > (that.value);
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
      case 48: // "class"
      case 49: // "conversion"
      case 50: // "get"
      case 51: // "set"
      case 52: // "namespace"
      case 53: // "if"
      case 54: // "for"
      case 55: // "else"
      case 56: // "while"
      case 57: // "do"
      case 58: // "switch"
      case 59: // "case"
      case 60: // "default"
      case 61: // "return"
      case 62: // "throw"
      case 63: // "goto"
      case 64: // "try"
      case 65: // "catch"
      case 66: // "finally"
      case 67: // "operator"
      case 68: // "using"
      case 69: // "enum"
      case 70: // ".."
      case 71: // "true"
      case 72: // "false"
      case 73: // "this"
      case 74: // "new"
      case 75: // "delete"
      case 76: // L_IDENT
      case 77: // L_STRING
      case 78: // L_NUMBER
      case 79: // L_CHAR
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
      case 82: // using
      case 83: // using_low
      case 84: // filestmnt
      case 85: // filestmnts
      case 86: // classstmnt
      case 87: // classstmnts
      case 88: // codestmnt
      case 89: // codestmnts
      case 90: // type_ident
      case 91: // type
      case 92: // typelist
      case 93: // encpsltn
      case 94: // encpsltn_n_cls
      case 95: // template_def
      case 96: // template_defs
      case 97: // template_use
      case 98: // template
      case 99: // namespace
      case 100: // enum
      case 101: // enum_body
      case 102: // enum_values
      case 103: // enum_value
      case 104: // class
      case 105: // classhead
      case 106: // classbody
      case 107: // mthd
      case 108: // mthd_head
      case 109: // mthd_args
      case 110: // mthd_body
      case 111: // mthd_arglist
      case 112: // mthd_arg
      case 113: // cnvrsn
      case 114: // mthdop
      case 115: // mthdop_head
      case 116: // mthdop_args
      case 117: // mthdop_ops1p
      case 118: // mthdop_ops1s
      case 119: // mthdop_ops2
      case 120: // mthdcnst
      case 121: // mthdcnst_head
      case 122: // prop
      case 123: // prop_head
      case 124: // prop_body
      case 125: // prop_set
      case 126: // prop_get
      case 127: // ifelse
      case 128: // for
      case 129: // for_step
      case 130: // for_step_arg
      case 131: // for_each
      case 132: // while
      case 133: // switch
      case 134: // switch_cases
      case 135: // switch_case
      case 136: // case
      case 137: // try
      case 138: // catch
      case 139: // catchlist
      case 140: // finally
      case 141: // trycatch
      case 142: // statement
      case 143: // declaration
      case 144: // scope
      case 145: // val
      case 146: // assignment
      case 147: // explist
      case 148: // exp01
      case 149: // exp02
      case 150: // exp03
      case 151: // exp04
      case 152: // exp05
      case 153: // exp06
      case 154: // exp07
      case 155: // exp08
      case 156: // exp09
      case 157: // exp10
      case 158: // exp11
      case 159: // exp12
      case 160: // arrget
      case 161: // dotnav
      case 162: // call
      case 163: // expp
      case 164: // cval
        yylhs.value.build< yaoosl::compiler::cstnode > ();
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
      case 48: // "class"
      case 49: // "conversion"
      case 50: // "get"
      case 51: // "set"
      case 52: // "namespace"
      case 53: // "if"
      case 54: // "for"
      case 55: // "else"
      case 56: // "while"
      case 57: // "do"
      case 58: // "switch"
      case 59: // "case"
      case 60: // "default"
      case 61: // "return"
      case 62: // "throw"
      case 63: // "goto"
      case 64: // "try"
      case 65: // "catch"
      case 66: // "finally"
      case 67: // "operator"
      case 68: // "using"
      case 69: // "enum"
      case 70: // ".."
      case 71: // "true"
      case 72: // "false"
      case 73: // "this"
      case 74: // "new"
      case 75: // "delete"
      case 76: // L_IDENT
      case 77: // L_STRING
      case 78: // L_NUMBER
      case 79: // L_CHAR
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
#line 141 "parser.y" // lalr1.cc:859
    { result = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::START, tokenizer.create_token(), {} }; }
#line 1029 "parser.tab.cc" // lalr1.cc:859
    break;

  case 3:
#line 142 "parser.y" // lalr1.cc:859
    { result = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::START, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1035 "parser.tab.cc" // lalr1.cc:859
    break;

  case 4:
#line 144 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 1041 "parser.tab.cc" // lalr1.cc:859
    break;

  case 5:
#line 145 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1047 "parser.tab.cc" // lalr1.cc:859
    break;

  case 6:
#line 147 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1053 "parser.tab.cc" // lalr1.cc:859
    break;

  case 7:
#line 148 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1059 "parser.tab.cc" // lalr1.cc:859
    break;

  case 8:
#line 149 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1065 "parser.tab.cc" // lalr1.cc:859
    break;

  case 9:
#line 151 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1071 "parser.tab.cc" // lalr1.cc:859
    break;

  case 10:
#line 152 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1077 "parser.tab.cc" // lalr1.cc:859
    break;

  case 11:
#line 153 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1083 "parser.tab.cc" // lalr1.cc:859
    break;

  case 12:
#line 154 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1089 "parser.tab.cc" // lalr1.cc:859
    break;

  case 13:
#line 155 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1095 "parser.tab.cc" // lalr1.cc:859
    break;

  case 14:
#line 157 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1101 "parser.tab.cc" // lalr1.cc:859
    break;

  case 15:
#line 158 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1107 "parser.tab.cc" // lalr1.cc:859
    break;

  case 16:
#line 160 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1113 "parser.tab.cc" // lalr1.cc:859
    break;

  case 17:
#line 161 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1119 "parser.tab.cc" // lalr1.cc:859
    break;

  case 18:
#line 162 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1125 "parser.tab.cc" // lalr1.cc:859
    break;

  case 19:
#line 163 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1131 "parser.tab.cc" // lalr1.cc:859
    break;

  case 20:
#line 164 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1137 "parser.tab.cc" // lalr1.cc:859
    break;

  case 21:
#line 165 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1143 "parser.tab.cc" // lalr1.cc:859
    break;

  case 22:
#line 166 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1149 "parser.tab.cc" // lalr1.cc:859
    break;

  case 23:
#line 167 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1155 "parser.tab.cc" // lalr1.cc:859
    break;

  case 24:
#line 169 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1161 "parser.tab.cc" // lalr1.cc:859
    break;

  case 25:
#line 170 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1167 "parser.tab.cc" // lalr1.cc:859
    break;

  case 26:
#line 172 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1173 "parser.tab.cc" // lalr1.cc:859
    break;

  case 27:
#line 173 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1179 "parser.tab.cc" // lalr1.cc:859
    break;

  case 28:
#line 174 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1185 "parser.tab.cc" // lalr1.cc:859
    break;

  case 29:
#line 175 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1191 "parser.tab.cc" // lalr1.cc:859
    break;

  case 30:
#line 176 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1197 "parser.tab.cc" // lalr1.cc:859
    break;

  case 31:
#line 177 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1203 "parser.tab.cc" // lalr1.cc:859
    break;

  case 32:
#line 178 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1209 "parser.tab.cc" // lalr1.cc:859
    break;

  case 33:
#line 179 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1215 "parser.tab.cc" // lalr1.cc:859
    break;

  case 34:
#line 180 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1221 "parser.tab.cc" // lalr1.cc:859
    break;

  case 35:
#line 181 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
#line 1227 "parser.tab.cc" // lalr1.cc:859
    break;

  case 36:
#line 182 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
#line 1233 "parser.tab.cc" // lalr1.cc:859
    break;

  case 37:
#line 184 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1239 "parser.tab.cc" // lalr1.cc:859
    break;

  case 38:
#line 185 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1245 "parser.tab.cc" // lalr1.cc:859
    break;

  case 39:
#line 188 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, tokenizer.create_token(), { yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} } } }; }
#line 1251 "parser.tab.cc" // lalr1.cc:859
    break;

  case 40:
#line 189 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }); }
#line 1257 "parser.tab.cc" // lalr1.cc:859
    break;

  case 41:
#line 191 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1263 "parser.tab.cc" // lalr1.cc:859
    break;

  case 42:
#line 192 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > (), {}, {} } }; }
#line 1269 "parser.tab.cc" // lalr1.cc:859
    break;

  case 43:
#line 193 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1275 "parser.tab.cc" // lalr1.cc:859
    break;

  case 44:
#line 194 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > (), {}, {} } }; }
#line 1281 "parser.tab.cc" // lalr1.cc:859
    break;

  case 45:
#line 195 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::tokenizer::token > () } }; }
#line 1287 "parser.tab.cc" // lalr1.cc:859
    break;

  case 46:
#line 196 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > () } }; }
#line 1293 "parser.tab.cc" // lalr1.cc:859
    break;

  case 47:
#line 197 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::tokenizer::token > () } }; }
#line 1299 "parser.tab.cc" // lalr1.cc:859
    break;

  case 48:
#line 198 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > () } }; }
#line 1305 "parser.tab.cc" // lalr1.cc:859
    break;

  case 49:
#line 200 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1311 "parser.tab.cc" // lalr1.cc:859
    break;

  case 50:
#line 201 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1317 "parser.tab.cc" // lalr1.cc:859
    break;

  case 51:
#line 202 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1323 "parser.tab.cc" // lalr1.cc:859
    break;

  case 52:
#line 205 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 1329 "parser.tab.cc" // lalr1.cc:859
    break;

  case 53:
#line 206 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1335 "parser.tab.cc" // lalr1.cc:859
    break;

  case 54:
#line 207 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1341 "parser.tab.cc" // lalr1.cc:859
    break;

  case 55:
#line 209 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN_N_CLS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1347 "parser.tab.cc" // lalr1.cc:859
    break;

  case 56:
#line 210 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN_N_CLS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1353 "parser.tab.cc" // lalr1.cc:859
    break;

  case 57:
#line 213 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEF, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1359 "parser.tab.cc" // lalr1.cc:859
    break;

  case 58:
#line 214 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEF, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1365 "parser.tab.cc" // lalr1.cc:859
    break;

  case 59:
#line 216 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1371 "parser.tab.cc" // lalr1.cc:859
    break;

  case 60:
#line 217 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1377 "parser.tab.cc" // lalr1.cc:859
    break;

  case 61:
#line 218 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1383 "parser.tab.cc" // lalr1.cc:859
    break;

  case 62:
#line 220 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_USE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1389 "parser.tab.cc" // lalr1.cc:859
    break;

  case 63:
#line 222 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1395 "parser.tab.cc" // lalr1.cc:859
    break;

  case 64:
#line 225 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::NAMESPACE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1401 "parser.tab.cc" // lalr1.cc:859
    break;

  case 65:
#line 228 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1407 "parser.tab.cc" // lalr1.cc:859
    break;

  case 66:
#line 229 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1413 "parser.tab.cc" // lalr1.cc:859
    break;

  case 67:
#line 231 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_BODY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1419 "parser.tab.cc" // lalr1.cc:859
    break;

  case 68:
#line 232 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1425 "parser.tab.cc" // lalr1.cc:859
    break;

  case 69:
#line 234 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1431 "parser.tab.cc" // lalr1.cc:859
    break;

  case 70:
#line 235 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1437 "parser.tab.cc" // lalr1.cc:859
    break;

  case 71:
#line 236 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1443 "parser.tab.cc" // lalr1.cc:859
    break;

  case 72:
#line 237 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1449 "parser.tab.cc" // lalr1.cc:859
    break;

  case 73:
#line 238 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1455 "parser.tab.cc" // lalr1.cc:859
    break;

  case 74:
#line 240 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUE, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1461 "parser.tab.cc" // lalr1.cc:859
    break;

  case 75:
#line 241 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1467 "parser.tab.cc" // lalr1.cc:859
    break;

  case 76:
#line 244 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1473 "parser.tab.cc" // lalr1.cc:859
    break;

  case 77:
#line 245 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1479 "parser.tab.cc" // lalr1.cc:859
    break;

  case 78:
#line 246 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1485 "parser.tab.cc" // lalr1.cc:859
    break;

  case 79:
#line 247 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1491 "parser.tab.cc" // lalr1.cc:859
    break;

  case 80:
#line 249 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSHEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1497 "parser.tab.cc" // lalr1.cc:859
    break;

  case 81:
#line 251 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSBODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1503 "parser.tab.cc" // lalr1.cc:859
    break;

  case 82:
#line 254 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1509 "parser.tab.cc" // lalr1.cc:859
    break;

  case 83:
#line 255 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1515 "parser.tab.cc" // lalr1.cc:859
    break;

  case 84:
#line 257 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1521 "parser.tab.cc" // lalr1.cc:859
    break;

  case 85:
#line 258 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_HEAD_UNBOUND, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1527 "parser.tab.cc" // lalr1.cc:859
    break;

  case 86:
#line 260 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGS, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1533 "parser.tab.cc" // lalr1.cc:859
    break;

  case 87:
#line 261 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGS, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1539 "parser.tab.cc" // lalr1.cc:859
    break;

  case 88:
#line 263 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_BODY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1545 "parser.tab.cc" // lalr1.cc:859
    break;

  case 89:
#line 264 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1551 "parser.tab.cc" // lalr1.cc:859
    break;

  case 90:
#line 266 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1557 "parser.tab.cc" // lalr1.cc:859
    break;

  case 91:
#line 267 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1563 "parser.tab.cc" // lalr1.cc:859
    break;

  case 92:
#line 268 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1569 "parser.tab.cc" // lalr1.cc:859
    break;

  case 93:
#line 270 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1575 "parser.tab.cc" // lalr1.cc:859
    break;

  case 94:
#line 271 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1581 "parser.tab.cc" // lalr1.cc:859
    break;

  case 95:
#line 274 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1587 "parser.tab.cc" // lalr1.cc:859
    break;

  case 96:
#line 275 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[5].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1593 "parser.tab.cc" // lalr1.cc:859
    break;

  case 97:
#line 278 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1599 "parser.tab.cc" // lalr1.cc:859
    break;

  case 98:
#line 279 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1605 "parser.tab.cc" // lalr1.cc:859
    break;

  case 99:
#line 281 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_HEAD, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1611 "parser.tab.cc" // lalr1.cc:859
    break;

  case 100:
#line 283 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1617 "parser.tab.cc" // lalr1.cc:859
    break;

  case 101:
#line 284 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1623 "parser.tab.cc" // lalr1.cc:859
    break;

  case 102:
#line 285 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1629 "parser.tab.cc" // lalr1.cc:859
    break;

  case 103:
#line 286 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[5].value.as< yaoosl::compiler::cstnode > (), yystack_[3].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1635 "parser.tab.cc" // lalr1.cc:859
    break;

  case 104:
#line 288 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1641 "parser.tab.cc" // lalr1.cc:859
    break;

  case 105:
#line 289 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1647 "parser.tab.cc" // lalr1.cc:859
    break;

  case 106:
#line 290 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1653 "parser.tab.cc" // lalr1.cc:859
    break;

  case 107:
#line 291 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1659 "parser.tab.cc" // lalr1.cc:859
    break;

  case 108:
#line 293 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1S, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1665 "parser.tab.cc" // lalr1.cc:859
    break;

  case 109:
#line 294 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1S, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1671 "parser.tab.cc" // lalr1.cc:859
    break;

  case 110:
#line 296 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1677 "parser.tab.cc" // lalr1.cc:859
    break;

  case 111:
#line 297 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1683 "parser.tab.cc" // lalr1.cc:859
    break;

  case 112:
#line 298 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1689 "parser.tab.cc" // lalr1.cc:859
    break;

  case 113:
#line 299 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1695 "parser.tab.cc" // lalr1.cc:859
    break;

  case 114:
#line 300 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1701 "parser.tab.cc" // lalr1.cc:859
    break;

  case 115:
#line 301 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1707 "parser.tab.cc" // lalr1.cc:859
    break;

  case 116:
#line 302 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1713 "parser.tab.cc" // lalr1.cc:859
    break;

  case 117:
#line 303 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1719 "parser.tab.cc" // lalr1.cc:859
    break;

  case 118:
#line 304 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1725 "parser.tab.cc" // lalr1.cc:859
    break;

  case 119:
#line 305 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1731 "parser.tab.cc" // lalr1.cc:859
    break;

  case 120:
#line 306 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1737 "parser.tab.cc" // lalr1.cc:859
    break;

  case 121:
#line 307 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1743 "parser.tab.cc" // lalr1.cc:859
    break;

  case 122:
#line 308 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1749 "parser.tab.cc" // lalr1.cc:859
    break;

  case 123:
#line 309 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1755 "parser.tab.cc" // lalr1.cc:859
    break;

  case 124:
#line 310 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1761 "parser.tab.cc" // lalr1.cc:859
    break;

  case 125:
#line 311 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1767 "parser.tab.cc" // lalr1.cc:859
    break;

  case 126:
#line 312 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1773 "parser.tab.cc" // lalr1.cc:859
    break;

  case 127:
#line 313 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1779 "parser.tab.cc" // lalr1.cc:859
    break;

  case 128:
#line 314 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1785 "parser.tab.cc" // lalr1.cc:859
    break;

  case 129:
#line 315 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1791 "parser.tab.cc" // lalr1.cc:859
    break;

  case 130:
#line 316 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1797 "parser.tab.cc" // lalr1.cc:859
    break;

  case 131:
#line 319 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDCNST, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1803 "parser.tab.cc" // lalr1.cc:859
    break;

  case 132:
#line 321 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDCNST_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1809 "parser.tab.cc" // lalr1.cc:859
    break;

  case 133:
#line 322 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDDST_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1815 "parser.tab.cc" // lalr1.cc:859
    break;

  case 134:
#line 325 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1821 "parser.tab.cc" // lalr1.cc:859
    break;

  case 135:
#line 327 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1827 "parser.tab.cc" // lalr1.cc:859
    break;

  case 136:
#line 328 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_HEAD_UNBOUND, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1833 "parser.tab.cc" // lalr1.cc:859
    break;

  case 137:
#line 330 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1839 "parser.tab.cc" // lalr1.cc:859
    break;

  case 138:
#line 331 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1845 "parser.tab.cc" // lalr1.cc:859
    break;

  case 139:
#line 332 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1851 "parser.tab.cc" // lalr1.cc:859
    break;

  case 140:
#line 333 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { {}, yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1857 "parser.tab.cc" // lalr1.cc:859
    break;

  case 141:
#line 334 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { {}, {} } }; }
#line 1863 "parser.tab.cc" // lalr1.cc:859
    break;

  case 142:
#line 336 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { {}, { yaoosl::compiler::cstnode::PROP_SET, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), {} }, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1869 "parser.tab.cc" // lalr1.cc:859
    break;

  case 143:
#line 337 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[5].value.as< yaoosl::compiler::cstnode > (), { yaoosl::compiler::cstnode::PROP_SET, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), {} }, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1875 "parser.tab.cc" // lalr1.cc:859
    break;

  case 144:
#line 338 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { {}, {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1881 "parser.tab.cc" // lalr1.cc:859
    break;

  case 145:
#line 339 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1887 "parser.tab.cc" // lalr1.cc:859
    break;

  case 146:
#line 341 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_GET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1893 "parser.tab.cc" // lalr1.cc:859
    break;

  case 147:
#line 342 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_GET, yystack_[2].value.as< yaoosl::compiler::cstnode > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::tokenizer::token > () } }; }
#line 1899 "parser.tab.cc" // lalr1.cc:859
    break;

  case 148:
#line 345 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::IFELSE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1905 "parser.tab.cc" // lalr1.cc:859
    break;

  case 149:
#line 346 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::IFELSE, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1911 "parser.tab.cc" // lalr1.cc:859
    break;

  case 150:
#line 349 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1917 "parser.tab.cc" // lalr1.cc:859
    break;

  case 151:
#line 350 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1923 "parser.tab.cc" // lalr1.cc:859
    break;

  case 152:
#line 352 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1929 "parser.tab.cc" // lalr1.cc:859
    break;

  case 153:
#line 353 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1935 "parser.tab.cc" // lalr1.cc:859
    break;

  case 154:
#line 354 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1941 "parser.tab.cc" // lalr1.cc:859
    break;

  case 155:
#line 355 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, {} } }; }
#line 1947 "parser.tab.cc" // lalr1.cc:859
    break;

  case 156:
#line 356 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1953 "parser.tab.cc" // lalr1.cc:859
    break;

  case 157:
#line 357 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1959 "parser.tab.cc" // lalr1.cc:859
    break;

  case 158:
#line 358 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1965 "parser.tab.cc" // lalr1.cc:859
    break;

  case 159:
#line 359 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, {} } }; }
#line 1971 "parser.tab.cc" // lalr1.cc:859
    break;

  case 160:
#line 361 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1977 "parser.tab.cc" // lalr1.cc:859
    break;

  case 161:
#line 362 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1983 "parser.tab.cc" // lalr1.cc:859
    break;

  case 162:
#line 363 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1989 "parser.tab.cc" // lalr1.cc:859
    break;

  case 163:
#line 365 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_EACH, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1995 "parser.tab.cc" // lalr1.cc:859
    break;

  case 164:
#line 366 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_EACH, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2001 "parser.tab.cc" // lalr1.cc:859
    break;

  case 165:
#line 369 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::WHILE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2007 "parser.tab.cc" // lalr1.cc:859
    break;

  case 166:
#line 370 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DO_WHILE, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[4].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2013 "parser.tab.cc" // lalr1.cc:859
    break;

  case 167:
#line 373 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2019 "parser.tab.cc" // lalr1.cc:859
    break;

  case 168:
#line 375 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASES, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2025 "parser.tab.cc" // lalr1.cc:859
    break;

  case 169:
#line 376 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[1].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 2031 "parser.tab.cc" // lalr1.cc:859
    break;

  case 170:
#line 378 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2037 "parser.tab.cc" // lalr1.cc:859
    break;

  case 171:
#line 379 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2043 "parser.tab.cc" // lalr1.cc:859
    break;

  case 172:
#line 381 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CASE, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2049 "parser.tab.cc" // lalr1.cc:859
    break;

  case 173:
#line 382 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CASE, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2055 "parser.tab.cc" // lalr1.cc:859
    break;

  case 174:
#line 385 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2061 "parser.tab.cc" // lalr1.cc:859
    break;

  case 175:
#line 387 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCH, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2067 "parser.tab.cc" // lalr1.cc:859
    break;

  case 176:
#line 388 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCH, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2073 "parser.tab.cc" // lalr1.cc:859
    break;

  case 177:
#line 390 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCHLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2079 "parser.tab.cc" // lalr1.cc:859
    break;

  case 178:
#line 391 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCHLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2085 "parser.tab.cc" // lalr1.cc:859
    break;

  case 179:
#line 393 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FINALLY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2091 "parser.tab.cc" // lalr1.cc:859
    break;

  case 180:
#line 395 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2097 "parser.tab.cc" // lalr1.cc:859
    break;

  case 181:
#line 396 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 2103 "parser.tab.cc" // lalr1.cc:859
    break;

  case 182:
#line 397 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2109 "parser.tab.cc" // lalr1.cc:859
    break;

  case 183:
#line 400 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2115 "parser.tab.cc" // lalr1.cc:859
    break;

  case 184:
#line 401 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2121 "parser.tab.cc" // lalr1.cc:859
    break;

  case 185:
#line 402 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2127 "parser.tab.cc" // lalr1.cc:859
    break;

  case 186:
#line 403 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2133 "parser.tab.cc" // lalr1.cc:859
    break;

  case 187:
#line 404 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2139 "parser.tab.cc" // lalr1.cc:859
    break;

  case 188:
#line 405 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} } } }; }
#line 2145 "parser.tab.cc" // lalr1.cc:859
    break;

  case 189:
#line 406 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2151 "parser.tab.cc" // lalr1.cc:859
    break;

  case 190:
#line 408 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DECLARATION, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2157 "parser.tab.cc" // lalr1.cc:859
    break;

  case 191:
#line 410 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SCOPE, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2163 "parser.tab.cc" // lalr1.cc:859
    break;

  case 192:
#line 411 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SCOPE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2169 "parser.tab.cc" // lalr1.cc:859
    break;

  case 193:
#line 413 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::VAL, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2175 "parser.tab.cc" // lalr1.cc:859
    break;

  case 194:
#line 414 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::VAL, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2181 "parser.tab.cc" // lalr1.cc:859
    break;

  case 195:
#line 416 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ASSIGNMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2187 "parser.tab.cc" // lalr1.cc:859
    break;

  case 196:
#line 418 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::EXPLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2193 "parser.tab.cc" // lalr1.cc:859
    break;

  case 197:
#line 419 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::EXPLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2199 "parser.tab.cc" // lalr1.cc:859
    break;

  case 198:
#line 420 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 2205 "parser.tab.cc" // lalr1.cc:859
    break;

  case 199:
#line 422 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2211 "parser.tab.cc" // lalr1.cc:859
    break;

  case 200:
#line 423 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TERNARY_OPERATOR, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2217 "parser.tab.cc" // lalr1.cc:859
    break;

  case 201:
#line 425 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2223 "parser.tab.cc" // lalr1.cc:859
    break;

  case 202:
#line 426 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2229 "parser.tab.cc" // lalr1.cc:859
    break;

  case 203:
#line 428 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2235 "parser.tab.cc" // lalr1.cc:859
    break;

  case 204:
#line 429 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2241 "parser.tab.cc" // lalr1.cc:859
    break;

  case 205:
#line 431 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2247 "parser.tab.cc" // lalr1.cc:859
    break;

  case 206:
#line 432 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2253 "parser.tab.cc" // lalr1.cc:859
    break;

  case 207:
#line 433 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2259 "parser.tab.cc" // lalr1.cc:859
    break;

  case 208:
#line 434 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2265 "parser.tab.cc" // lalr1.cc:859
    break;

  case 209:
#line 436 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2271 "parser.tab.cc" // lalr1.cc:859
    break;

  case 210:
#line 437 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2277 "parser.tab.cc" // lalr1.cc:859
    break;

  case 211:
#line 438 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2283 "parser.tab.cc" // lalr1.cc:859
    break;

  case 212:
#line 439 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2289 "parser.tab.cc" // lalr1.cc:859
    break;

  case 213:
#line 440 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2295 "parser.tab.cc" // lalr1.cc:859
    break;

  case 214:
#line 442 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2301 "parser.tab.cc" // lalr1.cc:859
    break;

  case 215:
#line 443 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2307 "parser.tab.cc" // lalr1.cc:859
    break;

  case 216:
#line 444 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2313 "parser.tab.cc" // lalr1.cc:859
    break;

  case 217:
#line 446 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2319 "parser.tab.cc" // lalr1.cc:859
    break;

  case 218:
#line 447 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2325 "parser.tab.cc" // lalr1.cc:859
    break;

  case 219:
#line 448 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2331 "parser.tab.cc" // lalr1.cc:859
    break;

  case 220:
#line 449 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2337 "parser.tab.cc" // lalr1.cc:859
    break;

  case 221:
#line 451 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2343 "parser.tab.cc" // lalr1.cc:859
    break;

  case 222:
#line 452 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2349 "parser.tab.cc" // lalr1.cc:859
    break;

  case 223:
#line 453 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2355 "parser.tab.cc" // lalr1.cc:859
    break;

  case 224:
#line 454 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2361 "parser.tab.cc" // lalr1.cc:859
    break;

  case 225:
#line 455 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2367 "parser.tab.cc" // lalr1.cc:859
    break;

  case 226:
#line 457 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2373 "parser.tab.cc" // lalr1.cc:859
    break;

  case 227:
#line 458 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2379 "parser.tab.cc" // lalr1.cc:859
    break;

  case 228:
#line 459 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2385 "parser.tab.cc" // lalr1.cc:859
    break;

  case 229:
#line 461 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2391 "parser.tab.cc" // lalr1.cc:859
    break;

  case 230:
#line 462 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2397 "parser.tab.cc" // lalr1.cc:859
    break;

  case 231:
#line 464 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2403 "parser.tab.cc" // lalr1.cc:859
    break;

  case 232:
#line 465 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::UNARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2409 "parser.tab.cc" // lalr1.cc:859
    break;

  case 233:
#line 466 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::UNARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2415 "parser.tab.cc" // lalr1.cc:859
    break;

  case 234:
#line 467 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[1].value.as< yaoosl::compiler::cstnode > (); }
#line 2421 "parser.tab.cc" // lalr1.cc:859
    break;

  case 235:
#line 469 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2427 "parser.tab.cc" // lalr1.cc:859
    break;

  case 236:
#line 470 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2433 "parser.tab.cc" // lalr1.cc:859
    break;

  case 237:
#line 471 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2439 "parser.tab.cc" // lalr1.cc:859
    break;

  case 238:
#line 472 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2445 "parser.tab.cc" // lalr1.cc:859
    break;

  case 239:
#line 474 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ARRGET, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2451 "parser.tab.cc" // lalr1.cc:859
    break;

  case 240:
#line 476 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DOTNAV, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2457 "parser.tab.cc" // lalr1.cc:859
    break;

  case 241:
#line 478 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CALL, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2463 "parser.tab.cc" // lalr1.cc:859
    break;

  case 242:
#line 479 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CALL, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2469 "parser.tab.cc" // lalr1.cc:859
    break;

  case 243:
#line 481 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2475 "parser.tab.cc" // lalr1.cc:859
    break;

  case 244:
#line 482 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::THIS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2481 "parser.tab.cc" // lalr1.cc:859
    break;

  case 245:
#line 483 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::NEW,  yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2487 "parser.tab.cc" // lalr1.cc:859
    break;

  case 246:
#line 484 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2493 "parser.tab.cc" // lalr1.cc:859
    break;

  case 247:
#line 486 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2499 "parser.tab.cc" // lalr1.cc:859
    break;

  case 248:
#line 487 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2505 "parser.tab.cc" // lalr1.cc:859
    break;

  case 249:
#line 488 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2511 "parser.tab.cc" // lalr1.cc:859
    break;

  case 250:
#line 489 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2517 "parser.tab.cc" // lalr1.cc:859
    break;

  case 251:
#line 490 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2523 "parser.tab.cc" // lalr1.cc:859
    break;

  case 252:
#line 491 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2529 "parser.tab.cc" // lalr1.cc:859
    break;


#line 2533 "parser.tab.cc" // lalr1.cc:859
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


  const short int parser::yypact_ninf_ = -330;

  const short int parser::yytable_ninf_ = -171;

  const short int
  parser::yypact_[] =
  {
     106,  -330,  -330,  -330,  -330,   377,   -43,    85,    38,  -330,
      89,  -330,   106,    27,   107,  -330,  -330,  -330,   115,  -330,
      68,     0,  -330,  -330,   120,     0,     0,     0,  -330,   119,
    -330,   -43,   122,   218,   146,   123,     0,     0,   358,   134,
    -330,    39,    32,   200,   156,   164,   106,   205,   209,   214,
    -330,   223,  -330,     0,   230,   224,  -330,     0,     4,  -330,
     243,     2,   219,   260,     5,    79,   231,  -330,  -330,   263,
     264,    48,  -330,  -330,  -330,  -330,  -330,   256,  -330,    32,
    -330,   148,     0,  -330,  -330,   239,    86,   274,   200,   379,
    -330,  -330,  -330,   292,  -330,  -330,  -330,   290,   296,     6,
    -330,   300,   314,  -330,   344,   121,    62,  -330,     0,  -330,
     368,  -330,  -330,     0,   318,     0,  -330,  -330,  -330,   200,
     340,   311,  -330,  -330,  -330,  -330,  -330,  -330,   728,   276,
     200,   352,   356,   200,   284,  -330,  -330,     2,   415,  -330,
       0,  -330,  -330,  -330,   658,   658,   426,  -330,   658,  -330,
     386,   406,   409,   520,   410,   658,   658,   113,   520,  -330,
    -330,  -330,     0,   658,  -330,  -330,  -330,  -330,  -330,   473,
     383,  -330,  -330,  -330,  -330,   195,  -330,   417,  -330,  -330,
     418,  -330,  -330,     7,   456,   293,   315,    20,   334,   327,
     268,   457,  -330,    84,  -330,  -330,  -330,  -330,  -330,  -330,
    -330,   425,  -330,  -330,  -330,    67,  -330,   399,   399,  -330,
    -330,  -330,    67,  -330,   437,   401,  -330,   208,  -330,  -330,
    -330,  -330,  -330,  -330,  -330,  -330,  -330,  -330,  -330,  -330,
    -330,  -330,  -330,  -330,  -330,  -330,  -330,  -330,  -330,   439,
     442,   200,  -330,   446,   447,  -330,   200,   129,   271,   610,
     112,  -330,    67,  -330,  -330,   149,  -330,  -330,   454,   455,
     658,   614,   658,   440,   658,  -330,  -330,    67,  -330,  -330,
    -330,  -330,  -330,  -330,  -330,  -330,  -330,   567,   520,   441,
     448,  -330,  -330,  -330,   658,   658,   658,   658,   658,   658,
     658,   658,   658,   658,   658,   658,   658,   658,   658,   658,
     658,   658,   658,   658,   658,   658,   658,   157,   658,   431,
    -330,  -330,  -330,  -330,  -330,  -330,     0,   259,     0,     0,
    -330,     0,     0,  -330,   432,  -330,   200,   236,  -330,   463,
     477,  -330,   466,   480,  -330,  -330,  -330,   479,   629,   482,
     481,   483,   501,   484,   485,   493,   499,  -330,   658,  -330,
    -330,  -330,  -330,   456,   514,   293,   315,   315,   315,    20,
      20,    20,    20,   334,   334,   327,   327,   327,   268,   268,
     268,   268,   457,   457,  -330,  -330,  -330,   498,   183,   511,
    -330,   503,   515,   510,   518,   521,   522,  -330,   488,  -330,
    -330,  -330,   520,   658,   517,   520,   638,   520,   658,   658,
     520,   658,   528,   526,   658,  -330,  -330,   658,  -330,   200,
    -330,     0,  -330,  -330,   200,   527,   516,  -330,   658,  -330,
     658,   529,  -330,   500,  -330,  -330,   535,   301,   520,  -330,
    -330,  -330,   539,  -330,   200,   520,  -330,  -330,   658,   658,
    -330,   110,  -330,   550,  -330,  -330,  -330,  -330,  -330,  -330,
    -330,  -330,   332,  -330
  };

  const unsigned char
  parser::yydefact_[] =
  {
       2,    55,    56,    53,    54,     0,     0,     0,     0,    12,
       4,    14,     3,     0,    52,    10,    13,     9,     0,    11,
       0,     0,    52,    39,     0,     0,     0,     0,     1,     0,
      15,     0,     0,    44,     0,     0,     0,     0,     0,     0,
      76,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       5,    42,    80,     0,     0,    41,    84,     0,     0,    65,
      49,     0,     0,    59,     0,     0,     0,    22,    24,     0,
       0,     0,    23,    20,    16,    19,    17,     0,    18,     0,
      21,     0,     0,    78,    86,     0,     0,    90,     0,     0,
      82,    85,    40,     0,     8,     6,     7,     0,    43,     0,
      48,     0,     0,    67,    74,     0,    69,    50,     0,    77,
      57,    60,    63,     0,     0,     0,   133,    81,    25,     0,
      39,     0,   105,   107,   108,   106,   109,   104,     0,     0,
       0,     0,     0,     0,     0,   141,   134,     0,    93,    87,
       0,    91,    83,    36,     0,     0,     0,    88,     0,    35,
       0,     0,     0,     0,     0,   183,   185,     0,     0,   250,
     251,   244,     0,     0,   248,   247,   249,    27,    37,     0,
     252,    26,    28,    30,    31,     0,    29,     0,   246,    33,
       0,   194,   193,   199,   201,   203,   205,   209,   214,   217,
     221,   226,   229,   231,   238,   237,   236,   235,   243,    64,
      46,     0,    62,    45,    66,     0,    68,     0,     0,    71,
      70,    51,     0,    61,     0,    99,    95,    84,   126,   125,
     113,   112,   130,   128,   127,   129,   110,   111,   119,   118,
     120,   121,   115,   114,   116,   117,   123,   122,   124,     0,
       0,     0,    97,     0,     0,   131,     0,     0,     0,     0,
       0,    79,     0,    92,   233,   231,   232,   191,     0,     0,
       0,     0,     0,     0,     0,   184,   186,     0,   173,   188,
     189,   174,   245,   187,    89,    38,   190,     0,     0,   177,
     181,   182,    32,    34,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
      47,   252,    75,    73,    72,    58,     0,    85,     0,     0,
      98,     0,     0,   146,     0,   144,     0,     0,   140,     0,
       0,   138,     0,     0,    94,   192,   234,     0,     0,     0,
       0,     0,   246,   160,     0,     0,     0,   172,     0,   175,
     179,   178,   180,   202,     0,   204,   206,   207,   208,   211,
     210,   213,   212,   215,   216,   219,   218,   220,   222,   223,
     224,   225,   228,   227,   230,   195,   242,   196,     0,     0,
     240,     0,     0,     0,     0,     0,     0,   147,     0,   145,
     139,   137,     0,   159,     0,     0,     0,     0,     0,   161,
       0,     0,     0,   246,     0,   197,   241,     0,   239,     0,
     100,     0,   101,   102,     0,     0,   148,   158,   157,   150,
     155,     0,   151,   163,   162,   165,     0,     0,     0,   200,
     198,    96,     0,   142,     0,     0,   156,   154,   153,     0,
     166,     0,   168,     0,   176,   103,   143,   149,   152,   164,
     167,   169,     0,   171
  };

  const short int
  parser::yypgoto_[] =
  {
    -330,  -330,    19,  -330,     9,  -330,   531,  -330,  -142,  -330,
      23,   -13,    69,    13,    -3,   467,  -330,   538,    -5,  -330,
      52,   505,  -330,   194,    59,  -330,   -29,    63,  -330,   -16,
     -85,  -330,  -135,  -330,  -330,  -330,   461,  -330,   458,  -330,
    -330,  -330,  -330,  -330,  -330,   351,   353,  -330,  -330,  -330,
    -329,  -330,  -330,  -330,  -330,   167,   452,  -330,  -330,   326,
     331,  -330,  -330,  -244,  -330,   -82,  -330,  -330,  -284,  -330,
     328,   333,   125,   138,   126,    76,   171,   135,  -138,   -84,
    -330,  -330,  -330,  -330,  -175
  };

  const short int
  parser::yydefgoto_[] =
  {
      -1,     8,   167,    10,    11,    12,    68,    69,   168,   169,
      33,   170,    61,    13,    14,    63,    64,    55,    39,    15,
      16,    59,   105,   106,    17,    18,    40,    19,    20,    43,
      90,    86,    87,    75,    76,    77,   130,   131,   132,   240,
      78,    79,    80,    81,   136,   249,   250,   171,   172,   339,
     340,   341,   173,   174,   441,   442,   443,   175,   279,   280,
     281,   176,   177,   178,   179,   180,   181,   378,   182,   183,
     184,   185,   186,   187,   188,   189,   190,   191,   192,   193,
     194,   195,   196,   197,   198
  };

  const short int
  parser::yytable_[] =
  {
      34,   354,    22,   142,   258,   253,   254,   256,    44,   394,
      83,   263,    47,    48,    49,    42,   271,   342,    21,     9,
      31,    30,   284,    60,    62,    70,    88,   275,    85,    24,
     312,     9,   109,    23,   216,   112,   202,   315,    28,    38,
      60,   294,   103,   295,   102,   242,   108,    31,   245,   113,
     108,    71,   114,   285,    51,    93,    70,    67,   121,    31,
     255,   255,    22,   133,   417,     9,   259,   421,    31,    60,
     424,    41,   129,   265,   266,    32,    23,   334,   115,    84,
     104,   273,    71,     1,     2,     3,     4,    31,    67,   436,
      72,   437,   347,   306,    37,   211,    32,    73,    29,    31,
      62,    74,   215,    23,   403,   209,   210,    41,   251,   448,
       1,     2,     3,     4,     5,    23,     1,     2,     3,     4,
     429,    72,    99,   307,   120,   308,   139,    85,    73,   309,
     140,    22,    74,    25,    36,   349,   350,    26,   159,   160,
      45,    37,    57,    23,   164,   165,   166,   248,   450,   272,
     331,   137,    38,    82,    27,    23,   320,    46,     6,   206,
      58,   323,   325,   247,   207,   208,    89,   374,   324,   267,
     268,    38,   267,   268,     7,   144,    35,    31,   337,   343,
     344,   381,   346,   382,   383,   134,   384,   385,   307,   269,
     308,   135,   311,   145,   309,    50,   148,   376,    52,   311,
     255,   255,   255,   255,   255,   255,   255,   255,   255,   255,
     255,   255,   255,   255,   255,   255,   255,   255,   255,   255,
     255,   255,    56,   406,   375,   377,   379,   407,   159,   160,
     161,   162,    91,    23,   164,   165,   166,    89,    45,   311,
      92,   387,   389,    45,    53,  -135,    22,    22,    94,    53,
     416,  -135,    95,   419,   311,   422,   343,    96,   425,    54,
     277,   278,   329,   332,    97,   101,   259,     1,     2,     3,
       4,    65,   100,    89,   122,   388,   432,   123,   124,   125,
     126,    66,    37,    31,   303,   304,   444,   107,     1,     2,
       3,     4,   127,   447,   122,   110,  -136,   123,   124,   125,
     126,   117,  -136,    85,   111,    85,    85,   116,    85,    85,
     453,   343,   127,   119,   343,   138,   423,   343,   141,   426,
     255,   326,   327,   128,   431,   430,   287,   288,   289,   433,
     199,     7,   200,   143,   246,   247,   343,   201,   343,    23,
     290,   291,   203,   128,   292,   293,   296,   297,   298,   446,
     144,    58,    31,   205,   299,   300,   343,   449,   301,   302,
     267,   268,     1,     2,     3,     4,    65,   214,   145,   146,
    -170,   148,   365,   366,   367,   149,    66,   212,    31,  -132,
     143,     1,     2,     3,     4,   150,   151,   217,   152,   153,
     154,  -170,  -170,   155,   156,   157,   158,   144,    85,    31,
       7,   313,   314,   159,   160,   161,   162,   163,    23,   164,
     165,   166,   356,   357,   358,   145,   146,   147,   148,   243,
     363,   364,   149,   244,   252,   260,     7,   143,   359,   360,
     361,   362,   150,   151,    23,   152,   153,   154,   372,   373,
     155,   156,   157,   158,   144,   261,    31,     7,   262,   264,
     159,   160,   161,   162,   163,    23,   164,   165,   166,   276,
     282,   283,   145,   146,   257,   148,   286,   310,   305,   149,
     368,   369,   370,   371,   143,   104,   316,   317,   318,   150,
     151,   319,   152,   153,   154,   321,   322,   155,   156,   157,
     158,   144,   335,    31,     7,   336,   345,   159,   160,   161,
     162,   163,    23,   164,   165,   166,   277,   380,   386,   145,
     146,   274,   148,   326,   278,   390,   149,   327,   391,   392,
     398,   143,   395,   397,   396,   400,   150,   151,   399,   152,
     153,   154,   401,   404,   155,   156,   157,   158,   144,   402,
      31,     7,   405,   409,   159,   160,   161,   162,   163,    23,
     164,   165,   166,   408,   411,   410,   145,   146,   412,   148,
     418,   413,   414,   149,   415,   427,   428,   434,   143,   452,
     439,   435,   438,   150,   151,   440,   152,   153,   154,   445,
     213,   155,   156,   157,   158,   144,   239,    31,     7,    98,
     241,   159,   160,   161,   162,   163,    23,   164,   165,   166,
     118,   333,   330,   145,   146,   351,   348,   204,   451,   270,
     149,   352,   353,     0,     1,     2,     3,     4,     0,   355,
     150,   151,     0,   152,   153,   154,     0,     0,   155,   156,
     157,   158,   144,     0,    31,     7,     0,     0,   159,   160,
     161,   162,   163,    23,   164,   165,   166,   144,   328,    31,
     145,     0,     0,   148,     0,     0,   144,   338,    31,     0,
     246,     0,     0,     0,     0,   145,     0,     0,   148,     0,
       0,     0,   393,     0,   145,     0,   144,   148,    31,     0,
       0,   420,     0,     0,     0,   159,   160,   161,   162,     0,
      23,   164,   165,   166,   145,     0,     0,   148,     0,     0,
     159,   160,   161,   162,     0,    23,   164,   165,   166,   159,
     160,   161,   162,     0,    23,   164,   165,   166,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,   159,
     160,   161,   162,     0,    23,   164,   165,   166,   218,   219,
     220,   221,   222,   223,   224,   225,     0,     0,     0,   226,
     124,   227,   126,   228,   229,   230,   231,   232,   233,   234,
     235,   236,     0,   237,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,   238
  };

  const short int
  parser::yycheck_[] =
  {
      13,   285,     5,    88,   146,   140,   144,   145,    21,   338,
      39,   153,    25,    26,    27,    20,   158,   261,     5,     0,
      20,    12,    15,    36,    37,    38,    42,   169,    41,     6,
     205,    12,    61,    76,   119,    30,    30,   212,     0,    37,
      53,    21,    38,    23,    57,   130,    44,    20,   133,    44,
      44,    38,    65,    46,    31,    46,    69,    38,    71,    20,
     144,   145,    65,    79,   393,    46,   148,   396,    20,    82,
     399,    39,    77,   155,   156,    48,    76,   252,    65,    40,
      76,   163,    69,     4,     5,     6,     7,    20,    69,   418,
      38,   420,   267,     9,    26,   108,    48,    38,     9,    20,
     113,    38,   115,    76,   348,    43,    44,    39,   137,   438,
       4,     5,     6,     7,     8,    76,     4,     5,     6,     7,
     404,    69,    53,    39,    76,    41,    40,   140,    69,    45,
      44,   134,    69,    48,    19,   277,   278,    52,    71,    72,
      20,    26,    19,    76,    77,    78,    79,   134,    38,   162,
      38,    82,    37,    19,    69,    76,   241,    37,    52,    38,
      37,   246,   247,    51,    43,    44,    37,   305,    39,    59,
      60,    37,    59,    60,    68,    18,    69,    20,   260,   261,
     262,   316,   264,   318,   319,    37,   321,   322,    39,    76,
      41,    43,   205,    36,    45,    76,    39,    40,    76,   212,
     284,   285,   286,   287,   288,   289,   290,   291,   292,   293,
     294,   295,   296,   297,   298,   299,   300,   301,   302,   303,
     304,   305,    76,    40,   306,   307,   308,    44,    71,    72,
      73,    74,    76,    76,    77,    78,    79,    37,    20,   252,
      76,   326,   327,    20,    26,    37,   249,   250,    43,    26,
     392,    43,    43,   395,   267,   397,   338,    43,   400,    41,
      65,    66,   249,   250,    41,    41,   348,     4,     5,     6,
       7,     8,    42,    37,    18,    39,   411,    21,    22,    23,
      24,    18,    26,    20,    16,    17,   428,    44,     4,     5,
       6,     7,    36,   435,    18,    76,    37,    21,    22,    23,
      24,    38,    43,   316,    44,   318,   319,    76,   321,   322,
     452,   393,    36,    49,   396,    76,   398,   399,    44,   401,
     404,    50,    51,    67,   409,   407,    33,    34,    35,   414,
      38,    68,    42,     1,    50,    51,   418,    41,   420,    76,
      25,    26,    42,    67,    29,    30,    12,    13,    14,   434,
      18,    37,    20,     9,    27,    28,   438,   439,    31,    32,
      59,    60,     4,     5,     6,     7,     8,    49,    36,    37,
      38,    39,   296,   297,   298,    43,    18,     9,    20,    39,
       1,     4,     5,     6,     7,    53,    54,    76,    56,    57,
      58,    59,    60,    61,    62,    63,    64,    18,   411,    20,
      68,   207,   208,    71,    72,    73,    74,    75,    76,    77,
      78,    79,   287,   288,   289,    36,    37,    38,    39,    67,
     294,   295,    43,    67,     9,    39,    68,     1,   290,   291,
     292,   293,    53,    54,    76,    56,    57,    58,   303,   304,
      61,    62,    63,    64,    18,    39,    20,    68,    39,    39,
      71,    72,    73,    74,    75,    76,    77,    78,    79,    76,
      43,    43,    36,    37,    38,    39,    10,    42,    11,    43,
     299,   300,   301,   302,     1,    76,    39,    76,    39,    53,
      54,    39,    56,    57,    58,    39,    39,    61,    62,    63,
      64,    18,    38,    20,    68,    40,    56,    71,    72,    73,
      74,    75,    76,    77,    78,    79,    65,    76,    76,    36,
      37,    38,    39,    50,    66,    38,    43,    51,    38,    40,
      19,     1,    40,    40,    43,    40,    53,    54,    44,    56,
      57,    58,    39,    19,    61,    62,    63,    64,    18,    40,
      20,    68,    44,    40,    71,    72,    73,    74,    75,    76,
      77,    78,    79,    42,    44,    40,    36,    37,    40,    39,
      43,    40,    40,    43,    76,    37,    40,    40,     1,    19,
      70,    55,    43,    53,    54,    40,    56,    57,    58,    40,
     113,    61,    62,    63,    64,    18,   128,    20,    68,    51,
     129,    71,    72,    73,    74,    75,    76,    77,    78,    79,
      69,   250,   249,    36,    37,   279,    39,   102,   441,   157,
      43,   280,   284,    -1,     4,     5,     6,     7,    -1,   286,
      53,    54,    -1,    56,    57,    58,    -1,    -1,    61,    62,
      63,    64,    18,    -1,    20,    68,    -1,    -1,    71,    72,
      73,    74,    75,    76,    77,    78,    79,    18,    38,    20,
      36,    -1,    -1,    39,    -1,    -1,    18,    43,    20,    -1,
      50,    -1,    -1,    -1,    -1,    36,    -1,    -1,    39,    -1,
      -1,    -1,    43,    -1,    36,    -1,    18,    39,    20,    -1,
      -1,    43,    -1,    -1,    -1,    71,    72,    73,    74,    -1,
      76,    77,    78,    79,    36,    -1,    -1,    39,    -1,    -1,
      71,    72,    73,    74,    -1,    76,    77,    78,    79,    71,
      72,    73,    74,    -1,    76,    77,    78,    79,    -1,    -1,
      -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    71,
      72,    73,    74,    -1,    76,    77,    78,    79,    10,    11,
      12,    13,    14,    15,    16,    17,    -1,    -1,    -1,    21,
      22,    23,    24,    25,    26,    27,    28,    29,    30,    31,
      32,    33,    -1,    35,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    70
  };

  const unsigned char
  parser::yystos_[] =
  {
       0,     4,     5,     6,     7,     8,    52,    68,    81,    82,
      83,    84,    85,    93,    94,    99,   100,   104,   105,   107,
     108,    93,    94,    76,    90,    48,    52,    69,     0,     9,
      84,    20,    48,    90,    91,    69,    19,    26,    37,    98,
     106,    39,    98,   109,    91,    20,    37,    91,    91,    91,
      76,    90,    76,    26,    41,    97,    76,    19,    37,   101,
      91,    92,    91,    95,    96,     8,    18,    82,    86,    87,
      91,    93,   100,   104,   107,   113,   114,   115,   120,   121,
     122,   123,    19,   106,    40,    91,   111,   112,   109,    37,
     110,    76,    76,    84,    43,    43,    43,    41,    97,    92,
      42,    41,    91,    38,    76,   102,   103,    44,    44,   106,
      76,    44,    30,    44,    91,    93,    76,    38,    86,    49,
      76,    91,    18,    21,    22,    23,    24,    36,    67,    98,
     116,   117,   118,   109,    37,    43,   124,    92,    76,    40,
      44,    44,   110,     1,    18,    36,    37,    38,    39,    43,
      53,    54,    56,    57,    58,    61,    62,    63,    64,    71,
      72,    73,    74,    75,    77,    78,    79,    82,    88,    89,
      91,   127,   128,   132,   133,   137,   141,   142,   143,   144,
     145,   146,   148,   149,   150,   151,   152,   153,   154,   155,
     156,   157,   158,   159,   160,   161,   162,   163,   164,    38,
      42,    41,    30,    42,   101,     9,    38,    43,    44,    43,
      44,    91,     9,    95,    49,    91,   110,    76,    10,    11,
      12,    13,    14,    15,    16,    17,    21,    23,    25,    26,
      27,    28,    29,    30,    31,    32,    33,    35,    70,   118,
     119,   116,   110,    67,    67,   110,    50,    51,    93,   125,
     126,   106,     9,   112,   158,   159,   158,    38,    88,   145,
      39,    39,    39,    88,    39,   145,   145,    59,    60,    76,
     136,    88,    91,   145,    38,    88,    76,    65,    66,   138,
     139,   140,    43,    43,    15,    46,    10,    33,    34,    35,
      25,    26,    29,    30,    21,    23,    12,    13,    14,    27,
      28,    31,    32,    16,    17,    11,     9,    39,    41,    45,
      42,    91,   164,   103,   103,   164,    39,    76,    39,    39,
     110,    39,    39,   110,    39,   110,    50,    51,    38,    93,
     126,    38,    93,   125,   164,    38,    40,   145,    43,   129,
     130,   131,   143,   145,   145,    56,   145,   164,    39,    88,
      88,   139,   140,   150,   148,   151,   152,   152,   152,   153,
     153,   153,   153,   154,   154,   155,   155,   155,   156,   156,
     156,   156,   157,   157,   158,   145,    40,   145,   147,   145,
      76,   112,   112,   112,   112,   112,    76,   110,    39,   110,
      38,    38,    40,    43,   130,    40,    43,    40,    19,    44,
      40,    39,    40,   143,    19,    44,    40,    44,    42,    40,
      40,    44,    40,    40,    40,    76,    88,   130,    43,    88,
      43,   130,    88,   145,   130,    88,   145,    37,    40,   148,
     145,   110,   112,   110,    40,    55,   130,   130,    43,    70,
      40,   134,   135,   136,    88,    40,   110,    88,   130,   145,
      38,   135,    19,    88
  };

  const unsigned char
  parser::yyr1_[] =
  {
       0,    80,    81,    81,    82,    82,    83,    83,    83,    84,
      84,    84,    84,    84,    85,    85,    86,    86,    86,    86,
      86,    86,    86,    86,    87,    87,    88,    88,    88,    88,
      88,    88,    88,    88,    88,    88,    88,    89,    89,    90,
      90,    91,    91,    91,    91,    91,    91,    91,    91,    92,
      92,    92,    93,    93,    93,    94,    94,    95,    95,    96,
      96,    96,    97,    98,    99,   100,   100,   101,   101,   102,
     102,   102,   102,   102,   103,   103,   104,   104,   104,   104,
     105,   106,   107,   107,   108,   108,   109,   109,   110,   110,
     111,   111,   111,   112,   112,   113,   113,   114,   114,   115,
     116,   116,   116,   116,   117,   117,   117,   117,   118,   118,
     119,   119,   119,   119,   119,   119,   119,   119,   119,   119,
     119,   119,   119,   119,   119,   119,   119,   119,   119,   119,
     119,   120,   121,   121,   122,   123,   123,   124,   124,   124,
     124,   124,   125,   125,   125,   125,   126,   126,   127,   127,
     128,   128,   129,   129,   129,   129,   129,   129,   129,   129,
     130,   130,   130,   131,   131,   132,   132,   133,   134,   134,
     135,   135,   136,   136,   137,   138,   138,   139,   139,   140,
     141,   141,   141,   142,   142,   142,   142,   142,   142,   142,
     143,   144,   144,   145,   145,   146,   147,   147,   147,   148,
     148,   149,   149,   150,   150,   151,   151,   151,   151,   152,
     152,   152,   152,   152,   153,   153,   153,   154,   154,   154,
     154,   155,   155,   155,   155,   155,   156,   156,   156,   157,
     157,   158,   158,   158,   158,   159,   159,   159,   159,   160,
     161,   162,   162,   163,   163,   163,   163,   164,   164,   164,
     164,   164,   164
  };

  const unsigned char
  parser::yyr2_[] =
  {
       0,     2,     0,     1,     1,     3,     4,     4,     4,     1,
       1,     1,     1,     1,     1,     2,     1,     1,     1,     1,
       1,     1,     1,     1,     1,     2,     1,     1,     1,     1,
       1,     1,     2,     1,     2,     1,     1,     1,     2,     1,
       3,     2,     2,     3,     1,     4,     4,     5,     3,     1,
       2,     3,     1,     1,     1,     1,     1,     2,     4,     1,
       2,     3,     3,     3,     5,     3,     5,     2,     3,     1,
       2,     2,     3,     3,     1,     3,     2,     4,     3,     5,
       3,     3,     3,     4,     3,     4,     2,     3,     2,     3,
       1,     2,     3,     2,     4,     3,     7,     3,     4,     3,
       5,     5,     5,     7,     1,     1,     1,     1,     1,     1,
       1,     1,     1,     1,     1,     1,     1,     1,     1,     1,
       1,     1,     1,     1,     1,     1,     1,     1,     1,     1,
       1,     3,     2,     2,     2,     3,     4,     4,     3,     4,
       3,     1,     5,     6,     2,     3,     2,     3,     5,     7,
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
  "$end", "error", "$undefined", "NA", "\"public\"", "\"local\"",
  "\"derived\"", "\"private\"", "\"unbound\"", "\"=\"", "\"&&\"", "\"&\"",
  "\"/\"", "\"*\"", "\"%\"", "\"||\"", "\"|\"", "\"^\"", "\"~\"", "\":\"",
  "\"::\"", "\"+\"", "\"++\"", "\"-\"", "\"--\"", "\"<=\"", "\"<\"",
  "\"<<\"", "\"<<<\"", "\">=\"", "\">\"", "\">>\"", "\">>>\"", "\"==\"",
  "\"~=\"", "\"!=\"", "\"!\"", "\"{\"", "\"}\"", "\"(\"", "\")\"", "\"[\"",
  "\"]\"", "\";\"", "\",\"", "\".\"", "\"?\"", "\"=>\"", "\"class\"",
  "\"conversion\"", "\"get\"", "\"set\"", "\"namespace\"", "\"if\"",
  "\"for\"", "\"else\"", "\"while\"", "\"do\"", "\"switch\"", "\"case\"",
  "\"default\"", "\"return\"", "\"throw\"", "\"goto\"", "\"try\"",
  "\"catch\"", "\"finally\"", "\"operator\"", "\"using\"", "\"enum\"",
  "\"..\"", "\"true\"", "\"false\"", "\"this\"", "\"new\"", "\"delete\"",
  "L_IDENT", "L_STRING", "L_NUMBER", "L_CHAR", "$accept", "start", "using",
  "using_low", "filestmnt", "filestmnts", "classstmnt", "classstmnts",
  "codestmnt", "codestmnts", "type_ident", "type", "typelist", "encpsltn",
  "encpsltn_n_cls", "template_def", "template_defs", "template_use",
  "template", "namespace", "enum", "enum_body", "enum_values",
  "enum_value", "class", "classhead", "classbody", "mthd", "mthd_head",
  "mthd_args", "mthd_body", "mthd_arglist", "mthd_arg", "cnvrsn", "mthdop",
  "mthdop_head", "mthdop_args", "mthdop_ops1p", "mthdop_ops1s",
  "mthdop_ops2", "mthdcnst", "mthdcnst_head", "prop", "prop_head",
  "prop_body", "prop_set", "prop_get", "ifelse", "for", "for_step",
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
       0,   141,   141,   142,   144,   145,   147,   148,   149,   151,
     152,   153,   154,   155,   157,   158,   160,   161,   162,   163,
     164,   165,   166,   167,   169,   170,   172,   173,   174,   175,
     176,   177,   178,   179,   180,   181,   182,   184,   185,   188,
     189,   191,   192,   193,   194,   195,   196,   197,   198,   200,
     201,   202,   205,   206,   207,   209,   210,   213,   214,   216,
     217,   218,   220,   222,   225,   228,   229,   231,   232,   234,
     235,   236,   237,   238,   240,   241,   244,   245,   246,   247,
     249,   251,   254,   255,   257,   258,   260,   261,   263,   264,
     266,   267,   268,   270,   271,   274,   275,   278,   279,   281,
     283,   284,   285,   286,   288,   289,   290,   291,   293,   294,
     296,   297,   298,   299,   300,   301,   302,   303,   304,   305,
     306,   307,   308,   309,   310,   311,   312,   313,   314,   315,
     316,   319,   321,   322,   325,   327,   328,   330,   331,   332,
     333,   334,   336,   337,   338,   339,   341,   342,   345,   346,
     349,   350,   352,   353,   354,   355,   356,   357,   358,   359,
     361,   362,   363,   365,   366,   369,   370,   373,   375,   376,
     378,   379,   381,   382,   385,   387,   388,   390,   391,   393,
     395,   396,   397,   400,   401,   402,   403,   404,   405,   406,
     408,   410,   411,   413,   414,   416,   418,   419,   420,   422,
     423,   425,   426,   428,   429,   431,   432,   433,   434,   436,
     437,   438,   439,   440,   442,   443,   444,   446,   447,   448,
     449,   451,   452,   453,   454,   455,   457,   458,   459,   461,
     462,   464,   465,   466,   467,   469,   470,   471,   472,   474,
     476,   478,   479,   481,   482,   483,   484,   486,   487,   488,
     489,   490,   491
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
#line 3306 "parser.tab.cc" // lalr1.cc:1167
#line 493 "parser.y" // lalr1.cc:1168



namespace yaoosl::compiler
{
     void parser::error(const location_type& loc, const std::string& msg)
     {
         std::cout <<
             tokenizer.create_token().path << std::endl <<
             "[L" << loc.begin.line << "|C" << loc.begin.column << "]  " << msg << std::endl;
     }
     inline parser::symbol_type yylex(yaoosl::compiler::tokenizer& tokenizer)
     {
         auto token = tokenizer.next();
         parser::location_type loc;
         loc.begin.line = token.line;
         loc.begin.column = token.column;
         loc.end.line = token.line;
         loc.end.column = token.column + token.contents.length();

         switch (token.type)
         {
         case tokenizer::etoken::eof: return parser::make_NA(token, loc);
         case tokenizer::etoken::invalid: return parser::make_NA(token, loc);


         case tokenizer::etoken::m_line: return yylex(tokenizer);

         case tokenizer::etoken::i_comment_line: return yylex(tokenizer);
         case tokenizer::etoken::i_comment_block: return yylex(tokenizer);
         case tokenizer::etoken::i_whitespace: return yylex(tokenizer);



         case tokenizer::etoken::l_ident: return parser::make_L_IDENT(token, loc);
         case tokenizer::etoken::l_string: return parser::make_L_STRING(token, loc);
         case tokenizer::etoken::l_number: return parser::make_L_NUMBER(token, loc);
         case tokenizer::etoken::l_char: return parser::make_L_CHAR(token, loc);

         case tokenizer::etoken::t_case: return parser::make_CASE(token, loc);
         case tokenizer::etoken::t_catch: return parser::make_CATCH(token, loc);
         case tokenizer::etoken::t_class: return parser::make_CLASS(token, loc);
         case tokenizer::etoken::t_conversion: return parser::make_CONVERSION(token, loc);
         case tokenizer::etoken::t_default: return parser::make_DEFAULT(token, loc);
         case tokenizer::etoken::t_delete: return parser::make_DELETE(token, loc);
         case tokenizer::etoken::t_derived: return parser::make_DERIVED(token, loc);
         case tokenizer::etoken::t_do: return parser::make_DO(token, loc);
         case tokenizer::etoken::t_else: return parser::make_ELSE(token, loc);
         case tokenizer::etoken::t_enum: return parser::make_ENUM(token, loc);
         case tokenizer::etoken::t_false: return parser::make_FALSE(token, loc);
         case tokenizer::etoken::t_finally: return parser::make_FINALLY(token, loc);
         case tokenizer::etoken::t_for: return parser::make_FOR(token, loc);
         case tokenizer::etoken::t_get: return parser::make_GET(token, loc);
         case tokenizer::etoken::t_goto: return parser::make_GOTO(token, loc);
         case tokenizer::etoken::t_if: return parser::make_IF(token, loc);
         case tokenizer::etoken::t_local: return parser::make_LOCAL(token, loc);
         case tokenizer::etoken::t_namespace: return parser::make_NAMESPACE(token, loc);
         case tokenizer::etoken::t_new: return parser::make_NEW(token, loc);
         case tokenizer::etoken::t_operator: return parser::make_OPERATOR(token, loc);
         case tokenizer::etoken::t_private: return parser::make_PRIVATE(token, loc);
         case tokenizer::etoken::t_public: return parser::make_PUBLIC(token, loc);
         case tokenizer::etoken::t_return: return parser::make_RETURN(token, loc);
         case tokenizer::etoken::t_set: return parser::make_SET(token, loc);
         case tokenizer::etoken::t_switch: return parser::make_SWITCH(token, loc);
         case tokenizer::etoken::t_this: return parser::make_THIS(token, loc);
         case tokenizer::etoken::t_throw: return parser::make_THROW(token, loc);
         case tokenizer::etoken::t_true: return parser::make_TRUE(token, loc);
         case tokenizer::etoken::t_try: return parser::make_TRY(token, loc);
         case tokenizer::etoken::t_unbound: return parser::make_UNBOUND(token, loc);
         case tokenizer::etoken::t_using: return parser::make_USING(token, loc);
         case tokenizer::etoken::t_while: return parser::make_WHILE(token, loc);

         case tokenizer::etoken::s_and: return parser::make_AND(token, loc);
         case tokenizer::etoken::s_andand: return parser::make_ANDAND(token, loc);
         case tokenizer::etoken::s_arrowhead: return parser::make_ARROWHEAD(token, loc);
         case tokenizer::etoken::s_circumflex: return parser::make_CIRCUMFLEX(token, loc);
         case tokenizer::etoken::s_colon: return parser::make_COLON(token, loc);
         case tokenizer::etoken::s_coloncolon: return parser::make_COLONCOLON(token, loc);
         case tokenizer::etoken::s_comma: return parser::make_COMMA(token, loc);
         case tokenizer::etoken::s_curlyc: return parser::make_CURLYC(token, loc);
         case tokenizer::etoken::s_curlyo: return parser::make_CURLYO(token, loc);
         case tokenizer::etoken::s_dot: return parser::make_DOT(token, loc);
         case tokenizer::etoken::s_equal: return parser::make_EQUAL(token, loc);
         case tokenizer::etoken::s_equalequal: return parser::make_EQUALEQUAL(token, loc);
         case tokenizer::etoken::s_exclamationmarkequal: return parser::make_EXCLAMATIONMARKEQUAL(token, loc);
         case tokenizer::etoken::s_exclamationmark: return parser::make_EXCLAMATIONMARK(token, loc);
         case tokenizer::etoken::s_gt: return parser::make_GT(token, loc);
         case tokenizer::etoken::s_gtequal: return parser::make_GTEQUAL(token, loc);
         case tokenizer::etoken::s_gtgt: return parser::make_GTGT(token, loc);
         case tokenizer::etoken::s_gtgtgt: return parser::make_GTGTGT(token, loc);
         case tokenizer::etoken::s_lt: return parser::make_LT(token, loc);
         case tokenizer::etoken::s_ltequal: return parser::make_LTEQUAL(token, loc);
         case tokenizer::etoken::s_ltlt: return parser::make_LTLT(token, loc);
         case tokenizer::etoken::s_ltltlt: return parser::make_LTLTLT(token, loc);
         case tokenizer::etoken::s_minus: return parser::make_MINUS(token, loc);
         case tokenizer::etoken::s_minusminus: return parser::make_MINUSMINUS(token, loc);
         case tokenizer::etoken::s_percent: return parser::make_PERCENT(token, loc);
         case tokenizer::etoken::s_plus: return parser::make_PLUS(token, loc);
         case tokenizer::etoken::s_plusplus: return parser::make_PLUSPLUS(token, loc);
         case tokenizer::etoken::s_questionmark: return parser::make_QUESTIONMARK(token, loc);
         case tokenizer::etoken::s_roundc: return parser::make_ROUNDC(token, loc);
         case tokenizer::etoken::s_roundo: return parser::make_ROUNDO(token, loc);
         case tokenizer::etoken::s_semicolon: return parser::make_SEMICOLON(token, loc);
         case tokenizer::etoken::s_slash: return parser::make_SLASH(token, loc);
         case tokenizer::etoken::s_squarec: return parser::make_SQUAREC(token, loc);
         case tokenizer::etoken::s_squareo: return parser::make_SQUAREO(token, loc);
         case tokenizer::etoken::s_star: return parser::make_STAR(token, loc);
         case tokenizer::etoken::s_tilde: return parser::make_TILDE(token, loc);
         case tokenizer::etoken::s_tildeequal: return parser::make_TILDEEQUAL(token, loc);
         case tokenizer::etoken::s_vline: return parser::make_VLINE(token, loc);
         case tokenizer::etoken::s_vlinevline: return parser::make_VLINEVLINE(token, loc);

         default:
             return parser::make_NA(token, loc);
         }
     }
}
