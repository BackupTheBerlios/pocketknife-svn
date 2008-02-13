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
using de.christianleberfinger.dotnet.pocketknife.IO;
using System.Diagnostics;

namespace de.christianleberfinger.dotnet.pocketknife.media
{
    /// <summary>
    /// Contains information about a played media file.
    /// </summary>
    public class Media : IDisposable
    {
        /// <summary>
        /// The play states a media file can be in.
        /// </summary>
        public enum PlayStates
        {
            /// <summary>The media file is playing.</summary>
            Playing,
            /// <summary>The media file is paused.</summary>
            Paused,
            /// <summary>The media file is stopped.</summary>
            Stopped,
            /// <summary>The media file status is unknown.</summary>
            Unknown
        }

        string _filename;

        /// <summary>
        /// Creates a new media object
        /// </summary>
        /// <param name="filename"></param>
        public Media(string filename)
        {
            Debug.Assert(filename != null);

            _filename = filename;
            _alias = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~Media()
        {
            Dispose();
        }

        /// <summary>
        /// The filename
        /// </summary>
        public string Filename
        {
            get { return _filename; }
        }

        /// <summary>
        /// Plays the media file.
        /// </summary>
        public void play()
        {
            try
            {
                string dosFileName = PathTool.ConvertToDosPath(Filename);
                MCIHelper.sendMCICommand(string.Format("open {0} type MPEGVideo alias {1}", dosFileName, Alias));
                MCIHelper.sendMCICommand(string.Format("play {0} from 0", Alias));
            }
            catch (Exception ex)
            {
                throw new MediaPlayerException("Error while starting to play", ex);
            }
        }

        private string _alias;
        private string Alias
        {
            get { return _alias; }
        }

        /// <summary>
        /// Gets or sets the media file's volume.
        /// </summary>
        public int Volume
        {
            get
            {
                string currentVolume = MCIHelper.sendMCICommand(string.Format("STATUS {0} VOLUME", Alias));
                return int.Parse(currentVolume);
            }
            set
            {
                MCIHelper.sendMCICommand(string.Format("SETAUDIO {0} VOLUME TO {1}", Alias, value));
            }
        }

        /// <summary>
        /// Stops playing this media file.
        /// </summary>
        /// <exception cref="MediaPlayerException"></exception>
        public void stop()
        {
            try
            {
                MCIHelper.sendMCICommand(string.Format("stop {0}", Alias));
            }
            catch (Exception ex)
            {
                throw new MediaPlayerException("Error while stopping", ex);
            }
        }

        /// <summary>
        /// Gets or sets paused state.
        /// </summary>
        /// <exception cref="MediaPlayerException"></exception>
        public bool Paused
        {
            get
            {
                return PlayState == PlayStates.Paused;
            }
            set
            {
                if (value != Paused)
                {
                    if (value)
                    {
                        try
                        {
                            // PAUSE
                            MCIHelper.sendMCICommand(string.Format("PAUSE {0}", Alias));
                        }
                        catch(Exception ex)
                        {
                            throw new MediaPlayerException("Cannot pause", ex);
                        }
                    }
                    else
                    {
                        try
                        {
                            MCIHelper.sendMCICommand(string.Format("RESUME {0}", Alias));
                        }
                        catch (Exception ex)
                        {
                            throw new MediaPlayerException("Cannot resume", ex);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the current PlayState of this media file.
        /// </summary>
        public PlayStates PlayState
        {
            get
            {
                try
                {
                    string mciState = MCIHelper.sendMCICommand(string.Format("STATUS {0} MODE"));
                    switch (mciState.ToUpper())
                    {
                        case "STOPPED":
                            return PlayStates.Stopped;
                        case "PLAYING":
                            return PlayStates.Playing;
                        case "PAUSED":
                            return PlayStates.Paused;
                        default:
                            return PlayStates.Unknown;
                    }
                }
                catch
                {
                    return PlayStates.Unknown;
                }
            }
        }

        #region IDisposable Member

        bool _disposed = false;
        /// <summary>
        /// Disposes the media file.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                try
                {
                    stop();
                }
                catch { }
                finally
                {
                    try
                    {
                        MCIHelper.sendMCICommand(string.Format("close {0}", Alias));
                    }
                    catch { }
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
