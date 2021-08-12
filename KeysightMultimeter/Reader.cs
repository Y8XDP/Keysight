
namespace KeysightMultimeter
{
    using System.Threading;
    using NationalInstruments.Visa;

    public class Reader
    {
        private const string RecModeCommand = "SYST:REM\n";
        private const string LocModeCommand = "SYST:LOC\n";
        private const string ReadCommand = "READ?\n";

        public OnDataAccepted dataAccepted;

        private MessageBasedSession session;
        private ResourceManager manager;

        private bool isReading = false;

        public delegate void OnDataAccepted(string data);

        // resourceName = DeviceID
        public void OpenSession(string resourceName)
        {
            this.manager = new ResourceManager();
            this.session = (MessageBasedSession)this.manager.Open(resourceName);
            this.session.TimeoutMilliseconds = 5000;

            this.Write(RecModeCommand);
        }

        public void CloseSession()
        {
            this.Write(LocModeCommand);
            this.session.Dispose();
            this.manager.Dispose();
        }

        public bool Write(string msg)
        {
            if (this.session != null)
            {
                this.session.RawIO.Write(msg);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartReadingData()
        {
            this.isReading = true;

            new Thread(new ThreadStart(() =>
            {
                while (this.isReading)
                {
                    dataAccepted(Read());
                }
            })).Start();
        }

        public void StopReadingData()
        {
            this.isReading = false;
        }

        public virtual string Read()
        {
            this.Write(ReadCommand);
            string reply = this.session.RawIO.ReadString();

            return reply.TrimEnd(new char[] { '\n', '\r' });
        }
    }
}