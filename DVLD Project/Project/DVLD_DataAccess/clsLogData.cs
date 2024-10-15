using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    static class clsLogData
    {
        static public bool Log(string LogName, string Message, EventLogEntryType entryType)
        {
            string SourceName = "DVLD";


            //Create The Event Source if it Does not Exists
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
                EventLog.WriteEntry(SourceName, Message, entryType);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
