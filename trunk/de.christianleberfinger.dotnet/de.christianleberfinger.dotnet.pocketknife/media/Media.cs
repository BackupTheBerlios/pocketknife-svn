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
    internal interface IMediaCallback
    {
        void callback();
    }

    /// <summary>
    /// A media file. You can play, stop and pause it and do some other
    /// stuff you would expect from a media file.
    /// </summary>
    public class Media : IDisposable, IMediaCallback
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

            _callbackControl = new CallbackControl(this);

            _filename = filename;
            _alias = System.Guid.NewGuid().ToString();

            open();
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
        /// <exception cref="MediaPlayerException"></exception>
        public void play()
        {
            try
            {
                MCIHelper.sendMCICommand(string.Format("play {0} from 0", Alias), _callbackControl);

                // set time format to milliseconds
                MCIHelper.sendMCICommand(string.Format("set {0} time format milliseconds", Alias));
            }
            catch (Exception ex)
            {
                throw new MediaPlayerException("Error while starting to play", ex);
            }
        }

        void open()
        {
            try
            {
                string dosFileName = PathTool.ConvertToDosPath(Filename);
                MCIHelper.sendMCICommand(string.Format("open {0} type MPEGVideo alias {1}", dosFileName, Alias));

                // set time format to milliseconds
                MCIHelper.sendMCICommand(string.Format("set {0} time format milliseconds", Alias));
            }
            catch (Exception ex)
            {
                throw new MediaPlayerException("Error while starting to play", ex);
            }
        }

        private class CallbackControl : System.Windows.Forms.Control
        {
            const int MM_MCINOTIFY = 0x3B9;

            IMediaCallback _callback;

            public CallbackControl(IMediaCallback callback)
            {
                Debug.Assert(callback != null);
                _callback = callback;
            }

            protected override void WndProc(ref System.Windows.Forms.Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == MM_MCINOTIFY)
                {
                    _callback.callback();
                    //string s = (string)m.GetLParam(typeof(string));
                    //string test = System.Runtime.InteropServices.Marshal.PtrToStringAuto(m.LParam);
                }
            }
        }

        CallbackControl _callbackControl;

        /// <summary>
        /// Plays the current media within a control.
        /// </summary>
        /// <param name="parent"></param>
        public void play(System.Windows.Forms.Control parent)
        {
            try
            {
                int handle = parent.Handle.ToInt32();
                string dosFileName = PathTool.ConvertToDosPath(Filename);
                MCIHelper.sendMCICommand(string.Format("open {0} type MPEGVideo alias {1} parent {2} style child", dosFileName, Alias, handle));
                MCIHelper.sendMCICommand(string.Format("play {0} from 0 notify", Alias), _callbackControl);
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
                        catch (Exception ex)
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
        /// When an error occured, "PlayStates.Unknown" will be returned (no exception is thrown here).
        /// </summary>
        public PlayStates PlayState
        {
            get
            {
                try
                {
                    string mciState = MCIHelper.sendMCICommand(string.Format("STATUS {0} MODE", Alias));
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

        /// <summary>
        /// Returns the current position in a relative value between 0 and 1.
        /// </summary>
        public double RelativePosition
        {
            get
            {
                double duration = Duration.TotalMilliseconds;
                double pos = Position.TotalMilliseconds;
                return pos / duration;
            }
        }

        /// <summary>
        /// Gets the current position within the media object.
        /// </summary>
        /// <exception cref="MediaPlayerException"></exception>
        public TimeSpan Position
        {
            get
            {
                try
                {
                    string millisString = MCIHelper.sendMCICommand(string.Format("status {0} position", Alias));
                    return TimeSpan.FromMilliseconds(double.Parse(millisString));
                }
                catch (Exception ex)
                {
                    throw new MediaPlayerException("Cannot acquire the current position in media file.", ex);
                }
            }
            set
            {
                try
                {
                    bool playing = PlayState == PlayStates.Playing;
                    int millis = (int)value.TotalMilliseconds;
                    MCIHelper.sendMCICommand(string.Format("seek {0} to {1}", Alias, millis));

                    // continue playing after position was set.
                    if (playing)
                    {
                        MCIHelper.sendMCICommand(string.Format("play {0} notify", Alias), _callbackControl);
                    }
                }
                catch (Exception ex)
                {
                    throw new MediaPlayerException("Cannot set position to " + value.ToString(), ex);
                }
            }
        }

        TimeSpan _duration = TimeSpan.Zero;

        /// <summary>
        /// Returns the duration of the current media object.
        /// If the length of the current media object can't be acquired, Timespan.Zero is being returned (no exception is thrown).
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                if (_duration != TimeSpan.Zero)
                    return _duration;

                try
                {
                    string millisString = MCIHelper.sendMCICommand(string.Format("status {0} length", Alias));
                    _duration = TimeSpan.FromMilliseconds(double.Parse(millisString));
                    return _duration;
                }
                catch
                {
                    return TimeSpan.Zero;
                }
            }
        }

        ///// <summary>
        ///// Gets the file format of the current media object.
        ///// If file format cannot be acquired, "unknown" is being returned.
        ///// </summary>
        //public string FileFormat
        //{
        //    get
        //    {
        //        try
        //        {
        //            return MCIHelper.sendMCICommand(string.Format("status {0} file format", Alias));
        //        }
        //        catch
        //        {
        //            return "unknown";
        //        }
        //    }
        //}

        #region IDisposable Member

        bool _disposed = false;
        /// <summary>
        /// Disposes the media file.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                if (_callbackControl != null)
                    _callbackControl.Dispose();

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

        /// <summary>
        /// EventArgs for media events
        /// </summary>
        public class MediaEventArgs : EventArgs
        {
            PlayStates _newState;

            /// <summary>
            /// ctor.
            /// </summary>
            /// <param name="newState"></param>
            internal MediaEventArgs(PlayStates newState)
            {
                _newState = newState;
            }

            PlayStates NewState
            {
                get { return _newState; }
            }
        }

        /// <summary>
        /// Is raised when the media state has changed.
        /// </summary>
        public event GenericEventHandler<Media, MediaEventArgs> OnMediaStateChanged;

        #region IMediaCallback Member

        void IMediaCallback.callback()
        {
            MediaEventArgs e = new MediaEventArgs(PlayState);
            EventHelper.invoke<Media, MediaEventArgs>(OnMediaStateChanged, this, e);
        }

        #endregion
    }
}
