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
        public static Xbox XBox { get { return _xbox; } }
        static Xbox _xbox = new Xbox();

        public static void FindXBox(string xbox)
        {
            WaitDialog.Show("Finding XBox...");
            new Thread(new ParameterizedThreadStart(Connect)).Start(xbox); 
        }

        public static void FindXBox()
        {
            WaitDialog.Show("Finding XBox...");
            new Thread(new ThreadStart(Connect)).Start(); 
        }

        static void Connect(object xboxAddress)
        {
            try
            {
                if (!XBox.Connected || !XBox.Ping())
                {
                    _xbox.Connect((string)xboxAddress);
                    UpdateConnectionInfo();
                }
            }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            { WaitDialog.Hide(); }

            LaunchHalo2();
        }

        static void Connect()
        {
            try
            {
                if (!XBox.Connected || !XBox.Ping())
                {
                    _xbox.Connect();
                    UpdateConnectionInfo();
                }
            }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            { WaitDialog.Hide(); }

            LaunchHalo2();
        }

        static void AsyncReconnect()
        { AsyncReconnect("Waiting For XBox..."); }

        static void AsyncReconnect(string message)
        {
            WaitDialog.Show(message);
            new Thread(new ThreadStart(Reconnect)).Start();
        }

        static void Reconnect()
        {
            string ip = XBox.DebugIP.ToString();
            Thread.Sleep(0);
            try
            {
                do { XBox.ConnectToIP(ip); } while (!XBox.Connected && WaitDialog.Visible);
                UpdateConnectionInfo();
            }
            catch (Exception e)
            { if (WaitDialog.Visible) MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            { WaitDialog.Hide(); }

            LaunchHalo2();
        }

        static void UpdateConnectionInfo()
        {
            DebugConnectionInfo = Program.XBox.DebugName + " - " + Program.XBox.DebugIP;
            if (CarnageHistory != null) CarnageHistory.UpdateConnectionInfo();
            if (MapDownloader != null) MapDownloader.UpdateConnectionInfo();
        }

        static bool Halo2IsRunning()
        {
            foreach (ModuleInfo mi in XBox.Modules)
            {
                if (mi.Name != "halo2ship.exe") continue;
                if (mi.Checksum == 5928412u) return true;
                if (MessageBox.Show("Incorrect Halo 2 Version Running! Launch " + Halo2XBE() + "?", "Problem", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                    == DialogResult.Yes)
                    return false;
                else
                {
                    //TODO: Prevent Stat Recording
                    return true;
                }
            }
            return false;
        }

        static void LaunchHalo2()
        {
            if (!XBox.Connected) new ConnectionSettings().ShowDialog();
            if (!XBox.Connected) return;

            while(!Halo2IsRunning())
            {
                if (XBox.FileExists(Halo2XBE()))
                {
                    XBox.LaunchTitle(Halo2XBE(), false);
                    AsyncReconnect("Launching Halo 2...");
                    if (Halo2IsRunning()) return;
                }

                if (new Halo2Settings().ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                    break;
                }
            }
        }

        public static string Halo2XBE()
        { return Path.Combine(Properties.Settings.Default.Halo2Dir, "default.xbe"); }
    };
}