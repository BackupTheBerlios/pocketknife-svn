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
using System.Diagnostics;

namespace de.christianleberfinger.dotnet.pocketknife.controls
{
    /// <summary>
    /// A progress bar that raises a ValueChanged event after a single click
    /// A preview during dragging is also supported.
    /// This behaviour is similar to the progress bars often seen in media players.
    /// </summary>
    public partial class SimpleProgressBar : UserControl
    {
        Color _color = Color.DarkGray;
        Color _draggingColor = Color.Yellow;

        Brush _posBrush = Brushes.DarkGray;
        Brush _draggingBrush = Brushes.Yellow;

        /// <summary>
        /// Gets or sets the bar's temporary fill color that's used while dragging with the mouse.
        /// </summary>
        public Color ColorWhileDragging
        {
            get { return _draggingColor; }
            set 
            { 
                _draggingColor = value;
                _draggingBrush = new SolidBrush(value);
            }
        }

        /// <summary>
        /// Gets or sets the bar's fill color.
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set 
            { 
                _color = value;
                _posBrush = new SolidBrush(Color);
            }
        }

        double _minimum = 0, _maximum = 1000, _value = 0;

        double Value
        {
            get { return _value; }
            set {
                if (_value == value)
                    return;

                _value = value;
                Invalidate();

            }
        }

        ValueUpdatedEventArgs _cachedEventArgs = new ValueUpdatedEventArgs();

        void OnValueUpdated()
        {
            EventHelper.invoke<SimpleProgressBar, ValueUpdatedEventArgs>(OnValueChanged, this, _cachedEventArgs);
        }

        double Maximum
        {
            get
            {
                return _maximum;
            }

        }

        double Minimum
        {
            get { return _minimum; }
        }

        /// <summary>
        /// ctor.
        /// </summary>
        public SimpleProgressBar()
        {
            // Make this control double buffered.
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            InitializeComponent();
        }

        public class ValueUpdatedEventArgs : EventArgs
        {
        }
        public event GenericEventHandler<SimpleProgressBar, ValueUpdatedEventArgs> OnValueChanged;

        /// <summary>
        /// Gets or sets the relative position in a double value between 0 and 1.
        /// </summary>
        public double RelativePosition
        {
            get { 
                return (Value - Minimum) / Maximum; 
            }
            set
            {
                Value = value * (Maximum - Minimum) + Minimum;
            }
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            
            // Draw rectangle for the current position
            Rectangle r = new Rectangle(0, 0, 0, Height);
            r.Width = (int)(this.Width * RelativePosition);
            g.FillRectangle(_posBrush, r);

            // Draw rectangle while dragging
            Rectangle tempRect = new Rectangle(0, 0, Width, Height);
            tempRect.Width = (int)(tempRect.Width * TempRelativePosition);
            g.FillRectangle(_draggingBrush, tempRect);
        }

        double getRelativePosFromMousePos(Point p)
        {
            return (double)p.X / (double)this.Width;
        }

        double _temporaryValue = 0;
        double TemporaryValue
        {
            get
            {
                return _temporaryValue;
            }
            set
            {
                _temporaryValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the relative position in a double value between 0 and 1.
        /// </summary>
        protected double TempRelativePosition
        {
            get
            {
                return (TemporaryValue - Minimum) / Maximum;
            }
            set
            {
                TemporaryValue = value * (Maximum - Minimum) + Minimum;
            }
        }

        private void TimeProgressBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TemporaryValue = getRelativePosFromMousePos(e.Location) * Maximum;
                Invalidate();
            }
        }

        private void TimeProgressBar_Load(object sender, EventArgs e)
        {

        }

        private void TimeProgressBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TemporaryValue = getRelativePosFromMousePos(e.Location) * Maximum;
                Invalidate();
            }
        }

        private void TimeProgressBar_MouseUp(object sender, MouseEventArgs e)
        {
            Value = TemporaryValue;
            TemporaryValue = 0;
            OnValueUpdated();
        }
    }
}
