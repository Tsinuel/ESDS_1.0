using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ESADS.GUI;

namespace ESADS.EGraphics.Beam
{
    /// <summary>
    /// A small square box used to move and resize graphic objects using the mouse.
    /// </summary>
    public class eGripBox : eIDrawing
    {
        /// <summary>
        /// The inside color of the grip box.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Value of 'Layer"
        /// </summary>
        private eLayer layer;
        /// <summary>
        /// Value of 'On'
        /// </summary>
        private bool on;
        /// <summary>
        /// The rectangle that represents the grip box.
        /// </summary>
        private RectangleF rectangle;
        private bool highlight;
        /// <param name="location">The location of the center of the grip box.</param>
        /// <param name="layer">The layer on which the grip box is drawn.</param>
        /// <param name="min_x">The minimum x coordinate.</param>
        /// <param name="max_x">The maximum x coordinate.</param>
        /// <param name="dwgForm">The form on which the grip box is shown.</param>
        public eGripBox(PointF location, eLayer layer, float min_x, float max_x, Form dwgForm)
        {
            this.layer = layer;
            this.color = layer.Color;
            this.on = false;
            this.highlight = false;
            this.visible = false;
            this.min_x = min_x;
            this.max_x = max_x;
            this.rectangle = new RectangleF(location.X - 5, location.Y - 5, 10, 10);

            dwgForm.KeyPreview = true;
            dwgForm.MouseClick += new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.KeyDown += new KeyEventHandler(dwgForm_KeyDown);
        }

        private void dwgForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (this.on)
                {
                    this.on = false;
                    this.OnTurnedOff(new eGripBoxEventArgs(true, this.Location));
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (this.on)
                {
                    this.on = false;
                    this.OnTurnedOff(new eGripBoxEventArgs(false, this.Location));
                }
            }
        }

        private void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.visible)
            {
                if (this.on)
                {
                    PointF p = new PointF(e.Location.X, this.Location.Y);
                    if (p.X < min_x)
                        p.X = min_x;
                    else if (p.X > max_x)
                        p.X = max_x;
                    this.Location = p;
                    this.OnMove(new eGripBoxEventArgs(p));
                    (sender as eModelForm).Invalidate();
                }
                else
                {
                    highlight = ((new Region(this.rectangle)).IsVisible(e.Location));
                    (sender as Form).Invalidate();
                }
            }
        }

        private void dwgForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.on)
                (sender as eModelForm).ObjFoundBelowClickPt = true;

            if (!this.on && visible && (new Region(this.rectangle)).IsVisible(e.Location))
            {
                this.On = true;
                (sender as eModelForm).ObjFoundBelowClickPt = true;
            }
            else
            {
                if (this.on)
                    this.On = false;
            }
        }
    
        public PointF Location
        {
            get
            {
                return new PointF(rectangle.X + 5, rectangle.Y + 5);
            }
            set
            {
                rectangle.X = value.X - 5;
                rectangle.Y = value.Y - 5;
            }
        }

        public eLayer Layer
        {
            get { return layer; }
        }

        public eColor Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// Gets or sets if the grip box is turned on.
        /// </summary>
        public bool On
        {
            get
            {
                return on;
            }
            set
            {
                on = value;
                if (value)
                    OnTrunedOn(new eGripBoxEventArgs(false, this.Location));
                else
                    OnTurnedOff(new eGripBoxEventArgs(false, this.Location));
            }
        }

        public void Zoom(System.Drawing.PointF ZoomCenter, float ZoomFactor)
        {
            if (on)
                this.On = false;
            min_x = ZoomFactor * (min_x - ZoomCenter.X) + ZoomCenter.X;
            max_x = ZoomFactor * (max_x - ZoomCenter.X) + ZoomCenter.X;
        }

        public void Pan(float Xoffset, float Yoffset)
        {
            if (on)
                this.On = false;
            min_x += Xoffset;
            max_x += Xoffset;
        }

        public void Draw(System.Drawing.Graphics g)
        {
            if (!visible)
                return;
            SolidBrush b = new SolidBrush(System.Drawing.Color.Blue);
            if (this.on)
                b.Color = System.Drawing.Color.Red;
            else if (this.highlight)
                b.Color = System.Drawing.Color.Green;
            g.FillRectangle(b, rectangle);
        }

        /// <summary>
        /// Gets or sets if the grip box is visible to the user.
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                if (!value)
                    On = false;
            }
        }

        /// <summary>
        /// Value of 'Visible'.
        /// </summary>
        private bool visible;
        /// <summary>
        /// Value of 'Max_X'
        /// </summary>
        private float max_x;

        /// <summary>
        /// Gets or sets the maximum x coordinate value that the grip can move to.
        /// </summary>
        public float Min_X
        {
            get
            {
                return min_x;
            }
            set
            {
                min_x = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum x coordinate value that the grip can move to.
        /// </summary>
        public float Max_X
        {
            get
            {
                return max_x;
            }
            set
            {
                max_x = value;
            }
        }

        /// <summary>
        /// Value of 'Min_X'
        /// </summary>
        private float min_x;

        /// <summary>
        /// Fired when the grip box is turned on.
        /// </summary>
        public event eGripBoxEventHandler TurnedOn;

        /// <summary>
        /// Fired when the grip box is turned off.
        /// </summary>
        public event eGripBoxEventHandler TurnedOff;

        /// <summary>
        /// Fires the 'TurnedOff' event
        /// </summary>
        /// <param name="e">Teh event argument when the event occured.</param>
        protected void OnTurnedOff(eGripBoxEventArgs e)
        {
            if (this.TurnedOff != null)
            {
                TurnedOff(this, e);
            }
        }

        /// <summary>
        /// Fires the 'TurnedOn' event
        /// </summary>
        /// <param name="e">The event argument when it occured.</param>
        protected void OnTrunedOn(eGripBoxEventArgs e)
        {
            if (this.TurnedOn != null)
            {
                TurnedOn(this, e);
            }
        }

        /// <summary>
        /// Occurs when the location of the grip box is changed.
        /// </summary>
        public event eGripBoxEventHandler Move;

        /// <summary>
        /// Fires the 'Move' event.
        /// </summary>
        protected void OnMove(eGripBoxEventArgs e)
        {
            if (this.Move != null)
            {
                Move(this, e);
            }
        }

        /// <summary>
        /// Releases all the handlers of events from external objects
        /// </summary>
        /// <param name="dwgForm">The drawing form.</param>
        internal void ReleaseHandlers(Form dwgForm)
        {
            dwgForm.MouseClick -= new MouseEventHandler(dwgForm_MouseClick);
            dwgForm.MouseMove -= new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.KeyDown -= new KeyEventHandler(dwgForm_KeyDown);
        }
    }
}
