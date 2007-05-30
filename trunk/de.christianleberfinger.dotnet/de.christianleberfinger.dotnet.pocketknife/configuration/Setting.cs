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
using System.Xml.Serialization;

namespace de.christianleberfinger.dotnet.pocketknife.configuration
{
    /// <summary>
    /// Generic class for storing settings. Especially useful when used in combination with
    /// the Configuration class for storing settings in xml files.
    /// </summary>
    /// <typeparam name="T">The setting's value type.</typeparam>
    [Serializable]
    public class Setting<T>
    {
        /// <summary>
        /// Represents the method that handles changes of the setting's value.
        /// </summary>
        /// <param name="setting"></param>
        public delegate void ValueChangedHandler(Setting<T> setting);

        /// <summary>
        /// Occurs when the setting's value is set.
        /// </summary>
        public event ValueChangedHandler OnValueChange;

        private T value;

        /// <summary>
        /// Creates a new instance of setting.
        /// </summary>
        public Setting(){}

        /// <summary>
        /// Creates a new instance of setting with a given value.
        /// </summary>
        /// <param name="value">The setting's value.</param>
        public Setting(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the setting's value.
        /// </summary>
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;

                // inform subscribed clients about the change of the value.
                ValueChangedHandler handler = OnValueChange;
                if (handler != null)
                    OnValueChange(this);
            }
        }
    }
}
