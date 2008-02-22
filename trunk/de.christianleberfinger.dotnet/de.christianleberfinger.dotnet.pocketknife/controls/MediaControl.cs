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

namespace de.christianleberfinger.dotnet.pocketknife.controls
{
    public partial class MediaControl : UserControl
    {
        Media _media = null;

        /// <summary>
        /// Gets or sets the current media object. 
        /// </summary>
        public Media Media
        {
            get { return _media; }
        }

        /// <summary>
        /// Sets the current media object.
        /// </summary>
        /// <param name="media"></param>
        public void setMedia(Media media)
        {
            if (Media != null)
            {
                Media.stop();
                Media.OnMediaStateChanged -= mediaStateChanged;
            }

            // set new media object
            _media = media;

            updateGUI();

            Media.OnMediaStateChanged += new de.christianleberfinger.dotnet.pocketknife.GenericEventHandler<Media, Media.MediaEventArgs>(mediaStateChanged);
        }

        void mediaStateChanged(Media sender, Media.MediaEventArgs e)
        {
            Debug.WriteLine("New media state: " + e.NewState);

            updateGUI();
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
                if (Media == null)
                {
                    lblTime.Text = "No media.";
                    lblStatus.Text = "No media.";
                    timeProgressBar1.RelativePosition = 0;
                    return;
                }
                else
                {
                    lblTime.Text = getPositionString();

                    if (Media.PlayState == PlayStates.Stopped || Media.PlayState == PlayStates.Unknown)
                    {
                        rbtStop.Checked = true;
                    }
                    else if (Media.PlayState == PlayStates.Paused)
                    {
                        rbtPause.Checked = true;
                    }

                    timeProgressBar1.RelativePosition = Media.RelativePosition;
                    lblStatus.Text = Media.Filename;
                }
            }
        }

        string getPositionString()
        {
            if (Media == null)
            {
                return "No media.";
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

        public MediaControl()
        {
            InitializeComponent();
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
    }
}
