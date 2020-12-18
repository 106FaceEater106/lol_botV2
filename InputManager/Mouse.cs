using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace InputManager
{
	// Token: 0x02000009 RID: 9
	public class Mouse
	{
		// Token: 0x06000028 RID: 40
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetSystemMetrics(int smIndex);

		// Token: 0x06000029 RID: 41
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern int SendInput(int cInputs, ref Mouse.INPUT pInputs, int cbSize);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000026D4 File Offset: 0x00000AD4
		public static bool IsLeftHanded
		{
			get
			{
				bool result;
				try
				{
					result = (Mouse.GetSystemMetrics(23) == 1);
				}
				catch (Exception)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002710 File Offset: 0x00000B10
		public static void SendButton(Mouse.MouseButtons mButton)
		{
			Mouse.INPUT input = default(Mouse.INPUT);
			input.dwType = 0;
			input.mkhi.mi = default(Mouse.MOUSEINPUT);
			input.mkhi.mi.dwExtraInfo = IntPtr.Zero;
			input.mkhi.mi.dwFlags = (int)mButton;
			input.mkhi.mi.dx = 0;
			input.mkhi.mi.dy = 0;
			int cbSize = Marshal.SizeOf(typeof(Mouse.INPUT));
			Mouse.SendInput(1, ref input, cbSize);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027A8 File Offset: 0x00000BA8
		public static void PressButton(Mouse.MouseKeys mKey, int Delay = 0)
		{
			Mouse.ButtonDown(mKey);
			if (Delay > 0)
			{
				Thread.Sleep(Delay);
			}
			Mouse.ButtonUp(mKey);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027C0 File Offset: 0x00000BC0
		public static void ButtonDown(Mouse.MouseKeys mKey)
		{
			switch (mKey)
			{
			case Mouse.MouseKeys.Middle:
				Mouse.SendButton(Mouse.MouseButtons.MiddleDown);
				break;
			case Mouse.MouseKeys.Right:
				Mouse.SendButton(Mouse.MouseButtons.RightDown);
				break;
			case Mouse.MouseKeys.Left:
				Mouse.SendButton(Mouse.MouseButtons.LeftDown);
				break;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027FC File Offset: 0x00000BFC
		public static void ButtonUp(Mouse.MouseKeys mKey)
		{
			switch (mKey)
			{
			case Mouse.MouseKeys.Middle:
				Mouse.SendButton(Mouse.MouseButtons.MiddleUp);
				break;
			case Mouse.MouseKeys.Right:
				Mouse.SendButton(Mouse.MouseButtons.RightUp);
				break;
			case Mouse.MouseKeys.Left:
				Mouse.SendButton(Mouse.MouseButtons.LeftUp);
				break;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002838 File Offset: 0x00000C38
		public static void Move(int X, int Y)
		{
			Mouse.INPUT input = default(Mouse.INPUT);
			input.dwType = 0;
			input.mkhi.mi = default(Mouse.MOUSEINPUT);
			input.mkhi.mi.dwExtraInfo = IntPtr.Zero;
			input.mkhi.mi.dwFlags = 32769;
			checked
			{
				input.mkhi.mi.dx = (int)Math.Round(unchecked((double)X * (65535.0 / (double)Screen.PrimaryScreen.Bounds.Width)));
				input.mkhi.mi.dy = (int)Math.Round(unchecked((double)Y * (65535.0 / (double)Screen.PrimaryScreen.Bounds.Height)));
				int cbSize = Marshal.SizeOf(typeof(Mouse.INPUT));
				Mouse.SendInput(1, ref input, cbSize);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000291C File Offset: 0x00000D1C
		public static void MoveRelative(int X, int Y)
		{
			Mouse.INPUT input = default(Mouse.INPUT);
			input.dwType = 0;
			input.mkhi.mi = default(Mouse.MOUSEINPUT);
			input.mkhi.mi.dwExtraInfo = IntPtr.Zero;
			input.mkhi.mi.dwFlags = 1;
			input.mkhi.mi.dx = X;
			input.mkhi.mi.dy = Y;
			int cbSize = Marshal.SizeOf(typeof(Mouse.INPUT));
			Mouse.SendInput(1, ref input, cbSize);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029B4 File Offset: 0x00000DB4
		public static void Scroll(Mouse.ScrollDirection Direction)
		{
			Mouse.INPUT input = default(Mouse.INPUT);
			input.dwType = 0;
			input.mkhi.mi = default(Mouse.MOUSEINPUT);
			input.mkhi.mi.dwExtraInfo = IntPtr.Zero;
			input.mkhi.mi.dwFlags = 2048;
			input.mkhi.mi.mouseData = (int)Direction;
			input.mkhi.mi.dx = 0;
			input.mkhi.mi.dy = 0;
			int cbSize = Marshal.SizeOf(typeof(Mouse.INPUT));
			Mouse.SendInput(1, ref input, cbSize);
		}

		// Token: 0x0400001D RID: 29
		private const int SM_SWAPBUTTON = 23;

		// Token: 0x0400001E RID: 30
		private const uint INPUT_MOUSE = 0U;

		// Token: 0x0400001F RID: 31
		private const int INPUT_KEYBOARD = 1;

		// Token: 0x04000020 RID: 32
		private const int INPUT_HARDWARE = 2;

		// Token: 0x04000021 RID: 33
		private const uint KEYEVENTF_EXTENDEDKEY = 1U;

		// Token: 0x04000022 RID: 34
		private const uint KEYEVENTF_KEYUP = 2U;

		// Token: 0x04000023 RID: 35
		private const uint KEYEVENTF_UNICODE = 4U;

		// Token: 0x04000024 RID: 36
		private const uint KEYEVENTF_SCANCODE = 8U;

		// Token: 0x04000025 RID: 37
		private const uint XBUTTON1 = 1U;

		// Token: 0x04000026 RID: 38
		private const uint XBUTTON2 = 2U;

		// Token: 0x04000027 RID: 39
		private const uint MOUSEEVENTF_MOVE = 1U;

		// Token: 0x04000028 RID: 40
		private const uint MOUSEEVENTF_LEFTDOWN = 2U;

		// Token: 0x04000029 RID: 41
		private const uint MOUSEEVENTF_LEFTUP = 4U;

		// Token: 0x0400002A RID: 42
		private const uint MOUSEEVENTF_RIGHTDOWN = 8U;

		// Token: 0x0400002B RID: 43
		private const uint MOUSEEVENTF_RIGHTUP = 16U;

		// Token: 0x0400002C RID: 44
		private const uint MOUSEEVENTF_MIDDLEDOWN = 32U;

		// Token: 0x0400002D RID: 45
		private const uint MOUSEEVENTF_MIDDLEUP = 64U;

		// Token: 0x0400002E RID: 46
		private const uint MOUSEEVENTF_XDOWN = 128U;

		// Token: 0x0400002F RID: 47
		private const uint MOUSEEVENTF_XUP = 256U;

		// Token: 0x04000030 RID: 48
		private const uint MOUSEEVENTF_WHEEL = 2048U;

		// Token: 0x04000031 RID: 49
		private const uint MOUSEEVENTF_VIRTUALDESK = 16384U;

		// Token: 0x04000032 RID: 50
		private const uint MOUSEEVENTF_ABSOLUTE = 32768U;

		// Token: 0x02000015 RID: 21
		private struct INPUT
		{
			// Token: 0x0400006F RID: 111
			public int dwType;

			// Token: 0x04000070 RID: 112
			public Mouse.MOUSEKEYBDHARDWAREINPUT mkhi;
		}

		// Token: 0x02000016 RID: 22
		private struct KEYBDINPUT
		{
			// Token: 0x04000071 RID: 113
			public short wVk;

			// Token: 0x04000072 RID: 114
			public short wScan;

			// Token: 0x04000073 RID: 115
			public int dwFlags;

			// Token: 0x04000074 RID: 116
			public int time;

			// Token: 0x04000075 RID: 117
			public IntPtr dwExtraInfo;
		}

		// Token: 0x02000017 RID: 23
		private struct HARDWAREINPUT
		{
			// Token: 0x04000076 RID: 118
			public int uMsg;

			// Token: 0x04000077 RID: 119
			public short wParamL;

			// Token: 0x04000078 RID: 120
			public short wParamH;
		}

		// Token: 0x02000018 RID: 24
		[StructLayout(LayoutKind.Explicit)]
		private struct MOUSEKEYBDHARDWAREINPUT
		{
			// Token: 0x04000079 RID: 121
			[FieldOffset(0)]
			public Mouse.MOUSEINPUT mi;

			// Token: 0x0400007A RID: 122
			[FieldOffset(0)]
			public Mouse.KEYBDINPUT ki;

			// Token: 0x0400007B RID: 123
			[FieldOffset(0)]
			public Mouse.HARDWAREINPUT hi;
		}

		// Token: 0x02000019 RID: 25
		private struct MOUSEINPUT
		{
			// Token: 0x0400007C RID: 124
			public int dx;

			// Token: 0x0400007D RID: 125
			public int dy;

			// Token: 0x0400007E RID: 126
			public int mouseData;

			// Token: 0x0400007F RID: 127
			public int dwFlags;

			// Token: 0x04000080 RID: 128
			public int time;

			// Token: 0x04000081 RID: 129
			public IntPtr dwExtraInfo;
		}

		// Token: 0x0200001A RID: 26
		public enum MouseButtons
		{
			// Token: 0x04000083 RID: 131
			LeftDown = 2,
			// Token: 0x04000084 RID: 132
			LeftUp = 4,
			// Token: 0x04000085 RID: 133
			RightDown = 8,
			// Token: 0x04000086 RID: 134
			RightUp = 16,
			// Token: 0x04000087 RID: 135
			MiddleDown = 32,
			// Token: 0x04000088 RID: 136
			MiddleUp = 64,
			// Token: 0x04000089 RID: 137
			Absolute = 32768,
			// Token: 0x0400008A RID: 138
			Wheel = 2048
		}

		// Token: 0x0200001B RID: 27
		public enum MouseKeys
		{
			// Token: 0x0400008C RID: 140
			Left = -1,
			// Token: 0x0400008D RID: 141
			Right = -2,
			// Token: 0x0400008E RID: 142
			Middle = -3
		}

		// Token: 0x0200001C RID: 28
		public enum ScrollDirection
		{
			// Token: 0x04000090 RID: 144
			Up = 120,
			// Token: 0x04000091 RID: 145
			Down = -120
		}
	}
}
