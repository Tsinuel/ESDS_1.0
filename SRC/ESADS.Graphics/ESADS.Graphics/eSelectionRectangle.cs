using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ESADS.GUI;

namespace ESADS.EGraphics
{
    /// <summary>
    /// A dynamic rectangle used to select object that it touched or included depending on the type of selection.
    /// </summary>
    public class eSelectionRectangle
    {
        private Rectangle rectangle;
        private bool on;
        private Point oppCorner;
        private bool isPositive;

        public event eRectangularSelectorEventHandler EndSelection;
        private bool dragging;

        public eSelectionRectangle(System.Windows.Forms.Form dwgForm)
        {
            dwgForm.KeyPreview = true;
            dwgForm.KeyDown += new System.Windows.Forms.KeyEventHandler(dwgForm_KeyDown);
            dwgForm.MouseMove += new System.Windows.Forms.MouseEventHandler(dwgForm_MouseMove);
            dwgForm.MouseDown += new MouseEventHandler(dwgForm_MouseDown);
            dwgForm.MouseUp += new MouseEventHandler(dwgForm_MouseUp);
        }

        private void dwgForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (on && oppCorner.X - 10.0f <= e.X && e.X <= oppCorner.X + 10.0f && oppCorner.Y - 10.0f <= e.Y && e.Y <= oppCorner.Y + 10.0f)
                dragging = false;
            else
                dragging = true;

            if (!dragging)
                return;
            dragging = false;
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            this.on = false;

            this.OnEndSelection();

            (sender as Form).Invalidate();
            (sender as eModelForm).ObjFoundBelowClickPt = false;
        }

        private void dwgForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            if ((sender as eModelForm).Locked)
                return;

            this.on = !this.on;

            if (this.on)
            {
                this.oppCorner = e.Location;
                rectangle.Width = 0;
                rectangle.Height = 0;
            }
            else
            {
                this.OnEndSelection();
            }

            (sender as Form).Invalidate();
            (sender as eModelForm).ObjFoundBelowClickPt = false;
        }

        private void dwgForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                if (on)
                {
                    on = false;
                    (sender as Form).Invalidate();
                }
            }
        }

        public bool On
        {
            get
            {
                return this.on;
            }
        }

        private void dwgForm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.on)
            {
                //if (e.Button == MouseButtons.Left)
                //    this.dragging = true;

                if ((sender as eModelForm).ObjFoundBelowClickPt)
                {
                    this.on = false;
                    return;
                }
                if (e.Location.X < oppCorner.X)
                {
                    this.isPositive = false;

                    rectangle.X = e.Location.X;
                    rectangle.Y = e.Location.Y < oppCorner.Y ? e.Location.Y : oppCorner.Y;

                    rectangle.Width = oppCorner.X - e.Location.X;
                    rectangle.Height = Math.Abs(e.Location.Y - oppCorner.Y);
                }
                else
                {
                    this.isPositive = true;

                    rectangle.X = oppCorner.X;
                    rectangle.Y = oppCorner.Y < e.Location.Y ? oppCorner.Y : e.Location.Y;

                    rectangle.Width = e.Location.X - oppCorner.X;
                    rectangle.Height = Math.Abs(e.Location.Y - oppCorner.Y);
                }
                (sender as Form).Invalidate();
            }
        }

        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            if (!on)
                return;
            oppCorner.X = (int)((oppCorner.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X);
            oppCorner.Y = (int)((oppCorner.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y);
        }

        public void Pan(float Xoffset, float Yoffset)
        {
            if (!on)
                return;
            oppCorner.X = (int)(oppCorner.X + Xoffset);
            oppCorner.Y = (int)(oppCorner.Y + Yoffset);
        }

        public void Draw(Graphics g)
        {
            if (!on)
                return;

            if (isPositive)
            {
                Pen p = new Pen(Color.White);

                SolidBrush b = new SolidBrush(Color.FromArgb(40, 50, 50, 250));

                g.FillRectangle(b, rectangle);
                g.DrawRectangle(p, rectangle);
            }
            else
            {
                Pen p = new Pen(Color.White);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                SolidBrush b = new SolidBrush(Color.FromArgb(40, 50, 250, 50));

                g.FillRectangle(b, rectangle);
                g.DrawRectangle(p, rectangle);
            }
        }

        private void OnEndSelection()
        {
            if (this.EndSelection != null)
            {
                eRectangularSelectionEventArgs e = new eRectangularSelectionEventArgs(this.GetRegion(), isPositive);

                this.EndSelection(this, e);
            }
        }

        private Region GetRegion()
        {
            return new Region(rectangle);
        }
    }
}
