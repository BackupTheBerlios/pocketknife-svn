/*
 * 
 * Copyright (c) 2007 Christian Leberfinger
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN 
 * AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace de.christianleberfinger.dotnet.pocketknife.Logging
{
    /// <summary>
    /// Provides a logbook.
    /// </summary>
    public class Logbook : List<LogEntry>
    {
        //todo: serialize it
        //http://www.codeproject.com/csharp/XmlSerializerForUnknown.asp?print=true

        /// <summary>
        /// Adds a new entry to the logbook.
        /// </summary>
        /// <param name="entry">The entry to add.</param>
        public void addEntry(LogEntry entry)
        {
            Add(entry);
        }

        public void saveToXML(string filename)
        {
            StreamWriter sw = null;
            try
            {
                // serialize the settings object
                XmlSerializer serializer = new XmlSerializer(typeof(Logbook));
                sw = new StreamWriter(filename);
                serializer.Serialize(sw, this);
                sw.Close();
            }
            catch (Exception e)
            {
                // close the stream writer
                if (sw != null)
                {
                    try
                    {
                        sw.Close();
                    }
                    catch { }
                }
                throw e;
            }
        }
    }
}
