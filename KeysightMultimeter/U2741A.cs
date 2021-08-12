
namespace KeysightMultimeter
{
    public class U2741A : Reader
    {
        private const string VoltDcConf = "CONF:VOLT:DC AUTO\n";
        private const string VoltAcConf = "CONF:VOLT:AC AUTO\n";
        private const string AmpDcConf = "CONF:CURR:DC AUTO\n";
        private const string AmpAcConf = "CONF:CURR:AC AUTO\n";
        private const string ResistanceConf = "CONF:RES AUTO\n";

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
                    this.Write(VoltDcConf);
                    break;
                case MeasType.VoltAc:
                    this.Write(VoltAcConf);
                    break;
                case MeasType.AmpDc:
                    this.Write(AmpDcConf);
                    break;
                case MeasType.AmpAc:
                    this.Write(AmpAcConf);
                    break;
                case MeasType.Resist:
                    this.Write(ResistanceConf);
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