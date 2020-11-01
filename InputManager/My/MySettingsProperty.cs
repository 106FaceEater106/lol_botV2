using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace InputManager.My
{
	// Token: 0x0200002B RID: 43
	[HideModuleName]
	[StandardModule]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal sealed class MySettingsProperty
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002E14 File Offset: 0x00001214
		[HelpKeyword("My.Settings")]
		internal static MySettings Settings
		{
			get
			{
				return MySettings.Default;
			}
		}
	}
}
