using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace InputManager.My
{
	// Token: 0x0200002A RID: 42
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
	[CompilerGenerated]
	internal sealed partial class MySettings : ApplicationSettingsBase
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E00 File Offset: 0x00001200
		public static MySettings Default
		{
			get
			{
				return MySettings.defaultInstance;
			}
		}

		// Token: 0x040000AE RID: 174
		private static MySettings defaultInstance = (MySettings)SettingsBase.Synchronized(new MySettings());
	}
}
