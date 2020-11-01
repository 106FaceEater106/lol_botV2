using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace InputManager.My.Resources
{
	// Token: 0x02000029 RID: 41
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[StandardModule]
	[HideModuleName]
	[CompilerGenerated]
	internal sealed class Resources
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002D84 File Offset: 0x00001184
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("InputManager.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002DC4 File Offset: 0x000011C4
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002DD8 File Offset: 0x000011D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x040000AC RID: 172
		private static ResourceManager resourceMan;

		// Token: 0x040000AD RID: 173
		private static CultureInfo resourceCulture;
	}
}
