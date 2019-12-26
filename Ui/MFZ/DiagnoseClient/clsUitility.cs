using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace DiagnoseClient
{
    public class Interop
    {
        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public extern static int ReleaseCapture();

        [DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public static int MoveForm(IntPtr hwnd)
        {
            ReleaseCapture();
            return SendMessage(hwnd, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }

    // ***********************************************************************
    //  KeyboardLLWindowsHook class
    //  
    // 
    //  Provide a general infrastructure for using Win32 
    //  hooks in .NET applications
    // 
    // ***********************************************************************
    #region WindowsHook

    #region Class HookEventArgs
    public class HookEventArgs : EventArgs
    {
        public KBDLLHOOKSTRUCT kbInfo;
    }
    #endregion

    #region KBDLLHOOKSTRUCT

    public struct KBDLLHOOKSTRUCT
    {
        public int vkCode;
        //int scanCode;
        public int flags;
        //int time;
        //int dwExtraInfo;
    }
    #endregion

    #region Enum HookType
    // Hook Types
    public enum HookType : int
    {
        //WH_JOURNALRECORD = 0,
        //WH_JOURNALPLAYBACK = 1,
        //WH_KEYBOARD = 2,
        //WH_GETMESSAGE = 3,
        //WH_CALLWNDPROC = 4,
        //WH_CBT = 5,
        //WH_SYSMSGFILTER = 6,
        //WH_MOUSE = 7,
        //WH_HARDWARE = 8,
        //WH_DEBUG = 9,
        //WH_SHELL = 10,
        //WH_FOREGROUNDIDLE = 11,
        //WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        //WH_MOUSE_LL = 14
    }
    #endregion

    public class WindowsHook
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        private IntPtr m_hhook = IntPtr.Zero;
        private HookProc m_filterFunc = null;
        private HookType m_hookType;

        private delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        public delegate void HookEventHandler(object sender, HookEventArgs e);
        public event HookEventHandler HookInvoked;

        public WindowsHook(HookType hook)
        {
            m_hookType = hook;
            m_filterFunc = new HookProc(this.CoreHookProc);
        }

        private int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0 || !(wParam.ToInt32() == WM_KEYDOWN || wParam.ToInt32() == WM_SYSKEYDOWN))
                return CallNextHookEx(m_hhook, code, wParam, lParam);

            // Let clients determine what to do
            HookEventArgs e = new HookEventArgs();
            e.kbInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            if (HookInvoked != null)
                HookInvoked(this, e);

            // Yield to the next hook in the chain
            return CallNextHookEx(m_hhook, code, wParam, lParam);
        }

        public void Install()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {

                IntPtr hInstance = GetModuleHandle(curModule.ModuleName);
                m_hhook = SetWindowsHookEx(
                    m_hookType,
                    m_filterFunc,
                    hInstance,
                    0);
            }
        }
        public void Uninstall()
        {
            UnhookWindowsHookEx(m_hhook);
        }

        #region Win32 Imports
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr hInstance, int threadID);

        [DllImport("user32.dll")]
        private static extern int UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll")]
        private static extern int CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string moduleName);
        #endregion
    }
    #endregion
}
