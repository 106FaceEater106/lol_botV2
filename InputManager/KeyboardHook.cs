using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace InputManager
{
	// Token: 0x0200000A RID: 10
	public class KeyboardHook
	{
		// Token: 0x06000033 RID: 51
		[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SetWindowsHookExA", ExactSpelling = true, SetLastError = true)]
		private static extern int SetWindowsHookEx(int idHook, KeyboardHook.KeyboardProcDelegate lpfn, int hmod, int dwThreadId);

		// Token: 0x06000034 RID: 52
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int CallNextHookEx(int hHook, int nCode, int wParam, KeyboardHook.KBDLLHOOKSTRUCT lParam);

		// Token: 0x06000035 RID: 53
		[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int UnhookWindowsHookEx(int hHook);

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000036 RID: 54 RVA: 0x00002A68 File Offset: 0x00000E68
		// (remove) Token: 0x06000037 RID: 55 RVA: 0x00002A80 File Offset: 0x00000E80
		public static event KeyboardHook.KeyDownEventHandler KeyDown;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000038 RID: 56 RVA: 0x00002A98 File Offset: 0x00000E98
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002AB0 File Offset: 0x00000EB0
		public static event KeyboardHook.KeyUpEventHandler KeyUp;

		// Token: 0x0600003A RID: 58 RVA: 0x00002AC8 File Offset: 0x00000EC8
		private static int KeyboardProc(int nCode, int wParam, ref KeyboardHook.KBDLLHOOKSTRUCT lParam)
		{
			if (nCode == 0)
			{
				switch (wParam)
				{
				case 256:
				case 260:
				{
					KeyboardHook.KeyDownEventHandler keyDownEvent = KeyboardHook.KeyDown;
					if (keyDownEvent != null)
					{
						keyDownEvent(lParam.vkCode);
					}
					break;
				}
				case 257:
				case 261:
				{
					KeyboardHook.KeyUpEventHandler keyUpEvent = KeyboardHook.KeyUp;
					if (keyUpEvent != null)
					{
						keyUpEvent(lParam.vkCode);
					}
					break;
				}
				}
			}
			return KeyboardHook.CallNextHookEx(KeyboardHook.KeyHook, nCode, wParam, lParam);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B40 File Offset: 0x00000F40
		public static void InstallHook()
		{
			KeyboardHook.KeyHookDelegate = new KeyboardHook.KeyboardProcDelegate(KeyboardHook.KeyboardProc);
			KeyboardHook.KeyHook = KeyboardHook.SetWindowsHookEx(13, KeyboardHook.KeyHookDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B8C File Offset: 0x00000F8C
		public static void UninstallHook()
		{
			KeyboardHook.UnhookWindowsHookEx(KeyboardHook.KeyHook);
		}

        // Token: 0x0600003D RID: 61 RVA: 0x00002B9C File Offset: 0x00000F9C
        ~KeyboardHook()
        {
			KeyboardHook.UnhookWindowsHookEx(KeyboardHook.KeyHook);
		}



		// Token: 0x04000033 RID: 51
		private const int HC_ACTION = 0;

		// Token: 0x04000034 RID: 52
		private const int WH_KEYBOARD_LL = 13;

		// Token: 0x04000035 RID: 53
		private const int WM_KEYDOWN = 256;

		// Token: 0x04000036 RID: 54
		private const int WM_KEYUP = 257;

		// Token: 0x04000037 RID: 55
		private const int WM_SYSKEYDOWN = 260;

		// Token: 0x04000038 RID: 56
		private const int WM_SYSKEYUP = 261;

		// Token: 0x04000039 RID: 57
		private const int WM_KEYLAST = 264;

		// Token: 0x0400003C RID: 60
		private static int KeyHook;

		// Token: 0x0400003D RID: 61
		private static KeyboardHook.KeyboardProcDelegate KeyHookDelegate;

		// Token: 0x0200001D RID: 29
		public struct KBDLLHOOKSTRUCT
		{
			// Token: 0x04000092 RID: 146
			public int vkCode;

			// Token: 0x04000093 RID: 147
			public int scancode;

			// Token: 0x04000094 RID: 148
			public int flags;

			// Token: 0x04000095 RID: 149
			public int time;

			// Token: 0x04000096 RID: 150
			public int dwExtraInfo;
		}

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x06000052 RID: 82
		private delegate int KeyboardProcDelegate(int nCode, int wParam, ref KeyboardHook.KBDLLHOOKSTRUCT lParam);

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x06000056 RID: 86
		public delegate void KeyDownEventHandler(int vkCode);

		// Token: 0x02000020 RID: 32
		// (Invoke) Token: 0x0600005A RID: 90
		public delegate void KeyUpEventHandler(int vkCode);
	}
}
