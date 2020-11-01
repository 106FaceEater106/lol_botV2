using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace InputManager
{
	// Token: 0x02000007 RID: 7
	public class Keyboard
	{
		// Token: 0x06000012 RID: 18
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int SendInput(int cInputs, ref Keyboard.INPUT pInputs, int cbSize);

		// Token: 0x06000013 RID: 19
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern uint MapVirtualKey(uint uCode, Keyboard.MapVirtualKeyMapTypes uMapType);

		// Token: 0x06000014 RID: 20
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern uint MapVirtualKeyEx(uint uCode, Keyboard.MapVirtualKeyMapTypes uMapType, IntPtr dwhkl);

		// Token: 0x06000015 RID: 21
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetKeyboardLayout(uint idThread);

		// Token: 0x06000016 RID: 22 RVA: 0x00002290 File Offset: 0x00000690
		private static Keyboard.ScanKey GetScanKey(uint VKey)
		{
			uint sCode = Keyboard.MapVirtualKey(VKey, Keyboard.MapVirtualKeyMapTypes.MAPVK_VK_TO_VSC);
			bool ex = (ulong)VKey == 165UL | (ulong)VKey == 163UL | (ulong)VKey == 37UL | (ulong)VKey == 39UL | (ulong)VKey == 38UL | (ulong)VKey == 40UL | (ulong)VKey == 36UL | (ulong)VKey == 46UL | (ulong)VKey == 33UL | (ulong)VKey == 34UL | (ulong)VKey == 35UL | (ulong)VKey == 45UL | (ulong)VKey == 144UL | (ulong)VKey == 44UL | (ulong)VKey == 111UL;
			Keyboard.ScanKey result = new Keyboard.ScanKey(sCode, ex);
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002330 File Offset: 0x00000730
		public static void ShortcutKeys(Keys[] kCode, int Delay = 0)
		{
			Keyboard.KeyPressStruct keyPressStruct = new Keyboard.KeyPressStruct(kCode, Delay);
			Thread thread = new Thread(delegate(object a0)
			{
				Keyboard.KeyPressThread((a0 != null) ? ((Keyboard.KeyPressStruct)a0) : keyPressStruct);
			});
			thread.Start(keyPressStruct);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002364 File Offset: 0x00000764
		public static void KeyDown(Keys kCode)
		{
			checked
			{
				Keyboard.ScanKey scanKey = Keyboard.GetScanKey((uint)kCode);
				Keyboard.INPUT input = default(Keyboard.INPUT);
				input.dwType = 1;
				input.mkhi.ki = default(Keyboard.KEYBDINPUT);
				input.mkhi.ki.wScan = (short)scanKey.ScanCode;
				input.mkhi.ki.dwExtraInfo = IntPtr.Zero;
				input.mkhi.ki.dwFlags = Conversions.ToInteger(Operators.OrObject(8U, Interaction.IIf(scanKey.Extended, 1U, null)));
				int cbSize = Marshal.SizeOf(typeof(Keyboard.INPUT));
				Keyboard.SendInput(1, ref input, cbSize);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002418 File Offset: 0x00000818
		public static void KeyUp(Keys kCode)
		{
			checked
			{
				Keyboard.ScanKey scanKey = Keyboard.GetScanKey((uint)kCode);
				Keyboard.INPUT input = default(Keyboard.INPUT);
				input.dwType = 1;
				input.mkhi.ki = default(Keyboard.KEYBDINPUT);
				input.mkhi.ki.wScan = (short)scanKey.ScanCode;
				input.mkhi.ki.dwExtraInfo = IntPtr.Zero;
				input.mkhi.ki.dwFlags = Conversions.ToInteger(Operators.OrObject(10U, Interaction.IIf(scanKey.Extended, 1U, null)));
				int cbSize = Marshal.SizeOf(typeof(Keyboard.INPUT));
				Keyboard.SendInput(1, ref input, cbSize);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024D0 File Offset: 0x000008D0
		public static void KeyPress(Keys kCode, int Delay = 0)
		{
			Keys[] keysToPress = new Keys[]
			{
				kCode
			};
			Keyboard.KeyPressStruct keyPressStruct = new Keyboard.KeyPressStruct(keysToPress, Delay);
			Thread thread = new Thread(delegate(object a0)
			{
				Keyboard.KeyPressThread((a0 != null) ? ((Keyboard.KeyPressStruct)a0) : keyPressStruct);
			});
			thread.Start(keyPressStruct);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002514 File Offset: 0x00000914
		private static void KeyPressThread(Keyboard.KeyPressStruct KeysP)
		{
			foreach (Keys kCode in KeysP.Keys)
			{
				Keyboard.KeyDown(kCode);
			}
			if (KeysP.Delay > 0)
			{
				Thread.Sleep(KeysP.Delay);
			}
			foreach (Keys kCode2 in KeysP.Keys)
			{
				Keyboard.KeyUp(kCode2);
			}
		}

		// Token: 0x04000006 RID: 6
		private const uint INPUT_MOUSE = 0U;

		// Token: 0x04000007 RID: 7
		private const int INPUT_KEYBOARD = 1;

		// Token: 0x04000008 RID: 8
		private const int INPUT_HARDWARE = 2;

		// Token: 0x04000009 RID: 9
		private const uint KEYEVENTF_EXTENDEDKEY = 1U;

		// Token: 0x0400000A RID: 10
		private const uint KEYEVENTF_KEYUP = 2U;

		// Token: 0x0400000B RID: 11
		private const uint KEYEVENTF_UNICODE = 4U;

		// Token: 0x0400000C RID: 12
		private const uint KEYEVENTF_SCANCODE = 8U;

		// Token: 0x0400000D RID: 13
		private const uint XBUTTON1 = 1U;

		// Token: 0x0400000E RID: 14
		private const uint XBUTTON2 = 2U;

		// Token: 0x0400000F RID: 15
		private const uint MOUSEEVENTF_MOVE = 1U;

		// Token: 0x04000010 RID: 16
		private const uint MOUSEEVENTF_LEFTDOWN = 2U;

		// Token: 0x04000011 RID: 17
		private const uint MOUSEEVENTF_LEFTUP = 4U;

		// Token: 0x04000012 RID: 18
		private const uint MOUSEEVENTF_RIGHTDOWN = 8U;

		// Token: 0x04000013 RID: 19
		private const uint MOUSEEVENTF_RIGHTUP = 16U;

		// Token: 0x04000014 RID: 20
		private const uint MOUSEEVENTF_MIDDLEDOWN = 32U;

		// Token: 0x04000015 RID: 21
		private const uint MOUSEEVENTF_MIDDLEUP = 64U;

		// Token: 0x04000016 RID: 22
		private const uint MOUSEEVENTF_XDOWN = 128U;

		// Token: 0x04000017 RID: 23
		private const uint MOUSEEVENTF_XUP = 256U;

		// Token: 0x04000018 RID: 24
		private const uint MOUSEEVENTF_WHEEL = 2048U;

		// Token: 0x04000019 RID: 25
		private const uint MOUSEEVENTF_VIRTUALDESK = 16384U;

		// Token: 0x0400001A RID: 26
		private const uint MOUSEEVENTF_ABSOLUTE = 32768U;

		// Token: 0x0200000C RID: 12
		private struct INPUT
		{
			// Token: 0x04000050 RID: 80
			public int dwType;

			// Token: 0x04000051 RID: 81
			public Keyboard.MOUSEKEYBDHARDWAREINPUT mkhi;
		}

		// Token: 0x0200000D RID: 13
		private struct KEYBDINPUT
		{
			// Token: 0x04000052 RID: 82
			public short wVk;

			// Token: 0x04000053 RID: 83
			public short wScan;

			// Token: 0x04000054 RID: 84
			public int dwFlags;

			// Token: 0x04000055 RID: 85
			public int time;

			// Token: 0x04000056 RID: 86
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200000E RID: 14
		private struct HARDWAREINPUT
		{
			// Token: 0x04000057 RID: 87
			public int uMsg;

			// Token: 0x04000058 RID: 88
			public short wParamL;

			// Token: 0x04000059 RID: 89
			public short wParamH;
		}

		// Token: 0x0200000F RID: 15
		[StructLayout(LayoutKind.Explicit)]
		private struct MOUSEKEYBDHARDWAREINPUT
		{
			// Token: 0x0400005A RID: 90
			[FieldOffset(0)]
			public Keyboard.MOUSEINPUT mi;

			// Token: 0x0400005B RID: 91
			[FieldOffset(0)]
			public Keyboard.KEYBDINPUT ki;

			// Token: 0x0400005C RID: 92
			[FieldOffset(0)]
			public Keyboard.HARDWAREINPUT hi;
		}

		// Token: 0x02000010 RID: 16
		private struct MOUSEINPUT
		{
			// Token: 0x0400005D RID: 93
			public int dx;

			// Token: 0x0400005E RID: 94
			public int dy;

			// Token: 0x0400005F RID: 95
			public int mouseData;

			// Token: 0x04000060 RID: 96
			public int dwFlags;

			// Token: 0x04000061 RID: 97
			public int time;

			// Token: 0x04000062 RID: 98
			public IntPtr dwExtraInfo;
		}

		// Token: 0x02000011 RID: 17
		public enum MapVirtualKeyMapTypes : uint
		{
			// Token: 0x04000064 RID: 100
			MAPVK_VK_TO_VSC,
			// Token: 0x04000065 RID: 101
			MAPVK_VSC_TO_VK,
			// Token: 0x04000066 RID: 102
			MAPVK_VK_TO_CHAR,
			// Token: 0x04000067 RID: 103
			MAPVK_VSC_TO_VK_EX,
			// Token: 0x04000068 RID: 104
			MAPVK_VK_TO_VSC_EX
		}

		// Token: 0x02000012 RID: 18
		private struct ScanKey
		{
			// Token: 0x0600004C RID: 76 RVA: 0x00002580 File Offset: 0x00000980
			public ScanKey(uint sCode, bool ex = false)
			{
				this = default(Keyboard.ScanKey);
				this.ScanCode = sCode;
				this.Extended = ex;
			}

			// Token: 0x04000069 RID: 105
			public uint ScanCode;

			// Token: 0x0400006A RID: 106
			public bool Extended;
		}

		// Token: 0x02000013 RID: 19
		private struct KeyPressStruct
		{
			// Token: 0x0600004D RID: 77 RVA: 0x00002598 File Offset: 0x00000998
			public KeyPressStruct(Keys[] KeysToPress, int DelayTime = 0)
			{
				this = default(Keyboard.KeyPressStruct);
				this.Keys = KeysToPress;
				this.Delay = DelayTime;
			}

			// Token: 0x0400006B RID: 107
			public Keys[] Keys;

			// Token: 0x0400006C RID: 108
			public int Delay;
		}
	}
}
