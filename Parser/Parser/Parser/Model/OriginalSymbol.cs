﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public abstract class OriginalSymbol
    {
        public object First { get; set; }
        public object Second { get; set; }
        public object Arity { get; set; }
    }
}
