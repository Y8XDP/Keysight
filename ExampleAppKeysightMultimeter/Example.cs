
namespace ExampleAppKeysightMultimeter
{
    using System;
    using KeysightMultimeter;

    class Example
    {

        private U2741A instrument;

        public void Start()
        {
            this.instrument = new U2741A();
            this.instrument.OpenSession("DEVICE_ID");
            this.instrument.SetMeasType(U2741A.MeasType.VoltDc);

            this.instrument.dataAccepted += OnDataAccepted;

            this.instrument.StartReadingData();

            ConsoleKeyInfo k = Console.ReadKey();

            if (k.KeyChar == '0')
            {
                this.instrument.StopReadingData();
                this.instrument.CloseSession();
            }
        }

        private void OnDataAccepted(string data)
        {
            Console.WriteLine(data);
        }
    }
}
