﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public interface Variable : IInstruction
    {
        Identifier getVariableIdentifier();
    }
}