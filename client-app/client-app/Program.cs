using System;
using System.Windows.Forms;

namespace client_app
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Languages.LoadWords();
			Application.Run(new Login());
		}
	}
}
