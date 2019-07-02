//By loathing from StackOverflow

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgricoveBilling
{
    class NW : NativeWindow
    {

        public NW(IntPtr hwnd)
        {
            AssignHandle(hwnd);
        }
        const int WM_NCHITTEST = 0x84;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
                return;

            base.WndProc(ref m);
        }
    }
}
