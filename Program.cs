using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IRCclient
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]		 // 절대로 MTAThread로 바꾸지 말것!!!
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Application.Run(new MainForm());
			}
			catch { }
		}
	}
}
