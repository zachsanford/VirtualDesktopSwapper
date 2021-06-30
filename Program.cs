using System;
using System.Collections.Generic;
using WindowsInput;
using WindowsInput.Native;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace VirtualDesktopSwapper
{
    class Program
    {
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);

        static void Main(string[] args)
        {
            Clear();
            ShowTitle();
            Thread.Sleep(1750);
            int[] settings = GetConfig();
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(handle, 6);
            RunVirtualDesktopSwapper(settings[0], settings[1]);
        }

        private static void ShowTitle()
        {
            WriteLine();
            WriteLine("_/      _/  _/  _/_/_/   _/_/_/_/  _/    _/      _/      _/");
            WriteLine(" _/    _/  _/  _/    _/    _/     _/    _/     _/_/     _/");
            WriteLine("  _/  _/  _/  _/_/_/      _/     _/    _/    _/  _/    _/");
            WriteLine("   _/_/  _/  _/    _/    _/     _/    _/   _/_/_/_/   _/");
            WriteLine("    _/  _/  _/    _/    _/       _/_/    _/      _/  _/_/_/_/");
            WriteLine();
            WriteLine("    _/_/      _/_/_/_/  _/_/    _/   _/  _/_/_/_/   _/_/_/    _/_/_/");
            WriteLine("   _/   _/   _/       _/   _/  _/  _/      _/     _/     _/  _/    _/");
            WriteLine("  _/    _/  _/_/_/      _/    _/_/        _/     _/     _/  _/_/_/");
            WriteLine(" _/   _/   _/       _/   _/  _/  _/      _/     _/     _/  _/");
            WriteLine("_/_/_/    _/_/_/_/   _/_/   _/    _/    _/       _/_/_/   _/");
            WriteLine();
            WriteLine("   _/_/   _/        _/      _/      _/_/_/    _/_/_/    _/_/_/_/  _/_/_/");
            WriteLine(" _/   _/  _/       _/     _/_/     _/    _/  _/    _/  _/        _/    _/");
            WriteLine("   _/     _/  _/  _/    _/  _/    _/_/_/    _/_/_/    _/_/_/    _/_/_/");
            WriteLine("_/   _/   _/_/ _/_/   _/_/_/_/   _/        _/        _/        _/    _/");
            WriteLine(" _/_/     _/    _/  _/      _/  _/        _/        _/_/_/_/  _/    _/");
            WriteLine();
        }

        private static void RunVirtualDesktopSwapper(int _vdCount, int _time)
        {
            InputSimulator _sim = new InputSimulator();
            Write("Press Ctrl + C to quit...");

            while (true)
            {
                for (int i = 0; i < _vdCount; i++)
                {
                    _sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN }, VirtualKeyCode.LEFT);
                }

                for (int i = 0; i < _vdCount; i++)
                {
                    Thread.Sleep(_time * 1000);
                    _sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN }, VirtualKeyCode.RIGHT);
                }
            }
        }

        private static int[] GetConfig()
        {
            int[] _configSettings = new int[2];
            string[] _disposableArray;
            string[] file = System.IO.File.ReadAllLines("vds.cfg");
            foreach (string line in file)
            {
                if (!line.StartsWith("#"))
                {
                    _disposableArray = line.Split('=');
                    try
                    {
                        if (_disposableArray[0] == "DESKTOPS")
                        {
                            _configSettings[0] = Convert.ToInt32(_disposableArray[1]);
                        }
                        else if (_disposableArray[0] == "TIME_BETWEEN_SWAP")
                        {
                            _configSettings[1] = Convert.ToInt32(_disposableArray[1]);
                        }
                        else
                        {
                            WriteLine("ERROR IN CONFIGURATION: Incorrect configuration type. Make sure settings are in a numeric value and there are no extra lines or values in the configuration file");
                            ReadLine();
                        }
                    }
                    catch (Exception)
                    {
                        WriteLine("ERROR IN CONFIGURATION: Wrong input type. Make sure settings are in a numeric value and there are no extra lines or values in the configuration file.");
                        WriteLine();
                        Write("Press any key to QUIT...");
                        ReadLine();
                        Environment.Exit(1);
                    }
                }
            }

            return _configSettings;
        }
    }
}
