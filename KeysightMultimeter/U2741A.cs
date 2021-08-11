using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeysightMultimeter
{
    public class U2741A: Reader
    {
        public enum MeasType
        {
            //Напряжение постоянный ток
            VoltDC,

            //Напряжение переменный ток
            VoltAC,

            //Сила постоянный ток
            AmpDC,

            //Сила переменный ток
            AmpAC,

            //Сопротивление
            Resist,

            //Неизвестный тип до первой установки типа 
            Unknown
        }

        MeasType currentType = MeasType.Unknown;

        //Установка типа измеряемых данных
        public void setMeasType(MeasType type)
        {
            Boolean isSuccess = true;

            switch (type)
            {
                case MeasType.VoltDC:
                    write("CONF:VOLT:DC AUTO\n");
                    break;
                case MeasType.VoltAC:
                    write("CONF:VOLT:AC AUTO\n");
                    break;
                case MeasType.AmpDC:
                    write("CONF:CURR:DC AUTO\n");
                    break;
                case MeasType.AmpAC:
                    write("CONF:CURR:AC AUTO\n");
                    break;
                case MeasType.Resist:
                    write("CONF:RES AUTO\n");
                    break;
            }

            if (isSuccess)
            {
                currentType = type;
            }
        }

        public string getResult()
        {
            if (currentType == MeasType.Unknown)
            {
                throw new TypeUnknownException("Не установлен тип измеряемых данных");
            }

            return read();
        }
    }
}