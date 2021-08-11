using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeysightMultimeter
{
    public class TypeUnknownException: Exception
    {
        public TypeUnknownException(string msg): base(msg) {} 
    }
}