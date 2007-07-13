using System;
using System.Collections.Generic;
using System.Text;

namespace de.christianleberfinger.dotnet.pocketknife.Logging
{
    /// <summary>
    /// Represents an entry of the logbook. Extend this type to store 
    /// your data.
    /// </summary>
    public class LogEntry
    {
        private DateTime _timestamp = DateTime.Now;

        /// <summary>
        /// Gets or sets the timestamp when the entry was created.
        /// </summary>
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

    }
}
