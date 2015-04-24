using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yelo.Debug;
using System.Threading;
using System.Xml;
using System.IO;
using Yelo.Debug.Exceptions;
using Yelo.Neighborhood.System_Tools;
using Yelo.Shared;

namespace Yelo.Neighborhood
{
    static class Program
    {
        public static XBoxExplorer MainWindow { get { return _mainWindow; } }
        static XBoxExplorer _mainWindow;

        public static MemoryHacker MemoryHacker { get { return _memoryHacker; } }
        static MemoryHacker _memoryHacker;

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

            _LEDStateChanger = new LEDStateChanger();
            _memoryHacker = new MemoryHacker();

            XBoxIO.LoadSettings();
            if (XBoxIO.FindXBox())
                ShowXBoxExplorer();
        }

        static void ShowXBoxExplorer()
        {
            if (_mainWindow == null)
            {
                _mainWindow = new XBoxExplorer();
                _mainWindow.ShowDialog();
                XBoxIO.XBox.Disconnect();
            }
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
    };
}