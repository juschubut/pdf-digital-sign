using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.WinPdfDigitalSign
{
	public class PersonalData
	{
		private static string _fileName = "juschubut-pdf-digital-sign.json";

		public string Cargo  { get; set; }
		public string Leyenda { get; set; }

		public bool FirmaOlografa { get; set; }

		public bool FirmaOlografaPropia { get; set; }

		public string FirmaOlografaPropiaPath { get; set; }

		public string PosicionX { get; set; }
		public string PosicionY { get; set; }
		public string PosicionAlto { get; set; }
		public string PosicionAncho { get; set; }

		internal static PersonalData Load()
		{
			try
			{
				var fileInfo = new FileInfo(_fileName);

				if (fileInfo.Exists)
				{
					var json = File.ReadAllText(_fileName);

					return JsonConvert.DeserializeObject<PersonalData>(json);
				}
			}
			catch
			{ }

			return new PersonalData();
		}

		internal void Save()
		{
			try
			{
				var json = Newtonsoft.Json.JsonConvert.SerializeObject(this);

				File.WriteAllText(_fileName, json);
			}
			catch { }
		}
	}
}
