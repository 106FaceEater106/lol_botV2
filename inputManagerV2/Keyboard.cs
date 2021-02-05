using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace inputManagerV2 {
    public class kb {

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        public static void Send(string key) {
            //Process p = Process.GetProcessesByName("League of Legends").FirstOrDefault();
            //IntPtr h = p.MainWindowHandle;
            //SetForegroundWindow(h);


        }

    }
}
