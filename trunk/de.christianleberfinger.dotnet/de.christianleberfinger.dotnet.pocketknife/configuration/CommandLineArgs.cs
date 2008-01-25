/*
 * 
 * Copyright (c) 2008 Christian Leberfinger
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
using System.Reflection;
using System.Text.RegularExpressions;

namespace de.christianleberfinger.dotnet.pocketknife.configuration
{
    /// <summary>
    /// Helps you importing command line arguments.
    /// </summary>
    public class CommandLineArgs
    {
        /// <summary>
        /// Returns all the fields for the given object. 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string getPossibleOptions(object instance)
        {
            Type t = instance.GetType();
            FieldInfo[] fis = t.GetFields();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < fis.Length; i++)
            {
                sb.Append(t.Name);
                sb.Append('.');
                sb.Append(fis[i].Name);

                if (i < fis.Length - 1)
                    sb.Append('|');
            }

            return sb.ToString();
        }

        /// <summary>
        /// Takes the given object and sets all fields that match
        /// to the given command line arguments, e.g. Config.port=2345
        /// </summary>
        /// <remarks>At the moment only values of the following types can be set 
        /// according to parsing from string.</remarks>
        /// <param name="instance">The instance into which the data should be imported.</param>
        public static void import(object instance)
        {
            string[] arguments = System.Environment.GetCommandLineArgs();

            // regular expression for searching class.key=value combination
            Regex keyVal = new Regex(@"((?<class>\w*)\.)?((?<key>\w+))*=(?<value>.*)((?=\W$)|\z)", RegexOptions.CultureInvariant);

            int i = 0;
            foreach (string a in arguments)
            {
                // first argument is path of application (e.g. C:\\test.exe)
                if (i++ == 0)
                    continue;

                Match m = keyVal.Match(a);
                string clazz = m.Groups["class"].Value;
                string key = m.Groups["key"].Value;
                string val = m.Groups["value"].Value;

                // no need to set anything if no field was specified
                if(key!=null && key.Length>0)
                    setFieldValueByString(instance, clazz, key, val);
            }
        }

        private static void setFieldValueByString(object instance, string clazz, string key, string value)
        {
            Type t = instance.GetType();
            string typeName = t.Name;
            if (clazz == null || clazz.Length<1 || clazz==typeName)
            {
                FieldInfo fi = t.GetField(key);

                if (fi == null)
                    throw new ArgumentException("key", "The field "+clazz+"."+key+" couldn't be found.");

                setFieldValueByString(instance, fi, value);
            }
        }

        private static void setFieldValueByString(object instance, FieldInfo fi, string newValue)
        {
            if (fi == null)
                throw new ArgumentNullException("key", "The field couldn't be found.");

            if (fi.FieldType == typeof(string))
            {
                fi.SetValue(instance, newValue);
            }
            else if (fi.FieldType == typeof(int))
            {
                int i = int.Parse(newValue);
                fi.SetValue(instance, i);
            }
            else if (fi.FieldType == typeof(bool))
            {
                bool b = bool.Parse(newValue);
                fi.SetValue(instance, b);
            }
        }
    }
}
