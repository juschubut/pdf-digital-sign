using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juschubut.WinPdfDigitalSign
{
    public class Log
    {

        private static WinLogger _logger = new WinLogger();

        public class WinLogger : PdfDigitalSign.Logger
        {
            private bool _writingStatus = false;
            private StringBuilder _textLog = new StringBuilder();

            public override void Debug(string texto)
            {
                if (App.Setup != null && App.Setup.IsDebugging)
                {
                    Write(texto, EventLogEntryType.Information);
                }
            }

            public void Error(string text)
            {
                Write(text, EventLogEntryType.Error);
            }

            private void Write(string text, EventLogEntryType type)
            {
                Juschubut.PdfSign.Common.StatusCode code = PdfSign.Common.StatusCode.Debug;

                if (type == EventLogEntryType.Error || type == EventLogEntryType.FailureAudit)
                    code = PdfSign.Common.StatusCode.Error;

                if (!_writingStatus)
                {
                    _writingStatus = true;

                    WebHelper.Status(code, text);

                    _textLog.AppendLine(string.Format("{0} - {1}", code, text));

                    _writingStatus = false;
                }
            }

            public void Reset()
            {
                _textLog.Clear();
            }

            public string GetLogs()
            {
                return _textLog.ToString();
            }
        }


        public static WinLogger Logger
        {
            get { return _logger; }
        }
        public static void Debug(string text)
        {
            _logger.Debug(text);
        }         

        public static void Error(string text)
        {
            _logger.Error(text);
        }

        public static void Reset()
        {
            _logger.Reset();
        }

        public static string GetLogs()
        {
            return _logger.GetLogs();
        }
    }
}
