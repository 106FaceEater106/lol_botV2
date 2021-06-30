using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows;

using WindowsInput;
using WindowsInput.Native;
using System.Windows.Forms;

namespace LeagueBotV3 {

    public struct Rect {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }

    public static class Windows {

        //https://github.com/michaelnoonan/inputsimulator
        private static InputSimulator simulator = new();

        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SWP_NOMOVE = 0x0200;

        #region Windows
        public static bool hasWindow(string proc) {
            try {
                Process[] processes = Process.GetProcessesByName(proc);

                if (processes.Length > 1) {
                    DBG.log($"More than one proc with name {proc}. using first", MessageLevel.Warning);
                } else if(processes.Length == 0) {
                    return false;
                }

                return processes[0].MainWindowHandle != IntPtr.Zero;

            } catch (InvalidOperationException) {
                return false;
            } catch (Exception err) {
                DBG.log(err,MessageLevel.Critical);
                return false;
            }
        }

        public static void bringWindowToFront(string proc) {
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length > 1) {
                DBG.log($"More than one proc with name {proc}. using first", MessageLevel.Warning);
            } else if (processes.Length == 0) {
                return;
            }

            bringWindowToFront(processes[0].MainWindowHandle);
        }

        public static Point lolToScreenSpace(string name, Point lolPoint) {
            Process process = Process.GetProcessesByName(name).FirstOrDefault();

            if (process == null || process?.MainWindowHandle == IntPtr.Zero) return null;

            IntPtr handle = process.MainWindowHandle;

            Rect rct = new();
            GetWindowRect(handle, ref rct);

            return new Point { X = rct.Left + lolPoint.X, Y = rct.Top + lolPoint.Y };
        }

        public static void bringWindowToFront(IntPtr ptr) {
            
            if(ptr == IntPtr.Zero) {
                DBG.log($"No window to bting to front");
            }
            
            try {
                SetForegroundWindow(ptr);
            } catch(Exception err) {
                DBG.log(err,MessageLevel.Critical);
            }
        }

        public static Rect getWindowRect(IntPtr ptr) {
            Rect o = new();
            GetWindowRect(ptr,ref o);
            return o;
        }
        #endregion

        #region Keyboard
        public static void sendText(string keySet) {
            simulator.Keyboard.TextEntry(keySet);
        }

        public static void pressKey(VirtualKeyCode ck) {
            simulator.Keyboard.KeyPress(ck);
        }
        #endregion

        #region Mouse

        public static void MoveMouse(Point point, bool press = false) => MoveMouse(point.X,point.Y,press);
        public static void MoveMouse(double x, double y, bool press = false) {

            x = (x/double.Parse(Global.dict["SCREENWIDTH"])) * 65535;
            y = (y/double.Parse(Global.dict["SCREENHEIGHT"])) * 65535;


            simulator.Mouse.MoveMouseTo(x,y);
            if(press) {
                simulator.Mouse.LeftButtonClick();
            }
        }
        #endregion


        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
    }
}
