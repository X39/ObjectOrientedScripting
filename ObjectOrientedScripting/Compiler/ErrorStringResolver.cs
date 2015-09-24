using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public static class ErrorStringResolver
    {
        public enum ErrorCodeEnum
        {
            UNKNOWN,
            C0000, C0001, C0002, C0003, C0004,
            C0005, C0006, C0007, C0008, C0009,
            C0010, C0011, C0012, C0013, C0014,
            C0015, C0016, C0017, C0018, C0019,
            C0020, C0021, C0022, C0023, C0024,
            C0025, C0026, C0027, C0028, C0029,
            C0030, C0031
        }
        public static string resolve(ErrorCodeEnum errCode, int line = -1, int pos = -1)
        {
            return Enum.GetName(typeof(ErrorCodeEnum), errCode) + ": " + doResolve(errCode) + ". " + (line == -1 ? "" : "line " + line.ToString() + (pos == -1 ? "" : " col " + pos.ToString()));
        }
        private static string doResolve(ErrorCodeEnum errCode)
        {
            switch (errCode)
            {
                case ErrorCodeEnum.C0000: return "Could not locate EXPRESSION object for VariableAssignment";
                case ErrorCodeEnum.C0001: return "Type missmatch for EXPRESSION and VariableAssignment";
                case ErrorCodeEnum.C0002: return "Could not locate CLASS for THIS reference";
                case ErrorCodeEnum.C0003: return "Could not locate variable declaration";
                case ErrorCodeEnum.C0008: return "Could not locate variable or class declaration";
                case ErrorCodeEnum.C0004:
                case ErrorCodeEnum.C0005:
                case ErrorCodeEnum.C0006:
                case ErrorCodeEnum.C0007: return "Could not locate function object for IDENT";
                case ErrorCodeEnum.C0029: return "Could not locate class declaration";
                case ErrorCodeEnum.C0009: return "this reference used inside of static function";
                case ErrorCodeEnum.C0010: return "this reference can just be used with instance access";
                case ErrorCodeEnum.C0011: return "NewArray has unknown child (!= Expression)";
                case ErrorCodeEnum.C0012: return "Array type is not consistent (TypeMissmatch)";
                case ErrorCodeEnum.C0013: return "Variable gets invalid type assigned";
                case ErrorCodeEnum.C0014: return "Instance access on non-object ident";
                case ErrorCodeEnum.C0015: return "Cannot cast non-object to object";
                case ErrorCodeEnum.C0016: return "Cannot cast object to non-object but string";
                case ErrorCodeEnum.C0025: return "Cannot cast non-strict object with static cast (use dynamic cast instead)";
                case ErrorCodeEnum.C0031: return "Cannot cast object to itself";
                case ErrorCodeEnum.C0017: return "Too many arguments for function";
                case ErrorCodeEnum.C0018: return "Not enough arguments for function";
                case ErrorCodeEnum.C0019: return "Argument type missmatch for function";
                case ErrorCodeEnum.C0020: return "SQF Command is missing LArgs";
                case ErrorCodeEnum.C0021: return "SQF Command should have LArgs";
                case ErrorCodeEnum.C0022: return "SQF Command is missing RArgs";
                case ErrorCodeEnum.C0023: return "SQF Command should have RArgs";
                case ErrorCodeEnum.C0024: return "SQF Command is not registered in SupportInfo list";
                case ErrorCodeEnum.C0026: return "LArg has to refer to an object";
                case ErrorCodeEnum.C0027: return "RArg has to refer to a class or an interface";
                case ErrorCodeEnum.C0028: return "LArg is not allowed to be casted";
                case ErrorCodeEnum.C0030: return "Missing 'this' reference in front of class variable";
                default: return "Unknown Error";
            }
        }
    }
}
