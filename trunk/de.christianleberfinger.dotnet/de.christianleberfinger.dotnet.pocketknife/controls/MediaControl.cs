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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using de.christianleberfinger.dotnet.pocketknife.media;
using System.Diagnostics;
using System.IO;

namespace de.christianleberfinger.dotnet.pocketknife.controls
{
    /// <summary>
    /// A control for playing and controlling media files.
    /// </summary>
    public partial class MediaControl : UserControl
    {
        Media _media = null;
        int _volume = 100;
        bool _muted = false;

        /// <summary>
        /// Gets or sets the current media object. 
        /// </summary>
        public Media Media
        {
            get { return _media; }
            set
            {
                if (Media != null)
                {
                    Media.stop();
                    Media.OnMediaStateChanged -= mediaStateChanged;
                }

                // set new media object
                _media = value;
                _media.Muted = Muted;
                _media.Volume = _volume;
                _media.OnMediaStateChanged += new de.christianleberfinger.dotnet.pocketknife.GenericEventHandler<Media, Media.MediaEventArgs>(mediaStateChanged);

                updateGUI();
            }
        }

        void mediaStateChanged(Media sender, Media.MediaEventArgs e)
        {
            Debug.WriteLine("New media state: " + e.NewState);

            updateGUI();
        }

        /// <summary>
        /// Gets or sets muted state.
        /// </summary>
        public bool Muted
        {
            get { return _muted; }
            set 
            {
                _muted = value;

                Media m = Media;
                if (m != null)
                    m.Muted = value;
            }
        }

        delegate void VoidHandler();
        private void updateGUI()
        {
            if (this.InvokeRequired)
            {
                Invoke(new VoidHandler(updateGUI));
            }
            else
            {
                if (Muted)
                {
                    cbMuted.Image = global::de.christianleberfinger.dotnet.pocketknife.Properties.Resources.audio_volume_muted;
                }
                else
                {
                    cbMuted.Image = global::de.christianleberfinger.dotnet.pocketknife.Properties.Resources.audio_volume_medium;
                }

                tbVolume.Value = _volume;

                if (Media == null)
                {
                    lblTime.Text = "";
                    lblStatus.Text = "No media.";
                    timeProgressBar1.RelativePosition = 0;
                    return;
                }
                else
                {
                    lblTime.Text = getPositionString();

                    if (Media.PlayState == PlayStates.Playing)
                    {
                        rbtPlay.Checked = true;
                    }
                    else if (Media.PlayState == PlayStates.Paused)
                    {
                        rbtPause.Checked = true;
                    }
                    else
                    {
                        rbtStop.Checked = true;
                    }

                    timeProgressBar1.RelativePosition = Media.RelativePosition;
                    lblStatus.Text = Path.GetFileName( Media.Filename );
                }
            }
        }

        string getPositionString()
        {
            if (Media == null)
            {
                return "";
            }

            TimeSpan pos = TimeSpan.FromMilliseconds(0);
            try
            {
                pos = Media.Position;
            }
            catch { }

            TimeSpan duration = Media.Duration;

            return string.Format("{0}:{1:d2} / {2}:{3:d2}", (int)pos.TotalMinutes, pos.Seconds, (int)duration.TotalMinutes, duration.Seconds);
        }

        /// <summary>
        /// Creates a new MediaControl.
        /// </summary>
        public MediaControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets or gets the Volume (0-255)
        /// </summary>
        public int Volume
        {
            get { return _volume; }
            set { setVolume(value); }
        }

        private void setVolume(int value)
        {
            _volume = value;
            Media m = _media;
            if (m != null)
                m.Volume = value;

            updateGUI();
        }

        delegate void StringHandler(string s);
        void setStatusbarText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new StringHandler(setStatusbarText), text);
            }
            else
            {
                lblStatus.Text = text;
            }
        }

        private void checkMedia()
        {
            if (Media == null)
            {
                throw new ArgumentNullException("No media file was specified.");
            }
        }

        private void MediaControl_Load(object sender, EventArgs e)
        {
            updateGUI();
        }

        void setRelativePosition(int minimum, int maximum, int value)
        {
            if (Media == null)
                return;

            double relativePosition = ((double)value - (double)minimum) / (double)maximum;
            try
            {
                checkMedia();
                Media.Position = TimeSpan.FromMilliseconds(Media.Duration.TotalMilliseconds * relativePosition);
            }
            catch (Exception ex)
            {
                setStatusbarText(ex.Message);
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            updateGUI();
        }

        private void timeProgressBar1_OnValueChanged(de.christianleberfinger.dotnet.pocketknife.controls.SimpleProgressBar sender, de.christianleberfinger.dotnet.pocketknife.controls.SimpleProgressBar.ValueUpdatedEventArgs e)
        {
            if (Media == null)
                return;

            try
            {
                checkMedia();
                Media.Position = TimeSpan.FromMilliseconds(Media.Duration.TotalMilliseconds * timeProgressBar1.RelativePosition);
            }
            catch (Exception ex)
            {
                setStatusbarText(ex.Message);
            }
        }

        private void rbtPlay_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtPlay.Checked)
                return;

            try
            {
                checkMedia();

                if (Media.Paused)
                    Media.Paused = false;
                else
                    Media.play();
            }
            catch (Exception ex)
            {
                setStatusbarText(ex.Message);
            }
        }

        private void rbtStop_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtStop.Checked)
                return;

            try
            {
                checkMedia();
                Media.stop();
            }
            catch (Exception ex)
            {
                setStatusbarText(ex.Message);
            }
        }

        private void rbtPause_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtPause.Checked)
                return;

            try
            {
                checkMedia();
                Media.Paused = rbtPause.Checked;
            }
            catch (Exception ex)
            {
                setStatusbarText(ex.Message);
            }
        }

        private void cbMuted_MouseUp(object sender, MouseEventArgs e)
        {
            Muted = !Muted;
            updateGUI();
        }

        private void tbVolume_Scroll(object sender, EventArgs e)
        {
            _volume = tbVolume.Value;

            Media m = Media;
            if (m != null)
                m.Volume = _volume;
        }

        private void cbMuted_Click(object sender, EventArgs e)
        {

        }
    }
}
