using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yelo.Debug;
using System.Threading;
using System.Xml;
using System.IO;
using Yelo.Debug.Exceptions;

namespace Yelo.Carnage
{
    static partial class Program
    {
        static void StartWatchingHalo2()
        {
            XBox.SetMemory(0x1C79E5, 0x68, 0xF6, 0x79, 0x1C, 0x00, 0xC3); //Enable AI in MP
            XBox.SetBreakPoint(0x233194); //Breakpoint PGCR
            new Thread(new ThreadStart(WatchHalo2)).Start();
        }

        static void WatchHalo2()
        {
            Xbox notifer = new Xbox();
            notifer.ConnectToIP(XBox.DebugIP.ToString());
            notifer.EnableNotificationSession = true;
            notifer.RegisterNotificationSession();
            while (MainThreadRunning)
            {
                notifer.ReceiveNotifications();
                foreach (string note in notifer.Notifications)
                {
                    if (note == "Test")
                    {
                        bool test = true;
                    }
                }
                notifer.Notifications.Clear();
            }
            notifer.Disconnect();
        }
    }
}
