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
#line 143 "parser.y" // lalr1.cc:859
    { result = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::START, tokenizer.create_token(), {} }; }
#line 1041 "parser.tab.cc" // lalr1.cc:859
    break;

  case 3:
#line 144 "parser.y" // lalr1.cc:859
    { result = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::START, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1047 "parser.tab.cc" // lalr1.cc:859
    break;

  case 4:
#line 146 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[1].value.as< yaoosl::compiler::cstnode > (); }
#line 1053 "parser.tab.cc" // lalr1.cc:859
    break;

  case 5:
#line 147 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1059 "parser.tab.cc" // lalr1.cc:859
    break;

  case 6:
#line 149 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1065 "parser.tab.cc" // lalr1.cc:859
    break;

  case 7:
#line 150 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1071 "parser.tab.cc" // lalr1.cc:859
    break;

  case 8:
#line 151 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::USING_LOW, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1077 "parser.tab.cc" // lalr1.cc:859
    break;

  case 9:
#line 153 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1083 "parser.tab.cc" // lalr1.cc:859
    break;

  case 10:
#line 154 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1089 "parser.tab.cc" // lalr1.cc:859
    break;

  case 11:
#line 155 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1095 "parser.tab.cc" // lalr1.cc:859
    break;

  case 12:
#line 156 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1101 "parser.tab.cc" // lalr1.cc:859
    break;

  case 13:
#line 157 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1107 "parser.tab.cc" // lalr1.cc:859
    break;

  case 14:
#line 158 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1113 "parser.tab.cc" // lalr1.cc:859
    break;

  case 15:
#line 160 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1119 "parser.tab.cc" // lalr1.cc:859
    break;

  case 16:
#line 161 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FILESTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1125 "parser.tab.cc" // lalr1.cc:859
    break;

  case 17:
#line 163 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1131 "parser.tab.cc" // lalr1.cc:859
    break;

  case 18:
#line 164 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1137 "parser.tab.cc" // lalr1.cc:859
    break;

  case 19:
#line 165 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1143 "parser.tab.cc" // lalr1.cc:859
    break;

  case 20:
#line 166 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1149 "parser.tab.cc" // lalr1.cc:859
    break;

  case 21:
#line 167 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1155 "parser.tab.cc" // lalr1.cc:859
    break;

  case 22:
#line 168 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1161 "parser.tab.cc" // lalr1.cc:859
    break;

  case 23:
#line 169 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1167 "parser.tab.cc" // lalr1.cc:859
    break;

  case 24:
#line 170 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1173 "parser.tab.cc" // lalr1.cc:859
    break;

  case 25:
#line 172 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1179 "parser.tab.cc" // lalr1.cc:859
    break;

  case 26:
#line 173 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSSTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1185 "parser.tab.cc" // lalr1.cc:859
    break;

  case 27:
#line 175 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1191 "parser.tab.cc" // lalr1.cc:859
    break;

  case 28:
#line 176 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1197 "parser.tab.cc" // lalr1.cc:859
    break;

  case 29:
#line 177 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1203 "parser.tab.cc" // lalr1.cc:859
    break;

  case 30:
#line 178 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1209 "parser.tab.cc" // lalr1.cc:859
    break;

  case 31:
#line 179 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1215 "parser.tab.cc" // lalr1.cc:859
    break;

  case 32:
#line 180 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1221 "parser.tab.cc" // lalr1.cc:859
    break;

  case 33:
#line 181 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1227 "parser.tab.cc" // lalr1.cc:859
    break;

  case 34:
#line 182 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1233 "parser.tab.cc" // lalr1.cc:859
    break;

  case 35:
#line 183 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1239 "parser.tab.cc" // lalr1.cc:859
    break;

  case 36:
#line 184 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1245 "parser.tab.cc" // lalr1.cc:859
    break;

  case 37:
#line 185 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
#line 1251 "parser.tab.cc" // lalr1.cc:859
    break;

  case 38:
#line 186 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNT, tokenizer.create_token(), {} }; }
#line 1257 "parser.tab.cc" // lalr1.cc:859
    break;

  case 39:
#line 188 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNTS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1263 "parser.tab.cc" // lalr1.cc:859
    break;

  case 40:
#line 189 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CODESTMNTS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1269 "parser.tab.cc" // lalr1.cc:859
    break;

  case 41:
#line 192 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, tokenizer.create_token(), { yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} } } }; }
#line 1275 "parser.tab.cc" // lalr1.cc:859
    break;

  case 42:
#line 193 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE_IDENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }); }
#line 1281 "parser.tab.cc" // lalr1.cc:859
    break;

  case 43:
#line 195 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1287 "parser.tab.cc" // lalr1.cc:859
    break;

  case 44:
#line 196 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPE, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1293 "parser.tab.cc" // lalr1.cc:859
    break;

  case 45:
#line 197 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ARRTYPE, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1299 "parser.tab.cc" // lalr1.cc:859
    break;

  case 46:
#line 198 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ARRTYPE, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1305 "parser.tab.cc" // lalr1.cc:859
    break;

  case 47:
#line 200 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1311 "parser.tab.cc" // lalr1.cc:859
    break;

  case 48:
#line 201 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1317 "parser.tab.cc" // lalr1.cc:859
    break;

  case 49:
#line 202 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TYPELIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1323 "parser.tab.cc" // lalr1.cc:859
    break;

  case 50:
#line 205 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 1329 "parser.tab.cc" // lalr1.cc:859
    break;

  case 51:
#line 206 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1335 "parser.tab.cc" // lalr1.cc:859
    break;

  case 52:
#line 207 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1341 "parser.tab.cc" // lalr1.cc:859
    break;

  case 53:
#line 209 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN_N_CLS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1347 "parser.tab.cc" // lalr1.cc:859
    break;

  case 54:
#line 210 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENCPSLTN_N_CLS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1353 "parser.tab.cc" // lalr1.cc:859
    break;

  case 55:
#line 213 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEF, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1359 "parser.tab.cc" // lalr1.cc:859
    break;

  case 56:
#line 214 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEF, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1365 "parser.tab.cc" // lalr1.cc:859
    break;

  case 57:
#line 216 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1371 "parser.tab.cc" // lalr1.cc:859
    break;

  case 58:
#line 217 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1377 "parser.tab.cc" // lalr1.cc:859
    break;

  case 59:
#line 218 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_DEFS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1383 "parser.tab.cc" // lalr1.cc:859
    break;

  case 60:
#line 220 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ }; }
#line 1389 "parser.tab.cc" // lalr1.cc:859
    break;

  case 61:
#line 221 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE_USE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1395 "parser.tab.cc" // lalr1.cc:859
    break;

  case 62:
#line 223 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TEMPLATE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1401 "parser.tab.cc" // lalr1.cc:859
    break;

  case 63:
#line 226 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::NAMESPACE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1407 "parser.tab.cc" // lalr1.cc:859
    break;

  case 64:
#line 229 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1413 "parser.tab.cc" // lalr1.cc:859
    break;

  case 65:
#line 230 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1419 "parser.tab.cc" // lalr1.cc:859
    break;

  case 66:
#line 232 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_BODY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1425 "parser.tab.cc" // lalr1.cc:859
    break;

  case 67:
#line 233 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1431 "parser.tab.cc" // lalr1.cc:859
    break;

  case 68:
#line 235 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1437 "parser.tab.cc" // lalr1.cc:859
    break;

  case 69:
#line 236 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1443 "parser.tab.cc" // lalr1.cc:859
    break;

  case 70:
#line 237 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1449 "parser.tab.cc" // lalr1.cc:859
    break;

  case 71:
#line 238 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUES, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1455 "parser.tab.cc" // lalr1.cc:859
    break;

  case 72:
#line 239 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1461 "parser.tab.cc" // lalr1.cc:859
    break;

  case 73:
#line 241 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUE, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1467 "parser.tab.cc" // lalr1.cc:859
    break;

  case 74:
#line 242 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ENUM_VALUE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1473 "parser.tab.cc" // lalr1.cc:859
    break;

  case 75:
#line 245 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1479 "parser.tab.cc" // lalr1.cc:859
    break;

  case 76:
#line 246 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1485 "parser.tab.cc" // lalr1.cc:859
    break;

  case 77:
#line 247 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1491 "parser.tab.cc" // lalr1.cc:859
    break;

  case 78:
#line 248 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASS, tokenizer.create_token(), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1497 "parser.tab.cc" // lalr1.cc:859
    break;

  case 79:
#line 250 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSHEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1503 "parser.tab.cc" // lalr1.cc:859
    break;

  case 80:
#line 252 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CLASSBODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1509 "parser.tab.cc" // lalr1.cc:859
    break;

  case 81:
#line 255 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1515 "parser.tab.cc" // lalr1.cc:859
    break;

  case 82:
#line 256 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1521 "parser.tab.cc" // lalr1.cc:859
    break;

  case 83:
#line 258 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1527 "parser.tab.cc" // lalr1.cc:859
    break;

  case 84:
#line 259 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_HEAD_UNBOUND, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1533 "parser.tab.cc" // lalr1.cc:859
    break;

  case 85:
#line 261 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGS, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1539 "parser.tab.cc" // lalr1.cc:859
    break;

  case 86:
#line 262 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGS, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1545 "parser.tab.cc" // lalr1.cc:859
    break;

  case 87:
#line 264 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_BODY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1551 "parser.tab.cc" // lalr1.cc:859
    break;

  case 88:
#line 265 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1557 "parser.tab.cc" // lalr1.cc:859
    break;

  case 89:
#line 267 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1563 "parser.tab.cc" // lalr1.cc:859
    break;

  case 90:
#line 268 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 1569 "parser.tab.cc" // lalr1.cc:859
    break;

  case 91:
#line 269 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARGLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1575 "parser.tab.cc" // lalr1.cc:859
    break;

  case 92:
#line 271 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1581 "parser.tab.cc" // lalr1.cc:859
    break;

  case 93:
#line 272 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHD_ARG, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1587 "parser.tab.cc" // lalr1.cc:859
    break;

  case 94:
#line 275 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CONVERSION, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1593 "parser.tab.cc" // lalr1.cc:859
    break;

  case 95:
#line 278 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CONVERSION, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[5].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1599 "parser.tab.cc" // lalr1.cc:859
    break;

  case 96:
#line 281 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CONVERSION, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[6].value.as< yaoosl::compiler::cstnode > (), yystack_[5].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1605 "parser.tab.cc" // lalr1.cc:859
    break;

  case 97:
#line 284 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1611 "parser.tab.cc" // lalr1.cc:859
    break;

  case 98:
#line 285 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1617 "parser.tab.cc" // lalr1.cc:859
    break;

  case 99:
#line 287 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_HEAD, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1623 "parser.tab.cc" // lalr1.cc:859
    break;

  case 100:
#line 289 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1629 "parser.tab.cc" // lalr1.cc:859
    break;

  case 101:
#line 290 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1635 "parser.tab.cc" // lalr1.cc:859
    break;

  case 102:
#line 291 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1641 "parser.tab.cc" // lalr1.cc:859
    break;

  case 103:
#line 292 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_ARGS, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[5].value.as< yaoosl::compiler::cstnode > (), yystack_[3].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1647 "parser.tab.cc" // lalr1.cc:859
    break;

  case 104:
#line 294 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1653 "parser.tab.cc" // lalr1.cc:859
    break;

  case 105:
#line 295 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1659 "parser.tab.cc" // lalr1.cc:859
    break;

  case 106:
#line 296 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1665 "parser.tab.cc" // lalr1.cc:859
    break;

  case 107:
#line 297 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1P, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1671 "parser.tab.cc" // lalr1.cc:859
    break;

  case 108:
#line 299 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1S, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1677 "parser.tab.cc" // lalr1.cc:859
    break;

  case 109:
#line 300 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS1S, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1683 "parser.tab.cc" // lalr1.cc:859
    break;

  case 110:
#line 302 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1689 "parser.tab.cc" // lalr1.cc:859
    break;

  case 111:
#line 303 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1695 "parser.tab.cc" // lalr1.cc:859
    break;

  case 112:
#line 304 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1701 "parser.tab.cc" // lalr1.cc:859
    break;

  case 113:
#line 305 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1707 "parser.tab.cc" // lalr1.cc:859
    break;

  case 114:
#line 306 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1713 "parser.tab.cc" // lalr1.cc:859
    break;

  case 115:
#line 307 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1719 "parser.tab.cc" // lalr1.cc:859
    break;

  case 116:
#line 308 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1725 "parser.tab.cc" // lalr1.cc:859
    break;

  case 117:
#line 309 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1731 "parser.tab.cc" // lalr1.cc:859
    break;

  case 118:
#line 310 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1737 "parser.tab.cc" // lalr1.cc:859
    break;

  case 119:
#line 311 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1743 "parser.tab.cc" // lalr1.cc:859
    break;

  case 120:
#line 312 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1749 "parser.tab.cc" // lalr1.cc:859
    break;

  case 121:
#line 313 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1755 "parser.tab.cc" // lalr1.cc:859
    break;

  case 122:
#line 314 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1761 "parser.tab.cc" // lalr1.cc:859
    break;

  case 123:
#line 315 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1767 "parser.tab.cc" // lalr1.cc:859
    break;

  case 124:
#line 316 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1773 "parser.tab.cc" // lalr1.cc:859
    break;

  case 125:
#line 317 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1779 "parser.tab.cc" // lalr1.cc:859
    break;

  case 126:
#line 318 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1785 "parser.tab.cc" // lalr1.cc:859
    break;

  case 127:
#line 319 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1791 "parser.tab.cc" // lalr1.cc:859
    break;

  case 128:
#line 320 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1797 "parser.tab.cc" // lalr1.cc:859
    break;

  case 129:
#line 321 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1803 "parser.tab.cc" // lalr1.cc:859
    break;

  case 130:
#line 322 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDOP_OPS2, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1809 "parser.tab.cc" // lalr1.cc:859
    break;

  case 131:
#line 325 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDCNST, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1815 "parser.tab.cc" // lalr1.cc:859
    break;

  case 132:
#line 327 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDCNST_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1821 "parser.tab.cc" // lalr1.cc:859
    break;

  case 133:
#line 328 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::MTHDDST_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 1827 "parser.tab.cc" // lalr1.cc:859
    break;

  case 134:
#line 331 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1833 "parser.tab.cc" // lalr1.cc:859
    break;

  case 135:
#line 333 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_HEAD, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1839 "parser.tab.cc" // lalr1.cc:859
    break;

  case 136:
#line 334 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_HEAD_UNBOUND, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1845 "parser.tab.cc" // lalr1.cc:859
    break;

  case 137:
#line 336 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1851 "parser.tab.cc" // lalr1.cc:859
    break;

  case 138:
#line 337 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1857 "parser.tab.cc" // lalr1.cc:859
    break;

  case 139:
#line 338 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1863 "parser.tab.cc" // lalr1.cc:859
    break;

  case 140:
#line 339 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { {}, yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1869 "parser.tab.cc" // lalr1.cc:859
    break;

  case 141:
#line 340 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_BODY, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { {}, {} } }; }
#line 1875 "parser.tab.cc" // lalr1.cc:859
    break;

  case 142:
#line 342 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { {}, { yaoosl::compiler::cstnode::PROP_SET, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), {} }, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1881 "parser.tab.cc" // lalr1.cc:859
    break;

  case 143:
#line 343 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[5].value.as< yaoosl::compiler::cstnode > (), { yaoosl::compiler::cstnode::PROP_SET, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), {} }, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1887 "parser.tab.cc" // lalr1.cc:859
    break;

  case 144:
#line 344 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { {}, {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1893 "parser.tab.cc" // lalr1.cc:859
    break;

  case 145:
#line 345 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_SET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1899 "parser.tab.cc" // lalr1.cc:859
    break;

  case 146:
#line 347 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_GET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1905 "parser.tab.cc" // lalr1.cc:859
    break;

  case 147:
#line 348 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::PROP_GET, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1911 "parser.tab.cc" // lalr1.cc:859
    break;

  case 148:
#line 351 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::IFELSE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1917 "parser.tab.cc" // lalr1.cc:859
    break;

  case 149:
#line 352 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::IFELSE, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1923 "parser.tab.cc" // lalr1.cc:859
    break;

  case 150:
#line 355 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1929 "parser.tab.cc" // lalr1.cc:859
    break;

  case 151:
#line 356 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1935 "parser.tab.cc" // lalr1.cc:859
    break;

  case 152:
#line 358 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1941 "parser.tab.cc" // lalr1.cc:859
    break;

  case 153:
#line 359 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1947 "parser.tab.cc" // lalr1.cc:859
    break;

  case 154:
#line 360 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1953 "parser.tab.cc" // lalr1.cc:859
    break;

  case 155:
#line 361 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), {}, {} } }; }
#line 1959 "parser.tab.cc" // lalr1.cc:859
    break;

  case 156:
#line 362 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1965 "parser.tab.cc" // lalr1.cc:859
    break;

  case 157:
#line 363 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, yystack_[1].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 1971 "parser.tab.cc" // lalr1.cc:859
    break;

  case 158:
#line 364 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1977 "parser.tab.cc" // lalr1.cc:859
    break;

  case 159:
#line 365 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP, tokenizer.create_token(), { {}, {}, {} } }; }
#line 1983 "parser.tab.cc" // lalr1.cc:859
    break;

  case 160:
#line 367 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1989 "parser.tab.cc" // lalr1.cc:859
    break;

  case 161:
#line 368 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 1995 "parser.tab.cc" // lalr1.cc:859
    break;

  case 162:
#line 369 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_STEP_ARG, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2001 "parser.tab.cc" // lalr1.cc:859
    break;

  case 163:
#line 371 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_EACH, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2007 "parser.tab.cc" // lalr1.cc:859
    break;

  case 164:
#line 372 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FOR_EACH, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2013 "parser.tab.cc" // lalr1.cc:859
    break;

  case 165:
#line 375 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::WHILE, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2019 "parser.tab.cc" // lalr1.cc:859
    break;

  case 166:
#line 376 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DO_WHILE, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[4].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2025 "parser.tab.cc" // lalr1.cc:859
    break;

  case 167:
#line 379 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH, yystack_[6].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2031 "parser.tab.cc" // lalr1.cc:859
    break;

  case 168:
#line 381 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASES, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2037 "parser.tab.cc" // lalr1.cc:859
    break;

  case 169:
#line 382 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[1].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 2043 "parser.tab.cc" // lalr1.cc:859
    break;

  case 170:
#line 384 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE,          yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2049 "parser.tab.cc" // lalr1.cc:859
    break;

  case 171:
#line 385 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE_BREAK,    yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2055 "parser.tab.cc" // lalr1.cc:859
    break;

  case 172:
#line 386 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SWITCH_CASE_CONTINUE, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2061 "parser.tab.cc" // lalr1.cc:859
    break;

  case 173:
#line 388 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CASE, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2067 "parser.tab.cc" // lalr1.cc:859
    break;

  case 174:
#line 389 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CASE, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2073 "parser.tab.cc" // lalr1.cc:859
    break;

  case 175:
#line 392 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2079 "parser.tab.cc" // lalr1.cc:859
    break;

  case 176:
#line 394 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCH, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2085 "parser.tab.cc" // lalr1.cc:859
    break;

  case 177:
#line 395 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCH, yystack_[4].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2091 "parser.tab.cc" // lalr1.cc:859
    break;

  case 178:
#line 397 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCHLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2097 "parser.tab.cc" // lalr1.cc:859
    break;

  case 179:
#line 398 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CATCHLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2103 "parser.tab.cc" // lalr1.cc:859
    break;

  case 180:
#line 400 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::FINALLY, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2109 "parser.tab.cc" // lalr1.cc:859
    break;

  case 181:
#line 402 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2115 "parser.tab.cc" // lalr1.cc:859
    break;

  case 182:
#line 403 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > (), {} } }; }
#line 2121 "parser.tab.cc" // lalr1.cc:859
    break;

  case 183:
#line 404 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TRYCATCH, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > (), {}, yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2127 "parser.tab.cc" // lalr1.cc:859
    break;

  case 184:
#line 407 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2133 "parser.tab.cc" // lalr1.cc:859
    break;

  case 185:
#line 408 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2139 "parser.tab.cc" // lalr1.cc:859
    break;

  case 186:
#line 409 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2145 "parser.tab.cc" // lalr1.cc:859
    break;

  case 187:
#line 410 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2151 "parser.tab.cc" // lalr1.cc:859
    break;

  case 188:
#line 411 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2157 "parser.tab.cc" // lalr1.cc:859
    break;

  case 189:
#line 412 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} } } }; }
#line 2163 "parser.tab.cc" // lalr1.cc:859
    break;

  case 190:
#line 413 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2169 "parser.tab.cc" // lalr1.cc:859
    break;

  case 191:
#line 414 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2175 "parser.tab.cc" // lalr1.cc:859
    break;

  case 192:
#line 415 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::STATEMENT, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2181 "parser.tab.cc" // lalr1.cc:859
    break;

  case 193:
#line 417 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DECLARATION, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2187 "parser.tab.cc" // lalr1.cc:859
    break;

  case 194:
#line 419 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SCOPE, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2193 "parser.tab.cc" // lalr1.cc:859
    break;

  case 195:
#line 420 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::SCOPE, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2199 "parser.tab.cc" // lalr1.cc:859
    break;

  case 196:
#line 422 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::VAL, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2205 "parser.tab.cc" // lalr1.cc:859
    break;

  case 197:
#line 423 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::VAL, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2211 "parser.tab.cc" // lalr1.cc:859
    break;

  case 198:
#line 425 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ASSIGNMENT, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2217 "parser.tab.cc" // lalr1.cc:859
    break;

  case 199:
#line 427 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::EXPLIST, tokenizer.create_token(), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2223 "parser.tab.cc" // lalr1.cc:859
    break;

  case 200:
#line 428 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[2].value.as< yaoosl::compiler::cstnode > (); yylhs.value.as< yaoosl::compiler::cstnode > ().nodes.push_back(yystack_[0].value.as< yaoosl::compiler::cstnode > ()); }
#line 2229 "parser.tab.cc" // lalr1.cc:859
    break;

  case 201:
#line 429 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::EXPLIST, tokenizer.create_token(), { yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2235 "parser.tab.cc" // lalr1.cc:859
    break;

  case 202:
#line 431 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2241 "parser.tab.cc" // lalr1.cc:859
    break;

  case 203:
#line 432 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::TERNARY_OPERATOR, yystack_[3].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[4].value.as< yaoosl::compiler::cstnode > (), yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2247 "parser.tab.cc" // lalr1.cc:859
    break;

  case 204:
#line 434 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2253 "parser.tab.cc" // lalr1.cc:859
    break;

  case 205:
#line 435 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2259 "parser.tab.cc" // lalr1.cc:859
    break;

  case 206:
#line 437 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2265 "parser.tab.cc" // lalr1.cc:859
    break;

  case 207:
#line 438 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2271 "parser.tab.cc" // lalr1.cc:859
    break;

  case 208:
#line 440 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2277 "parser.tab.cc" // lalr1.cc:859
    break;

  case 209:
#line 441 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2283 "parser.tab.cc" // lalr1.cc:859
    break;

  case 210:
#line 442 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2289 "parser.tab.cc" // lalr1.cc:859
    break;

  case 211:
#line 443 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2295 "parser.tab.cc" // lalr1.cc:859
    break;

  case 212:
#line 445 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2301 "parser.tab.cc" // lalr1.cc:859
    break;

  case 213:
#line 446 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2307 "parser.tab.cc" // lalr1.cc:859
    break;

  case 214:
#line 447 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2313 "parser.tab.cc" // lalr1.cc:859
    break;

  case 215:
#line 448 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2319 "parser.tab.cc" // lalr1.cc:859
    break;

  case 216:
#line 449 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2325 "parser.tab.cc" // lalr1.cc:859
    break;

  case 217:
#line 451 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2331 "parser.tab.cc" // lalr1.cc:859
    break;

  case 218:
#line 452 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2337 "parser.tab.cc" // lalr1.cc:859
    break;

  case 219:
#line 453 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2343 "parser.tab.cc" // lalr1.cc:859
    break;

  case 220:
#line 455 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2349 "parser.tab.cc" // lalr1.cc:859
    break;

  case 221:
#line 456 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2355 "parser.tab.cc" // lalr1.cc:859
    break;

  case 222:
#line 457 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2361 "parser.tab.cc" // lalr1.cc:859
    break;

  case 223:
#line 458 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2367 "parser.tab.cc" // lalr1.cc:859
    break;

  case 224:
#line 460 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2373 "parser.tab.cc" // lalr1.cc:859
    break;

  case 225:
#line 461 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2379 "parser.tab.cc" // lalr1.cc:859
    break;

  case 226:
#line 462 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2385 "parser.tab.cc" // lalr1.cc:859
    break;

  case 227:
#line 463 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2391 "parser.tab.cc" // lalr1.cc:859
    break;

  case 228:
#line 464 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2397 "parser.tab.cc" // lalr1.cc:859
    break;

  case 229:
#line 466 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2403 "parser.tab.cc" // lalr1.cc:859
    break;

  case 230:
#line 467 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2409 "parser.tab.cc" // lalr1.cc:859
    break;

  case 231:
#line 468 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2415 "parser.tab.cc" // lalr1.cc:859
    break;

  case 232:
#line 470 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2421 "parser.tab.cc" // lalr1.cc:859
    break;

  case 233:
#line 471 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::BINARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > (), yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2427 "parser.tab.cc" // lalr1.cc:859
    break;

  case 234:
#line 473 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2433 "parser.tab.cc" // lalr1.cc:859
    break;

  case 235:
#line 474 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::UNARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2439 "parser.tab.cc" // lalr1.cc:859
    break;

  case 236:
#line 475 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::UNARY_OPERATOR, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2445 "parser.tab.cc" // lalr1.cc:859
    break;

  case 237:
#line 476 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[1].value.as< yaoosl::compiler::cstnode > (); }
#line 2451 "parser.tab.cc" // lalr1.cc:859
    break;

  case 238:
#line 478 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2457 "parser.tab.cc" // lalr1.cc:859
    break;

  case 239:
#line 479 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2463 "parser.tab.cc" // lalr1.cc:859
    break;

  case 240:
#line 480 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2469 "parser.tab.cc" // lalr1.cc:859
    break;

  case 241:
#line 481 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2475 "parser.tab.cc" // lalr1.cc:859
    break;

  case 242:
#line 483 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::ARRGET, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2481 "parser.tab.cc" // lalr1.cc:859
    break;

  case 243:
#line 485 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::DOTNAV, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2487 "parser.tab.cc" // lalr1.cc:859
    break;

  case 244:
#line 487 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CALL, yystack_[2].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[3].value.as< yaoosl::compiler::cstnode > (), yystack_[1].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2493 "parser.tab.cc" // lalr1.cc:859
    break;

  case 245:
#line 488 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CALL, yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[2].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2499 "parser.tab.cc" // lalr1.cc:859
    break;

  case 246:
#line 490 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2505 "parser.tab.cc" // lalr1.cc:859
    break;

  case 247:
#line 491 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::THIS, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {    } }; }
#line 2511 "parser.tab.cc" // lalr1.cc:859
    break;

  case 248:
#line 492 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::NEW,  yystack_[1].value.as< yaoosl::compiler::tokenizer::token > (), { yystack_[0].value.as< yaoosl::compiler::cstnode > () } }; }
#line 2517 "parser.tab.cc" // lalr1.cc:859
    break;

  case 249:
#line 493 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2523 "parser.tab.cc" // lalr1.cc:859
    break;

  case 250:
#line 495 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2529 "parser.tab.cc" // lalr1.cc:859
    break;

  case 251:
#line 496 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2535 "parser.tab.cc" // lalr1.cc:859
    break;

  case 252:
#line 497 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2541 "parser.tab.cc" // lalr1.cc:859
    break;

  case 253:
#line 498 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2547 "parser.tab.cc" // lalr1.cc:859
    break;

  case 254:
#line 499 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yaoosl::compiler::cstnode{ yaoosl::compiler::cstnode::CVAL, yystack_[0].value.as< yaoosl::compiler::tokenizer::token > (), {} }; }
#line 2553 "parser.tab.cc" // lalr1.cc:859
    break;

  case 255:
#line 500 "parser.y" // lalr1.cc:859
    { yylhs.value.as< yaoosl::compiler::cstnode > () = yystack_[0].value.as< yaoosl::compiler::cstnode > (); }
#line 2559 "parser.tab.cc" // lalr1.cc:859
    break;


#line 2563 "parser.tab.cc" // lalr1.cc:859
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


  const short int parser::yypact_ninf_ = -318;

  const short int parser::yytable_ninf_ = -171;

  const short int
  parser::yypact_[] =
  {
      43,  -318,  -318,  -318,  -318,   346,   -43,   121,    99,  -318,
      68,  -318,    43,    11,     8,  -318,  -318,  -318,    15,  -318,
     170,  -318,    -3,  -318,  -318,   134,    -3,    -3,    -3,  -318,
      57,  -318,  -318,   -43,    82,   254,    97,   115,    -3,    -3,
     137,   129,  -318,     4,   177,   195,    96,   157,    43,  -318,
    -318,  -318,   199,   254,  -318,    -3,   220,  -318,    -3,    20,
    -318,  -318,   154,   210,  -318,   139,   346,   215,  -318,  -318,
     100,   251,    58,  -318,  -318,  -318,  -318,  -318,   184,  -318,
     177,  -318,   244,    -3,  -318,  -318,   230,   174,  -318,   195,
     501,  -318,   259,  -318,  -318,   274,  -318,   280,   151,   273,
     299,  -318,   316,   224,   234,    -3,  -318,   338,  -318,    -3,
      -3,  -318,  -318,  -318,   195,   315,   285,  -318,  -318,  -318,
    -318,  -318,  -318,   897,   261,   195,   311,   320,   195,   109,
    -318,  -318,   154,   380,  -318,    -3,  -318,  -318,    -3,   630,
     630,   582,  -318,   630,  -318,  -318,  -318,   744,   630,   351,
    -318,    45,   352,    -3,   630,   353,   630,   744,  -318,  -318,
     354,  -318,  -318,  -318,  -318,  -318,   663,   317,  -318,  -318,
    -318,  -318,  -318,   158,  -318,   378,  -318,  -318,   379,  -318,
    -318,    -7,   384,   216,   301,    67,   288,   312,   329,   385,
    -318,    85,  -318,  -318,  -318,  -318,  -318,    -3,  -318,   381,
    -318,  -318,  -318,   179,  -318,   347,   347,  -318,  -318,  -318,
     179,  -318,   348,  -318,   249,  -318,  -318,  -318,  -318,  -318,
    -318,  -318,  -318,  -318,  -318,  -318,  -318,  -318,  -318,  -318,
    -318,  -318,  -318,  -318,  -318,  -318,   389,   390,   195,  -318,
     391,   392,  -318,   195,   155,   164,    49,    79,  -318,   179,
    -318,   370,  -318,   231,  -318,  -318,   394,   393,   357,  -318,
     150,   179,  -318,  -318,  -318,   630,  -318,  -318,   630,  -318,
    -318,   630,  -318,  -318,  -318,   825,   744,   382,   375,  -318,
    -318,  -318,   630,   630,   630,   630,   630,   630,   630,   630,
     630,   630,   630,   630,   630,   630,   630,   630,   630,   630,
     630,   630,   630,   630,   630,   302,   630,   360,   400,  -318,
    -318,  -318,  -318,  -318,  -318,   253,    -3,    -3,  -318,    -3,
      -3,  -318,   363,  -318,   195,   226,  -318,   383,   404,  -318,
     373,   406,  -318,   408,  -318,  -318,   409,   467,   405,   318,
     410,   430,  -318,  -318,   411,   412,   413,   630,  -318,  -318,
    -318,  -318,   384,   435,   216,   301,   301,   301,    67,    67,
      67,    67,   288,   288,   312,   312,   312,   329,   329,   329,
     329,   385,   385,  -318,  -318,  -318,  -318,   193,   417,  -318,
     195,   420,   421,   423,   424,   426,  -318,   395,  -318,  -318,
    -318,    -3,   630,   630,   322,   744,   549,   630,   744,   630,
     744,   434,   744,   427,   630,  -318,   630,  -318,  -318,  -318,
      -3,  -318,  -318,   195,   432,   437,   438,   436,   630,  -318,
     630,   324,  -318,  -318,   441,   403,   101,  -318,   744,  -318,
    -318,   442,  -318,   195,   195,  -318,   436,   436,   630,   630,
     744,   188,  -318,   482,  -318,  -318,  -318,  -318,   436,  -318,
    -318,  -318,  -318,   419,   240,   461,   462,  -318,  -318
  };

  const unsigned char
  parser::yydefact_[] =
  {
       2,    53,    54,    51,    52,     0,     0,     0,     0,    13,
       0,    15,     3,     0,    50,    10,    14,     9,     0,    11,
       0,    12,     0,    50,    41,     0,     0,     0,     0,     1,
       0,     4,    16,     0,     0,    60,     0,     0,     0,     0,
       0,     0,    75,     0,     0,     0,     0,     0,     0,     8,
       7,     6,     0,    60,    79,     0,    43,    83,     0,     0,
      64,    47,     0,     0,    57,     0,     0,     0,    23,    25,
       0,     0,     0,    24,    21,    17,    20,    18,     0,    19,
       0,    22,     0,     0,    77,    85,     0,     0,    89,     0,
       0,    81,     0,    84,    42,     0,     5,    44,     0,     0,
       0,    66,    73,     0,    68,    49,    76,    55,    62,    59,
       0,   133,    80,    26,     0,    41,     0,   105,   107,   108,
     106,   109,   104,     0,     0,     0,     0,     0,     0,     0,
     141,   134,     0,    92,    86,    91,    82,    38,     0,     0,
       0,     0,    87,     0,    37,   191,   192,     0,     0,     0,
     254,     0,     0,     0,   184,     0,   186,     0,   253,   247,
       0,   251,   250,   252,    29,    39,     0,   255,    28,    27,
      30,    32,    33,     0,    31,     0,   249,    35,     0,   197,
     196,   202,   204,   206,   208,   212,   217,   220,   224,   229,
     232,   234,   241,   240,   239,   238,   246,     0,    63,     0,
      61,    45,    65,     0,    67,     0,     0,    70,    69,    48,
       0,    58,    99,    94,    83,   126,   125,   113,   112,   130,
     128,   127,   129,   110,   111,   119,   118,   120,   121,   115,
     114,   116,   117,   123,   122,   124,     0,     0,     0,    97,
       0,     0,   131,     0,     0,     0,     0,     0,    78,     0,
      90,     0,   236,   234,   235,   194,     0,     0,     0,   188,
       0,     0,   174,   189,   190,     0,   248,   185,     0,   187,
     175,     0,    88,    40,   193,     0,     0,   178,   182,   183,
      34,    36,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,    46,
     255,    74,    72,    71,    56,    84,     0,     0,    98,     0,
       0,   146,     0,   144,     0,     0,   140,     0,     0,   138,
       0,     0,    93,     0,   195,   237,     0,     0,     0,     0,
       0,   249,   160,   173,     0,     0,     0,     0,   176,   180,
     179,   181,   205,     0,   207,   209,   210,   211,   214,   213,
     216,   215,   218,   219,   222,   221,   223,   225,   226,   227,
     228,   231,   230,   233,   198,   245,   199,     0,     0,   243,
       0,     0,     0,     0,     0,     0,   147,     0,   145,   139,
     137,     0,     0,   159,     0,     0,     0,   161,     0,     0,
       0,     0,     0,   249,     0,   244,   201,   242,    96,   100,
       0,   101,   102,     0,     0,     0,     0,   158,   157,   150,
     155,     0,   162,   151,   163,   148,     0,   165,     0,   203,
     200,     0,   142,     0,     0,   166,   156,   154,   153,     0,
       0,     0,   168,     0,   177,   103,   143,    95,   152,   164,
     149,   167,   169,     0,     0,     0,     0,   171,   172
  };

  const short int
  parser::yypgoto_[] =
  {
    -318,  -318,    21,  -318,    24,  -318,   443,  -318,  -129,  -318,
     131,   -13,    84,    -2,     0,   398,  -318,   455,     2,  -318,
      91,   414,  -318,   169,    92,  -318,   -30,    93,  -318,   -34,
     -85,  -318,  -116,  -318,  -318,  -318,  -318,  -318,   387,  -318,
     399,  -318,  -318,  -318,  -318,  -318,  -318,   265,   269,  -318,
    -318,  -318,  -317,  -318,  -318,  -318,  -318,    75,   366,  -318,
    -318,   241,   245,  -318,  -318,  -254,  -318,   -83,  -318,  -318,
    -276,  -318,   242,   243,    32,    69,    94,    38,    73,    83,
    -138,   116,  -318,  -318,  -318,  -318,  -187
  };

  const short int
  parser::yydefgoto_[] =
  {
      -1,     8,   164,    10,    11,    12,    69,    70,   165,   166,
      35,   167,    62,    13,    14,    64,    65,    56,    41,    15,
      16,    60,   103,   104,    17,    18,    42,    19,    20,    45,
      91,    87,    88,    76,   168,    21,    77,    78,   125,   126,
     127,   237,    79,    80,    81,    82,   131,   246,   247,   169,
     170,   338,   339,   340,   171,   172,   441,   442,   443,   173,
     277,   278,   279,   174,   175,   176,   177,   178,   179,   377,
     180,   181,   182,   183,   184,   185,   186,   187,   188,   189,
     190,   191,   192,   193,   194,   195,   196
  };

  const short int
  parser::yytable_[] =
  {
      36,   252,   254,    22,   136,    23,   341,   353,   282,    46,
      89,    84,   256,    49,    50,    51,   311,    33,   258,   250,
     394,     9,    44,   314,    33,    61,    63,    71,   270,   213,
      86,    33,   106,     9,    38,    24,    32,   273,    72,   283,
     239,    39,    61,   242,    85,   100,   128,     1,     2,     3,
       4,     5,    40,     1,     2,     3,     4,    71,   101,   116,
     257,    68,   332,    34,   110,   259,    23,    37,    72,     9,
      61,   267,    95,   269,   343,    24,   417,    30,    33,   421,
     124,   308,    24,     1,     2,     3,     4,   326,   292,    24,
     293,    68,   209,   403,   304,   261,    63,   212,   102,    29,
     262,   436,   248,   437,     1,     2,     3,     4,    66,     6,
      34,    31,   243,     1,     2,     3,     4,   329,    67,     7,
      33,   448,    86,   263,   305,   251,   306,   245,   429,    23,
     307,    73,    74,    75,    58,    52,   115,    25,   112,    98,
     266,     1,     2,     3,     4,    66,   348,   349,    83,   244,
      92,   261,    59,   318,    47,    67,   262,    33,   321,   323,
      54,    73,    74,    75,    53,   373,    40,   132,   139,   108,
      33,    48,   243,    26,    93,    57,     7,   342,    24,   244,
      27,   200,   344,   109,    86,   345,   140,    28,   346,   143,
     310,    40,    90,   337,   322,   105,    39,   310,   105,    33,
     381,   382,   117,   383,   384,   118,   119,   120,   121,    43,
      39,   275,   150,     7,   134,    24,    43,   153,   135,   276,
     122,   374,   376,   378,   158,   159,   451,   324,    24,   161,
     162,   163,    90,   405,   325,    94,   310,   406,   261,   386,
     388,   150,    96,   262,   327,   330,    23,    23,   310,   285,
     286,   287,   123,   158,   342,   253,   253,    24,   161,   162,
     163,    99,   204,    90,   257,   387,   419,   205,   206,   423,
     305,   425,   306,   427,    47,   415,   307,   207,   208,   117,
      55,   129,   118,   119,   120,   121,  -135,   130,   107,   455,
    -136,   456,  -135,   111,   431,   408,  -136,   122,   197,   444,
     294,   295,   296,    86,    86,   114,    86,    86,   133,   416,
     342,   450,   198,   342,   422,   201,   424,   355,   356,   357,
     139,   199,    33,   430,   454,   203,   288,   289,   432,   123,
     290,   291,   364,   365,   366,   342,    59,   342,   140,   297,
     298,   143,   375,   299,   300,   301,   302,   210,   446,   447,
       1,     2,     3,     4,  -132,   342,   449,   358,   359,   360,
     361,   396,   397,   214,   150,   418,   397,   438,   397,   153,
     367,   368,   369,   370,   312,   313,   158,   159,    86,   240,
      24,   161,   162,   163,   371,   372,   362,   363,   241,   249,
     260,   265,   268,   271,   284,   274,   303,    86,   253,   253,
     253,   253,   253,   253,   253,   253,   253,   253,   253,   253,
     253,   253,   253,   253,   253,   253,   253,   253,   253,   253,
     137,   280,   281,   309,   333,   102,   315,   138,   316,   317,
     319,   320,   334,   335,   336,   275,   276,   139,   379,    33,
     380,   385,   389,   325,   390,   395,   324,   391,   392,   399,
     398,   400,   401,   402,   404,   140,   141,  -170,   143,   407,
     409,   440,   144,   411,   412,   410,   413,   428,   145,  -170,
     146,   426,   433,   414,  -170,   147,   148,   434,   435,   149,
     397,   150,   445,   151,   152,   139,   153,    33,   154,   439,
     155,   156,   157,   158,   159,     7,   160,    24,   161,   162,
     163,   453,   137,   140,   457,   458,   143,   211,    97,   138,
     393,   238,   331,   113,   202,   328,   452,   264,   350,   139,
     253,    33,   236,   351,   352,     0,     0,   354,     0,   150,
       0,     0,     0,     0,   153,     0,     0,   140,   141,   142,
     143,   158,   159,     0,   144,    24,   161,   162,   163,     0,
     145,     0,   146,     0,     0,     0,     0,   147,   148,     0,
       0,   149,     0,   150,     0,   151,   152,   139,   153,    33,
     154,     0,   155,   156,   157,   158,   159,     7,   160,    24,
     161,   162,   163,   137,     0,   140,     0,     0,   143,     0,
     138,     0,   420,     0,     0,     0,     0,     0,     0,     0,
     139,     0,    33,     0,     0,     0,     0,     0,     0,     0,
       0,   150,     0,     0,     0,     0,   153,     0,   140,   141,
     255,   143,     0,   158,   159,   144,     0,    24,   161,   162,
     163,   145,     0,   146,     0,     0,     0,     0,   147,   148,
       0,     0,   149,     0,   150,     0,   151,   152,   139,   153,
      33,   154,     0,   155,   156,   157,   158,   159,     7,   160,
      24,   161,   162,   163,   137,     0,   140,     0,     0,   143,
       0,   138,     0,     0,     0,     0,     0,     0,     0,     0,
       0,   139,     0,    33,     0,     0,     0,     0,     0,     0,
       0,     0,   150,     0,     0,     0,     0,   153,     0,   140,
     141,   272,   143,     0,   158,   159,   144,     0,    24,   161,
     162,   163,   145,     0,   146,     0,     0,     0,     0,   147,
     148,     0,     0,   149,     0,   150,     0,   151,   152,     0,
     153,     0,   154,     0,   155,   156,   157,   158,   159,     7,
     160,    24,   161,   162,   163,   137,     0,     0,     0,     0,
       0,     0,   138,     0,     0,     0,     0,     0,     0,     0,
       0,     0,   139,     0,    33,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
     140,   141,     0,   143,     0,     0,     0,   144,     0,     0,
       0,     0,     0,   145,     0,   146,     0,     0,     0,     0,
     147,   148,     0,     0,   149,     0,   150,     0,   151,   152,
       0,   153,     0,   154,     0,   155,   156,   157,   158,   159,
       7,   160,    24,   161,   162,   163,   137,     0,     0,     0,
       0,     0,     0,   138,     0,     0,     0,     0,     0,     0,
       0,     0,     0,   139,     0,    33,     0,     0,     0,     0,
       0,     0,     0,     0,     0,     0,     0,     0,     0,     0,
       0,   140,   141,     0,   347,     0,     0,     0,   144,     0,
       0,     0,     0,     0,   145,     0,   146,     0,     0,     0,
       0,   147,   148,     0,     0,   149,     0,   150,     0,   151,
     152,     0,   153,     0,   154,     0,   155,   156,   157,   158,
     159,     7,   160,    24,   161,   162,   163,   215,   216,   217,
     218,   219,   220,   221,   222,     0,     0,     0,   223,   119,
     224,   121,   225,   226,   227,   228,   229,   230,   231,   232,
     233,     0,   234,     0,     0,     0,     0,     0,     0,     0,
       0,     0,     0,     0,     0,   235
  };

  const short int
  parser::yycheck_[] =
  {
      13,   139,   140,     5,    89,     5,   260,   283,    15,    22,
      44,    41,   141,    26,    27,    28,   203,    20,   147,   135,
     337,     0,    20,   210,    20,    38,    39,    40,   157,   114,
      43,    20,    62,    12,    19,    78,    12,   166,    40,    46,
     125,    26,    55,   128,    40,    58,    80,     4,     5,     6,
       7,     8,    37,     4,     5,     6,     7,    70,    38,    72,
     143,    40,   249,    52,    66,   148,    66,    59,    70,    48,
      83,   154,    48,   156,   261,    78,   393,     9,    20,   396,
      78,   197,    78,     4,     5,     6,     7,    38,    21,    78,
      23,    70,   105,   347,     9,    50,   109,   110,    78,     0,
      55,   418,   132,   420,     4,     5,     6,     7,     8,    66,
      52,    43,    63,     4,     5,     6,     7,    38,    18,    76,
      20,   438,   135,    78,    39,   138,    41,   129,   404,   129,
      45,    40,    40,    40,    19,    78,    78,     6,    38,    55,
     153,     4,     5,     6,     7,     8,   275,   276,    19,    70,
      54,    50,    37,   238,    20,    18,    55,    20,   243,   244,
      78,    70,    70,    70,    33,   303,    37,    83,    18,    30,
      20,    37,    63,    52,    78,    78,    76,   260,    78,    70,
      59,    30,   265,    44,   197,   268,    36,    66,   271,    39,
     203,    37,    37,    43,    39,    44,    26,   210,    44,    20,
     316,   317,    18,   319,   320,    21,    22,    23,    24,    39,
      26,    53,    62,    76,    40,    78,    39,    67,    44,    61,
      36,   304,   305,   306,    74,    75,    38,    63,    78,    79,
      80,    81,    37,    40,    70,    78,   249,    44,    50,   324,
     325,    62,    43,    55,   246,   247,   246,   247,   261,    33,
      34,    35,    68,    74,   337,   139,   140,    78,    79,    80,
      81,    41,    38,    37,   347,    39,   395,    43,    44,   398,
      39,   400,    41,   402,    20,   391,    45,    43,    44,    18,
      26,    37,    21,    22,    23,    24,    37,    43,    78,    49,
      37,    51,    43,    78,   410,   380,    43,    36,    39,   428,
      12,    13,    14,   316,   317,    54,   319,   320,    78,   392,
     393,   440,    38,   396,   397,    42,   399,   285,   286,   287,
      18,    41,    20,   406,   453,     9,    25,    26,   413,    68,
      29,    30,   294,   295,   296,   418,    37,   420,    36,    27,
      28,    39,    40,    31,    32,    16,    17,     9,   433,   434,
       4,     5,     6,     7,    39,   438,   439,   288,   289,   290,
     291,    43,    44,    78,    62,    43,    44,    43,    44,    67,
     297,   298,   299,   300,   205,   206,    74,    75,   391,    68,
      78,    79,    80,    81,   301,   302,   292,   293,    68,     9,
      39,    39,    39,    39,    10,    78,    11,   410,   282,   283,
     284,   285,   286,   287,   288,   289,   290,   291,   292,   293,
     294,   295,   296,   297,   298,   299,   300,   301,   302,   303,
       1,    43,    43,    42,    54,    78,    78,     8,    39,    39,
      39,    39,    38,    40,    77,    53,    61,    18,    78,    20,
      40,    78,    38,    70,    38,    40,    63,    39,    39,    19,
      40,    40,    40,    40,    19,    36,    37,    38,    39,    42,
      40,    58,    43,    40,    40,    44,    40,    40,    49,    50,
      51,    37,    40,    78,    55,    56,    57,    40,    40,    60,
      44,    62,    40,    64,    65,    18,    67,    20,    69,    48,
      71,    72,    73,    74,    75,    76,    77,    78,    79,    80,
      81,    19,     1,    36,    43,    43,    39,   109,    53,     8,
      43,   124,   247,    70,   100,   246,   441,   151,   277,    18,
     404,    20,   123,   278,   282,    -1,    -1,   284,    -1,    62,
      -1,    -1,    -1,    -1,    67,    -1,    -1,    36,    37,    38,
      39,    74,    75,    -1,    43,    78,    79,    80,    81,    -1,
      49,    -1,    51,    -1,    -1,    -1,    -1,    56,    57,    -1,
      -1,    60,    -1,    62,    -1,    64,    65,    18,    67,    20,
      69,    -1,    71,    72,    73,    74,    75,    76,    77,    78,
      79,    80,    81,     1,    -1,    36,    -1,    -1,    39,    -1,
       8,    -1,    43,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      18,    -1,    20,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    62,    -1,    -1,    -1,    -1,    67,    -1,    36,    37,
      38,    39,    -1,    74,    75,    43,    -1,    78,    79,    80,
      81,    49,    -1,    51,    -1,    -1,    -1,    -1,    56,    57,
      -1,    -1,    60,    -1,    62,    -1,    64,    65,    18,    67,
      20,    69,    -1,    71,    72,    73,    74,    75,    76,    77,
      78,    79,    80,    81,     1,    -1,    36,    -1,    -1,    39,
      -1,     8,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    18,    -1,    20,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    62,    -1,    -1,    -1,    -1,    67,    -1,    36,
      37,    38,    39,    -1,    74,    75,    43,    -1,    78,    79,
      80,    81,    49,    -1,    51,    -1,    -1,    -1,    -1,    56,
      57,    -1,    -1,    60,    -1,    62,    -1,    64,    65,    -1,
      67,    -1,    69,    -1,    71,    72,    73,    74,    75,    76,
      77,    78,    79,    80,    81,     1,    -1,    -1,    -1,    -1,
      -1,    -1,     8,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    18,    -1,    20,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      36,    37,    -1,    39,    -1,    -1,    -1,    43,    -1,    -1,
      -1,    -1,    -1,    49,    -1,    51,    -1,    -1,    -1,    -1,
      56,    57,    -1,    -1,    60,    -1,    62,    -1,    64,    65,
      -1,    67,    -1,    69,    -1,    71,    72,    73,    74,    75,
      76,    77,    78,    79,    80,    81,     1,    -1,    -1,    -1,
      -1,    -1,    -1,     8,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    18,    -1,    20,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    36,    37,    -1,    39,    -1,    -1,    -1,    43,    -1,
      -1,    -1,    -1,    -1,    49,    -1,    51,    -1,    -1,    -1,
      -1,    56,    57,    -1,    -1,    60,    -1,    62,    -1,    64,
      65,    -1,    67,    -1,    69,    -1,    71,    72,    73,    74,
      75,    76,    77,    78,    79,    80,    81,    10,    11,    12,
      13,    14,    15,    16,    17,    -1,    -1,    -1,    21,    22,
      23,    24,    25,    26,    27,    28,    29,    30,    31,    32,
      33,    -1,    35,    -1,    -1,    -1,    -1,    -1,    -1,    -1,
      -1,    -1,    -1,    -1,    -1,    48
  };

  const unsigned char
  parser::yystos_[] =
  {
       0,     4,     5,     6,     7,     8,    66,    76,    83,    84,
      85,    86,    87,    95,    96,   101,   102,   106,   107,   109,
     110,   117,    95,    96,    78,    92,    52,    59,    66,     0,
       9,    43,    86,    20,    52,    92,    93,    59,    19,    26,
      37,   100,   108,    39,   100,   111,    93,    20,    37,    93,
      93,    93,    78,    92,    78,    26,    99,    78,    19,    37,
     103,    93,    94,    93,    97,    98,     8,    18,    84,    88,
      89,    93,    95,   102,   106,   109,   115,   118,   119,   124,
     125,   126,   127,    19,   108,    40,    93,   113,   114,   111,
      37,   112,    54,    78,    78,    86,    43,    99,    94,    41,
      93,    38,    78,   104,   105,    44,   108,    78,    30,    44,
      95,    78,    38,    88,    54,    78,    93,    18,    21,    22,
      23,    24,    36,    68,   100,   120,   121,   122,   111,    37,
      43,   128,    94,    78,    40,    44,   112,     1,     8,    18,
      36,    37,    38,    39,    43,    49,    51,    56,    57,    60,
      62,    64,    65,    67,    69,    71,    72,    73,    74,    75,
      77,    79,    80,    81,    84,    90,    91,    93,   116,   131,
     132,   136,   137,   141,   145,   146,   147,   148,   149,   150,
     152,   153,   154,   155,   156,   157,   158,   159,   160,   161,
     162,   163,   164,   165,   166,   167,   168,    39,    38,    41,
      30,    42,   103,     9,    38,    43,    44,    43,    44,    93,
       9,    97,    93,   112,    78,    10,    11,    12,    13,    14,
      15,    16,    17,    21,    23,    25,    26,    27,    28,    29,
      30,    31,    32,    33,    35,    48,   122,   123,   120,   112,
      68,    68,   112,    63,    70,    95,   129,   130,   108,     9,
     114,    93,   162,   163,   162,    38,    90,   149,    90,   149,
      39,    50,    55,    78,   140,    39,    93,   149,    39,   149,
      90,    39,    38,    90,    78,    53,    61,   142,   143,   144,
      43,    43,    15,    46,    10,    33,    34,    35,    25,    26,
      29,    30,    21,    23,    12,    13,    14,    27,    28,    31,
      32,    16,    17,    11,     9,    39,    41,    45,   114,    42,
      93,   168,   105,   105,   168,    78,    39,    39,   112,    39,
      39,   112,    39,   112,    63,    70,    38,    95,   130,    38,
      95,   129,   168,    54,    38,    40,    77,    43,   133,   134,
     135,   147,   149,   168,   149,   149,   149,    39,    90,    90,
     143,   144,   154,   152,   155,   156,   156,   156,   157,   157,
     157,   157,   158,   158,   159,   159,   159,   160,   160,   160,
     160,   161,   161,   162,   149,    40,   149,   151,   149,    78,
      40,   114,   114,   114,   114,    78,   112,    39,   112,    38,
      38,    39,    39,    43,   134,    40,    43,    44,    40,    19,
      40,    40,    40,   147,    19,    40,    44,    42,   112,    40,
      44,    40,    40,    40,    78,   114,   149,   134,    43,    90,
      43,   134,   149,    90,   149,    90,    37,    90,    40,   152,
     149,   114,   112,    40,    40,    40,   134,   134,    43,    48,
      58,   138,   139,   140,    90,    40,   112,   112,   134,   149,
      90,    38,   139,    19,    90,    49,    51,    43,    43
  };

  const unsigned char
  parser::yyr1_[] =
  {
       0,    82,    83,    83,    84,    84,    85,    85,    85,    86,
      86,    86,    86,    86,    86,    87,    87,    88,    88,    88,
      88,    88,    88,    88,    88,    89,    89,    90,    90,    90,
      90,    90,    90,    90,    90,    90,    90,    90,    90,    91,
      91,    92,    92,    93,    93,    93,    93,    94,    94,    94,
      95,    95,    95,    96,    96,    97,    97,    98,    98,    98,
      99,    99,   100,   101,   102,   102,   103,   103,   104,   104,
     104,   104,   104,   105,   105,   106,   106,   106,   106,   107,
     108,   109,   109,   110,   110,   111,   111,   112,   112,   113,
     113,   113,   114,   114,   115,   116,   117,   118,   118,   119,
     120,   120,   120,   120,   121,   121,   121,   121,   122,   122,
     123,   123,   123,   123,   123,   123,   123,   123,   123,   123,
     123,   123,   123,   123,   123,   123,   123,   123,   123,   123,
     123,   124,   125,   125,   126,   127,   127,   128,   128,   128,
     128,   128,   129,   129,   129,   129,   130,   130,   131,   131,
     132,   132,   133,   133,   133,   133,   133,   133,   133,   133,
     134,   134,   134,   135,   135,   136,   136,   137,   138,   138,
     139,   139,   139,   140,   140,   141,   142,   142,   143,   143,
     144,   145,   145,   145,   146,   146,   146,   146,   146,   146,
     146,   146,   146,   147,   148,   148,   149,   149,   150,   151,
     151,   151,   152,   152,   153,   153,   154,   154,   155,   155,
     155,   155,   156,   156,   156,   156,   156,   157,   157,   157,
     158,   158,   158,   158,   159,   159,   159,   159,   159,   160,
     160,   160,   161,   161,   162,   162,   162,   162,   163,   163,
     163,   163,   164,   165,   166,   166,   167,   167,   167,   167,
     168,   168,   168,   168,   168,   168
  };

  const unsigned char
  parser::yyr2_[] =
  {
       0,     2,     0,     1,     2,     4,     3,     3,     3,     1,
       1,     1,     1,     1,     1,     1,     2,     1,     1,     1,
       1,     1,     1,     1,     1,     1,     2,     1,     1,     1,
       1,     1,     1,     1,     2,     1,     2,     1,     1,     1,
       2,     1,     3,     2,     3,     4,     5,     1,     3,     2,
       1,     1,     1,     1,     1,     2,     4,     1,     3,     2,
       0,     3,     3,     5,     3,     5,     2,     3,     1,     2,
       2,     3,     3,     1,     3,     2,     4,     3,     5,     3,
       3,     3,     4,     3,     4,     2,     3,     2,     3,     1,
       3,     2,     2,     4,     3,     7,     8,     3,     4,     3,
       5,     5,     5,     7,     1,     1,     1,     1,     1,     1,
       1,     1,     1,     1,     1,     1,     1,     1,     1,     1,
       1,     1,     1,     1,     1,     1,     1,     1,     1,     1,
       1,     3,     2,     2,     2,     3,     4,     4,     3,     4,
       3,     1,     5,     6,     2,     3,     2,     3,     5,     7,
       5,     5,     5,     4,     4,     3,     4,     3,     3,     2,
       1,     2,     3,     3,     5,     5,     6,     7,     1,     2,
       2,     5,     5,     2,     1,     2,     2,     5,     1,     2,
       2,     3,     2,     2,     1,     2,     1,     2,     2,     2,
       2,     1,     1,     2,     2,     3,     1,     1,     3,     1,
       3,     2,     1,     5,     1,     3,     1,     3,     1,     3,
       3,     3,     1,     3,     3,     3,     3,     1,     3,     3,
       1,     3,     3,     3,     1,     3,     3,     3,     3,     1,
       3,     3,     1,     3,     1,     2,     2,     3,     1,     1,
       1,     1,     4,     3,     4,     3,     1,     1,     2,     1,
       1,     1,     1,     1,     1,     1
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
  "\"]\"", "\";\"", "\",\"", "\".\"", "\"?\"", "\"=>\"", "\"..\"",
  "\"break\"", "\"case\"", "\"continue\"", "\"class\"", "\"catch\"",
  "\"conversion\"", "\"default\"", "\"do\"", "\"delete\"", "\"else\"",
  "\"enum\"", "\"for\"", "\"finally\"", "\"false\"", "\"get\"", "\"goto\"",
  "\"if\"", "\"namespace\"", "\"new\"", "\"operator\"", "\"return\"",
  "\"set\"", "\"switch\"", "\"throw\"", "\"try\"", "\"true\"", "\"this\"",
  "\"using\"", "\"while\"", "L_IDENT", "L_STRING", "L_NUMBER", "L_CHAR",
  "$accept", "start", "using", "using_low", "filestmnt", "filestmnts",
  "classstmnt", "classstmnts", "codestmnt", "codestmnts", "type_ident",
  "type", "typelist", "encpsltn", "encpsltn_n_cls", "template_def",
  "template_defs", "template_use", "template", "namespace", "enum",
  "enum_body", "enum_values", "enum_value", "class", "classhead",
  "classbody", "mthd", "mthd_head", "mthd_args", "mthd_body",
  "mthd_arglist", "mthd_arg", "cnvrsn", "ucnvrsn", "uecnvrsn", "mthdop",
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
       0,   143,   143,   144,   146,   147,   149,   150,   151,   153,
     154,   155,   156,   157,   158,   160,   161,   163,   164,   165,
     166,   167,   168,   169,   170,   172,   173,   175,   176,   177,
     178,   179,   180,   181,   182,   183,   184,   185,   186,   188,
     189,   192,   193,   195,   196,   197,   198,   200,   201,   202,
     205,   206,   207,   209,   210,   213,   214,   216,   217,   218,
     220,   221,   223,   226,   229,   230,   232,   233,   235,   236,
     237,   238,   239,   241,   242,   245,   246,   247,   248,   250,
     252,   255,   256,   258,   259,   261,   262,   264,   265,   267,
     268,   269,   271,   272,   275,   277,   280,   284,   285,   287,
     289,   290,   291,   292,   294,   295,   296,   297,   299,   300,
     302,   303,   304,   305,   306,   307,   308,   309,   310,   311,
     312,   313,   314,   315,   316,   317,   318,   319,   320,   321,
     322,   325,   327,   328,   331,   333,   334,   336,   337,   338,
     339,   340,   342,   343,   344,   345,   347,   348,   351,   352,
     355,   356,   358,   359,   360,   361,   362,   363,   364,   365,
     367,   368,   369,   371,   372,   375,   376,   379,   381,   382,
     384,   385,   386,   388,   389,   392,   394,   395,   397,   398,
     400,   402,   403,   404,   407,   408,   409,   410,   411,   412,
     413,   414,   415,   417,   419,   420,   422,   423,   425,   427,
     428,   429,   431,   432,   434,   435,   437,   438,   440,   441,
     442,   443,   445,   446,   447,   448,   449,   451,   452,   453,
     455,   456,   457,   458,   460,   461,   462,   463,   464,   466,
     467,   468,   470,   471,   473,   474,   475,   476,   478,   479,
     480,   481,   483,   485,   487,   488,   490,   491,   492,   493,
     495,   496,   497,   498,   499,   500
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
#line 3367 "parser.tab.cc" // lalr1.cc:1167
#line 502 "parser.y" // lalr1.cc:1168




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
#if _DEBUG
        std::cout << "\u001b[33m" << "TOKEN: " << token.to_string() << "\u001b[0m" << std::endl;
#endif
         parser::location_type loc;
         loc.begin.line = token.line;
         loc.begin.column = token.column;
         loc.end.line = token.line;
         loc.end.column = token.column + token.contents.length();

         switch (token.type)
         {
         case tokenizer::etoken::eof:
         case tokenizer::etoken::invalid:
         default:
#if _DEBUG
         {
             tokenizer.undo_prev();
             auto token = tokenizer.next();
         }
#endif
             return parser::make_NA(token, loc);


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
         }
     }
}
