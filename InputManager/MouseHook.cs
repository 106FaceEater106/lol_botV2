using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace InputManager
{
	// Token: 0x0200000B RID: 11
	public class MouseHook
	{
		// Token: 0x0600003F RID: 63
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SetWindowsHookExA", ExactSpelling = true, SetLastError = true)]
		private static extern int SetWindowsHookEx(int idHook, MouseHook.MouseProcDelegate lpfn, int hmod, int dwThreadId);

		// Token: 0x06000040 RID: 64
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CallNextHookEx(int hHook, int nCode, int wParam, MouseHook.MSLLHOOKSTRUCT lParam);

		// Token: 0x06000041 RID: 65
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int UnhookWindowsHookEx(int hHook);

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002BB8 File Offset: 0x00000FB8
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002BD0 File Offset: 0x00000FD0
		public static event MouseHook.MouseMoveEventHandler MouseMove;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00002BE8 File Offset: 0x00000FE8
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00002C00 File Offset: 0x00001000
		public static event MouseHook.MouseEventEventHandler MouseEvent;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002C18 File Offset: 0x00001018
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00002C30 File Offset: 0x00001030
		public static event MouseHook.WheelEventEventHandler WheelEvent;

		// Token: 0x06000048 RID: 72 RVA: 0x00002C48 File Offset: 0x00001048
		public static void InstallHook()
		{
			InputManager.MouseHook.MouseHookDelegate = new MouseHook.MouseProcDelegate(InputManager.MouseHook.MouseProc);
			InputManager.MouseHook.Mouse_hook = InputManager.MouseHook.SetWindowsHookEx(14, InputManager.MouseHook.MouseHookDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C94 File Offset: 0x00001094
		public static void UninstallHook()
		{
			InputManager.MouseHook.UnhookWindowsHookEx(InputManager.MouseHook.Mouse_hook);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CA4 File Offset: 0x000010A4
		private static int MouseProc(int nCode, int wParam, ref MouseHook.MSLLHOOKSTRUCT lParam)
		{
			if (nCode == 0)
			{
				if (wParam == 512)
				{
					//MouseHook.MouseMoveEventHandler mouseMoveEvent = InputManager.MouseHook.MouseMoveEvent;
					MouseHook.MouseMoveEventHandler mouseMoveEvent = InputManager.MouseHook.MouseMove;
					if (mouseMoveEvent != null)
					{
						mouseMoveEvent(lParam.pt);
					}
				}
				else if (wParam == 513 | wParam == 514 | wParam == 515 | wParam == 516 | wParam == 517 | wParam == 518 | wParam == 519 | wParam == 520 | wParam == 521)
				{
					//MouseHook.MouseEventEventHandler mouseEventEvent = InputManager.MouseHook.MouseEventEvent;
					MouseHook.MouseEventEventHandler mouseEventEvent = InputManager.MouseHook.MouseEvent;
					if (mouseEventEvent != null)
					{
						mouseEventEvent((MouseHook.MouseEvents)wParam);
					}
				}
				else if (wParam == 522)
				{
					//MouseHook.WheelEventEventHandler wheelEventEvent = InputManager.MouseHook.WheelEventEvent;
					MouseHook.WheelEventEventHandler wheelEventEvent = InputManager.MouseHook.WheelEvent;
					if (wheelEventEvent != null)
					{
						wheelEventEvent((MouseHook.MouseWheelEvents)lParam.mouseData);
					}
				}
			}
			return InputManager.MouseHook.CallNextHookEx(InputManager.MouseHook.Mouse_hook, nCode, wParam, lParam);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D70 File Offset: 0x00001170
		~MouseHook()
		{
			InputManager.MouseHook.UnhookWindowsHookEx(InputManager.MouseHook.Mouse_hook);
		}

		// Token: 0x0400003E RID: 62
		private const int HC_ACTION = 0;

		// Token: 0x0400003F RID: 63
		private const int WH_MOUSE_LL = 14;

		// Token: 0x04000040 RID: 64
		private const int WM_MOUSEMOVE = 512;

		// Token: 0x04000041 RID: 65
		private const int WM_LBUTTONDOWN = 513;

		// Token: 0x04000042 RID: 66
		private const int WM_LBUTTONUP = 514;

		// Token: 0x04000043 RID: 67
		private const int WM_LBUTTONDBLCLK = 515;

		// Token: 0x04000044 RID: 68
		private const int WM_RBUTTONDOWN = 516;

		// Token: 0x04000045 RID: 69
		private const int WM_RBUTTONUP = 517;

		// Token: 0x04000046 RID: 70
		private const int WM_RBUTTONDBLCLK = 518;

		// Token: 0x04000047 RID: 71
		private const int WM_MBUTTONDOWN = 519;

		// Token: 0x04000048 RID: 72
		private const int WM_MBUTTONUP = 520;

		// Token: 0x04000049 RID: 73
		private const int WM_MBUTTONDBLCLK = 521;

		// Token: 0x0400004A RID: 74
		private const int WM_MOUSEWHEEL = 522;

		// Token: 0x0400004E RID: 78
		private static int Mouse_hook;

		// Token: 0x0400004F RID: 79
		private static MouseHook.MouseProcDelegate MouseHookDelegate;

		// Token: 0x02000021 RID: 33
		public enum MouseEvents
		{
			// Token: 0x04000098 RID: 152
			LeftDown = 513,
			// Token: 0x04000099 RID: 153
			LeftUp,
			// Token: 0x0400009A RID: 154
			LeftDoubleClick,
			// Token: 0x0400009B RID: 155
			RightDown,
			// Token: 0x0400009C RID: 156
			RightUp,
			// Token: 0x0400009D RID: 157
			RightDoubleClick,
			// Token: 0x0400009E RID: 158
			MiddleDown,
			// Token: 0x0400009F RID: 159
			MiddleUp,
			// Token: 0x040000A0 RID: 160
			MiddleDoubleClick,
			// Token: 0x040000A1 RID: 161
			MouseScroll
		}

		// Token: 0x02000022 RID: 34
		public enum MouseWheelEvents
		{
			// Token: 0x040000A3 RID: 163
			ScrollUp = 7864320,
			// Token: 0x040000A4 RID: 164
			ScrollDown = -7864320
		}

		// Token: 0x02000023 RID: 35
		public struct POINT
		{
			// Token: 0x040000A5 RID: 165
			public int x;

			// Token: 0x040000A6 RID: 166
			public int y;
		}

		// Token: 0x02000024 RID: 36
		public struct MSLLHOOKSTRUCT
		{
			// Token: 0x040000A7 RID: 167
			public MouseHook.POINT pt;

			// Token: 0x040000A8 RID: 168
			public int mouseData;

			// Token: 0x040000A9 RID: 169
			private int flags;

			// Token: 0x040000AA RID: 170
			private int time;

			// Token: 0x040000AB RID: 171
			private int dwExtraInfo;
		}

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x0600005E RID: 94
		private delegate int MouseProcDelegate(int nCode, int wParam, ref MouseHook.MSLLHOOKSTRUCT lParam);

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x06000062 RID: 98
		public delegate void MouseMoveEventHandler(MouseHook.POINT ptLocat);

		// Token: 0x02000027 RID: 39
		// (Invoke) Token: 0x06000066 RID: 102
		public delegate void MouseEventEventHandler(MouseHook.MouseEvents mEvent);

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x0600006A RID: 106
		public delegate void WheelEventEventHandler(MouseHook.MouseWheelEvents wEvent);
	}
}
