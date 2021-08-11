using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.Visa;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace KeysightMultimeter
{
    public abstract class Reader
    {
        private MessageBasedSession mSession;
        private ResourceManager manager;

        private DataListener listener;

        Boolean isReading = false;

        //resourceName = DeviceID
        public void openSession(string resourceName)
        {
            manager = new ResourceManager();
            mSession = (MessageBasedSession)manager.Open(resourceName);
            mSession.TimeoutMilliseconds = 5000;

            write("SYST:REM\n");
        }

        public void closeSession()
        {
            write("SYST:LOC\n");
            mSession.Dispose();
            manager.Dispose();
        }

        public Boolean write(String msg)
        {
            if (mSession != null)
            {
                mSession.RawIO.Write(msg);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string read()
        {
            write("READ?\n");
            string reply = mSession.RawIO.ReadString();

            return reply.TrimEnd(new char[]{'\n','\r'});
        }


        public void setListener(DataListener listener)
        {
            this.listener = listener;
        }

        public void startReadingData()
        {
            Console.WriteLine(listener != null);

            if (listener != null && !isReading)
            {
                isReading = true;

                Thread thread = new Thread(new ThreadStart(() =>
                {
                    while (isReading)
                    {
                        listener.onDataAccepted(read());
                    }
                }));

                thread.Start();
            }
        }

        public void stopReadingData()
        {
            isReading = false;
        }
    }
}