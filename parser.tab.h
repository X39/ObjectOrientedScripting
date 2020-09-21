/* A Bison parser, made by GNU Bison 3.0.4.  */

/* Skeleton interface for Bison GLR parsers in C

   Copyright (C) 2002-2015 Free Software Foundation, Inc.

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.  */

/* As a special exception, you may create a larger work that contains
   part or all of the Bison parser skeleton and distribute that work
   under terms of your choice, so long as that work isn't itself a
   parser generator using the skeleton or a modified version thereof
   as a parser skeleton.  Alternatively, if you modify or redistribute
   the parser skeleton itself, you may (at your option) remove this
   special exception, which will cause the skeleton and the resulting
   Bison output files to be licensed under the GNU General Public
   License without this special exception.

   This special exception was added by the Free Software Foundation in
   version 2.2 of Bison.  */

#ifndef YY_YY_PARSER_TAB_H_INCLUDED
# define YY_YY_PARSER_TAB_H_INCLUDED
/* Debug traces.  */
#ifndef YYDEBUG
# define YYDEBUG 1
#endif
#if YYDEBUG
extern int yydebug;
#endif

/* Token type.  */
#ifndef YYTOKENTYPE
# define YYTOKENTYPE
  enum yytokentype
  {
    PUBLIC = 258,
    LOCAL = 259,
    DERIVED = 260,
    PRIVATE = 261,
    UNBOUND = 262,
    EQUAL = 263,
    ANDAND = 264,
    AND = 265,
    SLASH = 266,
    STAR = 267,
    PERCENT = 268,
    VLINEVLINE = 269,
    VLINE = 270,
    CIRCUMFLEX = 271,
    TILDE = 272,
    COLON = 273,
    COLONCOLON = 274,
    PLUS = 275,
    PLUSPLUS = 276,
    MINUS = 277,
    MINUSMINUS = 278,
    LTEQUAL = 279,
    LT = 280,
    LTLT = 281,
    LTLTLT = 282,
    GTEQUAL = 283,
    GT = 284,
    GTGT = 285,
    GTGTGT = 286,
    EQUALEQUAL = 287,
    TILDEEQUAL = 288,
    EXCLAMATIONMARKEQUAL = 289,
    EXCLAMATIONMARK = 290,
    CURLYO = 291,
    CURLYC = 292,
    ROUNDO = 293,
    ROUNDC = 294,
    SQUAREO = 295,
    SQUAREC = 296,
    SEMICOLON = 297,
    COMMA = 298,
    DOT = 299,
    QUESTIONMARK = 300,
    ARROWHEAD = 301,
    CLASS = 302,
    GET = 303,
    SET = 304,
    NAMESPACE = 305,
    IF = 306,
    FOR = 307,
    ELSE = 308,
    WHILE = 309,
    DO = 310,
    SWITCH = 311,
    CASE = 312,
    DEFAULT = 313,
    return = 314,
    THROW = 315,
    GOTO = 316,
    TRY = 317,
    CATCH = 318,
    FINALLY = 319,
    OPERATOR = 320,
    USING = 321,
    ENUM = 322,
    DOTDOT = 323,
    TRUE = 324,
    FLASE = 325,
    THIS = 326,
    NEW = 327,
    DELETE = 328,
    IDENT = 329,
    L_STRING = 330,
    L_NUMBER = 331,
    L_CHAR = 332
  };
#endif

/* Value type.  */
#if ! defined YYSTYPE && ! defined YYSTYPE_IS_DECLARED

union YYSTYPE
{
#line 4 "parser.y" /* glr.c:197  */

    token token;

#line 136 "parser.tab.h" /* glr.c:197  */
};

typedef union YYSTYPE YYSTYPE;
# define YYSTYPE_IS_TRIVIAL 1
# define YYSTYPE_IS_DECLARED 1
#endif


extern YYSTYPE yylval;

int yyparse (void);

#endif /* !YY_YY_PARSER_TAB_H_INCLUDED  */
