using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace InputManager
{
	// Token: 0x02000008 RID: 8
	public class VirtualKeyboard
	{
		// Token: 0x0600001F RID: 31
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern bool keybd_event(int bVk, int bScan, int dwFlags, int dwExtraInfo);

		// Token: 0x06000020 RID: 32 RVA: 0x000025B8 File Offset: 0x000009B8
		public static void ShortcutKeys(Keys[] kCode, int Delay = 0)
		{
			VirtualKeyboard.KeyPressStruct keyPressStruct = new VirtualKeyboard.KeyPressStruct(kCode, Delay);
			Thread thread = new Thread(delegate(object a0)
			{
				VirtualKeyboard.KeyPressThread((a0 != null) ? ((VirtualKeyboard.KeyPressStruct)a0) : keyPressStruct);
			});
			thread.Start(keyPressStruct);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025EC File Offset: 0x000009EC
		public static void KeyDown(Keys kCode)
		{
			VirtualKeyboard.keybd_event((int)kCode, 0, 0, 0);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025F8 File Offset: 0x000009F8
		public static void KeyUp(Keys kCode)
		{
			VirtualKeyboard.keybd_event((int)kCode, 0, 2, 0);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002604 File Offset: 0x00000A04
		public static void KeyPress(Keys kCode, int Delay = 0)
		{
			Keys[] keysToPress = new Keys[]
			{
				kCode
			};
			VirtualKeyboard.KeyPressStruct keyPressStruct = new VirtualKeyboard.KeyPressStruct(keysToPress, Delay);
			Thread thread = new Thread(delegate(object a0)
			{
				VirtualKeyboard.KeyPressThread((a0 != null) ? ((VirtualKeyboard.KeyPressStruct)a0) : keyPressStruct);
			});
			thread.Start(keyPressStruct);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002648 File Offset: 0x00000A48
		private static void KeyPressThread(VirtualKeyboard.KeyPressStruct KeysP)
		{
			foreach (Keys kCode in KeysP.Keys)
			{
				VirtualKeyboard.KeyDown(kCode);
			}
			if (KeysP.Delay > 0)
			{
				Thread.Sleep(KeysP.Delay);
			}
			foreach (Keys kCode2 in KeysP.Keys)
			{
				VirtualKeyboard.KeyUp(kCode2);
			}
		}

		// Token: 0x0400001B RID: 27
		private const int KEYEVENTF_EXTENDEDKEY = 1;

		// Token: 0x0400001C RID: 28
		private const int KEYEVENTF_KEYUP = 2;

		// Token: 0x02000014 RID: 20
		private struct KeyPressStruct
		{
			// Token: 0x0600004E RID: 78 RVA: 0x000026B4 File Offset: 0x00000AB4
			public KeyPressStruct(Keys[] KeysToPress, int DelayTime = 0)
			{
				this = default(VirtualKeyboard.KeyPressStruct);
				this.Keys = KeysToPress;
				this.Delay = DelayTime;
			}

			// Token: 0x0400006D RID: 109
			public Keys[] Keys;

			// Token: 0x0400006E RID: 110
			public int Delay;
		}
	}
}
