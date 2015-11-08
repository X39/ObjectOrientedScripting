using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public static class ErrorStringResolver
    {


        public enum LinkerErrorCode
        {
            UNKNOWN,
            LNK0000, LNK0001, LNK0002, LNK0003, LNK0004,
            LNK0005, LNK0006, LNK0007, LNK0008, LNK0009,
            LNK0010, LNK0011, LNK0012, LNK0013, LNK0014,
            LNK0015, LNK0016, LNK0017, LNK0018, LNK0019,
            LNK0020, LNK0021, LNK0022, LNK0023, LNK0024,
            LNK0025, LNK0026, LNK0027, LNK0028, LNK0029,
            LNK0030, LNK0031, LNK0032, LNK0033, LNK0034,
            LNK0035, LNK0036, LNK0037, LNK0038, LNK0039,
            LNK0040, LNK0041, LNK0042, LNK0043, LNK0044,
            LNK0045

        }

        public static string resolve(LinkerErrorCode errCode, int line = -1, int pos = -1, string file = default(string))
        {
            return Enum.GetName(typeof(LinkerErrorCode), errCode) + ": " + doResolve(errCode) + ". " + (line == -1 ? "" : "line " + line.ToString() + (pos == -1 ? "" : " col " + pos.ToString()) + (string.IsNullOrEmpty(file) ? "" : " file '" + file + "'"));
        }
        private static string doResolve(LinkerErrorCode errCode)
        {
            switch (errCode)
            {
                //case LinkerErrorCode.LNK0000: return "Ident using acces operator '::' at non-allowed room";
                case LinkerErrorCode.LNK0001: return "Could not locate function reference for Ident";
                case LinkerErrorCode.LNK0002: return "Type Missmatch, function with given ArgList could not be located";
                case LinkerErrorCode.LNK0003: return "Invalid Encapsulation, accessing private scope outside of class context";
                case LinkerErrorCode.LNK0004: return "Invalid Encapsulation, accessing protected scope outside of parented context";
                case LinkerErrorCode.LNK0005: return "Invalid Operator, object is not implementing ArrayAccess operator";
                case LinkerErrorCode.LNK0006: return "Invalid Operator, vartype is not supporting ArrayAccess operator";
                case LinkerErrorCode.LNK0007: return "Missing Type, cannot evaluate type for 'auto' variable due to missing assign";
                case LinkerErrorCode.LNK0008: return "Type Missmatch, LValue type differs from RValue type";
                case LinkerErrorCode.LNK0009:
                case LinkerErrorCode.LNK0010:
                case LinkerErrorCode.LNK0011: return "Invalid Operation, variable is defined twice";
                case LinkerErrorCode.LNK0012: return "Could not locate reference for Ident";
                case LinkerErrorCode.LNK0013: return "Type Missmatch, Array type is not consistent";
                case LinkerErrorCode.LNK0014: return "Type Missmatch, Expression arg types not valid. ";
                case LinkerErrorCode.LNK0015: return "SQF Command is missing LArgs";
                case LinkerErrorCode.LNK0016: return "SQF Command should have LArgs";
                case LinkerErrorCode.LNK0017: return "SQF Command is missing RArgs";
                case LinkerErrorCode.LNK0018: return "SQF Command should have RArgs";
                case LinkerErrorCode.LNK0019: return "SQF Command is not registered in SupportInfo list";
                case LinkerErrorCode.LNK0020: return "Invalid case, default was experienced twice";
                case LinkerErrorCode.LNK0021: return "Function is not always returning a value";
                case LinkerErrorCode.LNK0022:
                case LinkerErrorCode.LNK0023: return "Type Missmatch, type differs from functions return type";
                case LinkerErrorCode.LNK0024: return "Type Missmatch, case type differs from switch type";
                case LinkerErrorCode.LNK0025: return "Invalid Operation, constructors are not allowed to return values";
                case LinkerErrorCode.LNK0026: return "Invalid Operation, constructors cannot be callen outside of a NEW operation";
                case LinkerErrorCode.LNK0027: return "Invalid Operation, entry points have to be static";
                case LinkerErrorCode.LNK0028: return "Type Missmatch, LArg has to refer to an object";
                case LinkerErrorCode.LNK0029: return "Type Missmatch, RArg has to refer to a class or an interface";
                case LinkerErrorCode.LNK0030: return "Type Missmatch, LArg is not allowed to be casted";
                case LinkerErrorCode.LNK0031: return "Cast Exception, non-object to object";
                case LinkerErrorCode.LNK0032: return "Cast Exception, non-static object got static-casted";
                case LinkerErrorCode.LNK0033: return "Cast Exception, object to non-object/{string}";
                case LinkerErrorCode.LNK0034:
                case LinkerErrorCode.LNK0035: return "Cast Exception, selfcast";
                case LinkerErrorCode.LNK0036: return "Cast Exception, non-object with dynamic cast";
                case LinkerErrorCode.LNK0037: return "Invalid Operation, implementing class";
                case LinkerErrorCode.LNK0038: return "Invalid Operation, extending interface";
                case LinkerErrorCode.LNK0039: return "Function overrides existing function";
                case LinkerErrorCode.LNK0040: return "Missing override keyword on overriding function";
                case LinkerErrorCode.LNK0041: return "Invalid Operation, too many arguments on override";
                case LinkerErrorCode.LNK0042: return "Invalid Operation, lacking arguments on override";
                case LinkerErrorCode.LNK0043: return "Type Missmatch, override has different type then base";
                case LinkerErrorCode.LNK0044: return "Type Missmatch, override return differs from base";
                case LinkerErrorCode.LNK0045: return "Resolving ident failed";


                default: return "Unknown Error, report to dev with reproduction code (fix other issues first).";
            }
        }


        //public enum ErrorCodeEnum
        //{
        //    UNKNOWN,
        //    C0000, C0001, C0002, C0003, C0004,
        //    C0005, C0006, C0007, C0008, C0009,
        //    C0010, C0011, C0012, C0013, C0014,
        //    C0015, C0016, C0017, C0018, C0019,
        //    C0020, C0021, C0022, C0023, C0024,
        //    C0025, C0026, C0027, C0028, C0029,
        //    C0030, C0031, C0032, C0033, C0034,
        //    C0035, C0036, C0037, C0038, C0039,
        //    C0040, C0041, C0042, C0043, C0044,
        //    C0045, C0046, C0047, C0048, C0049,
        //    C0050, C0051
        //}
        //public static string resolve(ErrorCodeEnum errCode, int line = -1, int pos = -1)
        //{
        //    return Enum.GetName(typeof(ErrorCodeEnum), errCode) + ": " + doResolve(errCode) + ". " + (line == -1 ? "" : "line " + line.ToString() + (pos == -1 ? "" : " col " + pos.ToString()));
        //}
        //private static string doResolve(ErrorCodeEnum errCode)
        //{
        //    switch (errCode)
        //    {
        //        case ErrorCodeEnum.C0000: return "Could not locate EXPRESSION object for VariableAssignment";
        //        case ErrorCodeEnum.C0001: return "Type missmatch for EXPRESSION and VariableAssignment";
        //        case ErrorCodeEnum.C0002: return "Could not locate CLASS for THIS reference";
        //        case ErrorCodeEnum.C0003: return "Could not locate variable declaration";
        //        case ErrorCodeEnum.C0008: return "Could not locate variable or class declaration";
        //        case ErrorCodeEnum.C0004:
        //        case ErrorCodeEnum.C0005:
        //        case ErrorCodeEnum.C0006:
        //        case ErrorCodeEnum.C0007: return "Could not locate function object for IDENT";
        //        case ErrorCodeEnum.C0029: return "Could not locate class declaration";
        //        case ErrorCodeEnum.C0009: return "this reference used inside of static function";
        //        case ErrorCodeEnum.C0010: return "this reference can just be used with instance access";
        //        case ErrorCodeEnum.C0011: return "NewArray has unknown child (!= Expression)";
        //        case ErrorCodeEnum.C0012: return "Array type is not consistent (TypeMissmatch)";
        //        case ErrorCodeEnum.C0013: return "Variable gets invalid type assigned";
        //        case ErrorCodeEnum.C0014: return "Instance access on non-object ident";
        //        case ErrorCodeEnum.C0015: return "Cannot cast non-object to object";
        //        case ErrorCodeEnum.C0016: return "Cannot cast object to non-object but string";
        //        case ErrorCodeEnum.C0025: return "Cannot cast non-strict object with static cast (use dynamic cast instead)";
        //        case ErrorCodeEnum.C0031: return "Cannot cast object to itself";
        //        case ErrorCodeEnum.C0017: return "Too many arguments for function";
        //        case ErrorCodeEnum.C0018: return "Not enough arguments for function";
        //        case ErrorCodeEnum.C0019: return "Argument type missmatch for function";
        //        case ErrorCodeEnum.C0020: return "SQF Command is missing LArgs";
        //        case ErrorCodeEnum.C0021: return "SQF Command should have LArgs";
        //        case ErrorCodeEnum.C0022: return "SQF Command is missing RArgs";
        //        case ErrorCodeEnum.C0023: return "SQF Command should have RArgs";
        //        case ErrorCodeEnum.C0024: return "SQF Command is not registered in SupportInfo list";
        //        case ErrorCodeEnum.C0026: return "LArg has to refer to an object";
        //        case ErrorCodeEnum.C0027: return "RArg has to refer to a class or an interface";
        //        case ErrorCodeEnum.C0028: return "LArg is not allowed to be casted";
        //        case ErrorCodeEnum.C0030: return "Missing 'this' reference in front of class variable";
        //        case ErrorCodeEnum.C0032: return "Constructors are not allowed to return anything but void";
        //        case ErrorCodeEnum.C0033: return "Function overrides existing function";
        //        case ErrorCodeEnum.C0034: return "Missing override keyword on overriding function";
        //        case ErrorCodeEnum.C0035: return "Function override has too many arguments";
        //        case ErrorCodeEnum.C0036: return "Function override lacks arguments";
        //        case ErrorCodeEnum.C0037: return "Function override type missmatch on argument";
        //        case ErrorCodeEnum.C0038: return "Function override return type missmatch";
        //        case ErrorCodeEnum.C0039: return "Return type is void but return instruction tries to return a value";
        //        case ErrorCodeEnum.C0040: return "Return type differs from function return type";
        //        case ErrorCodeEnum.C0041: return "Variable ReDeclaration found";
        //        case ErrorCodeEnum.C0042: return "Catch variable has to be string";
        //        case ErrorCodeEnum.C0043: return "Throw expression has to resolve to string";
        //        case ErrorCodeEnum.C0044: return "Case type not matching Switch type";
        //        case ErrorCodeEnum.C0045: return "Second 'default' case located";
        //        case ErrorCodeEnum.C0046: return "Interfaces are not allowed to be 'strict' type";
        //        case ErrorCodeEnum.C0047: return "Cannot cast staticly to interfaces";
        //        case ErrorCodeEnum.C0048: return "Dynamic cast is not allowed for primitive types";
        //        case ErrorCodeEnum.C0049: return "Type missmatch on template";
        //        case ErrorCodeEnum.C0050: return "It is not possible to implement classes";
        //        case ErrorCodeEnum.C0051: return "It is not possible to extend interfaces";
        //        default: return "Unknown Error, report to dev with reproduction code (fix other issues first).";
        //    }
        //}
    }
}
