using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ESADS.Mechanics.Design.Beam;

namespace ESADS.EGraphics.Beam
{
    public class eGStirrup : ESADS.EGraphics.eIDrawing
    {
        private eDShearSection shearSection;
        private eShearBar v_bar;
        private float size;
        private PointF location;
        private List<eIDrawing> objs_shearBars;
        private List<eIDrawing> objs_shearLabels;
        /// <summary>
        /// The layer on which shear reinforcements are drawn.
        /// </summary>
        private eLayer layer_ShearBars;
        private eLayer layer_ShearLabels;
        private bool isForBeam;
        private float l1;
        private float l2;
        private float l3;
        private string name;
        public eColor Color
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Draw(System.Drawing.Graphics g)
        {
            throw new NotImplementedException();
        }

        public eLayer Layer
        {
            get { throw new NotImplementedException(); }
        }

        public System.Drawing.PointF Location
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Pan(float Xoffset, float Yoffset)
        {
            throw new NotImplementedException();
        }

        public void Zoom(System.Drawing.PointF ZoomCenter, float ZoomFactor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates all the necessary objects to draw the stirrup.
        /// </summary>
        private void GenerateDwgObjects()
        {
            PointF p1, p2, p3, p4, p5, p6, p7, p8;
            float w, h;
            string text = "";
            eTextStyle txtStyle;
            p2 = p3 = p4 = p5 = p6 = p7 = new PointF();

            p1 = location;
            if (this.isForBeam)
            {
                if (v_bar.BarType == eShearBarTypes.EnclosingStirrup)
                {
                    l1 = (float)(v_bar.Lengths[0] * size);
                    l2 = (float)(v_bar.Lengths[1] * size);
                    l3 = (float)(v_bar.Lengths[2] * size);

                    p2.X = p1.X;
                    p2.Y = p1.Y + l3;
                    p3.X = p2.X + l2;
                    p3.Y = p2.Y;
                    p4.X = p3.X;
                    p4.Y = p1.Y;
                    p5.X = (float)(p4.X - l1 * Math.Cos(Math.PI / 4));
                    p5.Y = (float)(p4.Y + l1 * Math.Sin(Math.PI / 4));
                    p6.X = (float)(p1.X + l2 * Math.Cos(Math.PI / 6));
                    p6.Y = (float)(p1.Y - l2 * Math.Sin(Math.PI / 6));
                    p7.X = (float)(p6.X - l1 * Math.Sin(Math.PI / 12));
                    p7.Y = (float)(p6.Y + l1 * Math.Cos(Math.PI / 12));

                    objs_shearBars.Add(layer_ShearBars.AddLine(p1, p2));
                    objs_shearBars.Add(layer_ShearBars.AddLine(p2, p3));
                    objs_shearBars.Add(layer_ShearBars.AddLine(p3, p4));
                    objs_shearBars.Add(layer_ShearBars.AddLine(p4, p5));
                    objs_shearBars.Add(layer_ShearBars.AddLine(p1, p6));
                    objs_shearBars.Add(layer_ShearBars.AddLine(p6, p7));

                    name = v_bar.Name + " Φ" + Math.Round(eUtility.Convert(v_bar.Diameter, eUtility.SLU, eLengthUnits.mm), 0).ToString() + " c/c=" +
                        Math.Round(eUtility.Convert(v_bar.Spacing, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm L=" +
                        Math.Round(eUtility.Convert(v_bar.Length, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm";
                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p2.X + p3.X) / 2.0f, p2.Y + l3 / 2.0f)));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.2f * l2), eChangeBy.ByLayer);
                    w = Math.Max((objs_shearLabels[objs_shearLabels.Count - 1] as eText).Width, l1);

                    txtStyle = new eTextStyle(new Font("Arial", 0.1f * l2), eChangeBy.ByLayer);

                    text = Math.Round(eUtility.Convert(v_bar.Lengths[2], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF(p1.X, (p1.Y + p2.Y) / 2.0f), 90));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    h = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Height;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - h / 2, p8.Y);

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF(p4.X, (p4.Y + p3.Y) / 2.0f), 90));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + h / 2, p8.Y);

                    text = Math.Round(eUtility.Convert(v_bar.Lengths[1], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p2.X + p3.X) / 2, p2.Y)));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X, p8.Y + h / 2);

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p6.X) / 2, (p1.Y + p6.Y) / 2), 30));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - h / 4, p8.Y - 0.866f * h / 2);

                    text = Math.Round(eUtility.Convert(v_bar.Lengths[0], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p7.X + p6.X) / 2, (p6.Y + p7.Y) / 2), 75));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + 0.9659f * h / 2, p8.Y + 0.2588f * h / 2);

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p5.X + p4.X) / 2, (p5.Y + p4.Y) / 2), 45));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - 0.707f * h / 2, p8.Y - 0.707f * h / 2);
                }
                else
                {
                    l1 = (float)(v_bar.Lengths[0] * size);
                    l2 = (float)(v_bar.Lengths[1] * size);

                    p2.X = p1.X + l2;
                    p2.Y = p1.Y;
                    p3.X = (float)(p2.X - l1 * Math.Cos(Math.PI / 4));
                    if (v_bar.IsTop && shearSection.FlexureSection.Beam.StirrupPosn == eRelativeStirrupPosition.StirrupAtTop || !v_bar.IsTop && shearSection.FlexureSection.Beam.StirrupPosn == eRelativeStirrupPosition.StirrupAtBottom)
                        p3.Y = (float)(p2.Y - l1 * Math.Cos(Math.PI / 4));
                    else
                        p3.Y = (float)(p2.Y + l1 * Math.Cos(Math.PI / 4));
                    p4.X = (float)(p1.X + l1 * Math.Cos(Math.PI / 4));
                    p4.Y = p3.Y;

                    objs_shearBars.Add(layer_ShearBars.AddLine(p1, p2));
                    objs_shearBars.Add(layer_ShearBars.AddLine(p2, p3));
                    objs_shearBars.Add(layer_ShearBars.AddLine(p1, p4));

                    text = v_bar.Name + " Φ" + Math.Round(eUtility.Convert(v_bar.Diameter, eUtility.SLU, eLengthUnits.mm), 0).ToString() + " c/c=" +
                        Math.Round(eUtility.Convert(v_bar.Spacing, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm L=" +
                        Math.Round(eUtility.Convert(v_bar.Length, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm";
                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p2.X) / 2.0f, p1.Y + l2 / 2.0f)));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.2f * l2), eChangeBy.ByLayer);
                    w = Math.Max((objs_shearLabels[objs_shearLabels.Count - 1] as eText).Width, l1);

                    txtStyle = new eTextStyle(new Font("Arial", 0.1f * l2), eChangeBy.ByLayer);

                    text = Math.Round(eUtility.Convert(v_bar.Lengths[0], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p4.X) / 2, (p1.Y + p4.Y) / 2.0f), 45));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    h = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Height;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    if (v_bar.IsTop && shearSection.FlexureSection.Beam.StirrupPosn == eRelativeStirrupPosition.StirrupAtTop || !v_bar.IsTop && shearSection.FlexureSection.Beam.StirrupPosn == eRelativeStirrupPosition.StirrupAtBottom)
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - 0.707f * h / 2, p8.Y + 0.707f * h / 2);
                    else
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - 0.707f * h / 2, p8.Y - 0.707f * h / 2);

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2), -45));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    if (v_bar.IsTop && shearSection.FlexureSection.Beam.StirrupPosn == eRelativeStirrupPosition.StirrupAtTop || !v_bar.IsTop && shearSection.FlexureSection.Beam.StirrupPosn == eRelativeStirrupPosition.StirrupAtBottom)
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + 0.707f * h / 2, p8.Y - 0.707f * h / 2);
                    else
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + 0.707f * h / 2, p8.Y + 0.707f * h / 2);

                    text = Math.Round(eUtility.Convert(v_bar.Lengths[1], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                    objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p2.X) / 2, p1.Y)));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                    p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X, p8.Y + 0.5f * h / 2);
                }
            }
            else  //for purposes other than beam
            {
                l1 = (float)(v_bar.Lengths[0] * size);
                l2 = (float)(v_bar.Lengths[1] * size);
                l3 = (float)(v_bar.Lengths[2] * size);

                p2.X = p1.X;
                p2.Y = p1.Y + l3;
                p3.X = p2.X + l2;
                p3.Y = p2.Y;
                p4.X = p3.X;
                p4.Y = p1.Y;
                p5.X = (float)(p4.X - l1 * Math.Cos(Math.PI / 4));
                p5.Y = (float)(p4.Y + l1 * Math.Sin(Math.PI / 4));
                p6.X = (float)(p1.X + l2 * Math.Cos(Math.PI / 6));
                p6.Y = (float)(p1.Y - l2 * Math.Sin(Math.PI / 6));
                p7.X = (float)(p6.X - l1 * Math.Sin(Math.PI / 12));
                p7.Y = (float)(p6.Y + l1 * Math.Cos(Math.PI / 12));

                objs_shearBars.Add(layer_ShearBars.AddLine(p1, p2));
                objs_shearBars.Add(layer_ShearBars.AddLine(p2, p3));
                objs_shearBars.Add(layer_ShearBars.AddLine(p3, p4));
                objs_shearBars.Add(layer_ShearBars.AddLine(p4, p5));
                objs_shearBars.Add(layer_ShearBars.AddLine(p1, p6));
                objs_shearBars.Add(layer_ShearBars.AddLine(p6, p7));

                text = v_bar.Name + " Φ" + Math.Round(eUtility.Convert(v_bar.Diameter, eUtility.SLU, eLengthUnits.mm), 0).ToString() + " c/c=" +
                    Math.Round(eUtility.Convert(v_bar.Spacing, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm L=" +
                    Math.Round(eUtility.Convert(v_bar.Length, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm";
                objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p2.X + p3.X) / 2.0f, p2.Y + l3 / 2.0f)));
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.2f * l2), eChangeBy.ByLayer);
                w = Math.Max((objs_shearLabels[objs_shearLabels.Count - 1] as eText).Width, l1);

                txtStyle = new eTextStyle(new Font("Arial", 0.1f * l2), eChangeBy.ByLayer);

                text = Math.Round(eUtility.Convert(v_bar.Lengths[2], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF(p1.X, (p1.Y + p2.Y) / 2.0f), 90));
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                h = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Height;
                p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - h / 2, p8.Y);

                objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF(p4.X, (p4.Y + p3.Y) / 2.0f), 90));
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + h / 2, p8.Y);

                text = Math.Round(eUtility.Convert(v_bar.Lengths[1], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p2.X + p3.X) / 2, p2.Y)));
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X, p8.Y + h / 2);

                objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p6.X) / 2, (p1.Y + p6.Y) / 2), 30));
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - h / 4, p8.Y - 0.866f * h / 2);

                text = Math.Round(eUtility.Convert(v_bar.Lengths[0], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p7.X + p6.X) / 2, (p6.Y + p7.Y) / 2), 75));
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + 0.9659f * h / 2, p8.Y + 0.2588f * h / 2);

                objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p5.X + p4.X) / 2, (p5.Y + p4.Y) / 2), 45));
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - 0.707f * h / 2, p8.Y - 0.707f * h / 2);
                
            }
               
            
        }

        public eShearBar ShearBar
        {
            get
            {
                return this.v_bar;
            }
            set
            {
                this.v_bar = value;
            }
        }

        public eDShearSection ShearSection
        {
            get
            {
                return this.shearSection;
            }
            set
            {
                this.shearSection = value;
            }
        }
    }
}
