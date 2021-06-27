using System;
using System.Collections.Generic;
using WindowsInput;
using WindowsInput.Native;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace VirtualDesktopSwapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Clear();
            ShowTitle();
            Thread.Sleep(1750);
            int VDCount = GetVirtualDesktopCount();
            Clear();
            ShowTitle();
            int TimeBetween = GetTimeBetweenSwaps() * 1000;
            Clear();
            ShowTitle();
            RunVirtualDesktopSwapper(VDCount, TimeBetween);
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

        private static int GetVirtualDesktopCount()
        {
            int _vdCount;
            Write("Enter the total number of Virtual Desktops there are >> ");
            while (!Int32.TryParse(ReadLine(), out _vdCount))
            {
                Write("Not a valid input. Enter the total number of Virtual Desktops there are >> ");
            }
            return _vdCount;
        }

        private static int GetTimeBetweenSwaps()
        {
            int _time;
            Write("Enter the amount of time (in seconds) between Virtual Desktop swaps >> ");
            while (!Int32.TryParse(ReadLine(), out _time))
            {
                Write("Not a valid input. Enter the amount of time (in seconds) between Virtual Desktop swaps >> ");
            }
            return _time;
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
                    Thread.Sleep(_time);
                    _sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN }, VirtualKeyCode.RIGHT);
                }
            }
        }
    }
}
