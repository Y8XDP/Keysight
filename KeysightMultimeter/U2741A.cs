
namespace KeysightMultimeter
{
    public class U2741A : Reader
    {
        private MeasType currentType = MeasType.Unknown;

        public enum MeasType
        {
            // Напряжение постоянный ток
            VoltDc,

            // Напряжение переменный ток
            VoltAc,

            // Сила постоянный ток
            AmpDc,

            // Сила переменный ток
            AmpAc,

            // Сопротивление
            Resist,

            // Неизвестный тип до первой установки типа 
            Unknown
        }

        // Установка типа измеряемых данных
        public void SetMeasType(MeasType type)
        {
            var isSuccess = true;

            switch (type)
            {
                case MeasType.VoltDc:
                    this.Write("CONF:VOLT:DC AUTO\n");
                    break;
                case MeasType.VoltAc:
                    this.Write("CONF:VOLT:AC AUTO\n");
                    break;
                case MeasType.AmpDc:
                    this.Write("CONF:CURR:DC AUTO\n");
                    break;
                case MeasType.AmpAc:
                    this.Write("CONF:CURR:AC AUTO\n");
                    break;
                case MeasType.Resist:
                    this.Write("CONF:RES AUTO\n");
                    break;
            }

            if (isSuccess)
            {
                this.currentType = type;
            }
        }

        public override string Read()
        {
            if (this.currentType == MeasType.Unknown)
            {
                throw new TypeUnknownException("Не установлен тип измеряемых данных");
            }

            return base.Read();
        }
    }
}