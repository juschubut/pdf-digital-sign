using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.PdfDigitalSign.Jobs
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				using (var client = new WebClient())
				{
					var rs = client.DownloadString(Properties.Settings.Default.Url);

					Console.WriteLine(rs);
				}
			}
			catch 			
			{ 
			}
		}
	}
}
