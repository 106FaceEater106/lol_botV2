using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace inputManagerV2 {

    public static class Window {

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
            
    }
}
