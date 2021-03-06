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
using System.Runtime.InteropServices;
using System.Diagnostics;
using de.christianleberfinger.dotnet.pocketknife.IO;

namespace de.christianleberfinger.dotnet.pocketknife.media
{
    /// <summary>
    /// A class for easily playing media files. This class is internally using the Windows MCI API.
    /// </summary>
    public class MediaPlayer
    {
        /// <summary>
        /// Creates and plays a given media file.
        /// </summary>
        /// <param name="filename">The file you want to play.</param>
        /// <returns>A media object that represents the file you defined.</returns>
        /// <exception cref="MediaPlayerException"></exception>
        public static Media play(string filename)
        {
            Media media = new Media(filename);
            media.play();
            return media;
        }

        /// <summary>
        /// Creates and plays a given media file within a given control.
        /// </summary>
        /// <param name="filename">The file you want to play.</param>
        /// <param name="parent">The control in which the media file should be rendered. (for videos)</param>
        /// <returns>A media object that represents the file you defined.</returns>
        /// <exception cref="MediaPlayerException"></exception>
        public static Media play(string filename, System.Windows.Forms.Control parent)
        {
            Media media = new Media(filename);
            media.play(parent);
            return media;
        }

    }
}
