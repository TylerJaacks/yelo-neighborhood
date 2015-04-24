using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yelo.Debug;
using System.Threading;
using System.Xml;
using System.IO;
using Yelo.Debug.Exceptions;
using Yelo.Neighborhood.System_Tools;

namespace Yelo.Neighborhood
{
    static class Program
    {
        public static int Version { get { return 11; } }

        public static Xbox XBox { get { return _xbox; } }
        static Xbox _xbox;

        public static Main MainWindow { get { return _mainWindow; } }
        static Main _mainWindow;

        public static ScreenshotTool ScreenshotTool { get { return _screenshotTool; } }
        static ScreenshotTool _screenshotTool;

        public static MemoryHacker MemoryHacker { get { return _memoryHacker; } }
        static MemoryHacker _memoryHacker;

        public static XBoxLocator XBoxLocator { get { return _xboxLocator; } }
        static XBoxLocator _xboxLocator;

        public static LEDStateChanger LEDStateChanger { get { return _LEDStateChanger; } }
        static LEDStateChanger _LEDStateChanger;

        public static List<Executable> Executables { get { return _executables; } }
        static List<Executable> _executables = new List<Executable>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadExecutables();

            _xboxLocator = new XBoxLocator();
            _screenshotTool = new ScreenshotTool();
            _LEDStateChanger = new LEDStateChanger();
            _memoryHacker = new MemoryHacker();

            if (Properties.Settings.Default.AutoDiscover) FindXBox();
            else FindXBox(Properties.Settings.Default.XBoxIP);
        }

        public static void LoadExecutables()
        {
            if (!File.Exists("Executables.xml"))
            {
				using (var sw = File.CreateText("Executables.xml"))
				{
					sw.WriteLine("<Executables></Executables>");
				}
            }
			using (var xr = XmlReader.Create(File.OpenRead("Executables.xml")))
			{
				Executable workingExe = null;
				while (!xr.EOF)
				{
					xr.Read();
					switch (xr.Name)
					{
						case "Executable":
							if (xr.NodeType == XmlNodeType.EndElement) continue;
							workingExe = new Executable()
                            { 
                                Name = xr.GetAttribute("Name"),
                                Filename = xr.GetAttribute("Filename")
                            };
							_executables.Add(workingExe);
							break;
						case "Script":
							if (xr.NodeType == XmlNodeType.EndElement) continue;
							Executable.Script script = new Executable.Script()
                            {
							    Name = xr.GetAttribute("Name"),
							    FileType = xr.GetAttribute("FileType"),
							    Code = xr.ReadInnerXml(),
                            };
							workingExe.Scripts.Add(script);
							break;
					}
				}
			}
        }

        static void AsyncConnect(string xbox)
        { new Thread(new ParameterizedThreadStart(Connect)).Start(xbox); }

        static void AsyncConnect()
        { new Thread(new ThreadStart(Connect)).Start(); }

        static void Connect(object xbox)
        {
            try
            { _xbox.Connect((string)xbox); }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            { _xboxLocator.Hide(); }
        }

        static void Connect()
        {
            try
            { _xbox.Connect(); }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            { _xboxLocator.Hide(); }
        }

        public static void FindXBox()
        {
             _xbox = new Xbox();
             AsyncConnect();
            _xboxLocator.ShowDialog();

            if (XBox.Connected)
            {
                if (_mainWindow == null)
                {
                    _mainWindow = new Main();
                    _mainWindow.ShowDialog();
                    XBox.Disconnect();
                }
            }
            else new Settings().ShowDialog();
        }

        public static void FindXBox(string xbox)
        {
            _xbox = new Xbox();
            AsyncConnect(xbox);
            _xboxLocator.ShowDialog();

            if (XBox.Connected)
            {
                if (_mainWindow == null)
                {
                    _mainWindow = new Main();
                    _mainWindow.ShowDialog();
                    XBox.Disconnect();
                }
            }
            else new Settings().ShowDialog();
        }
    };
}