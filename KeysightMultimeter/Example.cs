using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace KeysightMultimeter
{
    public class Example: DataListener
    {
        private static U2741A instrument;

        public void onDataAccepted(string data)
        {
            Console.WriteLine(data);
        }

        public void start()
        {
            instrument = new U2741A();
            instrument.openSession("USB0::0x0957::0x4918::MY57029021::INSTR");
            instrument.setMeasType(U2741A.MeasType.VoltAC);
            instrument.setListener(this);

            instrument.startReadingData();

            ConsoleKeyInfo k = Console.ReadKey();

            if (k.KeyChar == '0')
            {
                instrument.stopReadingData();
                instrument.closeSession();
            }
        }
    }
}