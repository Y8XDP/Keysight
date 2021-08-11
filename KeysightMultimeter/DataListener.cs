using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeysightMultimeter
{
    public interface DataListener
    {
        void onDataAccepted(string data);
    }
}