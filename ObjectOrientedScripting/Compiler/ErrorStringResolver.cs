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
            C0000,
            C0001,
            C0002,
            C0003,
            C0004,
            C0005,
            C0006,
            C0007,
            C0008,
            C0009
        }
        public static string resolve(ErrorCodeEnum errCode, int line = -1, int pos = -1)
        {
            return Enum.GetName(typeof(ErrorCodeEnum), errCode) + ": " + doResolve(errCode) + ". " + (line == -1 ? "" : "line " + line.ToString() + (pos == -1 ? "" : " col " + pos.ToString()));
        }
        private static string doResolve(ErrorCodeEnum errCode)
        {
            switch (errCode)
            {
                case ErrorCodeEnum.C0000:
                    return "Could not locate EXPRESSION object for VariableAssignment";
                case ErrorCodeEnum.C0001:
                    return "Type missmatch for EXPRESSION and VariableAssignment";
                case ErrorCodeEnum.C0002:
                    return "Could not locate CLASS for THIS reference";
                case ErrorCodeEnum.C0003:
                    return "Could not locate VARIABLE for instance access";
                case ErrorCodeEnum.C0004: case ErrorCodeEnum.C0005: case ErrorCodeEnum.C0006: case ErrorCodeEnum.C0007:
                    return "Could not locate function object for IDENT";
                case ErrorCodeEnum.C0008: case ErrorCodeEnum.C0009:
                    return "Could not locate variable object for IDENT";
                default:
                    return "Unknown Error";
            }
        }
    }
}
