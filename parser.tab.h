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
    EXCLAMATIONMARKEQUAL = 288,
    EXCLAMATIONMARK = 289,
    CURLYO = 290,
    CURLYC = 291,
    ROUNDO = 292,
    ROUNDC = 293,
    SQUAREO = 294,
    SQUAREC = 295,
    SEMICOLON = 296,
    COMMA = 297,
    DOT = 298,
    QUESTIONMARK = 299,
    ARROWHEAD = 300,
    CLASS = 301,
    GET = 302,
    SET = 303,
    NAMESPACE = 304,
    IF = 305,
    FOR = 306,
    ELSE = 307,
    WHILE = 308,
    DO = 309,
    SWITCH = 310,
    CASE = 311,
    DEFAULT = 312,
    return = 313,
    THROW = 314,
    GOTO = 315,
    TRY = 316,
    CATCH = 317,
    FINALLY = 318,
    OPERATOR = 319,
    USING = 320,
    ENUM = 321,
    DOTDOT = 322,
    TRUE = 323,
    FLASE = 324,
    THIS = 325,
    NEW = 326,
    DELETE = 327,
    IDENT = 328,
    L_STRING = 329,
    L_NUMBER = 330,
    L_CHAR = 331
  };
#endif

/* Value type.  */
#if ! defined YYSTYPE && ! defined YYSTYPE_IS_DECLARED

union YYSTYPE
{
#line 4 "parser.y" /* glr.c:197  */

    token token;

#line 135 "parser.tab.h" /* glr.c:197  */
};

typedef union YYSTYPE YYSTYPE;
# define YYSTYPE_IS_TRIVIAL 1
# define YYSTYPE_IS_DECLARED 1
#endif


extern YYSTYPE yylval;

int yyparse (void);

#endif /* !YY_YY_PARSER_TAB_H_INCLUDED  */
