using System;
using System.Windows.Forms;

namespace LS1_FLASH_TOOL_V2.XX
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new Form1());
		}
	}
}
